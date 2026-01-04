# 🔧 Cara Fix Error "No Such Table: Employees"

## ❌ Error yang Anda Alami

```
SqliteException: SQLite Error 1: 'no such table: Employees'
```

---

## ✅ Solusi PALING MUDAH (Gunakan Script Otomatis)

Saya sudah buatkan script yang akan fix semua masalah secara otomatis!

### Langkah-langkah:

1. **Tutup semua browser** yang membuka aplikasi

2. **Double-click file ini:**
   ```
   fix-and-run.bat
   ```

3. **Tekan tombol apa saja** untuk melanjutkan

4. **Tunggu sampai muncul:**
   ```
   ✅ Admin user created
   ✅ Database ready
   Now listening on: http://localhost:5055
   ```

5. **Buka browser** dan login:
   ```
   http://localhost:5055/Auth/Login
   Username: admin
   Password: admin123
   ```

6. **Akses menu Karyawan:**
   ```
   http://localhost:5055/Admin/Employees
   ```

**SELESAI!** Error sudah fix! ✅

---

## 🔧 Solusi Manual (Jika Script Tidak Berhasil)

### Step 1: Stop Aplikasi

Cari terminal/command prompt yang menjalankan `dotnet run`, lalu:
- Tekan **Ctrl+C**
- Tunggu sampai aplikasi benar-benar stop

### Step 2: Hapus Database

Buka Command Prompt di folder `C:\BabyShopWeb2` dan jalankan:

```cmd
del /F babyshop.db
```

### Step 3: Jalankan Ulang

```cmd
dotnet run
```

### Step 4: Tunggu Sampai Siap

Anda akan melihat:
```
✅ Admin user created: username=admin, password=admin123
✅ Database ready
Now listening on: http://localhost:5055
```

### Step 5: Login dan Test

```
http://localhost:5055/Auth/Login
Username: admin
Password: admin123
```

Lalu akses:
```
http://localhost:5055/Admin/Employees
```

---

## ⚠️ PENTING: Data Anda

### ✅ Yang AMAN (Tidak Hilang):
- **Gambar produk** di `wwwroot/images/products/` ← 100% AMAN!
- Semua file code (.cs, .cshtml, .css, .js)
- Struktur folder

### ⚠️ Yang Akan Hilang (Dari Database):
- Produk custom yang Anda tambahkan
- Orders
- Newsletter subscribers
- Contact messages

### 🔄 Cara Restore Produk:
Karena **gambar masih ada**, Anda bisa:
1. Login sebagai admin
2. Klik "Kelola Produk" → "Tambah Produk"
3. Upload gambar yang sama (masih ada di folder)
4. Isi detail produk
5. Simpan

---

## 🎯 Mengapa Ini Terjadi?

Database lama dibuat **sebelum** fitur Employee ditambahkan, jadi tidak punya tabel Employees.

**Solusi:** Hapus database lama → Buat database baru dengan tabel Employees.

---

## 📊 Setelah Fix, Anda Akan Punya:

- ✅ Database baru dengan tabel Employees
- ✅ Menu Karyawan yang berfungsi
- ✅ Semua fitur CRUD karyawan
- ✅ Admin user (admin/admin123)
- ✅ Produk seed default (15 produk)
- ✅ Gambar produk yang masih ada di folder

---

## 🚨 Troubleshooting

### Error: "The process cannot access the file"
**Solusi:** Aplikasi masih berjalan. Stop dulu dengan Ctrl+C atau gunakan:
```cmd
taskkill /F /IM dotnet.exe
```

### Error: "Could not find file babyshop.db"
**Solusi:** Database sudah terhapus, langsung jalankan `dotnet run`

### Error masih muncul setelah fix
**Solusi:** 
1. Pastikan aplikasi benar-benar stop
2. Cek apakah file babyshop.db benar-benar terhapus
3. Restart terminal dan coba lagi

---

## ✅ Checklist

- [ ] Tutup semua browser
- [ ] Stop aplikasi (Ctrl+C)
- [ ] Hapus babyshop.db
- [ ] Jalankan `dotnet run`
- [ ] Tunggu sampai "Database ready"
- [ ] Login dengan admin/admin123
- [ ] Akses /Admin/Employees
- [ ] Verifikasi tidak ada error
- [ ] Tambah karyawan pertama untuk test

---

## 🎉 Selamat!

Setelah fix, fitur Employee Management siap digunakan!

**Quick Access:**
- Login: http://localhost:5055/Auth/Login
- Karyawan: http://localhost:5055/Admin/Employees
- Produk: http://localhost:5055/Admin/Products

**Selamat mengelola karyawan! 👥✨**
