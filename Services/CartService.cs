using BabyShopWeb2.Data;
using BabyShopWeb2.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyShopWeb2.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(string sessionId);
        Task AddToCartAsync(string sessionId, int productId, int quantity = 1);
        Task UpdateCartItemAsync(string sessionId, int productId, int quantity);
        Task RemoveFromCartAsync(string sessionId, int productId);
        Task ClearCartAsync(string sessionId);
        Task<int> GetCartItemCountAsync(string sessionId);
    }
    
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        
        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Cart> GetCartAsync(string sessionId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);
                
            if (cart == null)
            {
                cart = new Cart { SessionId = sessionId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            
            return cart;
        }
        
        public async Task AddToCartAsync(string sessionId, int productId, int quantity = 1)
        {
            var cart = await GetCartAsync(sessionId);
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null && product.IsActive && product.Stock >= quantity)
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
            }
            
            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateCartItemAsync(string sessionId, int productId, int quantity)
        {
            var cart = await GetCartAsync(sessionId);
            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.CartItems.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                
                cart.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task RemoveFromCartAsync(string sessionId, int productId)
        {
            var cart = await GetCartAsync(sessionId);
            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            
            if (item != null)
            {
                cart.CartItems.Remove(item);
                cart.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task ClearCartAsync(string sessionId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);
                
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<int> GetCartItemCountAsync(string sessionId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);
                
            return cart?.CartItems.Sum(ci => ci.Quantity) ?? 0;
        }
    }
}