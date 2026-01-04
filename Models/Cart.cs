using System.ComponentModel.DataAnnotations;

namespace BabyShopWeb2.Models
{
    public class Cart
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string SessionId { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        // Calculated properties
        public decimal TotalAmount => CartItems?.Sum(item => item.Quantity * (item.Product?.Price ?? 0)) ?? 0;
        public int TotalItems => CartItems?.Sum(item => item.Quantity) ?? 0;
    }
    
    public class CartItem
    {
        public int Id { get; set; }
        
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
        
        // Calculated properties
        public decimal Subtotal => Quantity * (Product?.Price ?? 0);
    }
}