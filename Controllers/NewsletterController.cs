using Microsoft.AspNetCore.Mvc;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyShopWeb2.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsletterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["Error"] = "Email tidak boleh kosong";
                return RedirectToAction("Index", "Home");
            }

            // Validasi format email
            if (!IsValidEmail(email))
            {
                TempData["Error"] = "Format email tidak valid";
                return RedirectToAction("Index", "Home");
            }

            // Cek apakah email sudah terdaftar
            var existingSubscriber = await _context.Newsletters
                .FirstOrDefaultAsync(n => n.Email.ToLower() == email.ToLower());

            if (existingSubscriber != null)
            {
                if (existingSubscriber.IsActive)
                {
                    TempData["Info"] = "Email Anda sudah terdaftar di newsletter kami";
                }
                else
                {
                    existingSubscriber.IsActive = true;
                    existingSubscriber.SubscribedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Selamat! Anda berhasil berlangganan newsletter kami kembali";
                }
                return RedirectToAction("ThankYou");
            }

            // Tambah subscriber baru
            var newsletter = new Newsletter
            {
                Email = email.Trim().ToLower(),
                SubscribedAt = DateTime.Now,
                IsActive = true
            };

            _context.Newsletters.Add(newsletter);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Terima kasih! Anda berhasil berlangganan newsletter kami";
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["Error"] = "Email tidak boleh kosong";
                return RedirectToAction("Index", "Home");
            }

            var subscriber = await _context.Newsletters
                .FirstOrDefaultAsync(n => n.Email.ToLower() == email.ToLower());

            if (subscriber != null)
            {
                subscriber.IsActive = false;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Anda berhasil berhenti berlangganan newsletter";
            }
            else
            {
                TempData["Error"] = "Email tidak ditemukan dalam daftar newsletter";
            }

            return RedirectToAction("Index", "Home");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
