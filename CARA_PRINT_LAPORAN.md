# 📄 CARA PRINT LAPORAN KEUANGAN

## ✅ Fitur Print Laporan Sudah Ditambahkan!

Sekarang halaman Laporan Bulanan sudah dilengkapi dengan:
- 📊 Grafik Bar (Pemasukan & Pengeluaran Harian)
- 🥧 Grafik Pie (Perbandingan Pemasukan vs Pengeluaran)
- 🖨️ Tampilan print-friendly yang rapi

---

## 🚀 CARA MENGGUNAKAN:

### 1️⃣ Akses Halaman Laporan Bulanan

**Dari Dashboard:**
- Buka: `http://localhost:5055/Admin`
- Klik tombol "Detail Keuangan"
- Klik tombol "Laporan" di kanan atas

**Atau langsung:**
```
http://localhost:5055/Financial/MonthlyReport
```

### 2️⃣ Pilih Bulan & Tahun

- Pilih bulan yang ingin dilihat
- Pilih tahun
- Sistem akan otomatis reload dengan data bulan tersebut

### 3️⃣ Lihat Laporan

Laporan menampilkan:
- ✅ **Ringkasan:** Total Pemasukan, Pengeluaran, Saldo Bersih
- ✅ **Rincian:** Breakdown pemasukan dan pengeluaran per kategori
- ✅ **Grafik Bar:** Tren harian pemasukan & pengeluaran
- ✅ **Grafik Pie:** Perbandingan pemasukan vs pengeluaran
- ✅ **Tabel:** Daftar semua transaksi bulan tersebut

### 4️⃣ Print Laporan

**Cara 1: Tombol Print**
- Klik tombol "Cetak Laporan" di kanan atas
- Dialog print akan muncul
- Pilih printer atau "Save as PDF"
- Klik "Print"

**Cara 2: Keyboard Shortcut**
- Tekan `Ctrl + P` (Windows) atau `Cmd + P` (Mac)
- Dialog print akan muncul
- Pilih printer atau "Save as PDF"
- Klik "Print"

---

## 🎨 TAMPILAN PRINT:

### Yang Ditampilkan:
✅ Header: Logo & Nama Toko (BabyShop3Berlian)
✅ Judul: Laporan Keuangan Bulanan
✅ Periode: Bulan & Tahun
✅ Tanggal Cetak
✅ Ringkasan Keuangan (3 kartu)
✅ Rincian Pemasukan & Pengeluaran
✅ Grafik Bar (Harian)
✅ Grafik Pie (Perbandingan)
✅ Tabel Transaksi Lengkap
✅ Footer: Copyright

### Yang Disembunyikan:
❌ Sidebar admin (pink)
❌ Navbar
❌ Tombol-tombol (Kembali, Cetak)
❌ Form filter bulan/tahun
❌ Footer website

---

## 💾 SAVE AS PDF:

### Windows (Chrome/Edge):
1. Tekan `Ctrl + P`
2. Di "Destination", pilih "Save as PDF"
3. Klik "Save"
4. Pilih lokasi penyimpanan
5. Beri nama file (misal: `Laporan-Desember-2024.pdf`)
6. Klik "Save"

### Windows (Firefox):
1. Tekan `Ctrl + P`
2. Di "Printer", pilih "Microsoft Print to PDF"
3. Klik "Print"
4. Pilih lokasi penyimpanan
5. Beri nama file
6. Klik "Save"

---

## 📊 TIPS PRINT YANG BAGUS:

### 1. Pastikan Ada Data
- Tambahkan data dummy jika belum ada transaksi:
  ```
  http://localhost:5055/Financial/SeedDummyData
  ```

### 2. Pilih Bulan yang Tepat
- Pilih bulan yang ada datanya
- Grafik akan lebih informatif jika ada banyak transaksi

### 3. Setting Print
- **Orientation:** Portrait (Potrait)
- **Paper Size:** A4
- **Margins:** Default
- **Scale:** 100% (atau "Fit to page")
- **Background Graphics:** ON (untuk warna grafik)

### 4. Preview Sebelum Print
- Lihat preview di dialog print
- Pastikan semua grafik terlihat
- Pastikan tidak ada halaman kosong

---

## 🔗 URL PENTING:

| Halaman | URL |
|---------|-----|
| Dashboard Admin | http://localhost:5055/Admin |
| Keuangan | http://localhost:5055/Admin/Financial |
| **Laporan Bulanan** | **http://localhost:5055/Financial/MonthlyReport** |
| Tambah Transaksi | http://localhost:5055/Financial/CreateTransaction |
| Data Dummy | http://localhost:5055/Financial/SeedDummyData |

---

## 📸 UNTUK PENGUMPULAN:

### Screenshot yang Perlu:
1. ✅ Halaman Laporan Bulanan (tampilan web)
2. ✅ Grafik Bar (zoom in)
3. ✅ Grafik Pie (zoom in)
4. ✅ Preview Print (Ctrl+P)
5. ✅ File PDF hasil print

### Cara Screenshot:
- **Windows:** `Windows + Shift + S`
- **Mac:** `Cmd + Shift + 4`

---

## ⚠️ TROUBLESHOOTING:

### Grafik Tidak Muncul di Print:
**Solusi:**
1. Pastikan "Background Graphics" ON di print settings
2. Tunggu grafik selesai loading sebelum print
3. Gunakan Chrome/Edge (lebih baik untuk print grafik)

### Halaman Terpotong:
**Solusi:**
1. Ubah scale menjadi "Fit to page"
2. Atau ubah margins menjadi "Minimum"

### Warna Tidak Keluar:
**Solusi:**
1. Di print settings, aktifkan "Background Graphics"
2. Atau "Print backgrounds" (tergantung browser)

---

## 🎉 SELESAI!

Fitur print laporan sudah siap digunakan!

**Langkah Cepat:**
1. Buka: `http://localhost:5055/Financial/MonthlyReport`
2. Pilih bulan & tahun
3. Klik "Cetak Laporan" atau tekan `Ctrl + P`
4. Save as PDF atau print langsung

Good luck untuk pengumpulan! 🚀
