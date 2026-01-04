# BabyShop3Berlian - E-Commerce Website Perlengkapan Bayi

Website e-commerce lengkap untuk toko perlengkapan bayi dengan sistem belanja online, keranjang, checkout, dan struk PDF.

## 🌟 Fitur Utama

### 🛒 E-Commerce Lengkap
- **Katalog Produk**: Tampilan produk dengan kategori, filter, dan pencarian
- **Keranjang Belanja**: Sistem keranjang dengan session management
- **Checkout**: Proses pemesanan dengan form pelanggan
- **Struk PDF**: Generate dan download struk pembelian dalam format PDF
- **Manajemen Stok**: Tracking stok produk real-time

### 🎨 Desain & UI
- **Desain Modern**: Interface yang clean dan menarik dengan tema pink
- **Responsive Design**: Tampilan optimal di semua perangkat
- **Smooth Navigation**: Navigasi halus dengan smooth scrolling
- **Interactive Elements**: Animasi dan hover effects yang menarik
- **Real-time Updates**: Cart count dan notifikasi real-time

### 📱 Fitur Teknis
- **Database**: SQLite dengan Entity Framework Core
- **Session Management**: Keranjang berbasis session
- **PDF Generation**: iTextSharp untuk generate struk
- **Seed Data**: 12 produk sample dalam 4 kategori
- **Responsive Design**: Bootstrap 5 dengan custom CSS

## 🛍️ Fitur Belanja

### Halaman Produk
- **Katalog Lengkap**: Semua produk dengan filter kategori dan pencarian
- **Detail Produk**: Informasi lengkap, harga, stok, dan produk terkait
- **Kategori**: 4 kategori utama (Pakaian, Mainan, Perlengkapan Makan, Perawatan)
- **Status Stok**: Indikator stok tersedia, terbatas, atau habis

### Keranjang Belanja
- **Add to Cart**: Tambah produk ke keranjang dari berbagai halaman
- **Update Quantity**: Ubah jumlah item dalam keranjang
- **Remove Items**: Hapus item individual atau kosongkan keranjang
- **Cart Summary**: Ringkasan total dengan perhitungan ongkir
- **Free Shipping**: Gratis ongkir untuk pembelian di atas Rp 200.000

### Checkout & Order
- **Customer Form**: Form informasi pelanggan dengan validasi
- **Order Summary**: Ringkasan pesanan sebelum konfirmasi
- **Order Success**: Halaman konfirmasi dengan detail pesanan
- **PDF Receipt**: Download struk dalam format PDF
- **Order Tracking**: Nomor pesanan unik untuk tracking

## 📊 Data & Models

### Database Models
- **Product**: Produk dengan kategori, harga, stok, gambar
- **Category**: Kategori produk dengan icon dan deskripsi
- **Cart & CartItem**: Keranjang belanja dengan item
- **Order & OrderItem**: Pesanan dengan detail item
- **Order Status**: Status pesanan (Pending, Processing, Shipped, dll)

### Sample Data
- **4 Kategori**: Pakaian Bayi, Mainan Edukatif, Perlengkapan Makan, Perawatan Bayi
- **12 Produk**: 3 produk per kategori dengan variasi harga dan diskon
- **Realistic Pricing**: Harga sesuai market produk bayi Indonesia
- **Stock Management**: Stok bervariasi untuk testing

## 🛠️ Teknologi yang Digunakan

### Backend
- **ASP.NET Core MVC 8.0**: Framework web utama
- **Entity Framework Core**: ORM untuk database
- **SQLite**: Database ringan untuk development
- **iTextSharp**: Library untuk generate PDF

### Frontend
- **HTML5, CSS3, JavaScript**: Frontend technologies
- **Bootstrap 5**: CSS framework untuk responsive design
- **Font Awesome 6**: Icon library
- **Google Fonts (Poppins)**: Typography

### Services & Architecture
- **Dependency Injection**: Service pattern untuk cart dan PDF
- **Session Management**: ASP.NET Core session untuk cart
- **Repository Pattern**: Data access dengan DbContext
- **MVC Pattern**: Model-View-Controller architecture

## 🚀 Cara Menjalankan

### Prerequisites
- .NET 8.0 SDK
- Visual Studio Code atau Visual Studio

### Installation
1. Clone atau download project
2. Buka terminal di folder project
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Build project:
   ```bash
   dotnet build
   ```
5. Jalankan aplikasi:
   ```bash
   dotnet run
   ```
6. Buka browser dan akses: `http://localhost:5055`

### Database
- Database SQLite akan dibuat otomatis saat pertama kali run
- Sample data akan di-seed secara otomatis
- File database: `babyshop.db` di root folder

## 📱 Halaman & Fitur

### 🏠 Homepage
- Hero section dengan CTA
- Features section (4 keunggulan toko)
- Kategori produk (4 kategori dengan link)
- Produk terbaru (8 produk terbaru)
- Testimonials (3 review pelanggan)
- Call-to-action section

### 🛍️ Product Pages
- **All Products** (`/Product`): Semua produk dengan filter dan search
- **Product Details** (`/Product/Details/{id}`): Detail produk dengan related products
- **Category** (`/Product/Category/{id}`): Produk per kategori

### 🛒 Shopping Cart
- **Cart** (`/Cart`): Keranjang belanja dengan update quantity
- **Add to Cart**: POST endpoint untuk tambah produk
- **Update/Remove**: Manage items dalam keranjang

### 📋 Order & Checkout
- **Checkout** (`/Order/Checkout`): Form checkout dengan validasi
- **Order Success** (`/Order/OrderSuccess/{id}`): Konfirmasi pesanan
- **Download Receipt** (`/Order/DownloadReceipt/{id}`): Download PDF

## 🎨 Design System

### Color Palette
- **Primary Pink**: `#ff6b9d`
- **Pink Light**: `#ffb3d1`
- **Pink Dark**: `#e55a87`
- **Light Gray**: `#f8f9fa`
- **Dark Gray**: `#6c757d`

### Typography
- **Font Family**: Poppins (Google Fonts)
- **Weights**: 300, 400, 500, 600, 700

### Components
- **Product Cards**: Hover effects, discount badges, stock indicators
- **Buttons**: Primary, outline, dan disabled states
- **Forms**: Validation styling, focus states
- **Alerts**: Success, error, dan info messages

## 📱 Responsive Breakpoints

- **Mobile**: < 576px
- **Tablet**: 576px - 768px
- **Desktop**: > 768px

## 🔧 Kustomisasi

### Mengubah Warna Tema
Edit variabel CSS di `wwwroot/css/site.css`:
```css
:root {
    --pink-color: #ff6b9d;
    --pink-light: #ffb3d1;
    --pink-dark: #e55a87;
}
```

### Menambah Produk
1. Tambah data di `Data/ApplicationDbContext.cs` dalam method `SeedData`
2. Atau tambah melalui database langsung
3. Restart aplikasi untuk reload data

### Mengubah Konten
- **Homepage**: Edit `Views/Home/Index.cshtml`
- **Layout**: Edit `Views/Shared/_Layout.cshtml`
- **Styling**: Edit `wwwroot/css/site.css`

## 📂 Struktur Project

```
BabyShopWeb2/
├── Controllers/
│   ├── HomeController.cs          # Homepage
│   ├── ProductController.cs       # Katalog produk
│   ├── CartController.cs          # Keranjang belanja
│   └── OrderController.cs         # Checkout & orders
├── Models/
│   ├── Product.cs                 # Model produk
│   ├── Category.cs                # Model kategori
│   ├── Cart.cs & CartItem.cs      # Model keranjang
│   ├── Order.cs & OrderItem.cs    # Model pesanan
│   └── ErrorViewModel.cs          # Error handling
├── Views/
│   ├── Home/
│   │   └── Index.cshtml           # Homepage
│   ├── Product/
│   │   ├── Index.cshtml           # Katalog produk
│   │   ├── Details.cshtml         # Detail produk
│   │   └── Category.cshtml        # Produk per kategori
│   ├── Cart/
│   │   └── Index.cshtml           # Keranjang belanja
│   ├── Order/
│   │   ├── Checkout.cshtml        # Form checkout
│   │   └── OrderSuccess.cshtml    # Konfirmasi pesanan
│   └── Shared/
│       ├── _Layout.cshtml         # Layout utama
│       └── Error.cshtml           # Error page
├── Services/
│   ├── CartService.cs             # Service keranjang
│   └── PdfService.cs              # Service PDF generation
├── Data/
│   └── ApplicationDbContext.cs    # Database context
├── wwwroot/
│   ├── css/
│   │   └── site.css               # Custom styles
│   ├── js/
│   │   └── site.js                # Custom JavaScript
│   ├── images/
│   │   └── products/              # Folder gambar produk
│   └── lib/                       # Bootstrap, jQuery, dll
├── Program.cs                     # Startup configuration
├── appsettings.json              # Configuration
└── README.md                     # Dokumentasi
```

## 🔒 Security Features

- **Input Validation**: Model validation dengan Data Annotations
- **CSRF Protection**: Anti-forgery tokens pada forms
- **Session Security**: Secure session configuration
- **SQL Injection Prevention**: Entity Framework parameterized queries

## 📈 Performance Features

- **Lazy Loading**: Entity Framework lazy loading
- **Caching**: Memory caching untuk session
- **Optimized Queries**: Include statements untuk related data
- **Minified Assets**: CSS dan JS optimization

## 🧪 Testing

### Manual Testing Checklist
- [ ] Homepage loading dan navigation
- [ ] Product catalog dengan filter
- [ ] Add to cart functionality
- [ ] Cart management (update, remove)
- [ ] Checkout process
- [ ] PDF generation
- [ ] Responsive design di berbagai device

### Sample Test Data
- 4 kategori produk
- 12 produk dengan variasi harga
- Beberapa produk dengan diskon
- Stok bervariasi untuk testing

## 📞 Kontak & Support

Untuk pertanyaan atau dukungan teknis:
- **Email**: info@babyshop3berlian.com
- **WhatsApp**: +62 812-3456-7890
- **Alamat**: Jl. Ganesha No. 99, Kp. Rengasbandung, Desa Karangsambung, Kec. Kedungwaringin, Bekasi

## 📄 Lisensi

© 2025 BabyShop3Berlian. All rights reserved.

## 🚀 Future Enhancements

### Planned Features
- User authentication & registration
- Admin panel untuk manage produk
- Payment gateway integration
- Email notifications
- Order tracking system
- Product reviews & ratings
- Wishlist functionality
- Multi-language support

### Technical Improvements
- Redis caching
- Image optimization
- Search engine optimization
- Performance monitoring
- Automated testing
- CI/CD pipeline