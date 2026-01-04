# 🚀 Quick Start - Employee Management

## ⚠️ PENTING: Restart Aplikasi Dulu!

Karena ada perubahan database schema (tambah tabel Employee), Anda perlu restart aplikasi.

### ✅ Jangan Khawatir: Data Anda AMAN!
- **Gambar produk TIDAK AKAN TERHAPUS** (ada di folder `wwwroot/images/products/`)
- **Produk yang sudah ditambahkan bisa tetap ada** (jika tidak hapus database)

### Langkah Restart (PILIH SALAH SATU):

#### Opsi 1: Restart Tanpa Hapus Database (RECOMMENDED)

1. **Stop aplikasi** (Ctrl+C)
2. **Jalankan ulang**:
   ```cmd
   dotnet run
   ```
3. **Jika TIDAK ada error** → Selesai! Data Anda tetap ada!
4. **Jika ADA error** → Lanjut ke Opsi 2

#### Opsi 2: Jika Ada Error, Hapus Database

1. **Stop aplikasi** (Ctrl+C)
2. **Backup dulu** (opsional tapi recommended):
   ```cmd
   copy babyshop.db babyshop-backup.db
   ```
3. **Hapus database**:
   ```cmd
   del babyshop.db
   ```
4. **Jalankan ulang**:
   ```cmd
   dotnet run
   ```
5. **Tunggu sampai muncul:**
   ```
   ✅ Admin user created
   ✅ Database ready
   Now listening on: http://localhost:5055
   ```

**Catatan:** Jika hapus database, produk custom Anda akan hilang dari database, tapi **gambar tetap ada di folder**. Anda bisa tambah produk ulang dengan gambar yang sama.

---

## 🎯 Akses Fitur Employee Management

### 1. Login Admin
```
URL: http://localhost:5055/Auth/Login
Username: admin
Password: admin123
```

### 2. Buka Menu Karyawan
Di sidebar admin, klik menu **"Karyawan"** (ikon 👥)

Atau akses langsung:
```
http://localhost:5055/Admin/Employees
```

---

## ✨ Fitur yang Tersedia

### 📋 Daftar Karyawan
- Lihat semua karyawan dalam tabel
- Badge warna untuk status, shift, jabatan
- Statistik: Total, Aktif, Per Jabatan, Per Shift

### ➕ Tambah Karyawan
**Data yang perlu diisi:**
- ID Karyawan (unique, contoh: EMP001)
- Nama Lengkap
- Jabatan (Kasir, Supervisor, Admin, Manager, Staff Gudang)
- No. HP
- Email (opsional)
- Alamat
- Tanggal Masuk
- Status (Aktif, Nonaktif, Cuti)
- Shift (Pagi, Siang, Malam)
- Gaji (opsional)
- Catatan (opsional)

### ✏️ Edit Karyawan
- Update semua informasi karyawan
- Tracking waktu update otomatis

### 👁️ Detail Karyawan
- Info lengkap karyawan
- Perhitungan lama bekerja otomatis
- Aksi cepat: Edit & Hapus

### 🗑️ Hapus Karyawan
- Konfirmasi modal sebelum hapus
- Data terhapus permanen

---

## 📊 Contoh Data Test

Gunakan data ini untuk testing:

```
ID Karyawan: EMP001
Nama: Siti Nurhaliza
Jabatan: Kasir
No. HP: 081234567890
Email: siti@babyshop.com
Alamat: Jl. Merdeka No. 123, Jakarta
Tanggal Masuk: 01/01/2024
Status: Aktif
Shift: Pagi
Gaji: 4500000
```

```
ID Karyawan: EMP002
Nama: Budi Santoso
Jabatan: Supervisor
No. HP: 081234567891
Email: budi@babyshop.com
Alamat: Jl. Sudirman No. 456, Jakarta
Tanggal Masuk: 15/01/2024
Status: Aktif
Shift: Siang
Gaji: 6000000
```

---

## 🎨 Badge Reference

### Status:
- 🟢 **Aktif** = Badge hijau
- 🟡 **Cuti** = Badge kuning
- 🔴 **Nonaktif** = Badge merah

### Shift:
- ☀️ **Pagi** (07:00-15:00) = Badge biru
- 🌤️ **Siang** (15:00-23:00) = Badge kuning
- 🌙 **Malam** (23:00-07:00) = Badge hitam

### Jabatan:
- 📋 Semua jabatan = Badge biru muda

---

## ✅ Quick Test

1. ✅ Login admin
2. ✅ Klik menu "Karyawan"
3. ✅ Klik "Tambah Karyawan"
4. ✅ Isi form dengan data test
5. ✅ Klik "Simpan"
6. ✅ Verifikasi karyawan muncul di tabel
7. ✅ Klik "Detail" untuk lihat info lengkap
8. ✅ Klik "Edit" untuk update data
9. ✅ Cek statistik di bawah tabel

---

## 🚨 Troubleshooting

### Menu Karyawan tidak muncul?
**Solusi:** Restart aplikasi dengan langkah di atas

### Error saat tambah karyawan?
**Solusi:** 
1. Stop aplikasi (Ctrl+C)
2. Hapus babyshop.db
3. Jalankan ulang: `dotnet run`

### ID Karyawan sudah digunakan?
**Solusi:** Gunakan ID yang berbeda (EMP001, EMP002, dst)

---

## 📖 Dokumentasi Lengkap

Lihat file: `EMPLOYEE_MANAGEMENT_FEATURE.md`

## 🧪 Test HTML

Buka file: `test_employee_management.html` di browser

---

## 🎉 Selamat!

Fitur Employee Management siap digunakan!

**URL:** http://localhost:5055/Admin/Employees
