using Microsoft.AspNetCore.Mvc;
using BabyShopWeb2.Data;
using BabyShopWeb2.Services;
using BabyShopWeb2.Models;
using BabyShopWeb2.Models.MongoDB;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace BabyShopWeb2.Controllers
{
    public class MongoAdminController : Controller
    {
        private readonly ApplicationDbContext _sqliteContext;
        private readonly MongoDbContext _mongoContext;
        
        public MongoAdminController(ApplicationDbContext sqliteContext, MongoDbContext mongoContext)
        {
            _sqliteContext = sqliteContext;
            _mongoContext = mongoContext;
        }
        
        // Index page with UI
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        // Seed MongoDB dengan data awal
        [HttpGet]
        public async Task<IActionResult> SeedMongoDB()
        {
            try
            {
                var seeder = new MongoDbSeeder(_mongoContext);
                await seeder.SeedAsync();
                
                return Content(@"
✅ MongoDB Seeding Completed!

Data yang di-seed:
- 4 Categories
- 15 Products
- 1 Admin User (username: admin, password: admin123)

Silakan cek di MongoDB Compass atau akses:
http://localhost:5055/MongoAdmin/CheckMongoDB

Kembali ke Admin: http://localhost:5055/Admin
                ", "text/plain");
            }
            catch (Exception ex)
            {
                return Content($"❌ Error seeding MongoDB: {ex.Message}\n\nPastikan MongoDB sudah berjalan!", "text/plain");
            }
        }
        
        // Cek data di MongoDB
        [HttpGet]
        public async Task<IActionResult> CheckMongoDB()
        {
            try
            {
                var productsCount = await _mongoContext.Products.CountDocumentsAsync(FilterDefinition<MongoProduct>.Empty);
                var categoriesCount = await _mongoContext.Categories.CountDocumentsAsync(FilterDefinition<MongoCategory>.Empty);
                var ordersCount = await _mongoContext.Orders.CountDocumentsAsync(FilterDefinition<MongoOrder>.Empty);
                var adminCount = await _mongoContext.AdminUsers.CountDocumentsAsync(FilterDefinition<MongoAdminUser>.Empty);
                
                var products = await _mongoContext.Products.Find(FilterDefinition<MongoProduct>.Empty).Limit(5).ToListAsync();
                
                var result = $@"
🍃 MongoDB Status Check

📊 Collections Count:
- Products: {productsCount}
- Categories: {categoriesCount}
- Orders: {ordersCount}
- Admin Users: {adminCount}

📦 Sample Products (first 5):
";
                
                foreach (var product in products)
                {
                    result += $@"
- {product.Name}
  Price: Rp {product.Price:N0}
  Stock: {product.Stock}
  Category: {product.CategoryName}
";
                }
                
                result += @"

✅ MongoDB Connection: OK
Database: BabyShop3Berlian

Actions:
- Seed Data: http://localhost:5055/MongoAdmin/SeedMongoDB
- Migrate from SQLite: http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB
- Back to Admin: http://localhost:5055/Admin
";
                
                return Content(result, "text/plain");
            }
            catch (Exception ex)
            {
                return Content($@"
❌ MongoDB Connection Failed!

Error: {ex.Message}

Troubleshooting:
1. Pastikan MongoDB sudah terinstall dan berjalan
2. Cek connection string di appsettings.json
3. Untuk local: mongodb://localhost:27017
4. Untuk Atlas: mongodb+srv://...

Install MongoDB:
https://www.mongodb.com/try/download/community

Back to Admin: http://localhost:5055/Admin
                ", "text/plain");
            }
        }
        
        // Migrasi data dari SQLite ke MongoDB
        [HttpGet]
        public async Task<IActionResult> MigrateSQLiteToMongoDB()
        {
            try
            {
                var result = "";
                
                // Migrate Categories
                var sqliteCategories = await _sqliteContext.Categories.ToListAsync();
                var mongoCategories = new List<MongoCategory>();
                
                foreach (var cat in sqliteCategories)
                {
                    mongoCategories.Add(new MongoCategory
                    {
                        Name = cat.Name,
                        Description = cat.Description,
                        IconClass = cat.IconClass,
                        IsActive = cat.IsActive,
                        CreatedAt = DateTime.Now
                    });
                }
                
                if (mongoCategories.Any())
                {
                    await _mongoContext.Categories.InsertManyAsync(mongoCategories);
                    result += $"✅ Migrated {mongoCategories.Count} categories\n";
                }
                
                // Get category mapping
                var mongoCategoriesFromDb = await _mongoContext.Categories.Find(FilterDefinition<MongoCategory>.Empty).ToListAsync();
                
                // Migrate Products
                var sqliteProducts = await _sqliteContext.Products.Include(p => p.Category).ToListAsync();
                var mongoProducts = new List<MongoProduct>();
                
                foreach (var prod in sqliteProducts)
                {
                    var mongoCat = mongoCategoriesFromDb.FirstOrDefault(c => c.Name == prod.Category?.Name);
                    
                    mongoProducts.Add(new MongoProduct
                    {
                        Name = prod.Name,
                        Description = prod.Description,
                        Price = prod.Price,
                        DiscountPrice = prod.DiscountPrice,
                        Stock = prod.Stock,
                        ImageUrl = prod.ImageUrl,
                        CategoryId = mongoCat?.Id ?? "",
                        CategoryName = mongoCat?.Name ?? "",
                        IsActive = prod.IsActive,
                        CreatedAt = DateTime.Now
                    });
                }
                
                if (mongoProducts.Any())
                {
                    await _mongoContext.Products.InsertManyAsync(mongoProducts);
                    result += $"✅ Migrated {mongoProducts.Count} products\n";
                }
                
                // Migrate Orders
                var sqliteOrders = await _sqliteContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
                var mongoOrders = new List<MongoOrder>();
                
                foreach (var order in sqliteOrders)
                {
                    var mongoOrder = new MongoOrder
                    {
                        OrderNumber = order.OrderNumber,
                        CustomerName = order.CustomerName,
                        CustomerEmail = order.CustomerEmail,
                        CustomerPhone = order.CustomerPhone,
                        ShippingAddress = order.ShippingAddress,
                        TotalAmount = order.TotalAmount,
                        PaymentMethod = order.Notes ?? "Unknown", // Use Notes as PaymentMethod temporarily
                        PaymentStatus = order.Status == OrderStatus.Delivered ? "Paid" : "Pending",
                        OrderStatus = order.Status.ToString(),
                        CreatedAt = order.OrderDate,
                        Items = new List<MongoOrderItem>()
                    };
                    
                    foreach (var item in order.OrderItems)
                    {
                        mongoOrder.Items.Add(new MongoOrderItem
                        {
                            ProductId = item.ProductId.ToString(),
                            ProductName = item.Product?.Name ?? "",
                            Quantity = item.Quantity,
                            Price = item.UnitPrice,
                            Subtotal = item.Subtotal
                        });
                    }
                    
                    mongoOrders.Add(mongoOrder);
                }
                
                if (mongoOrders.Any())
                {
                    await _mongoContext.Orders.InsertManyAsync(mongoOrders);
                    result += $"✅ Migrated {mongoOrders.Count} orders\n";
                }
                
                // Migrate Admin Users
                var sqliteAdmins = await _sqliteContext.AdminUsers.ToListAsync();
                var mongoAdmins = new List<MongoAdminUser>();
                
                foreach (var admin in sqliteAdmins)
                {
                    mongoAdmins.Add(new MongoAdminUser
                    {
                        Username = admin.Username,
                        PasswordHash = admin.PasswordHash,
                        FullName = admin.FullName,
                        CreatedAt = admin.CreatedAt,
                        LastLogin = admin.LastLogin
                    });
                }
                
                if (mongoAdmins.Any())
                {
                    await _mongoContext.AdminUsers.InsertManyAsync(mongoAdmins);
                    result += $"✅ Migrated {mongoAdmins.Count} admin users\n";
                }
                
                return Content($@"
🔄 Migration Completed!

{result}

📊 Summary:
- All data from SQLite has been copied to MongoDB
- SQLite data is still intact (not deleted)
- You can now use MongoDB for production

Next Steps:
1. Verify data: http://localhost:5055/MongoAdmin/CheckMongoDB
2. Check in MongoDB Compass
3. Update appsettings.json to use MongoDB

Back to Admin: http://localhost:5055/Admin
                ", "text/plain");
            }
            catch (Exception ex)
            {
                return Content($@"
❌ Migration Failed!

Error: {ex.Message}

Possible causes:
1. MongoDB not running
2. Data already exists in MongoDB (delete collections first)
3. Connection string incorrect

Back to Admin: http://localhost:5055/Admin
                ", "text/plain");
            }
        }
        
        // Clear MongoDB data
        [HttpGet]
        public async Task<IActionResult> ClearMongoDB()
        {
            try
            {
                await _mongoContext.Products.DeleteManyAsync(FilterDefinition<MongoProduct>.Empty);
                await _mongoContext.Categories.DeleteManyAsync(FilterDefinition<MongoCategory>.Empty);
                await _mongoContext.Orders.DeleteManyAsync(FilterDefinition<MongoOrder>.Empty);
                await _mongoContext.AdminUsers.DeleteManyAsync(FilterDefinition<MongoAdminUser>.Empty);
                
                return Content(@"
✅ MongoDB Cleared!

All collections have been emptied:
- Products
- Categories
- Orders
- Admin Users

You can now:
- Seed fresh data: http://localhost:5055/MongoAdmin/SeedMongoDB
- Migrate from SQLite: http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB

Back to Admin: http://localhost:5055/Admin
                ", "text/plain");
            }
            catch (Exception ex)
            {
                return Content($"❌ Error clearing MongoDB: {ex.Message}", "text/plain");
            }
        }
    }
}
