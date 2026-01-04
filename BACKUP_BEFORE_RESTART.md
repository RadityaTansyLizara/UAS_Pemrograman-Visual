# 💾 Backup Data Sebelum Restart

## ⚠️ PENTING: Backup Dulu!

Sebelum restart aplikasi untuk menambahkan fitur Employee, backup data Anda dulu!

---

## 📋 Langkah Backup yang Aman

### Step 1: Backup Database SQLite
```cmd
copy babyshop.db babyshop-backup.db
```

Ini akan membuat copy database Anda. Jika ada masalah, tinggal restore:
```cmd
copy babyshop-backup.db babyshop.db
```

### Step 2: Backup Folder Gambar (Opsional)
```cmd
xcopy wwwroot\images\products wwwroot\images\products-backup\ /E /I
```

Tapi sebenarnya gambar tidak akan terhapus, jadi ini opsional.

---

## 🚀 Cara Restart TANPA Kehilangan Data

### Opsi 1: Restart Tanpa Hapus Database (RECOMMENDED)

1. **Stop aplikasi** (Ctrl+C)

2. **Jalankan ulang**:
   ```cmd
   dotnet run
   ```

3. **Cek apakah ada error**:
   - Jika TIDAK ada error → Selesai! Data Anda aman!
   - Jika ADA error → Lanjut ke Opsi 2

### Opsi 2: Jika Ada Error, Gunakan Migration

Jika aplikasi error karena tabel Employee belum ada, kita bisa tambahkan tabel tanpa hapus data:

1. **Stop aplikasi** (Ctrl+C)

2. **Buat migration**:
   ```cmd
   dotnet ef migrations add AddEmployeeTable
   ```

3. **Update database**:
   ```cmd
   dotnet ef database update
   ```

4. **Jalankan ulang**:
   ```cmd
   dotnet run
   ```

Dengan cara ini, tabel Employee ditambahkan TANPA menghapus data produk yang sudah ada!

---

## 🔍 Cek Data Setelah Restart

### 1. Cek Produk
```
http://localhost:5055/Admin/Products
```
Semua produk yang sudah Anda tambahkan harus masih ada.

### 2. Cek Gambar
```
http://localhost:5055/images/products/[nama-file-gambar]
```
Semua gambar harus masih bisa diakses.

### 3. Cek Menu Karyawan
```
http://localhost:5055/Admin/Employees
```
Menu baru harus muncul dan berfungsi.

---

## ⚠️ Jika Terpaksa Hapus Database

Jika Anda terpaksa hapus `babyshop.db`, berikut yang terjadi:

### ❌ Yang Hilang:
- Produk custom yang Anda tambahkan (dari database)
- Order yang sudah ada
- Newsletter subscribers
- Contact messages
- Financial transactions

### ✅ Yang TETAP ADA:
- **Gambar produk di folder `wwwroot/images/products/`**
- Produk seed default (15 produk) akan kembali
- Admin user (admin/admin123)

### 🔄 Cara Restore Produk Custom:
Setelah restart, Anda bisa:
1. Tambah ulang produk via Admin Dashboard
2. Upload ulang gambar (gambar lama masih ada di folder)
3. Atau gunakan backup database: `copy babyshop-backup.db babyshop.db`

---

## 💡 Rekomendasi Saya

**Coba Opsi 1 dulu (restart tanpa hapus database):**

```cmd
# Stop aplikasi (Ctrl+C)
# Langsung jalankan ulang
dotnet run
```

**Jika error, coba Opsi 2 (migration):**

```cmd
# Stop aplikasi (Ctrl+C)
dotnet ef migrations add AddEmployeeTable
dotnet ef database update
dotnet run
```

**Jika masih error, baru hapus database:**

```cmd
# Backup dulu!
copy babyshop.db babyshop-backup.db
# Hapus database
del babyshop.db
# Jalankan ulang
dotnet run
```

---

## 🎯 Yang Pasti AMAN

1. ✅ **Gambar produk di `wwwroot/images/products/` TIDAK AKAN TERHAPUS**
2. ✅ **Jika restart tanpa hapus database, semua data tetap ada**
3. ✅ **Jika hapus database, gambar tetap ada, tinggal tambah produk ulang**
4. ✅ **Backup database bisa di-restore kapan saja**

---

## 🚨 Troubleshooting

### Error: "Table Employee not found"
**Solusi:** Gunakan migration (Opsi 2) atau hapus database (Opsi 3)

### Produk hilang setelah restart
**Solusi:** Restore backup:
```cmd
copy babyshop-backup.db babyshop.db
dotnet run
```

### Gambar tidak muncul
**Solusi:** Cek folder `wwwroot/images/products/`, gambar harus masih ada di sana.

---

## ✅ Kesimpulan

**Data Anda AMAN!** Terutama gambar produk yang tidak akan terhapus sama sekali.

**Langkah paling aman:**
1. Backup database dulu: `copy babyshop.db babyshop-backup.db`
2. Restart tanpa hapus database
3. Jika error, gunakan migration
4. Jika masih error, baru hapus database (tapi gambar tetap aman!)

**Selamat mencoba! 💪**
