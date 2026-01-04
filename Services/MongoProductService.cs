using MongoDB.Driver;
using BabyShopWeb2.Data;
using BabyShopWeb2.Models.MongoDB;

namespace BabyShopWeb2.Services
{
    public interface IMongoProductService
    {
        Task<List<MongoProduct>> GetAllProductsAsync();
        Task<MongoProduct?> GetProductByIdAsync(string id);
        Task<List<MongoProduct>> GetProductsByCategoryAsync(string categoryId);
        Task<MongoProduct> CreateProductAsync(MongoProduct product);
        Task<bool> UpdateProductAsync(string id, MongoProduct product);
        Task<bool> DeleteProductAsync(string id);
        Task<List<MongoProduct>> SearchProductsAsync(string searchTerm);
    }
    
    public class MongoProductService : IMongoProductService
    {
        private readonly MongoDbContext _context;
        
        public MongoProductService(MongoDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<MongoProduct>> GetAllProductsAsync()
        {
            return await _context.Products
                .Find(p => p.IsActive)
                .SortBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<MongoProduct?> GetProductByIdAsync(string id)
        {
            return await _context.Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
        
        public async Task<List<MongoProduct>> GetProductsByCategoryAsync(string categoryId)
        {
            return await _context.Products
                .Find(p => p.CategoryId == categoryId && p.IsActive)
                .SortBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<MongoProduct> CreateProductAsync(MongoProduct product)
        {
            product.CreatedAt = DateTime.Now;
            await _context.Products.InsertOneAsync(product);
            return product;
        }
        
        public async Task<bool> UpdateProductAsync(string id, MongoProduct product)
        {
            product.UpdatedAt = DateTime.Now;
            var result = await _context.Products
                .ReplaceOneAsync(p => p.Id == id, product);
            return result.ModifiedCount > 0;
        }
        
        public async Task<bool> DeleteProductAsync(string id)
        {
            // Soft delete - set IsActive to false
            var update = Builders<MongoProduct>.Update
                .Set(p => p.IsActive, false)
                .Set(p => p.UpdatedAt, DateTime.Now);
            
            var result = await _context.Products
                .UpdateOneAsync(p => p.Id == id, update);
            
            return result.ModifiedCount > 0;
        }
        
        public async Task<List<MongoProduct>> SearchProductsAsync(string searchTerm)
        {
            var filter = Builders<MongoProduct>.Filter.And(
                Builders<MongoProduct>.Filter.Eq(p => p.IsActive, true),
                Builders<MongoProduct>.Filter.Or(
                    Builders<MongoProduct>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                    Builders<MongoProduct>.Filter.Regex(p => p.Description, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
                )
            );
            
            return await _context.Products
                .Find(filter)
                .SortBy(p => p.Name)
                .ToListAsync();
        }
    }
}
