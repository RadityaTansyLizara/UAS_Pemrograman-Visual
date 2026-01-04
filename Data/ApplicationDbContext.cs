using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Models;

namespace BabyShopWeb2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
                
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);
                
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);
                
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
                
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
                
            modelBuilder.Entity<FinancialTransaction>()
                .HasOne(ft => ft.Order)
                .WithMany()
                .HasForeignKey(ft => ft.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
            
            // Seed data
            SeedData(modelBuilder);
        }
        
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Pakaian Bayi", Description = "Baju, celana, dan aksesoris bayi yang nyaman dan lucu", IconClass = "fas fa-tshirt", IsActive = true },
                new Category { Id = 2, Name = "Mainan Edukatif", Description = "Mainan yang aman dan mendukung perkembangan anak", IconClass = "fas fa-puzzle-piece", IsActive = true },
                new Category { Id = 3, Name = "Perlengkapan Makan", Description = "Botol susu, sendok, dan peralatan makan bayi", IconClass = "fas fa-utensils", IsActive = true },
                new Category { Id = 4, Name = "Perawatan Bayi", Description = "Sabun, shampoo, dan produk perawatan bayi", IconClass = "fas fa-bath", IsActive = true }
            );
            
            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                // Pakaian Bayi
                new Product { Id = 1, Name = "Baju Bayi Cussons Baby Pink", Description = "Baju bayi 100% katun organik dari Cussons Baby dengan motif lucu, nyaman untuk sehari-hari", Price = 65000, Stock = 50, ImageUrl = "/images/products/cussons-baju-pink.svg", CategoryId = 1, IsActive = true },
                new Product { Id = 2, Name = "Celana Bayi My Baby Soft Blue", Description = "Celana bayi berbahan lembut dari My Baby, elastis dan mudah dipakai untuk si kecil", Price = 45000, Stock = 40, ImageUrl = "/images/products/mybaby-celana-blue.svg", CategoryId = 1, IsActive = true },
                new Product { Id = 3, Name = "Set Baju Tidur Zwitsal", Description = "Set lengkap baju tidur bayi Zwitsal dengan topi, sarung tangan dan kaki", Price = 125000, DiscountPrice = 99000, Stock = 30, ImageUrl = "/images/products/zwitsal-set-tidur.svg", CategoryId = 1, IsActive = true },
                
                // Mainan Edukatif
                new Product { Id = 4, Name = "Puzzle Kayu Fisher-Price", Description = "Puzzle kayu edukatif Fisher-Price untuk belajar angka 1-10, aman untuk bayi", Price = 85000, Stock = 25, ImageUrl = "/images/products/fisherprice-puzzle.svg", CategoryId = 2, IsActive = true },
                new Product { Id = 5, Name = "Mainan Rattle Pigeon", Description = "Mainan rattle Pigeon dengan suara lembut, melatih motorik bayi", Price = 35000, Stock = 60, ImageUrl = "/images/products/pigeon-rattle.svg", CategoryId = 2, IsActive = true },
                new Product { Id = 6, Name = "Soft Book Fabric Chicco", Description = "Buku kain lembut Chicco dengan gambar-gambar menarik untuk stimulasi bayi", Price = 75000, DiscountPrice = 65000, Stock = 35, ImageUrl = "/images/products/chicco-softbook.svg", CategoryId = 2, IsActive = true },
                
                // Perlengkapan Makan
                new Product { Id = 7, Name = "Botol Susu Pigeon Anti Kolik", Description = "Botol susu Pigeon dengan sistem anti kolik, BPA free dan aman untuk bayi", Price = 125000, Stock = 20, ImageUrl = "/images/products/pigeon-botol.svg", CategoryId = 3, IsActive = true },
                new Product { Id = 8, Name = "Set Sendok Makan Dr. Brown's", Description = "Set sendok dan garpu khusus bayi Dr. Brown's, ergonomis dan aman", Price = 55000, Stock = 45, ImageUrl = "/images/products/drbrowns-sendok.svg", CategoryId = 3, IsActive = true },
                new Product { Id = 9, Name = "Mangkuk Anti Tumpah Munchkin", Description = "Mangkuk Munchkin dengan desain anti tumpah, mudah digenggam bayi", Price = 75000, Stock = 30, ImageUrl = "/images/products/munchkin-mangkuk.svg", CategoryId = 3, IsActive = true },
                
                // Perawatan Bayi
                new Product { Id = 10, Name = "Johnson's Baby Shampoo No More Tears", Description = "Shampoo bayi Johnson's formula gentle, tidak pedih di mata, 200ml", Price = 25000, Stock = 40, ImageUrl = "/images/products/johnsons-shampoo.svg", CategoryId = 4, IsActive = true },
                new Product { Id = 11, Name = "Cussons Baby Cream & Lotion", Description = "Lotion bayi Cussons dengan formula melembabkan, cocok untuk kulit sensitif", Price = 35000, DiscountPrice = 28000, Stock = 35, ImageUrl = "/images/products/cussons-lotion.svg", CategoryId = 4, IsActive = true },
                new Product { Id = 12, Name = "My Baby Minyak Telon Plus", Description = "Minyak telon My Baby dengan formula plus, menghangatkan dan melindungi bayi", Price = 15000, Stock = 80, ImageUrl = "/images/products/mybaby-telon.svg", CategoryId = 4, IsActive = true },
                new Product { Id = 13, Name = "Zwitsal Baby Powder Classic", Description = "Bedak bayi Zwitsal classic dengan formula lembut, menjaga kulit bayi tetap kering", Price = 18000, Stock = 60, ImageUrl = "/images/products/zwitsal-powder.svg", CategoryId = 4, IsActive = true },
                new Product { Id = 14, Name = "Johnson's Baby Wipes Sensitive", Description = "Tisu basah bayi Johnson's untuk kulit sensitif, alcohol free, 80 sheets", Price = 22000, Stock = 70, ImageUrl = "/images/products/johnsons-wipes.svg", CategoryId = 4, IsActive = true },
                new Product { Id = 15, Name = "Cussons Baby Hair & Body Wash", Description = "Sabun cair bayi Cussons 2 in 1 untuk rambut dan badan, formula mild", Price = 32000, Stock = 45, ImageUrl = "/images/products/cussons-wash.svg", CategoryId = 4, IsActive = true }
            );
        }
    }
}