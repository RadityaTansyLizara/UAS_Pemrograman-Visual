using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models;

namespace BabyShopWeb2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(int? categoryId, string? search, string? sortBy)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive);
            
            // Filter by category
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }
            
            // Search
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }
            
            // Get products first, then sort in memory (SQLite doesn't support decimal in ORDER BY)
            var products = await query.ToListAsync();
            
            // Sort in memory
            products = sortBy switch
            {
                "price_asc" => products.OrderBy(p => p.Price).ToList(),
                "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
                "name" => products.OrderBy(p => p.Name).ToList(),
                _ => products.OrderBy(p => p.Id).ToList()
            };
            
            ViewBag.Categories = await _context.Categories.Where(c => c.IsActive).ToListAsync();
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sortBy;
            
            return View(products);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
                
            if (product == null)
            {
                return NotFound();
            }
            
            // Get related products from same category
            var relatedProducts = await _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id && p.IsActive)
                .Take(4)
                .ToListAsync();
                
            ViewBag.RelatedProducts = relatedProducts;
            
            return View(product);
        }
        
        public async Task<IActionResult> Category(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
                
            if (category == null)
            {
                return NotFound();
            }
            
            var products = await _context.Products
                .Where(p => p.CategoryId == id && p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
                
            ViewBag.Category = category;
            return View(products);
        }
    }
}