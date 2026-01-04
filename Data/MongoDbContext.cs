using MongoDB.Driver;
using BabyShopWeb2.Models.MongoDB;

namespace BabyShopWeb2.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        
        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb") 
                ?? "mongodb://localhost:27017";
            var databaseName = configuration["MongoDb:DatabaseName"] ?? "BabyShop3Berlian";
            
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        
        public IMongoCollection<MongoProduct> Products => 
            _database.GetCollection<MongoProduct>("products");
        
        public IMongoCollection<MongoCategory> Categories => 
            _database.GetCollection<MongoCategory>("categories");
        
        public IMongoCollection<MongoOrder> Orders => 
            _database.GetCollection<MongoOrder>("orders");
        
        public IMongoCollection<MongoAdminUser> AdminUsers => 
            _database.GetCollection<MongoAdminUser>("adminUsers");
        
        public IMongoCollection<T> GetCollection<T>(string name) => 
            _database.GetCollection<T>(name);
    }
}
