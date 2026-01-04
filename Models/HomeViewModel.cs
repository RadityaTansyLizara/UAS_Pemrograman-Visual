namespace BabyShopWeb2.Models
{
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}