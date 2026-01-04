using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models;
using System.Security.Cryptography;
using System.Text;

namespace BabyShopWeb2.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Jika sudah login, redirect ke admin
            if (HttpContext.Session.GetString("AdminUsername") != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Hash password untuk pengecekan
            var passwordHash = HashPassword(model.Password);
            
            // Debug: Log untuk troubleshooting
            Console.WriteLine($"Login attempt - Username: {model.Username}");
            Console.WriteLine($"Password hash generated: {passwordHash}");
            
            // Cek apakah user ada di database
            var userExists = await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Username == model.Username);
            
            if (userExists != null)
            {
                Console.WriteLine($"User found in DB - Username: {userExists.Username}");
                Console.WriteLine($"Stored password hash: {userExists.PasswordHash}");
                Console.WriteLine($"Hashes match: {userExists.PasswordHash == passwordHash}");
            }
            else
            {
                Console.WriteLine("User not found in database");
            }
            
            // Cek user di database
            var user = await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.PasswordHash == passwordHash);

            if (user == null)
            {
                ModelState.AddModelError("", "Username atau password salah");
                return View(model);
            }

            // Update last login
            user.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            // Set session
            HttpContext.Session.SetString("AdminUsername", user.Username);
            HttpContext.Session.SetString("AdminFullName", user.FullName);
            HttpContext.Session.SetInt32("AdminId", user.Id);

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Debug endpoint untuk cek admin user
        [HttpGet]
        public async Task<IActionResult> CheckAdmin()
        {
            var adminUsers = await _context.AdminUsers.ToListAsync();
            var result = new System.Text.StringBuilder();
            
            result.AppendLine($"Total admin users: {adminUsers.Count}");
            result.AppendLine();
            
            foreach (var admin in adminUsers)
            {
                result.AppendLine($"ID: {admin.Id}");
                result.AppendLine($"Username: {admin.Username}");
                result.AppendLine($"PasswordHash: {admin.PasswordHash}");
                result.AppendLine($"FullName: {admin.FullName}");
                result.AppendLine($"CreatedAt: {admin.CreatedAt}");
                result.AppendLine($"LastLogin: {admin.LastLogin}");
                result.AppendLine();
            }
            
            // Test hash untuk password "admin123"
            var testHash = HashPassword("admin123");
            result.AppendLine($"Test hash for 'admin123': {testHash}");
            
            return Content(result.ToString(), "text/plain");
        }
        
        // Reset password admin
        [HttpGet]
        public async Task<IActionResult> ResetAdminPassword()
        {
            var admin = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Username == "admin");
            
            if (admin == null)
            {
                return Content("Admin user not found!", "text/plain");
            }
            
            // Reset password ke admin123
            var newPasswordHash = HashPassword("admin123");
            admin.PasswordHash = newPasswordHash;
            await _context.SaveChangesAsync();
            
            return Content($@"
✅ Password admin berhasil direset!

Username: admin
Password: admin123
New Hash: {newPasswordHash}

Silakan login kembali: http://localhost:5055/Auth/Login
            ", "text/plain");
        }

        // Helper method untuk hash password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
