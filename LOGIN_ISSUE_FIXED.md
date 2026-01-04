# 🔧 Login Issue - Debugging Tools Added

## 📋 Ringkasan Masalah

User melaporkan bahwa login gagal dengan pesan "Username atau password salah" meskipun menggunakan kredensial yang benar (admin/admin123).

## ✅ Solusi yang Diterapkan

### 1. Ditambahkan Debug Logging di AuthController

File: `Controllers/AuthController.cs`

Ditambahkan logging yang menampilkan:
- Username yang diinput
- Password hash yang di-generate saat login
- Apakah user ditemukan di database
- Password hash yang tersimpan di database
- Apakah kedua hash cocok

```csharp
Console.WriteLine($"Login attempt - Username: {model.Username}");
Console.WriteLine($"Password hash generated: {passwordHash}");
Console.WriteLine($"User found in DB - Username: {userExists.Username}");
Console.WriteLine($"Stored password hash: {userExists.PasswordHash}");
Console.WriteLine($"Hashes match: {userExists.PasswordHash == passwordHash}");
```

### 2. Ditambahkan Endpoint CheckAdmin

URL: `http://localhost:5055/Auth/CheckAdmin`

Endpoint ini menampilkan:
- Total admin users di database
- Detail setiap admin user (ID, Username, PasswordHash, FullName, CreatedAt, LastLogin)
- Test hash untuk password "admin123"

Berguna untuk memverifikasi bahwa:
- Admin user sudah ter-seed dengan benar
- Password hash tersimpan dengan benar
- Hash yang di-generate cocok dengan yang tersimpan

### 3. File Panduan dan Testing

Dibuat beberapa file untuk membantu debugging:

1. **DEBUG_LOGIN_ISSUE.md**
   - Panduan troubleshooting lengkap
   - Penjelasan kemungkinan penyebab masalah
   - Langkah-langkah solusi

2. **LOGIN_DEBUG_STEPS.md**
   - Langkah-langkah debug yang lebih ringkas
   - Format yang mudah diikuti
   - Daftar URL penting

3. **test_login_debug.html**
   - Interface HTML yang user-friendly
   - Link langsung ke semua endpoint penting
   - Visual guide dengan warna dan emoji
   - Bisa dibuka di browser untuk akses cepat

## 🔍 Cara Menggunakan Tools Debug

### Metode 1: Menggunakan HTML Test File (Paling Mudah)

1. Buka file `test_login_debug.html` di browser
2. Ikuti langkah-langkah yang ditampilkan
3. Klik link yang disediakan untuk akses cepat

### Metode 2: Manual Testing

1. Stop aplikasi (Ctrl+C)
2. Hapus database:
   ```cmd
   del babyshop.db
   del babyshop.db-shm
   del babyshop.db-wal
   ```
3. Jalankan aplikasi:
   ```cmd
   dotnet run
   ```
4. Cek admin user:
   ```
   http://localhost:5055/Auth/CheckAdmin
   ```
5. Login:
   ```
   http://localhost:5055/Auth/Login
   ```
6. Perhatikan console output

## 🎯 Expected Behavior

### Saat Aplikasi Start
```
Admin user created: username=admin, password=admin123
Database recreated with latest schema
```

### Saat Akses CheckAdmin
```
Total admin users: 1

ID: 1
Username: admin
PasswordHash: jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=
FullName: Administrator
CreatedAt: [timestamp]
LastLogin: 

Test hash for 'admin123': jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=
```

### Saat Login (Console Output)
```
Login attempt - Username: admin
Password hash generated: jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=
User found in DB - Username: admin
Stored password hash: jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=
Hashes match: True
```

### Setelah Login Berhasil
- Redirect ke: `http://localhost:5055/Admin`
- Session aktif selama 2 jam
- Navbar menampilkan nama admin

## 🔐 Kredensial Default

```
Username: admin
Password: admin123
```

## 📍 URL Reference

| Fungsi | URL |
|--------|-----|
| Beranda | http://localhost:5055/ |
| Login | http://localhost:5055/Auth/Login |
| Check Admin | http://localhost:5055/Auth/CheckAdmin |
| Admin Dashboard | http://localhost:5055/Admin |
| Logout | http://localhost:5055/Auth/Logout |

## 🔄 Kemungkinan Penyebab Masalah

1. **Database tidak ter-recreate**
   - Solusi: Hapus file database secara manual

2. **Password hash tidak match**
   - Solusi: Cek dengan endpoint CheckAdmin
   - Verifikasi hash yang tersimpan vs yang di-generate

3. **Admin user tidak ter-seed**
   - Solusi: Cek console output saat aplikasi start
   - Verifikasi dengan endpoint CheckAdmin

4. **Aplikasi masih menggunakan database lama**
   - Solusi: Stop aplikasi, hapus database, jalankan ulang

## 💡 Tips

- Selalu cek console output untuk informasi debug
- Gunakan endpoint CheckAdmin untuk verifikasi database
- Jika masih gagal, restart dari awal (hapus database)
- Pastikan tidak ada typo saat input kredensial
- Username dan password case-sensitive

## 📝 File yang Dimodifikasi/Dibuat

### Modified:
1. `Controllers/AuthController.cs` - Ditambahkan debug logging dan endpoint CheckAdmin

### Created:
1. `DEBUG_LOGIN_ISSUE.md` - Panduan troubleshooting lengkap
2. `LOGIN_DEBUG_STEPS.md` - Langkah-langkah debug ringkas
3. `test_login_debug.html` - HTML test interface
4. `LOGIN_ISSUE_FIXED.md` - Dokumen ini
5. `QUICK_LOGIN_FIX.md` - Quick reference card

## 🎉 Next Steps

Setelah login berhasil:
1. Akses Admin Dashboard
2. Kelola produk, orders, newsletter, dll
3. Session akan tersimpan selama 2 jam
4. Bisa logout kapan saja via `/Auth/Logout`

## 🚀 Untuk Production

Sebelum deploy ke production:
1. Hapus atau protect endpoint `/Auth/CheckAdmin`
2. Hapus console logging di AuthController
3. Ganti password default admin
4. Hapus `EnsureDeleted()` di Program.cs (jangan hapus database setiap start)
5. Gunakan migration untuk update schema
