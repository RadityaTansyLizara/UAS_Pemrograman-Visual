using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BabyShopWeb2.Models.MongoDB
{
    public class MongoCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
        
        [BsonElement("iconClass")]
        public string IconClass { get; set; } = string.Empty;
        
        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;
        
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
