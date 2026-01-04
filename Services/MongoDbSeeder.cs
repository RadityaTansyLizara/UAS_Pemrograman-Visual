using MongoDB.Driver;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models.MongoDB;
using System.Security.Cryptography;
using System.Text;

namespace BabyShopWeb2.Services
{
    public class MongoDbSeeder
    {
        private readonly MongoDbContext _context;
        
        public MongoDbSeeder(MongoDbContext context)
        {
            _context = context;
        }
        
        public async Task SeedAsync()
        {
            // Seed Categories
            await SeedCategoriesAsync();
            
            // Seed Products
            await SeedProductsAsync();
            
            // Seed Admin User
            await SeedAdminUserAsync();
        }
        
        private async Task SeedCategoriesAsync()
        {
            var count = await _context.Categories.CountDocumentsAsync(FilterDefinition<MongoCategory>.Empty);
            if (count > 0)
            {
                Console.WriteLine($"✅ Categories already exist ({count} categories)");
                return; // Already seeded
            }
            
            var categories = new List<MongoCategory>
            {
                new MongoCategory
                {
                    Name = "Pakaian Bayi",
                    Description = "Baju, celana, dan aksesoris bayi yang nyaman dan lucu",
                    IconClass = "fas fa-tshirt",
                    IsActive = true
                },
                new MongoCategory
                {
                    Name = "Mainan Edukatif",
                    Description = "Mainan yang aman dan mendukung perkembangan anak",
                    IconClass = "fas fa-puzzle-piece",
                    IsActive = true
                },
                new MongoCategory
                {
                    Name = "Perlengkapan Makanan",
                    Description = "Botol susu, sendok, dan peralatan makan bayi",
                    IconClass = "fas fa-utensils",
                    IsActive = true
                },
                new MongoCategory
                {
                    Name = "Perawatan Bayi",
                    Description = "Sabun, shampoo, dan produk perawatan bayi",
                    IconClass = "fas fa-bath",
                    IsActive = true
                }
            };
            
            await _context.Categories.InsertManyAsync(categories);
            Console.WriteLine($"✅ Seeded {categories.Count} categories to MongoDB");
        }
        
        private async Task SeedProductsAsync()
        {
            var count = await _context.Products.CountDocumentsAsync(FilterDefinition<MongoProduct>.Empty);
            if (count > 0)
            {
                Console.WriteLine($"✅ Products already exist ({count} products)");
                return; // Already seeded
            }
            
            // Get category IDs
            var categories = await _context.Categories.Find(FilterDefinition<MongoCategory>.Empty).ToListAsync();
            var pakaianCat = categories.FirstOrDefault(c => c.Name == "Pakaian Bayi");
            var mainanCat = categories.FirstOrDefault(c => c.Name == "Mainan Edukatif");
            var makanCat = categories.FirstOrDefault(c => c.Name == "Perlengkapan Makan");
            var perawatanCat = categories.FirstOrDefault(c => c.Name == "Perawatan Bayi");
            
            var products = new List<MongoProduct>
            {
                // Pakaian Bayi
                new MongoProduct
                {
                    Name = "Baju Bayi Cussons Baby Pink",
                    Description = "Baju bayi 100% katun organik dari Cussons Baby dengan motif lucu, nyaman untuk sehari-hari",
                    Price = 65000,
                    Stock = 50,
                    ImageUrl = "/images/products/cussons-baju-pink.svg",
                    CategoryId = pakaianCat?.Id ?? "",
                    CategoryName = pakaianCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Celana Bayi My Baby Soft Blue",
                    Description = "Celana bayi berbahan lembut dari My Baby, elastis dan mudah dipakai untuk si kecil",
                    Price = 45000,
                    Stock = 40,
                    ImageUrl = "/images/products/mybaby-celana-blue.svg",
                    CategoryId = pakaianCat?.Id ?? "",
                    CategoryName = pakaianCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Set Baju Tidur Zwitsal",
                    Description = "Set lengkap baju tidur bayi Zwitsal dengan topi, sarung tangan dan kaki",
                    Price = 125000,
                    DiscountPrice = 99000,
                    Stock = 30,
                    ImageUrl = "/images/products/zwitsal-set-tidur.svg",
                    CategoryId = pakaianCat?.Id ?? "",
                    CategoryName = pakaianCat?.Name ?? "",
                    IsActive = true
                },
                
                // Mainan Edukatif
                new MongoProduct
                {
                    Name = "Puzzle Kayu Fisher-Price",
                    Description = "Puzzle kayu edukatif Fisher-Price untuk belajar angka 1-10, aman untuk bayi",
                    Price = 85000,
                    Stock = 25,
                    ImageUrl = "/images/products/fisherprice-puzzle.svg",
                    CategoryId = mainanCat?.Id ?? "",
                    CategoryName = mainanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Mainan Rattle Pigeon",
                    Description = "Mainan rattle Pigeon dengan suara lembut, melatih motorik bayi",
                    Price = 35000,
                    Stock = 60,
                    ImageUrl = "/images/products/pigeon-rattle.svg",
                    CategoryId = mainanCat?.Id ?? "",
                    CategoryName = mainanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Soft Book Fabric Chicco",
                    Description = "Buku kain lembut Chicco dengan gambar-gambar menarik untuk stimulasi bayi",
                    Price = 75000,
                    DiscountPrice = 65000,
                    Stock = 35,
                    ImageUrl = "/images/products/chicco-softbook.svg",
                    CategoryId = mainanCat?.Id ?? "",
                    CategoryName = mainanCat?.Name ?? "",
                    IsActive = true
                },
                
                // Perlengkapan Makan
                new MongoProduct
                {
                    Name = "Botol Susu Pigeon Anti Kolik",
                    Description = "Botol susu Pigeon dengan sistem anti kolik, BPA free dan aman untuk bayi",
                    Price = 125000,
                    Stock = 20,
                    ImageUrl = "/images/products/pigeon-botol.svg",
                    CategoryId = makanCat?.Id ?? "",
                    CategoryName = makanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Set Sendok Makan Dr. Brown's",
                    Description = "Set sendok dan garpu khusus bayi Dr. Brown's, ergonomis dan aman",
                    Price = 55000,
                    Stock = 45,
                    ImageUrl = "/images/products/drbrowns-sendok.svg",
                    CategoryId = makanCat?.Id ?? "",
                    CategoryName = makanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Mangkuk Anti Tumpah Munchkin",
                    Description = "Mangkuk Munchkin dengan desain anti tumpah, mudah digenggam bayi",
                    Price = 75000,
                    Stock = 30,
                    ImageUrl = "/images/products/munchkin-mangkuk.svg",
                    CategoryId = makanCat?.Id ?? "",
                    CategoryName = makanCat?.Name ?? "",
                    IsActive = true
                },
                
                // Perawatan Bayi
                new MongoProduct
                {
                    Name = "Johnson's Baby Shampoo No More Tears",
                    Description = "Shampoo bayi Johnson's formula gentle, tidak pedih di mata, 200ml",
                    Price = 25000,
                    Stock = 40,
                    ImageUrl = "/images/products/johnsons-shampoo.svg",
                    CategoryId = perawatanCat?.Id ?? "",
                    CategoryName = perawatanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Cussons Baby Cream & Lotion",
                    Description = "Lotion bayi Cussons dengan formula melembabkan, cocok untuk kulit sensitif",
                    Price = 35000,
                    DiscountPrice = 28000,
                    Stock = 35,
                    ImageUrl = "/images/products/cussons-lotion.svg",
                    CategoryId = perawatanCat?.Id ?? "",
                    CategoryName = perawatanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "My Baby Minyak Telon Plus",
                    Description = "Minyak telon My Baby dengan formula plus, menghangatkan dan melindungi bayi",
                    Price = 15000,
                    Stock = 80,
                    ImageUrl = "/images/products/mybaby-telon.svg",
                    CategoryId = perawatanCat?.Id ?? "",
                    CategoryName = perawatanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Zwitsal Baby Powder Classic",
                    Description = "Bedak bayi Zwitsal classic dengan formula lembut, menjaga kulit bayi tetap kering",
                    Price = 18000,
                    Stock = 60,
                    ImageUrl = "/images/products/zwitsal-powder.svg",
                    CategoryId = perawatanCat?.Id ?? "",
                    CategoryName = perawatanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Johnson's Baby Wipes Sensitive",
                    Description = "Tisu basah bayi Johnson's untuk kulit sensitif, alcohol free, 80 sheets",
                    Price = 22000,
                    Stock = 70,
                    ImageUrl = "/images/products/johnsons-wipes.svg",
                    CategoryId = perawatanCat?.Id ?? "",
                    CategoryName = perawatanCat?.Name ?? "",
                    IsActive = true
                },
                new MongoProduct
                {
                    Name = "Cussons Baby Hair & Body Wash",
                    Description = "Sabun cair bayi Cussons 2 in 1 untuk rambut dan badan, formula mild",
                    Price = 32000,
                    Stock = 45,
                    ImageUrl = "/images/products/cussons-wash.svg",
                    CategoryId = perawatanCat?.Id ?? "",
                    CategoryName = perawatanCat?.Name ?? "",
                    IsActive = true
                }
            };
            
            await _context.Products.InsertManyAsync(products);
            Console.WriteLine($"✅ Seeded {products.Count} products to MongoDB");
        }
        
        private async Task SeedAdminUserAsync()
        {
            var count = await _context.AdminUsers.CountDocumentsAsync(FilterDefinition<MongoAdminUser>.Empty);
            if (count > 0)
            {
                Console.WriteLine($"✅ Admin user already exists");
                return; // Already seeded
            }
            
            var adminUser = new MongoAdminUser
            {
                Username = "admin",
                PasswordHash = HashPassword("admin123"),
                FullName = "Administrator",
                CreatedAt = DateTime.Now
            };
            
            await _context.AdminUsers.InsertOneAsync(adminUser);
            Console.WriteLine("✅ Seeded admin user to MongoDB (username: admin, password: admin123)");
        }
        
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
