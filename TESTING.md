# Manual Testing Guide - BabyShop3Berlian

## 🧪 Testing Checklist

### 1. Homepage Testing
- [ ] **Load Homepage**: Akses `http://localhost:5055`
- [ ] **Navigation**: Test semua link di navbar
- [ ] **Hero Section**: Verify CTA buttons berfungsi
- [ ] **Categories**: Click kategori produk menuju halaman yang benar
- [ ] **Featured Products**: Test tombol "Detail" dan "Beli"
- [ ] **Responsive**: Test di mobile, tablet, desktop
- [ ] **Smooth Scrolling**: Test anchor links (#about, #contact)

### 2. Product Catalog Testing
- [ ] **All Products**: Akses `/Product` - tampil semua produk
- [ ] **Category Filter**: Test filter berdasarkan kategori
- [ ] **Search**: Test pencarian produk berdasarkan nama
- [ ] **Sort**: Test sorting berdasarkan harga dan nama
- [ ] **Product Cards**: Verify info produk (nama, harga, stok)
- [ ] **Discount Badge**: Verify badge diskon muncul untuk produk diskon
- [ ] **Stock Status**: Test badge stok (tersedia, terbatas, habis)
- [ ] **Add to Cart**: Test tombol "Beli" dari katalog

### 3. Product Details Testing
- [ ] **Product Detail**: Akses `/Product/Details/{id}` untuk berbagai produk
- [ ] **Product Info**: Verify semua informasi produk tampil
- [ ] **Price Display**: Test tampilan harga normal dan diskon
- [ ] **Stock Alert**: Test alert untuk stok terbatas/habis
- [ ] **Quantity Selector**: Test +/- quantity dengan min/max
- [ ] **Add to Cart**: Test tambah ke keranjang dengan quantity berbeda
- [ ] **Related Products**: Verify produk terkait tampil dan berfungsi
- [ ] **Breadcrumb**: Test navigasi breadcrumb

### 4. Category Pages Testing
- [ ] **Category Access**: Akses `/Product/Category/{id}` untuk setiap kategori
- [ ] **Category Info**: Verify nama, deskripsi, dan icon kategori
- [ ] **Products Display**: Verify produk dalam kategori tampil
- [ ] **Empty Category**: Test kategori kosong (jika ada)
- [ ] **Back Navigation**: Test tombol kembali ke semua produk

### 5. Shopping Cart Testing
- [ ] **Empty Cart**: Akses `/Cart` saat keranjang kosong
- [ ] **Add Items**: Tambah berbagai produk ke keranjang
- [ ] **Cart Count**: Verify badge count di navbar update real-time
- [ ] **Item Display**: Verify semua info item tampil (nama, harga, qty, subtotal)
- [ ] **Update Quantity**: Test +/- quantity di keranjang
- [ ] **Remove Item**: Test hapus item individual
- [ ] **Clear Cart**: Test kosongkan seluruh keranjang
- [ ] **Price Calculation**: Verify perhitungan subtotal dan total
- [ ] **Shipping Cost**: Test gratis ongkir di atas Rp 200.000
- [ ] **Continue Shopping**: Test tombol lanjut belanja

### 6. Checkout Process Testing
- [ ] **Checkout Access**: Akses checkout dari keranjang berisi
- [ ] **Empty Cart Redirect**: Test akses checkout dengan keranjang kosong
- [ ] **Customer Form**: Test semua field form pelanggan
- [ ] **Form Validation**: Test validasi required fields
- [ ] **Email Validation**: Test format email
- [ ] **Phone Validation**: Test format nomor telepon
- [ ] **Order Summary**: Verify ringkasan pesanan benar
- [ ] **Shipping Calculation**: Verify perhitungan ongkir
- [ ] **Place Order**: Test submit form checkout

### 7. Order Success & PDF Testing
- [ ] **Order Success**: Verify halaman sukses setelah checkout
- [ ] **Order Details**: Verify semua detail pesanan tampil
- [ ] **Order Number**: Verify nomor pesanan unik generated
- [ ] **Customer Info**: Verify info pelanggan tampil benar
- [ ] **Items List**: Verify daftar item pesanan
- [ ] **Price Summary**: Verify total pembayaran
- [ ] **Download PDF**: Test download struk PDF
- [ ] **PDF Content**: Verify isi PDF lengkap dan benar
- [ ] **PDF Format**: Verify format PDF rapi dan readable

### 8. Navigation & UI Testing
- [ ] **Navbar**: Test semua link navbar
- [ ] **Logo**: Test klik logo kembali ke homepage
- [ ] **Cart Icon**: Test klik icon keranjang
- [ ] **Footer Links**: Test semua link di footer
- [ ] **Breadcrumbs**: Test navigasi breadcrumb di semua halaman
- [ ] **Back Buttons**: Test tombol kembali di berbagai halaman

### 9. Responsive Design Testing
- [ ] **Mobile (< 576px)**: Test di smartphone
- [ ] **Tablet (576-768px)**: Test di tablet
- [ ] **Desktop (> 768px)**: Test di desktop
- [ ] **Navigation Mobile**: Test hamburger menu di mobile
- [ ] **Cards Layout**: Verify layout cards responsive
- [ ] **Forms Mobile**: Test form di mobile device
- [ ] **Buttons Mobile**: Test ukuran tombol di mobile

### 10. Error Handling Testing
- [ ] **404 Pages**: Test akses URL tidak valid
- [ ] **Product Not Found**: Test akses produk ID tidak ada
- [ ] **Category Not Found**: Test akses kategori ID tidak ada
- [ ] **Server Errors**: Test handling error server
- [ ] **Network Errors**: Test handling network issues
- [ ] **Form Errors**: Test error messages pada form

### 11. Performance Testing
- [ ] **Page Load Speed**: Verify halaman load cepat
- [ ] **Image Loading**: Test loading gambar produk
- [ ] **Database Queries**: Monitor query performance
- [ ] **Memory Usage**: Check memory consumption
- [ ] **Session Management**: Test session persistence

### 12. Data Integrity Testing
- [ ] **Stock Updates**: Verify stok berkurang setelah order
- [ ] **Cart Persistence**: Test keranjang persist antar session
- [ ] **Order Data**: Verify data order tersimpan benar
- [ ] **Product Data**: Verify data produk konsisten
- [ ] **Category Data**: Verify data kategori benar

## 🔍 Test Data

### Sample Products untuk Testing
1. **Baju Bayi Lucu Pink** (ID: 1) - Rp 45.000 - Stok: 50
2. **Set Baju Tidur Bayi** (ID: 3) - Rp 85.000 → Rp 75.000 (Diskon) - Stok: 30
3. **Puzzle Kayu Angka** (ID: 4) - Rp 65.000 - Stok: 25
4. **Baby Wipes Sensitive** (ID: 12) - Rp 25.000 - Stok: 80

### Test Scenarios
1. **Normal Purchase**: Beli 1-2 produk, checkout normal
2. **Bulk Purchase**: Beli banyak produk, test gratis ongkir
3. **Discount Products**: Beli produk diskon, verify harga
4. **Low Stock**: Beli produk stok terbatas
5. **Mixed Cart**: Kombinasi produk normal dan diskon

## 🐛 Bug Reporting

Jika menemukan bug, catat:
- **URL**: Halaman dimana bug terjadi
- **Steps**: Langkah-langkah reproduce bug
- **Expected**: Hasil yang diharapkan
- **Actual**: Hasil yang terjadi
- **Browser**: Browser dan versi
- **Device**: Desktop/mobile/tablet
- **Screenshot**: Jika memungkinkan

## ✅ Success Criteria

Website dianggap berhasil jika:
- [ ] Semua fitur utama berfungsi
- [ ] Tidak ada error critical
- [ ] Responsive di semua device
- [ ] Performance acceptable
- [ ] PDF generation berfungsi
- [ ] Data integrity terjaga
- [ ] User experience smooth

## 🚀 Production Readiness

Sebelum production:
- [ ] All tests passed
- [ ] Performance optimized
- [ ] Security reviewed
- [ ] Error handling complete
- [ ] Documentation updated
- [ ] Backup strategy ready