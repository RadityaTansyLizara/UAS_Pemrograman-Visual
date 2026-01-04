using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BabyShopWeb2.Models.MongoDB
{
    public class MongoOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("orderNumber")]
        public string OrderNumber { get; set; } = string.Empty;
        
        [BsonElement("customerName")]
        public string CustomerName { get; set; } = string.Empty;
        
        [BsonElement("customerEmail")]
        public string CustomerEmail { get; set; } = string.Empty;
        
        [BsonElement("customerPhone")]
        public string CustomerPhone { get; set; } = string.Empty;
        
        [BsonElement("shippingAddress")]
        public string ShippingAddress { get; set; } = string.Empty;
        
        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }
        
        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty;
        
        [BsonElement("paymentStatus")]
        public string PaymentStatus { get; set; } = "Pending";
        
        [BsonElement("orderStatus")]
        public string OrderStatus { get; set; } = "Pending";
        
        [BsonElement("items")]
        public List<MongoOrderItem> Items { get; set; } = new List<MongoOrderItem>();
        
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
    
    public class MongoOrderItem
    {
        [BsonElement("productId")]
        public string ProductId { get; set; } = string.Empty;
        
        [BsonElement("productName")]
        public string ProductName { get; set; } = string.Empty;
        
        [BsonElement("quantity")]
        public int Quantity { get; set; }
        
        [BsonElement("price")]
        public decimal Price { get; set; }
        
        [BsonElement("subtotal")]
        public decimal Subtotal { get; set; }
    }
}
