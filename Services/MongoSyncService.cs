using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models;
using BabyShopWeb2.Models.MongoDB;

namespace BabyShopWeb2.Services
{
    public interface IMongoSyncService
    {
        Task SyncProductAsync(Product product);
        Task SyncProductUpdateAsync(Product product);
        Task SyncProductDeleteAsync(int productId);
        Task SyncOrderAsync(Order order);
        Task<bool> IsMongoDBAvailable();
    }
    
    public class MongoSyncService : IMongoSyncService
    {
        private readonly MongoDbContext _mongoContext;
        private readonly ILogger<MongoSyncService> _logger;
        private readonly IConfiguration _configuration;
        
        public MongoSyncService(
            MongoDbContext mongoContext, 
            ILogger<MongoSyncService> logger,
            IConfiguration configuration)
        {
            _mongoContext = mongoContext;
            _logger = logger;
            _configuration = configuration;
        }
        
        // Check if MongoDB is available
        public async Task<bool> IsMongoDBAvailable()
        {
            try
            {
                // Check if auto-sync is enabled in config
                var enableSync = _configuration.GetValue<bool>("DatabaseSettings:EnableMongoDBBackup");
                if (!enableSync)
                {
                    return false;
                }
                
                // Try to ping MongoDB
                await _mongoContext.Products.CountDocumentsAsync(FilterDefinition<MongoProduct>.Empty);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"MongoDB not available: {ex.Message}");
                return false;
            }
        }
        
        // Sync new product to MongoDB
        public async Task SyncProductAsync(Product product)
        {
            if (!await IsMongoDBAvailable()) return;
            
            try
            {
                var mongoProduct = new MongoProduct
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountPrice = product.DiscountPrice,
                    Stock = product.Stock,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId.ToString(),
                    CategoryName = product.Category?.Name ?? "",
                    IsActive = product.IsActive,
                    CreatedAt = DateTime.Now
                };
                
                await _mongoContext.Products.InsertOneAsync(mongoProduct);
                _logger.LogInformation($"✅ Product '{product.Name}' synced to MongoDB");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Failed to sync product to MongoDB: {ex.Message}");
            }
        }
        
        // Sync product update to MongoDB
        public async Task SyncProductUpdateAsync(Product product)
        {
            if (!await IsMongoDBAvailable()) return;
            
            try
            {
                // Find existing product in MongoDB by name (since SQLite ID != MongoDB ID)
                var filter = Builders<MongoProduct>.Filter.Eq(p => p.Name, product.Name);
                var existingProduct = await _mongoContext.Products.Find(filter).FirstOrDefaultAsync();
                
                if (existingProduct != null)
                {
                    // Update existing
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.DiscountPrice = product.DiscountPrice;
                    existingProduct.Stock = product.Stock;
                    existingProduct.ImageUrl = product.ImageUrl;
                    existingProduct.CategoryId = product.CategoryId.ToString();
                    existingProduct.CategoryName = product.Category?.Name ?? "";
                    existingProduct.IsActive = product.IsActive;
                    existingProduct.UpdatedAt = DateTime.Now;
                    
                    await _mongoContext.Products.ReplaceOneAsync(
                        p => p.Id == existingProduct.Id, 
                        existingProduct
                    );
                    
                    _logger.LogInformation($"✅ Product '{product.Name}' updated in MongoDB");
                }
                else
                {
                    // Product doesn't exist, create new
                    await SyncProductAsync(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Failed to update product in MongoDB: {ex.Message}");
            }
        }
        
        // Sync product delete to MongoDB
        public async Task SyncProductDeleteAsync(int productId)
        {
            if (!await IsMongoDBAvailable()) return;
            
            try
            {
                // Soft delete - set IsActive to false
                var filter = Builders<MongoProduct>.Filter.Eq(p => p.CategoryId, productId.ToString());
                var update = Builders<MongoProduct>.Update
                    .Set(p => p.IsActive, false)
                    .Set(p => p.UpdatedAt, DateTime.Now);
                
                await _mongoContext.Products.UpdateOneAsync(filter, update);
                _logger.LogInformation($"✅ Product ID {productId} deleted in MongoDB");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Failed to delete product in MongoDB: {ex.Message}");
            }
        }
        
        // Sync order to MongoDB
        public async Task SyncOrderAsync(Order order)
        {
            if (!await IsMongoDBAvailable()) return;
            
            try
            {
                var mongoOrder = new MongoOrder
                {
                    OrderNumber = order.OrderNumber,
                    CustomerName = order.CustomerName,
                    CustomerEmail = order.CustomerEmail,
                    CustomerPhone = order.CustomerPhone,
                    ShippingAddress = order.ShippingAddress,
                    TotalAmount = order.TotalAmount,
                    PaymentMethod = order.Notes ?? "Unknown",
                    PaymentStatus = order.Status == OrderStatus.Delivered ? "Paid" : "Pending",
                    OrderStatus = order.Status.ToString(),
                    CreatedAt = order.OrderDate,
                    Items = order.OrderItems.Select(item => new MongoOrderItem
                    {
                        ProductId = item.ProductId.ToString(),
                        ProductName = item.Product?.Name ?? "",
                        Quantity = item.Quantity,
                        Price = item.UnitPrice,
                        Subtotal = item.Subtotal
                    }).ToList()
                };
                
                await _mongoContext.Orders.InsertOneAsync(mongoOrder);
                _logger.LogInformation($"✅ Order '{order.OrderNumber}' synced to MongoDB");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Failed to sync order to MongoDB: {ex.Message}");
            }
        }
    }
}
