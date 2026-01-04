using System.ComponentModel.DataAnnotations;

namespace BabyShopWeb2.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<Order> RecentOrders { get; set; } = new List<Order>();
        
        // Financial data
        public decimal TodayIncome { get; set; }
        public decimal TodayExpense { get; set; }
        public decimal TodayProfit => TodayIncome - TodayExpense;
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpense { get; set; }
        public decimal MonthlyProfit => MonthlyIncome - MonthlyExpense;
    }
    
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Nama produk wajib diisi")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Deskripsi wajib diisi")]
        [StringLength(500, ErrorMessage = "Deskripsi maksimal 500 karakter")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Harga wajib diisi")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public decimal Price { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Harga diskon tidak boleh negatif")]
        public decimal? DiscountPrice { get; set; }
        
        [Required(ErrorMessage = "Stok wajib diisi")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int Stock { get; set; }
        
        [Required(ErrorMessage = "Kategori wajib dipilih")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Gambar Produk")]
        public IFormFile? ImageFile { get; set; }
    }
    
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nama produk wajib diisi")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Deskripsi wajib diisi")]
        [StringLength(500, ErrorMessage = "Deskripsi maksimal 500 karakter")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Harga wajib diisi")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public decimal Price { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Harga diskon tidak boleh negatif")]
        public decimal? DiscountPrice { get; set; }
        
        [Required(ErrorMessage = "Stok wajib diisi")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int Stock { get; set; }
        
        [Required(ErrorMessage = "Kategori wajib dipilih")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Gambar Produk")]
        public IFormFile? ImageFile { get; set; }
        
        public string? CurrentImageUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}