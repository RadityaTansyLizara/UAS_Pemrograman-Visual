using System.ComponentModel.DataAnnotations;

namespace BabyShopWeb2.Models
{
    public enum TransactionType
    {
        Income,      // Pemasukan
        Expense      // Pengeluaran
    }
    
    public enum TransactionCategory
    {
        // Income Categories
        Sales,              // Penjualan produk
        OtherIncome,        // Pemasukan lain
        
        // Expense Categories
        ProductPurchase,    // Pembelian produk/stok
        OperationalCost,    // Biaya operasional
        Salary,             // Gaji karyawan
        Rent,               // Sewa tempat
        Utilities,          // Listrik, air, internet
        Marketing,          // Biaya marketing
        OtherExpense        // Pengeluaran lain
    }
    
    public class FinancialTransaction
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime TransactionDate { get; set; }
        
        [Required]
        public TransactionType Type { get; set; }
        
        [Required]
        public TransactionCategory Category { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public decimal Amount { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        // Reference to order if this is from a sale
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        
        [StringLength(100)]
        public string CreatedBy { get; set; } = "Admin";
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public bool IsAutomatic { get; set; } = false; // True if created automatically from order
    }
}
