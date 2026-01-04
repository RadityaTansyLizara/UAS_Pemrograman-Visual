# 🔧 Fix Login Password Issue

## ❌ Masalah: Username atau Password Salah

Jika Anda mengalami error "Username atau password salah" saat login dengan admin/admin123, ini karena password hash di database lama berbeda.

---

## ✅ Solusi: Reset Password Admin

### Cara 1: Via Browser (Paling Mudah)

**Akses URL ini di browser:**
```
http://localhost:5055/Auth/ResetAdminPassword
```

**Hasil yang diharapkan:**
```
✅ Password admin berhasil direset!

Username: admin
Password: admin123
New Hash: jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=

Silakan login kembali: http://localhost:5055/Auth/Login
```

**Setelah itu:**
1. Kembali ke halaman login: http://localhost:5055/Auth/Login
2. Login dengan:
   - Username: `admin`
   - Password: `admin123`
3. Seharusnya berhasil! ✅

---

### Cara 2: Cek Admin User (Debug)

Jika ingin melihat data admin user di database:

```
http://localhost:5055/Auth/CheckAdmin
```

Ini akan menampilkan:
- Total admin users
- Username
- Password hash
- Full name
- Created date
- Last login

---

## 🔍 Mengapa Ini Terjadi?

Ketika database lama tidak dihapus, password hash yang tersimpan mungkin berbeda dengan yang diharapkan oleh sistem. Ini bisa terjadi karena:

1. Database dibuat dengan versi code yang berbeda
2. Password hash algorithm berubah
3. Ada perubahan di seeding data

---

## 🚀 Langkah Lengkap

### Step 1: Reset Password
```
http://localhost:5055/Auth/ResetAdminPassword
```

### Step 2: Login
```
http://localhost:5055/Auth/Login
Username: admin
Password: admin123
```

### Step 3: Verifikasi
Setelah login berhasil, Anda akan diarahkan ke Admin Dashboard.

---

## 🎯 Alternatif: Hapus Database dan Mulai Fresh

Jika reset password tidak berhasil, Anda bisa hapus database dan mulai dari awal:

### Step 1: Stop Aplikasi
Tekan `Ctrl+C` di terminal

### Step 2: Backup Database (Opsional)
```cmd
copy babyshop.db babyshop-old.db
```

### Step 3: Hapus Database
```cmd
del babyshop.db
```

### Step 4: Jalankan Ulang
```cmd
dotnet run
```

### Step 5: Login
Database baru akan dibuat dengan admin user default:
- Username: `admin`
- Password: `admin123`

**Catatan:** Jika hapus database, produk custom Anda akan hilang, tapi gambar tetap ada di folder `wwwroot/images/products/`

---

## ✅ Kesimpulan

**Cara paling mudah:**
1. Akses: http://localhost:5055/Auth/ResetAdminPassword
2. Login dengan admin/admin123
3. Selesai!

**Jika masih error:**
- Hapus database dan mulai fresh
- Atau hubungi developer untuk troubleshooting lebih lanjut

---

## 📖 Related Documentation

- `LOGIN_ISSUE_FIXED.md` - Dokumentasi fix login sebelumnya
- `LOGIN_DEBUG_STEPS.md` - Langkah debug login
- `DATA_AMAN_TIDAK_HILANG.md` - Keamanan data saat restart

---

**Selamat mencoba! 🎉**
