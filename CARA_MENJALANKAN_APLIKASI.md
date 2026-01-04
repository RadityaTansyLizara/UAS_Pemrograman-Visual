# 🚀 Cara Menjalankan Aplikasi BabyShopWeb2

## ✅ Semua Sudah Siap!

Login system sudah selesai dibuat dan siap digunakan.

## 📝 Langkah-langkah Menjalankan

### 1. Buka Terminal/PowerShell
Pastikan Anda berada di folder `C:\BabyShopWeb2`

### 2. Jalankan Aplikasi
```cmd
dotnet run
```

### 3. Tunggu Sampai Muncul
```
✅ Admin user already exists
✅ Database ready
Now listening on: http://localhost:5055
Application started. Press Ctrl+C to shut down.
```

### 4. Buka Browser
Akses salah satu URL berikut:
- **Beranda**: http://localhost:5055/
- **Login Admin**: http://localhost:5055/Auth/Login

### 5. Login ke Admin
Masukkan kredensial:
```
Username: admin
Password: admin123
```

## 🎯 Setelah Login Berhasil

Anda akan masuk ke Admin Dashboard dan bisa:
- ✅ Kelola Produk
- ✅ Lihat Orders
- ✅ Kelola Financial
- ✅ Lihat Newsletter Subscribers
- ✅ Lihat Contact Messages

## 🔗 URL Penting

| Halaman | URL |
|---------|-----|
| 🏠 Beranda | http://localhost:5055/ |
| 🔐 Login Admin | http://localhost:5055/Auth/Login |
| 📊 Admin Dashboard | http://localhost:5055/Admin |
| 📦 Kelola Produk | http://localhost:5055/Admin/Products |
| 📋 Lihat Orders | http://localhost:5055/Admin/Orders |
| 💰 Financial | http://localhost:5055/Financial |
| 📧 Newsletter | http://localhost:5055/Admin/Newsletters |
| 💬 Messages | http://localhost:5055/Admin/Messages |
| 🚪 Logout | http://localhost:5055/Auth/Logout |

## 🛑 Cara Menghentikan Aplikasi

Tekan `Ctrl+C` di terminal/PowerShell

## 🔧 Troubleshooting

### Error: "address already in use"
Artinya aplikasi masih berjalan di background. Solusi:
1. Cari process yang menggunakan port 5055
2. Atau restart komputer
3. Atau gunakan Task Manager untuk kill process "BabyShopWeb2.exe"

### Error: "Cannot access database"
Artinya database sedang digunakan. Solusi:
1. Stop semua instance aplikasi
2. Tunggu beberapa detik
3. Jalankan ulang

### Login Gagal
1. Cek console output untuk melihat error
2. Pastikan kredensial benar: admin / admin123
3. Akses http://localhost:5055/Auth/CheckAdmin untuk verifikasi

## 💡 Tips

### Session Login
- Session aktif selama 2 jam
- Setelah 2 jam harus login ulang
- Untuk logout manual: http://localhost:5055/Auth/Logout

### Data Aman
- Database tidak akan dihapus saat restart
- Produk yang ditambahkan akan tetap ada
- Orders akan tersimpan

### Reset Database
Jika ingin reset ke kondisi awal:
1. Stop aplikasi (Ctrl+C)
2. Hapus database:
   ```cmd
   del babyshop.db
   del babyshop.db-shm
   del babyshop.db-wal
   ```
3. Jalankan ulang aplikasi

## 📊 Debug Mode

Saat login, console akan menampilkan informasi debug:
```
Login attempt - Username: admin
Password hash generated: [hash]
User found in DB - Username: admin
Stored password hash: [hash]
Hashes match: True/False
```

Ini membantu troubleshooting jika ada masalah login.

## 🎉 Fitur yang Tersedia

### Untuk Pelanggan (Tanpa Login)
- ✅ Lihat produk
- ✅ Filter dan sort produk
- ✅ Tambah ke keranjang
- ✅ Checkout dan bayar
- ✅ Download struk PDF
- ✅ Subscribe newsletter
- ✅ Kirim pesan via Contact

### Untuk Admin (Perlu Login)
- ✅ Kelola produk (CRUD)
- ✅ Upload gambar produk
- ✅ Lihat dan kelola orders
- ✅ Kelola financial transactions
- ✅ Lihat newsletter subscribers
- ✅ Lihat contact messages
- ✅ Dashboard dengan statistik

## 📖 Dokumentasi Lainnya

- `LOGIN_SIAP_DIGUNAKAN.md` - Panduan login lengkap
- `test_login_debug.html` - Panduan visual di browser
- `QUICK_LOGIN_FIX.md` - Quick reference
- `LOGIN_SYSTEM_GUIDE.md` - Dokumentasi teknis login system

---

## 🎊 Selamat Menggunakan!

Aplikasi BabyShopWeb2 sudah siap digunakan dengan fitur login yang aman.

**Kredensial Admin:**
- Username: `admin`
- Password: `admin123`

**Jangan lupa ganti password default sebelum production!**
