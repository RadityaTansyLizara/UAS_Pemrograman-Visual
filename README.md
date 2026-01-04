
| Variable | Isi |
| -------- | --- |
| *Nama* | RADITYA TANSY LIZARA  |
| *NIM* | 312310454 |
| *Kelas* | TI.23.A5 |
| *Mata Kuliah* | Pemrograman Visual Desktop (UAS)|
| *Dosen Pengampu* | Dr. Muhamad Fatchan, S.Kom., M.Kom|

# BabyShop3Berlian

## 🍼 E-Commerce Website Perlengkapan Bayi

Website **BabyShop3Berlian** adalah aplikasi **e-commerce berbasis ASP.NET Core MVC** yang dirancang khusus untuk toko perlengkapan bayi. Website ini menyediakan fitur belanja online lengkap mulai dari katalog produk, keranjang belanja, checkout, hingga pembuatan struk pembelian dalam format **PDF**.

Project ini dikembangkan sebagai **Ujian Akhir Semester (UAS)** pada mata kuliah **Pemrograman Visual Desktop**.

---

## 🌟 Fitur Utama

### 🛒 Fitur E-Commerce

* Katalog produk dengan kategori, filter, dan pencarian
* Keranjang belanja berbasis session
* Proses checkout dengan validasi data pelanggan
* Generate dan download **struk pembelian (PDF)**
* Manajemen stok produk secara real-time

### 🎨 Desain & UI

* Desain modern, clean, dan bertema pink (baby friendly)
* Responsive design (mobile, tablet, desktop)
* Navigasi halus dan user-friendly
* Animasi ringan dan hover effects

### ⚙️ Fitur Teknis

* Database SQLite dengan Entity Framework Core
* Session management untuk keranjang belanja
* PDF generation menggunakan iTextSharp
* Seed data otomatis (produk & kategori)

---

## 🛍️ Fitur Belanja

### Halaman Produk

* Katalog produk lengkap
* Detail produk (harga, stok, deskripsi)
* 4 kategori utama:

  * Pakaian Bayi
  * Mainan Edukatif
  * Perlengkapan Makan
  * Perawatan Bayi

### Keranjang Belanja

* Tambah produk ke keranjang
* Update jumlah item
* Hapus item dari keranjang
* Ringkasan total belanja
* Gratis ongkir untuk pembelian di atas Rp 200.000

### Checkout & Order

* Form data pelanggan dengan validasi
* Ringkasan pesanan sebelum konfirmasi
* Halaman sukses pemesanan
* Download struk pembelian dalam format PDF

---

## 📊 Database & Model

### Database Models

* **Product** : Data produk (nama, harga, stok, gambar)
* **Category** : Kategori produk
* **Cart & CartItem** : Keranjang belanja
* **Order & OrderItem** : Data pesanan

### Sample Data

* 4 kategori produk
* 12 produk contoh (3 produk per kategori)
* Harga dan stok realistis untuk simulasi

---

## 🛠️ Teknologi yang Digunakan

### Backend

* ASP.NET Core MVC 8.0
* Entity Framework Core
* SQLite Database
* iTextSharp (PDF Generator)

### Frontend

* HTML5, CSS3, JavaScript
* Bootstrap 5
* Font Awesome 6
* Google Fonts (Poppins)

### Arsitektur

* MVC (Model-View-Controller)
* Dependency Injection
* Session-based Cart System

---

## 🚀 Cara Menjalankan Aplikasi

### Prasyarat

* .NET SDK 8.0
* Visual Studio / Visual Studio Code

### Langkah Menjalankan

1. Clone repository ini
2. Buka folder project
3. Jalankan perintah berikut:

```bash
dotnet restore
dotnet build
dotnet run
```

4. Buka browser dan akses:

```
http://localhost:5055
```

### Database

* Database SQLite dibuat otomatis saat aplikasi dijalankan
* File database: `babyshop.db`

---

## 📂 Struktur Project

```
BabyShopWeb2/
├── Controllers/
├── Models/
├── Views/
├── Services/
├── Data/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
├── Program.cs
├── appsettings.json
└── README.md
```

---

## 🔒 Keamanan

* Validasi input menggunakan Data Annotations
* Proteksi CSRF dengan Anti-forgery Token
* Query database aman melalui Entity Framework

---

## 📈 Performa

* Optimasi query database
* Session management efisien
* Asset frontend terorganisir

---

## 🧪 Pengujian Manual

* Homepage dan navigasi
* Katalog produk & filter
* Keranjang belanja
* Proses checkout
* Generate PDF struk
* Tampilan responsive

---

## 🚀 Pengembangan Selanjutnya

* Login & registrasi pengguna
* Admin dashboard
* Payment gateway
* Email notifikasi
* Review & rating produk
* Wishlist

---

## 📄 Lisensi

© 2025 **BabyShop3Berlian**
Project ini dibuat untuk keperluan akademik (UAS Pemrograman Visual Desktop).
