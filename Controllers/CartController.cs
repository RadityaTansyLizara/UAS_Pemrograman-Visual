using Microsoft.AspNetCore.Mvc;
using BabyShopWeb2.Services;
using BabyShopWeb2.Data;
using Microsoft.EntityFrameworkCore;

namespace BabyShopWeb2.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ApplicationDbContext _context;
        
        public CartController(ICartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
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
        
        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            var cart = await _cartService.GetCartAsync(sessionId);
            return View(cart);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var sessionId = GetSessionId();
            
            // Check if product exists and has enough stock
            var product = await _context.Products.FindAsync(productId);
            if (product == null || !product.IsActive || product.Stock < quantity)
            {
                TempData["Error"] = "Produk tidak tersedia atau stok tidak mencukupi.";
                return RedirectToAction("Details", "Product", new { id = productId });
            }
            
            await _cartService.AddToCartAsync(sessionId, productId, quantity);
            TempData["Success"] = "Produk berhasil ditambahkan ke keranjang.";
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            var sessionId = GetSessionId();
            await _cartService.UpdateCartItemAsync(sessionId, productId, quantity);
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var sessionId = GetSessionId();
            await _cartService.RemoveFromCartAsync(sessionId, productId);
            
            TempData["Success"] = "Produk berhasil dihapus dari keranjang.";
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var sessionId = GetSessionId();
            await _cartService.ClearCartAsync(sessionId);
            
            TempData["Success"] = "Keranjang berhasil dikosongkan.";
            return RedirectToAction("Index");
        }
        
        // API endpoint for cart count (for navbar)
        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var sessionId = GetSessionId();
            var count = await _cartService.GetCartItemCountAsync(sessionId);
            return Json(new { count });
        }
    }
}