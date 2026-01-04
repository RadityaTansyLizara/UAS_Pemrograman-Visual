# 🔍 Langkah-langkah Debug Login

## ⚠️ Masalah Saat Ini
Login gagal dengan pesan "Username atau password salah" padahal kredensial sudah benar.

## ✅ Solusi Cepat

### Langkah 1: Stop Aplikasi
Tekan `Ctrl+C` di terminal untuk menghentikan aplikasi.

### Langkah 2: Hapus Database Lama
```cmd
del babyshop.db
del babyshop.db-shm
del babyshop.db-wal
```

### Langkah 3: Jalankan Ulang Aplikasi
```cmd
dotnet run
```

**Perhatikan output console!** Seharusnya muncul:
```
Admin user created: username=admin, password=admin123
Database recreated with latest schema
```

### Langkah 4: Verifikasi Admin User
Buka browser dan akses:
```
http://localhost:5055/Auth/CheckAdmin
```

Anda akan melihat informasi admin user yang tersimpan di database.

### Langkah 5: Login
Akses halaman login:
```
http://localhost:5055/Auth/Login
```

Masukkan kredensial:
- **Username**: `admin`
- **Password**: `admin123`

### Langkah 6: Cek Console Log
Saat Anda klik tombol Login, perhatikan output di console/terminal. Akan muncul informasi debug seperti:
```
Login attempt - Username: admin
Password hash generated: jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=
User found in DB - Username: admin
Stored password hash: jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=
Hashes match: True
```

## 🎯 Apa yang Sudah Diperbaiki

1. **Ditambahkan Debug Logging** di `AuthController.cs`
   - Menampilkan username yang diinput
   - Menampilkan password hash yang di-generate
   - Menampilkan password hash yang tersimpan di database
   - Menampilkan apakah hash cocok atau tidak

2. **Ditambahkan Endpoint CheckAdmin**
   - URL: `http://localhost:5055/Auth/CheckAdmin`
   - Menampilkan semua admin user di database
   - Menampilkan test hash untuk password "admin123"

3. **Auto-seed Admin User**
   - Aplikasi otomatis membuat admin user saat start
   - Database di-recreate setiap kali aplikasi start (untuk development)

## 🔐 Kredensial Default

```
Username: admin
Password: admin123
```

## 📍 URL Penting

- **Login**: http://localhost:5055/Auth/Login
- **Check Admin**: http://localhost:5055/Auth/CheckAdmin
- **Admin Dashboard**: http://localhost:5055/Admin
- **Logout**: http://localhost:5055/Auth/Logout

## 🎉 Setelah Login Berhasil

Anda akan diarahkan ke Admin Dashboard:
```
http://localhost:5055/Admin
```

Session akan aktif selama 2 jam.

## 💡 Tips

- Jika masih gagal, coba restart aplikasi dan hapus database lagi
- Pastikan tidak ada typo saat memasukkan username/password
- Username dan password case-sensitive
- Cek console output untuk melihat detail error

## 📝 File yang Diupdate

1. `Controllers/AuthController.cs` - Ditambahkan debug logging
2. `DEBUG_LOGIN_ISSUE.md` - Panduan troubleshooting lengkap
3. `test_password_verification.cs` - Script test password hash
