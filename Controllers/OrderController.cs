using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models;
using BabyShopWeb2.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BabyShopWeb2.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly IPdfService _pdfService;
        private readonly IFinancialService _financialService;
        private readonly IMongoSyncService _mongoSyncService;
        
        public OrderController(
            ApplicationDbContext context, 
            ICartService cartService, 
            IPdfService pdfService, 
            IFinancialService financialService,
            IMongoSyncService mongoSyncService)
        {
            _context = context;
            _cartService = cartService;
            _pdfService = pdfService;
            _financialService = financialService;
            _mongoSyncService = mongoSyncService;
        }
        
        private string GetSessionId()
        {
            var sessionId = HttpContext.Session.GetString("CartSessionId");
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("CartSessionId", sessionId);
            }
            return sessionId;
        }
        
        public async Task<IActionResult> Checkout()
        {
            var sessionId = GetSessionId();
            var cart = await _cartService.GetCartAsync(sessionId);
            
            // Jika keranjang kosong, tampilkan pesan di checkout (JANGAN redirect ke Cart)
            if (!cart.CartItems.Any())
            {
                TempData["Error"] = "Keranjang belanja kosong. Silakan tambahkan produk terlebih dahulu.";
                // Buat model kosong untuk ditampilkan
                var emptyModel = new CheckoutViewModel
                {
                    Cart = cart,
                    ShippingCost = 0
                };
                return View(emptyModel);
            }
            
            // Get active employees (Kasir, Supervisor, Admin, Manager)
            var activeEmployees = await _context.Employees
                .Where(e => e.Status == EmployeeStatus.Aktif && 
                           (e.Position == EmployeePosition.Kasir || 
                            e.Position == EmployeePosition.Supervisor ||
                            e.Position == EmployeePosition.Admin ||
                            e.Position == EmployeePosition.Manager))
                .OrderBy(e => e.FullName)
                .ToListAsync();
            
            var checkoutModel = new CheckoutViewModel
            {
                Cart = cart,
                ShippingCost = 0, // Tidak ada ongkos kirim
                AvailableEmployees = activeEmployees
            };
            
            return View(checkoutModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            var sessionId = GetSessionId();
            var cart = await _cartService.GetCartAsync(sessionId);
            
            // Debug: Log untuk melihat apakah method ini dipanggil
            Console.WriteLine($"=== PlaceOrder START ===");
            Console.WriteLine($"Customer: {model.CustomerName}");
            Console.WriteLine($"Phone: {model.CustomerPhone}");
            Console.WriteLine($"Cart items: {cart.CartItems.Count}");
            
            // Jika keranjang kosong, TETAP di checkout (JANGAN redirect ke Cart)
            if (!cart.CartItems.Any())
            {
                Console.WriteLine("ERROR: Cart is empty");
                TempData["Error"] = "Keranjang belanja kosong. Silakan tambahkan produk terlebih dahulu.";
                model.Cart = cart;
                model.ShippingCost = 0;
                return View("Checkout", model);
            }
            
            // Jika validasi gagal, tetap di halaman checkout
            // Hanya validasi field yang diperlukan, bukan Cart
            var validationErrors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                validationErrors.Add("Nama lengkap wajib diisi");
            if (string.IsNullOrWhiteSpace(model.CustomerPhone))
                validationErrors.Add("Nomor telepon wajib diisi");
            if (string.IsNullOrWhiteSpace(model.ShippingAddress))
                validationErrors.Add("Alamat pengiriman wajib diisi");
            if (string.IsNullOrWhiteSpace(model.CashierName))
                validationErrors.Add("Kasir wajib dipilih");
                
            if (validationErrors.Any())
            {
                Console.WriteLine("ERROR: Manual validation failed");
                foreach (var error in validationErrors)
                {
                    Console.WriteLine($"Validation error: {error}");
                }
                
                foreach (var error in validationErrors)
                {
                    ModelState.AddModelError("", error);
                }
                
                // Reload employees for the view
                model.AvailableEmployees = await _context.Employees
                    .Where(e => e.Status == EmployeeStatus.Aktif && 
                               (e.Position == EmployeePosition.Kasir || 
                                e.Position == EmployeePosition.Supervisor ||
                                e.Position == EmployeePosition.Admin ||
                                e.Position == EmployeePosition.Manager))
                    .OrderBy(e => e.FullName)
                    .ToListAsync();
                
                model.Cart = cart;
                model.ShippingCost = 0;
                return View("Checkout", model);
            }
            
            try
            {
                Console.WriteLine("Creating order...");
                
                // Create order
                var order = new Order
                {
                    OrderNumber = GenerateOrderNumber(),
                    CustomerName = model.CustomerName,
                    CustomerEmail = $"{model.CustomerName.Replace(" ", "").Replace(".", "").ToLower()}@babyshop3berlian.com",
                    CustomerPhone = model.CustomerPhone,
                    ShippingAddress = model.ShippingAddress,
                    SubTotal = cart.TotalAmount,
                    ShippingCost = 0, // Tidak ada ongkos kirim
                    Notes = model.Notes ?? string.Empty,
                    CashierName = model.CashierName,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending
                    // PaymentMethod akan diset nanti di Payment page
                };
                
                order.TotalAmount = order.SubTotal + order.ShippingCost;
                
                Console.WriteLine($"Order number: {order.OrderNumber}");
                Console.WriteLine($"Total amount: {order.TotalAmount}");
                
                // Add order items
                foreach (var cartItem in cart.CartItems)
                {
                    var unitPrice = cartItem.Product?.DiscountPrice ?? cartItem.Product?.Price ?? 0;
                    var orderItem = new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        UnitPrice = unitPrice,
                        Subtotal = cartItem.Quantity * unitPrice
                    };
                    order.OrderItems.Add(orderItem);
                    
                    Console.WriteLine($"Added item: {cartItem.Product?.Name} x{cartItem.Quantity}");
                    
                    // Update stock
                    var product = await _context.Products.FindAsync(cartItem.ProductId);
                    if (product != null && product.Stock >= cartItem.Quantity)
                    {
                        product.Stock -= cartItem.Quantity;
                        Console.WriteLine($"Updated stock for {product.Name}: {product.Stock}");
                    }
                }
                
                _context.Orders.Add(order);
                
                Console.WriteLine("Saving order to database...");
                var saveResult = await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChanges result: {saveResult} rows affected");
                
                // Reload order to get the generated ID
                await _context.Entry(order).ReloadAsync();
                Console.WriteLine($"Order saved with ID: {order.Id}");
                
                // Auto-sync to MongoDB
                await _mongoSyncService.SyncOrderAsync(order);
                
                // Clear cart setelah berhasil
                await _cartService.ClearCartAsync(sessionId);
                Console.WriteLine("Cart cleared");
                
                TempData["Success"] = "Pesanan berhasil dibuat!";
                
                // REDIRECT KE PAYMENT SIMPLE - INI YANG PENTING!
                Console.WriteLine($"Redirecting to PaymentSimple with ID: {order.Id}");
                Console.WriteLine("=== PlaceOrder SUCCESS ===");
                
                return RedirectToAction("PaymentSimple", new { id = order.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR creating order: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                
                TempData["Error"] = $"Terjadi kesalahan saat memproses pesanan: {ex.Message}";
                model.Cart = cart;
                model.ShippingCost = 0;
                return View("Checkout", model);
            }
        }
        
        // Action untuk Payment Page (Simple Version)
        public async Task<IActionResult> PaymentSimple(int id)
        {
            Console.WriteLine($"=== PaymentSimple action called with ID: {id} ===");
            
            if (id <= 0)
            {
                Console.WriteLine($"ERROR: Invalid order ID: {id}");
                TempData["Error"] = "ID pesanan tidak valid.";
                return RedirectToAction("Index", "Home");
            }
            
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Category)
                    .FirstOrDefaultAsync(o => o.Id == id);
                    
                if (order == null)
                {
                    Console.WriteLine($"ERROR: Order not found with ID: {id}");
                    TempData["Error"] = "Pesanan tidak ditemukan.";
                    return RedirectToAction("Index", "Home");
                }
                
                Console.WriteLine($"SUCCESS: Order found for payment - {order.OrderNumber}");
                Console.WriteLine($"Total amount: {order.TotalAmount}");
                
                return View(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in PaymentSimple action: {ex.Message}");
                TempData["Error"] = "Terjadi kesalahan saat menampilkan halaman pembayaran.";
                return RedirectToAction("Index", "Home");
            }
        }
        
        // Action untuk Payment Page
        public async Task<IActionResult> Payment(int id)
        {
            Console.WriteLine($"=== Payment action called with ID: {id} ===");
            
            if (id <= 0)
            {
                Console.WriteLine($"ERROR: Invalid order ID: {id}");
                TempData["Error"] = "ID pesanan tidak valid.";
                return RedirectToAction("Index", "Home");
            }
            
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Category)
                    .FirstOrDefaultAsync(o => o.Id == id);
                    
                if (order == null)
                {
                    Console.WriteLine($"ERROR: Order not found with ID: {id}");
                    TempData["Error"] = "Pesanan tidak ditemukan.";
                    return RedirectToAction("Index", "Home");
                }
                
                Console.WriteLine($"SUCCESS: Order found for payment - {order.OrderNumber}");
                Console.WriteLine($"Total amount: {order.TotalAmount}");
                
                return View(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in Payment action: {ex.Message}");
                TempData["Error"] = "Terjadi kesalahan saat menampilkan halaman pembayaran.";
                return RedirectToAction("Index", "Home");
            }
        }
        
        // Action untuk konfirmasi pembayaran
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(int orderId, string paymentMethod)
        {
            Console.WriteLine($"=== ConfirmPayment called for Order ID: {orderId}, Method: {paymentMethod} ===");
            
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    TempData["Error"] = "Pesanan tidak ditemukan.";
                    return RedirectToAction("Index", "Home");
                }
                
                // Update order status dan payment method
                order.Status = OrderStatus.Processing; // Ubah status menjadi diproses
                // Temporary: Store payment method in Notes until database is updated
                order.Notes = $"{order.Notes} | Metode Pembayaran: {paymentMethod}".Trim('|').Trim();
                
                await _context.SaveChangesAsync();
                
                // Record financial transaction for the sale
                await _financialService.RecordSaleTransactionAsync(order);
                
                Console.WriteLine($"Payment confirmed for order {order.OrderNumber} with method {paymentMethod}");
                
                // Set success message based on payment method
                if (paymentMethod == "Cash")
                {
                    TempData["Success"] = "Pembayaran tunai berhasil dikonfirmasi! Silakan lakukan pembayaran saat pengambilan barang.";
                }
                else
                {
                    TempData["Success"] = $"Pembayaran {paymentMethod} berhasil dikonfirmasi! Terima kasih telah menggunakan {paymentMethod}.";
                }
                
                // Redirect ke Struk setelah pembayaran
                return RedirectToAction("Struk", new { id = orderId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR confirming payment: {ex.Message}");
                TempData["Error"] = "Terjadi kesalahan saat mengkonfirmasi pembayaran.";
                return RedirectToAction("Payment", new { id = orderId });
            }
        }
        
        // Action khusus untuk Struk/Invoice
        public async Task<IActionResult> Struk(int id)
        {
            Console.WriteLine($"=== Struk action called with ID: {id} ===");
            
            if (id <= 0)
            {
                Console.WriteLine($"ERROR: Invalid order ID: {id}");
                TempData["Error"] = "ID pesanan tidak valid.";
                return RedirectToAction("Index", "Home");
            }
            
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Category)
                    .FirstOrDefaultAsync(o => o.Id == id);
                    
                if (order == null)
                {
                    Console.WriteLine($"ERROR: Order not found with ID: {id}");
                    
                    // Debug: List all orders in database
                    var allOrders = await _context.Orders.Select(o => new { o.Id, o.OrderNumber }).ToListAsync();
                    Console.WriteLine($"Available orders in database: {string.Join(", ", allOrders.Select(o => $"ID:{o.Id} Number:{o.OrderNumber}"))}");
                    
                    TempData["Error"] = "Pesanan tidak ditemukan.";
                    return RedirectToAction("Index", "Home");
                }
                
                Console.WriteLine($"SUCCESS: Order found - {order.OrderNumber} with {order.OrderItems.Count} items");
                Console.WriteLine($"Customer: {order.CustomerName}");
                Console.WriteLine($"Total: {order.TotalAmount}");
                
                return View(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in Struk action: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                TempData["Error"] = "Terjadi kesalahan saat menampilkan struk.";
                return RedirectToAction("Index", "Home");
            }
        }
        
        public async Task<IActionResult> Receipt(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }
        
        public async Task<IActionResult> OrderSuccess(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }
        
        public async Task<IActionResult> DownloadReceipt(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (order == null)
            {
                TempData["Error"] = "Pesanan tidak ditemukan.";
                return RedirectToAction("Index", "Home");
            }
            
            try
            {
                var pdfBytes = _pdfService.GenerateReceipt(order);
                var fileName = $"Struk-{order.OrderNumber}.pdf";
                
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception)
            {
                TempData["Error"] = "Gagal membuat PDF. Silakan coba lagi.";
                return RedirectToAction("Receipt", new { id = order.Id });
            }
        }
        
        private string GenerateOrderNumber()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(100, 999);
            return $"BSB{timestamp}{random}";
        }
        
        // Test action untuk debugging
        public async Task<IActionResult> TestOrder()
        {
            Console.WriteLine("=== TEST ORDER ACTION ===");
            
            try
            {
                // Create a test order
                var testOrder = new Order
                {
                    OrderNumber = GenerateOrderNumber(),
                    CustomerName = "Test Customer",
                    CustomerEmail = "test@babyshop3berlian.com",
                    CustomerPhone = "081234567890",
                    ShippingAddress = "Jl. Test No. 123, Jakarta",
                    SubTotal = 100000,
                    ShippingCost = 0, // Tidak ada ongkos kirim
                    TotalAmount = 100000,
                    Notes = "Test order",
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending
                };
                
                _context.Orders.Add(testOrder);
                await _context.SaveChangesAsync();
                
                Console.WriteLine($"Test order created with ID: {testOrder.Id}");
                
                // Redirect to Struk
                return RedirectToAction("Struk", new { id = testOrder.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR creating test order: {ex.Message}");
                return Json(new { error = ex.Message });
            }
        }
    }
    
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Nama lengkap wajib diisi")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter")]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Nomor telepon wajib diisi")]
        [Phone(ErrorMessage = "Format nomor telepon tidak valid")]
        public string CustomerPhone { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Alamat pengiriman wajib diisi")]
        [StringLength(500, ErrorMessage = "Alamat maksimal 500 karakter")]
        public string ShippingAddress { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Catatan maksimal 200 karakter")]
        public string? Notes { get; set; }
        
        [Required(ErrorMessage = "Kasir wajib dipilih")]
        public string CashierName { get; set; } = string.Empty;
        
        [BindNever]
        public Cart Cart { get; set; } = new Cart();
        
        [BindNever]
        public decimal ShippingCost { get; set; }
        
        [BindNever]
        public decimal TotalAmount => Cart.TotalAmount + ShippingCost;
        
        [BindNever]
        public List<Employee> AvailableEmployees { get; set; } = new List<Employee>();
    }
}