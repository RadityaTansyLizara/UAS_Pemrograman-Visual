using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BabyShopWeb2.Models.MongoDB
{
    public class MongoProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
        
        [BsonElement("price")]
        public decimal Price { get; set; }
        
        [BsonElement("discountPrice")]
        public decimal? DiscountPrice { get; set; }
        
        [BsonElement("stock")]
        public int Stock { get; set; }
        
        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;
        
        [BsonElement("categoryId")]
        public string CategoryId { get; set; } = string.Empty;
        
        [BsonElement("categoryName")]
        public string CategoryName { get; set; } = string.Empty;
        
        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;
        
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
