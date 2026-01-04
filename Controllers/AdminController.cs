using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models;
using BabyShopWeb2.Services;

namespace BabyShopWeb2.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFinancialService _financialService;
        private readonly IMongoSyncService _mongoSyncService;
        
        public AdminController(
            ApplicationDbContext context, 
            IWebHostEnvironment webHostEnvironment, 
            IFinancialService financialService,
            IMongoSyncService mongoSyncService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _financialService = financialService;
            _mongoSyncService = mongoSyncService;
        }
        
        // Dashboard Admin
        public async Task<IActionResult> Index(DateTime? date, int? month, int? year)
        {
            // Use provided date or default to today
            var selectedDate = date ?? DateTime.Today;
            var selectedMonth = month ?? selectedDate.Month;
            var selectedYear = year ?? selectedDate.Year;
            
            // Get financial data for selected date
            var financialData = await _financialService.GetDashboardDataAsync(selectedDate, selectedMonth, selectedYear);
            
            // Get orders and calculate total revenue in memory
            var orders = await _context.Orders.ToListAsync();
            
            var stats = new AdminDashboardViewModel
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalOrders = orders.Count,
                TotalRevenue = orders.Sum(o => o.TotalAmount),
                RecentOrders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .ToListAsync(),
                    
                // Add financial data for selected date
                TodayIncome = financialData.DailyIncome,
                TodayExpense = financialData.DailyExpense,
                MonthlyIncome = financialData.MonthlyIncome,
                MonthlyExpense = financialData.MonthlyExpense
            };
            
            // Pass selected date to view
            ViewBag.SelectedDate = selectedDate;
            ViewBag.SelectedMonth = selectedMonth;
            ViewBag.SelectedYear = selectedYear;
            
            return View(stats);
        }
        
        // CRUD Produk
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return View(products);
        }
        
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    DiscountPrice = model.DiscountPrice,
                    Stock = model.Stock,
                    CategoryId = model.CategoryId,
                    IsActive = true
                };
                
                // Handle image upload
                if (model.ImageFile != null)
                {
                    var fileName = await SaveImageAsync(model.ImageFile);
                    product.ImageUrl = $"/images/products/{fileName}";
                }
                
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                
                // Auto-sync to MongoDB
                await _mongoSyncService.SyncProductAsync(product);
                
                TempData["Success"] = "Produk berhasil ditambahkan!";
                return RedirectToAction(nameof(Products));
            }
            
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(model);
        }
        
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            var model = new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CurrentImageUrl = product.ImageUrl,
                IsActive = product.IsActive
            };
            
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(model.Id);
                if (product == null)
                {
                    return NotFound();
                }
                
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.DiscountPrice = model.DiscountPrice;
                product.Stock = model.Stock;
                product.CategoryId = model.CategoryId;
                product.IsActive = model.IsActive;
                
                // Handle image upload
                if (model.ImageFile != null)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        DeleteImage(product.ImageUrl);
                    }
                    
                    var fileName = await SaveImageAsync(model.ImageFile);
                    product.ImageUrl = $"/images/products/{fileName}";
                }
                
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                
                // Auto-sync to MongoDB
                await _mongoSyncService.SyncProductUpdateAsync(product);
                
                TempData["Success"] = "Produk berhasil diperbarui!";
                return RedirectToAction(nameof(Products));
            }
            
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            // Delete image if exists
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                DeleteImage(product.ImageUrl);
            }
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            // Auto-sync to MongoDB (soft delete)
            await _mongoSyncService.SyncProductDeleteAsync(id);
            
            TempData["Success"] = "Produk berhasil dihapus!";
            return RedirectToAction(nameof(Products));
        }
        
        // Orders Management
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }
        
        // Contact Messages Management
        public async Task<IActionResult> Messages()
        {
            var messages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return View(messages);
        }
        
        public async Task<IActionResult> MessageDetails(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            
            // Mark as read
            if (!message.IsRead)
            {
                message.IsRead = true;
                message.ReadAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            
            return View(message);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            
            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Pesan berhasil dihapus!";
            return RedirectToAction(nameof(Messages));
        }
        
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            
            message.IsRead = true;
            message.ReadAt = DateTime.Now;
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Messages));
        }
        
        // Newsletter Subscribers Management
        public async Task<IActionResult> Newsletters()
        {
            var subscribers = await _context.Newsletters
                .OrderByDescending(n => n.SubscribedAt)
                .ToListAsync();
            return View(subscribers);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var subscriber = await _context.Newsletters.FindAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }
            
            _context.Newsletters.Remove(subscriber);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Subscriber berhasil dihapus!";
            return RedirectToAction(nameof(Newsletters));
        }
        
        [HttpPost]
        public async Task<IActionResult> ExportNewsletters()
        {
            var subscribers = await _context.Newsletters
                .OrderBy(n => n.Email)
                .ToListAsync();
            
            var csv = "Email,Tanggal Subscribe\n";
            foreach (var sub in subscribers)
            {
                csv += $"{sub.Email},{sub.SubscribedAt:yyyy-MM-dd HH:mm:ss}\n";
            }
            
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", $"newsletter-subscribers-{DateTime.Now:yyyyMMdd}.csv");
        }
        
        // Create Employee Table (SAFE - Tidak hapus data!)
        [HttpGet]
        public async Task<IActionResult> CreateEmployeeTable()
        {
            try
            {
                // Cek apakah tabel sudah ada
                var tableExists = false;
                try
                {
                    await _context.Employees.AnyAsync();
                    tableExists = true;
                }
                catch
                {
                    tableExists = false;
                }
                
                if (tableExists)
                {
                    return Content(@"
✅ Tabel Employees sudah ada!

Tidak perlu dibuat lagi.

Akses menu Karyawan: http://localhost:5055/Admin/Employees
                    ", "text/plain");
                }
                
                // Buat tabel Employees dengan SQL
                var sql = @"
CREATE TABLE IF NOT EXISTS Employees (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeId TEXT NOT NULL UNIQUE,
    FullName TEXT NOT NULL,
    Position INTEGER NOT NULL,
    PhoneNumber TEXT NOT NULL,
    Email TEXT,
    Address TEXT NOT NULL,
    JoinDate TEXT NOT NULL,
    Status INTEGER NOT NULL,
    Shift INTEGER NOT NULL,
    Salary REAL,
    Notes TEXT,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT
);

CREATE INDEX IF NOT EXISTS IX_Employees_EmployeeId ON Employees(EmployeeId);
CREATE INDEX IF NOT EXISTS IX_Employees_Status ON Employees(Status);
                ";
                
                await _context.Database.ExecuteSqlRawAsync(sql);
                
                return Content(@"
✅ SUKSES! Tabel Employees berhasil ditambahkan!

Data lama Anda AMAN:
- ✅ Produk tetap ada
- ✅ Orders tetap ada  
- ✅ Newsletter tetap ada
- ✅ Semua data lain tetap ada

Sekarang akses menu Karyawan:
http://localhost:5055/Admin/Employees

Atau tambah karyawan pertama:
http://localhost:5055/Admin/CreateEmployee
                ", "text/plain");
            }
            catch (Exception ex)
            {
                return Content($@"
❌ Error: {ex.Message}

Coba cara alternatif:
1. Stop aplikasi (Ctrl+C)
2. Backup database: copy babyshop.db babyshop-backup.db
3. Hapus database: del babyshop.db
4. Jalankan ulang: dotnet run

Gambar produk Anda tetap aman di folder wwwroot/images/products/
                ", "text/plain");
            }
        }
        
        // Add CashierName column to Orders table (SAFE - Tidak hapus data!)
        [HttpGet]
        public async Task<IActionResult> AddCashierColumn()
        {
            try
            {
                // Cek apakah kolom sudah ada
                var checkSql = "PRAGMA table_info(Orders);";
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                
                var command = connection.CreateCommand();
                command.CommandText = checkSql;
                
                var reader = await command.ExecuteReaderAsync();
                var columnExists = false;
                
                while (await reader.ReadAsync())
                {
                    var columnName = reader.GetString(1); // Column name is at index 1
                    if (columnName == "CashierName")
                    {
                        columnExists = true;
                        break;
                    }
                }
                
                await reader.CloseAsync();
                await connection.CloseAsync();
                
                if (columnExists)
                {
                    return Content(@"
✅ Kolom CashierName sudah ada di tabel Orders!

Tidak perlu ditambahkan lagi.

Fitur kasir sudah siap digunakan!
                    ", "text/plain");
                }
                
                // Tambah kolom CashierName
                var sql = @"ALTER TABLE Orders ADD COLUMN CashierName TEXT;";
                await _context.Database.ExecuteSqlRawAsync(sql);
                
                return Content(@"
✅ SUKSES! Kolom CashierName berhasil ditambahkan ke tabel Orders!

Data lama Anda AMAN:
- ✅ Semua order tetap ada
- ✅ Tidak ada data yang hilang

Sekarang nama kasir akan muncul di struk!

Test dengan membuat order baru:
http://localhost:5055/Product
                ", "text/plain");
            }
            catch (Exception ex)
            {
                return Content($@"
❌ Error: {ex.Message}

Coba cara alternatif:
1. Stop aplikasi (Ctrl+C)
2. Jalankan ulang: dotnet run
3. Akses endpoint ini lagi

Atau hubungi developer untuk bantuan.
                ", "text/plain");
            }
        }
        
        // Employee Management
        public async Task<IActionResult> Employees()
        {
            var employees = await _context.Employees
                .OrderBy(e => e.EmployeeId)
                .ToListAsync();
            return View(employees);
        }
        
        public IActionResult CreateEmployee()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                // Check if EmployeeId already exists
                var existingEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeId == model.EmployeeId);
                
                if (existingEmployee != null)
                {
                    ModelState.AddModelError("EmployeeId", "ID Karyawan sudah digunakan");
                    return View(model);
                }
                
                model.CreatedAt = DateTime.Now;
                _context.Employees.Add(model);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Karyawan berhasil ditambahkan!";
                return RedirectToAction(nameof(Employees));
            }
            
            return View(model);
        }
        
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            return View(employee);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees.FindAsync(model.Id);
                if (employee == null)
                {
                    return NotFound();
                }
                
                // Check if EmployeeId is changed and already exists
                if (employee.EmployeeId != model.EmployeeId)
                {
                    var existingEmployee = await _context.Employees
                        .FirstOrDefaultAsync(e => e.EmployeeId == model.EmployeeId && e.Id != model.Id);
                    
                    if (existingEmployee != null)
                    {
                        ModelState.AddModelError("EmployeeId", "ID Karyawan sudah digunakan");
                        return View(model);
                    }
                }
                
                employee.EmployeeId = model.EmployeeId;
                employee.FullName = model.FullName;
                employee.Position = model.Position;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Address = model.Address;
                employee.JoinDate = model.JoinDate;
                employee.Status = model.Status;
                employee.Shift = model.Shift;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Notes = model.Notes;
                employee.UpdatedAt = DateTime.Now;
                
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Data karyawan berhasil diperbarui!";
                return RedirectToAction(nameof(Employees));
            }
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Karyawan berhasil dihapus!";
            return RedirectToAction(nameof(Employees));
        }
        
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            return View(employee);
        }
        
        // Financial Management - Route through Admin
        public async Task<IActionResult> Financial(DateTime? date, int? month, int? year)
        {
            var selectedDate = date ?? DateTime.Today;
            var selectedMonth = month ?? DateTime.Today.Month;
            var selectedYear = year ?? DateTime.Today.Year;
            
            var viewModel = await _financialService.GetDashboardDataAsync(selectedDate, selectedMonth, selectedYear);
            
            return View(viewModel);
        }
        
        public async Task<IActionResult> OrderDetails(int id)
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
        
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            
            order.Status = status;
            if (status == OrderStatus.Shipped && !order.ShippedDate.HasValue)
            {
                order.ShippedDate = DateTime.Now;
            }
            else if (status == OrderStatus.Delivered && !order.DeliveredDate.HasValue)
            {
                order.DeliveredDate = DateTime.Now;
            }
            
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Status pesanan berhasil diperbarui!";
            return RedirectToAction(nameof(Orders));
        }
        
        // Helper methods
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
            Directory.CreateDirectory(uploadsFolder);
            
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            
            return fileName;
        }
        
        private void DeleteImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;
            
            var fileName = Path.GetFileName(imageUrl);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", fileName);
            
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}