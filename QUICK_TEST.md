# Quick Test Guide - BabyShop3Berlian

## 🚀 Aplikasi Berjalan di: `http://localhost:5055`

## ✅ Test Checklist Cepat

### 1. Homepage Test
- [ ] Buka `http://localhost:5055`
- [ ] Pastikan kategori dan produk terbaru tampil
- [ ] Klik tombol "Lihat Produk" pada kategori
- [ ] Klik tombol "Detail" dan "Beli" pada produk

### 2. Product Catalog Test
- [ ] Akses `/Product` dari navbar "Produk"
- [ ] Test filter kategori
- [ ] Test search produk
- [ ] Test sorting (harga, nama)
- [ ] Klik "Detail" pada produk

### 3. Product Detail Test
- [ ] Dari katalog, klik "Detail" produk
- [ ] Ubah quantity dengan +/-
- [ ] Klik "Tambah ke Keranjang"
- [ ] Perhatikan badge cart count di navbar

### 4. Shopping Cart Test
- [ ] Klik icon keranjang di navbar
- [ ] Verify produk yang ditambahkan tampil
- [ ] Test update quantity dengan +/-
- [ ] Test remove item
- [ ] Klik "Checkout"

### 5. Checkout Test
- [ ] Isi form customer:
  - Nama: Test User
  - Email: test@example.com
  - Phone: 081234567890
  - Alamat: Jl. Test No. 123, Jakarta
- [ ] Klik "Buat Pesanan"

### 6. Order Success & PDF Test
- [ ] Verify halaman success tampil
- [ ] Klik "Download Struk PDF"
- [ ] Verify PDF ter-download dan bisa dibuka

## 🐛 Jika Ada Error

### Error di Homepage
- Refresh browser (Ctrl+F5)
- Check console browser (F12)

### Error di Cart
- Clear browser cache
- Restart aplikasi

### Error PDF
- Check apakah file ter-download
- Buka dengan PDF reader

## 📱 Test Responsive

### Mobile Test
- Buka Developer Tools (F12)
- Toggle device toolbar
- Test di iPhone/Android view
- Verify navigation mobile

### Tablet Test
- Test di iPad view
- Verify layout tablet

## 🔄 Reset Data

Jika perlu reset data:
```bash
# Stop aplikasi (Ctrl+C)
# Delete database
rm babyshop.db
# Restart aplikasi
dotnet run
```

## ✨ Expected Results

### Homepage
- 4 kategori produk tampil
- 8 produk terbaru tampil
- Semua link berfungsi

### Product Pages
- 12 produk total (3 per kategori)
- Filter dan search berfungsi
- Detail produk lengkap

### Shopping Flow
- Add to cart berfungsi
- Cart count update real-time
- Checkout form validation
- PDF generation berhasil

## 📞 Troubleshooting

### Common Issues
1. **Database Error**: Restart aplikasi
2. **Null Reference**: Clear browser cache
3. **PDF Error**: Check browser download settings
4. **Session Error**: Clear cookies

### Success Indicators
- ✅ No console errors
- ✅ All pages load
- ✅ Cart functionality works
- ✅ PDF downloads successfully
- ✅ Responsive design works