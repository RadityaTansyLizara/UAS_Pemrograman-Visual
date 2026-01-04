# Panduan Upload Gambar Produk - BabyShop3Berlian

## ✅ Fitur yang Sudah Ada

### 1. Upload Gambar Saat Create Product
- Form upload di `/Admin/CreateProduct`
- Gambar disimpan ke folder `wwwroot/images/products/`
- Nama file: GUID unik + ekstensi asli
- Path disimpan ke database di kolom `ImageUrl`

### 2. Upload Gambar Saat Edit Product
- Form upload di `/Admin/EditProduct`
- Preview gambar saat ini
- Preview gambar baru sebelum save
- Gambar lama otomatis dihapus jika upload baru
- Jika tidak upload baru, gambar lama tetap digunakan

### 3. Tampilan Gambar
- Gambar ditampilkan di semua halaman:
  - Home page (featured products)
  - Product listing
  - Product details
  - Cart
  - Checkout
  - Order receipt
  - Admin product list

## 📋 Cara Upload Gambar Produk

### Untuk Produk Baru:
1. Buka `/Admin/Products`
2. Klik "Tambah Produk Baru"
3. Isi semua field
4. Klik "Choose File" di field "Gambar Produk"
5. Pilih gambar (JPG, PNG, GIF, max 5MB)
6. Preview akan muncul otomatis
7. Klik "Simpan"
8. ✅ Gambar tersimpan permanen

### Untuk Edit Gambar:
1. Buka `/Admin/Products`
2. Klik "Edit" pada produk
3. Lihat "Gambar Saat Ini" (gambar lama)
4. Untuk ganti gambar:
   - Klik "Choose File"
   - Pilih gambar baru
   - Preview muncul
5. Klik "Simpan Perubahan"
6. ✅ Gambar lama dihapus, gambar baru tersimpan

### Untuk Tetap Pakai Gambar Lama:
1. Buka `/Admin/EditProduct`
2. **JANGAN** upload file baru
3. Edit field lain (nama, harga, dll)
4. Klik "Simpan Perubahan"
5. ✅ Gambar lama tetap ada

## 🔧 Teknis

### Lokasi Penyimpanan:
```
wwwroot/
  └── images/
      └── products/
          ├── abc123-def456.jpg
          ├── xyz789-ghi012.png
          └── ...
```

### Database:
```sql
Products table:
- ImageUrl: "/images/products/abc123-def456.jpg"
```

### Controller Logic:
```csharp
// Create: Upload gambar baru
if (model.ImageFile != null) {
    var fileName = await SaveImageAsync(model.ImageFile);
    product.ImageUrl = $"/images/products/{fileName}";
}

// Edit: Upload gambar baru (opsional)
if (model.ImageFile != null) {
    // Hapus gambar lama
    DeleteImage(product.ImageUrl);
    // Simpan gambar baru
    var fileName = await SaveImageAsync(model.ImageFile);
    product.ImageUrl = $"/images/products/{fileName}";
}
// Jika tidak upload, ImageUrl tetap sama (tidak diubah)
```

## ✨ Fitur Tambahan

### Preview Image:
- Preview otomatis saat pilih file
- Menggunakan FileReader JavaScript
- Tidak perlu upload dulu untuk lihat preview

### Validasi:
- Accept: image/* (hanya file gambar)
- Recommended: JPG, PNG, GIF
- Max size: 5MB (bisa diatur di server)

### Fallback:
- Jika gambar tidak ada: tampilkan placeholder
- Jika gambar error: tampilkan icon default

## 🎯 Testing

### Test Upload Gambar Baru:
1. Buka `/Admin/CreateProduct`
2. Upload gambar test
3. Save
4. Cek folder `wwwroot/images/products/` - file ada
5. Cek database - ImageUrl terisi
6. Buka `/Product` - gambar muncul
7. Refresh page - gambar tetap ada ✅

### Test Edit Gambar:
1. Buka `/Admin/EditProduct/{id}`
2. Lihat gambar saat ini
3. Upload gambar baru
4. Save
5. Cek folder - gambar lama terhapus, gambar baru ada
6. Refresh - gambar baru muncul ✅

### Test Edit Tanpa Ganti Gambar:
1. Buka `/Admin/EditProduct/{id}`
2. Edit nama/harga saja
3. **JANGAN** upload gambar baru
4. Save
5. Refresh - gambar lama tetap ada ✅

## 🐛 Troubleshooting

### Gambar Hilang Setelah Refresh:
**Penyebab:** Database di-recreate (EnsureDeleted)
**Solusi:** 
- Comment `context.Database.EnsureDeleted()` di Program.cs
- Atau backup folder `wwwroot/images/products/` sebelum restart

### Gambar Tidak Muncul:
**Cek:**
1. Path di database benar? (harus `/images/products/filename.ext`)
2. File ada di folder? (cek `wwwroot/images/products/`)
3. Permission folder? (harus writable)
4. Browser cache? (hard refresh: Ctrl+F5)

### Upload Gagal:
**Cek:**
1. File size > 5MB?
2. Format file valid? (JPG/PNG/GIF)
3. Folder `wwwroot/images/products/` ada?
4. Permission write?

## 📝 Catatan Penting

1. **Gambar Tersimpan Permanen:**
   - Selama database tidak di-delete
   - Selama folder wwwroot tidak dihapus
   - Refresh page tidak akan menghilangkan gambar

2. **Edit Produk:**
   - Upload gambar baru = ganti gambar
   - Tidak upload = gambar lama tetap dipakai
   - Gambar lama otomatis dihapus saat ganti

3. **Seed Data:**
   - Produk seed menggunakan SVG placeholder
   - Bisa diganti dengan upload gambar real
   - Path SVG: `/images/products/nama-produk.svg`

## 🎨 Rekomendasi Gambar

- **Ukuran:** 800x800px (square)
- **Format:** JPG (untuk foto), PNG (untuk logo/transparent)
- **File size:** < 500KB (optimal)
- **Background:** Putih atau transparent
- **Quality:** High resolution untuk zoom

## ✅ Status Fitur

- ✅ Upload gambar saat create
- ✅ Upload gambar saat edit
- ✅ Preview sebelum save
- ✅ Hapus gambar lama otomatis
- ✅ Gambar tersimpan permanen
- ✅ Tampil di semua halaman
- ✅ Responsive image display
- ✅ Fallback untuk gambar error

**Kesimpulan:** Sistem upload gambar sudah lengkap dan berfungsi dengan baik! 🎉
