using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models;

namespace BabyShopWeb2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                FeaturedProducts = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive)
                    .OrderByDescending(p => p.Id)
                    .Take(8)
                    .ToListAsync(),
                Categories = await _context.Categories
                    .Where(c => c.IsActive)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(string name, string email, string subject, string message)
        {
            if (ModelState.IsValid)
            {
                var contactMessage = new ContactMessage
                {
                    Name = name,
                    Email = email,
                    Subject = subject,
                    Message = message,
                    CreatedAt = DateTime.Now,
                    IsRead = false
                };

                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Terima kasih! Pesan Anda telah dikirim. Kami akan segera menghubungi Anda.";
            }
            
            return RedirectToAction("Contact");
        }
    }
}
