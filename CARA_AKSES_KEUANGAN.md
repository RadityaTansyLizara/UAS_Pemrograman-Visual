# 💰 Cara Akses Halaman Keuangan

## 🔐 Penting: Harus Login sebagai Admin!

Halaman Keuangan hanya bisa diakses setelah login sebagai admin.

---

## 🚀 Langkah-Langkah:

### 1. Login sebagai Admin
```
http://localhost:5055/Auth/Login

Username: admin
Password: admin123
```

### 2. Akses Dashboard Admin
Setelah login, Anda akan diarahkan ke Dashboard Admin:
```
http://localhost:5055/Admin
```

### 3. Klik Menu "Keuangan" atau "Detail Keuangan"

**Dari Dashboard Admin:**
- Klik tombol "Detail Keuangan" di bagian Aksi Cepat

**Atau langsung akses:**
```
http://localhost:5055/Financial
```

---

## 📊 Fitur Halaman Keuangan:

### 1. Dashboard Keuangan
- Pemasukan hari ini
- Pengeluaran hari ini
- Saldo hari ini
- Pemasukan bulan ini
- Pengeluaran bulan ini
- Saldo bulan ini
- Grafik tren bulanan (line chart)
- Grafik perbandingan (pie chart)
- Grafik breakdown kategori (bar chart)
- Daftar transaksi harian

### 2. Tambah Transaksi
```
http://localhost:5055/Financial/CreateTransaction
```

Form untuk menambah transaksi manual:
- Jenis: Pemasukan / Pengeluaran
- Kategori: Penjualan, Pembelian, Gaji, dll
- Jumlah
- Deskripsi
- Tanggal
- Catatan (opsional)

### 3. Laporan Bulanan
```
http://localhost:5055/Financial/MonthlyReport
```

Laporan lengkap per bulan:
- Total pemasukan
- Total pengeluaran
- Saldo
- Daftar semua transaksi
- Grafik

---

## 🎯 Cara Menggunakan:

### Filter Tanggal:
1. Pilih tanggal untuk melihat transaksi harian
2. Pilih bulan dan tahun untuk melihat data bulanan
3. Grafik akan update otomatis

### Tambah Transaksi Manual:
1. Klik "Tambah Transaksi"
2. Pilih jenis (Pemasukan/Pengeluaran)
3. Pilih kategori
4. Isi jumlah dan deskripsi
5. Klik "Simpan"

### Hapus Transaksi:
- Hanya transaksi manual yang bisa dihapus
- Transaksi otomatis (dari penjualan) tidak bisa dihapus
- Klik tombol merah (trash icon) untuk hapus

---

## 📈 Grafik yang Tersedia:

### 1. Grafik Tren Bulanan (Line Chart)
- Menampilkan pemasukan, pengeluaran, dan saldo per hari
- Bisa melihat tren naik/turun
- Hover untuk melihat detail

### 2. Grafik Perbandingan (Pie Chart)
- Perbandingan pemasukan vs pengeluaran
- Menampilkan persentase
- Warna hijau = pemasukan, merah = pengeluaran

### 3. Grafik Breakdown Kategori (Bar Chart)
- Pemasukan dan pengeluaran per kategori
- Bisa membandingkan kategori mana yang paling besar
- Warna hijau = pemasukan, merah = pengeluaran

---

## 🚨 Troubleshooting:

### Halaman Kosong / Tidak Ada Grafik?
**Penyebab:** Belum ada transaksi
**Solusi:** Tambah transaksi manual atau lakukan penjualan

### Tidak Bisa Akses Halaman?
**Penyebab:** Belum login sebagai admin
**Solusi:** Login dulu di `http://localhost:5055/Auth/Login`

### Grafik Tidak Muncul?
**Penyebab:** Belum ada data untuk periode yang dipilih
**Solusi:** 
1. Pilih bulan/tahun yang ada datanya
2. Atau tambah transaksi baru

### Error "Unauthorized"?
**Penyebab:** Session login expired
**Solusi:** Login ulang

---

## 💡 Tips:

### Untuk Melihat Grafik:
1. Tambah beberapa transaksi manual terlebih dahulu
2. Atau lakukan beberapa penjualan (transaksi otomatis)
3. Grafik akan muncul setelah ada data

### Contoh Data Test:
**Transaksi 1:**
- Jenis: Pemasukan
- Kategori: Penjualan
- Jumlah: 500000
- Deskripsi: Penjualan produk hari ini

**Transaksi 2:**
- Jenis: Pengeluaran
- Kategori: Pembelian Produk
- Jumlah: 200000
- Deskripsi: Beli stock produk baru

**Transaksi 3:**
- Jenis: Pengeluaran
- Kategori: Biaya Operasional
- Jumlah: 50000
- Deskripsi: Listrik dan air

Setelah tambah 3 transaksi ini, grafik akan muncul!

---

## ✅ Checklist:

- [ ] Sudah login sebagai admin
- [ ] Bisa akses Dashboard Admin
- [ ] Bisa akses halaman Keuangan
- [ ] Bisa tambah transaksi
- [ ] Grafik muncul (setelah ada data)
- [ ] Bisa filter tanggal/bulan
- [ ] Bisa lihat laporan bulanan

---

## 📞 Jika Masih Bermasalah:

1. Pastikan aplikasi running: `dotnet run`
2. Pastikan sudah login sebagai admin
3. Coba tambah beberapa transaksi manual
4. Refresh halaman (F5)
5. Clear browser cache (Ctrl+Shift+Delete)

---

**Status:** ✅ Halaman Keuangan sudah siap digunakan!

**Akses:** `http://localhost:5055/Financial` (setelah login admin)

**Good luck! 💰📊**
