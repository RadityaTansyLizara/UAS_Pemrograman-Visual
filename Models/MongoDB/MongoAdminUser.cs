using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BabyShopWeb2.Models.MongoDB
{
    public class MongoAdminUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;
        
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;
        
        [BsonElement("fullName")]
        public string FullName { get; set; } = string.Empty;
        
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [BsonElement("lastLogin")]
        public DateTime? LastLogin { get; set; }
    }
}
