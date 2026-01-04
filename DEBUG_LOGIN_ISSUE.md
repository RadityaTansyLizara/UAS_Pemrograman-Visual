# Debug Login Issue - Panduan Troubleshooting

## Masalah
Login gagal dengan pesan "Username atau password salah" meskipun menggunakan kredensial yang benar (admin/admin123).

## Langkah-langkah Debug

### 1. Stop aplikasi yang sedang berjalan
Pastikan tidak ada instance aplikasi yang masih berjalan:
```
Ctrl+C di terminal
```

### 2. Hapus database lama (opsional tapi disarankan)
```
del babyshop.db
del babyshop.db-shm
del babyshop.db-wal
```

### 3. Jalankan aplikasi
```
dotnet run
```

Perhatikan output console, seharusnya muncul:
```
Admin user created: username=admin, password=admin123
Database recreated with latest schema
```

### 4. Cek admin user di database
Buka browser dan akses:
```
http://localhost:5055/Auth/CheckAdmin
```

Ini akan menampilkan:
- Jumlah admin users di database
- Detail setiap admin user (username, password hash, dll)
- Test hash untuk password "admin123"

### 5. Coba login
Akses:
```
http://localhost:5055/Auth/Login
```

Masukkan:
- Username: `admin`
- Password: `admin123`

### 6. Lihat console output
Saat mencoba login, perhatikan output di console/terminal. Akan muncul informasi debug:
```
Login attempt - Username: admin
Password hash generated: [hash]
User found in DB - Username: admin
Stored password hash: [hash]
Hashes match: True/False
```

## Kemungkinan Penyebab

### A. Database tidak ter-recreate
- Solusi: Hapus file database secara manual (langkah 2)

### B. Password hash tidak match
- Cek output dari `/Auth/CheckAdmin`
- Bandingkan hash yang tersimpan dengan hash yang di-generate
- Seharusnya: `jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=`

### C. Admin user tidak ter-seed
- Cek output console saat aplikasi start
- Cek `/Auth/CheckAdmin` untuk memastikan user ada

## Kredensial Default
```
Username: admin
Password: admin123
```

## File yang Sudah Diupdate
1. `Controllers/AuthController.cs` - Ditambahkan logging dan endpoint CheckAdmin
2. `test_password_verification.cs` - Script untuk test password hash

## Setelah Login Berhasil
Setelah berhasil login, Anda akan diarahkan ke:
```
http://localhost:5055/Admin
```

Dan session akan tersimpan selama 2 jam.
