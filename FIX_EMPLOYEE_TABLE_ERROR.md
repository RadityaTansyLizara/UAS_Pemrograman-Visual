# 🔧 Fix: No Such Table 'Employees' Error

## ❌ Error yang Terjadi

```
SqliteException: SQLite Error 1: 'no such table: Employees'
```

Error ini muncul saat membuka halaman `/Admin/Employees` karena database lama tidak memiliki tabel Employees.

---

## ✅ Solusi: Hapus Database dan Buat Ulang

### ⚠️ PENTING: Gambar Produk Anda AMAN!

Sebelum melanjutkan, ingat bahwa:
- ✅ **Gambar produk di folder `wwwroot/images/products/` TIDAK AKAN TERHAPUS**
- ⚠️ Produk custom dari database akan hilang (tapi bisa ditambah ulang dengan gambar yang sama)
- ✅ Database baru akan dibuat dengan tabel Employees

---

## 🚀 Langkah-langkah Fix

### Step 1: Stop Aplikasi

Di terminal yang menjalankan `dotnet run`, tekan **Ctrl+C**

### Step 2: Hapus Database Lama

```cmd
del babyshop.db
```

Jika ada konfirmasi, ketik **Y** dan Enter.

### Step 3: Jalankan Ulang Aplikasi

```cmd
dotnet run
```

### Step 4: Tunggu Sampai Aplikasi Siap

Anda akan melihat output:
```
✅ Admin user created: username=admin, password=admin123
✅ Database ready
Now listening on: http://localhost:5055
```

### Step 5: Login

```
URL: http://localhost:5055/Auth/Login
Username: admin
Password: admin123
```

### Step 6: Akses Menu Karyawan

```
http://localhost:5055/Admin/Employees
```

Sekarang halaman Employees harus berfungsi tanpa error! ✅

---

## 📊 Yang Terjadi Setelah Hapus Database

### ❌ Yang Hilang:
- Produk custom yang Anda tambahkan (dari database)
- Orders
- Newsletter subscribers
- Contact messages
- Financial transactions

### ✅ Yang TETAP ADA:
- **Gambar produk di folder `wwwroot/images/products/`** ← PENTING!
- Produk seed default (15 produk) akan kembali
- Admin user (admin/admin123)
- **Tabel Employees baru** ← INI YANG KITA BUTUHKAN!

### 🔄 Cara Restore Produk Custom:
Karena gambar masih ada di folder, Anda bisa:
1. Login sebagai admin
2. Tambah produk via Admin Dashboard
3. Upload gambar yang sama (masih ada di folder)
4. Selesai!

---

## 💡 Mengapa Ini Terjadi?

Database lama dibuat sebelum fitur Employee ditambahkan, jadi tidak memiliki tabel Employees. Ketika code mencoba akses tabel yang tidak ada, muncul error.

**Solusi:** Hapus database lama dan buat ulang dengan schema terbaru (termasuk tabel Employees).

---

## 🎯 Alternatif: Gunakan Migration (Advanced)

Jika Anda familiar dengan Entity Framework migrations, bisa gunakan cara ini untuk menambahkan tabel tanpa hapus database:

```cmd
# Install EF tools jika belum
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add AddEmployeeTable

# Update database
dotnet ef database update

# Jalankan aplikasi
dotnet run
```

Tapi cara paling mudah adalah hapus database dan buat ulang.

---

## ✅ Checklist

- [ ] Stop aplikasi (Ctrl+C)
- [ ] Hapus babyshop.db
- [ ] Jalankan ulang: `dotnet run`
- [ ] Login dengan admin/admin123
- [ ] Akses /Admin/Employees
- [ ] Verifikasi tidak ada error
- [ ] Tambah karyawan pertama
- [ ] (Opsional) Tambah ulang produk custom

---

## 📖 Related Documentation

- `DATA_AMAN_TIDAK_HILANG.md` - Penjelasan keamanan data
- `BACKUP_BEFORE_RESTART.md` - Cara backup yang aman
- `EMPLOYEE_QUICK_START.md` - Quick start guide Employee

---

## 🎉 Setelah Fix

Setelah database dibuat ulang, Anda akan memiliki:
- ✅ Tabel Employees yang berfungsi
- ✅ Menu Karyawan yang bisa diakses
- ✅ Semua fitur CRUD karyawan
- ✅ Gambar produk yang masih ada
- ✅ Admin user yang siap digunakan

**Selamat! Fitur Employee Management siap digunakan! 🎊**
