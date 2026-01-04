# 📊 Cara Melihat Data Produk & Gambar di MongoDB

## 🎯 Pertanyaan: "Bagaimana cara melihat database produk atau gambar produk yang ada di MongoDB?"

## 📍 Lokasi Collections

Berdasarkan screenshot MongoDB Compass Anda:

**Database:** `BabyShop3Berlian`

**Collections yang sudah ada:**
- 📦 Mainan Edukatif (0 documents)
- 👶 Pakaian Bayi (0 documents)
- 🧴 Perawatan Bayi (0 documents)
- 🍼 Perlengkapan Makanan (0 documents)

**Status:** Collections masih kosong karena belum ada data yang di-sync atau di-seed.

---

## 🔍 3 Cara Melihat Data di MongoDB

### 1️⃣ MongoDB Compass (GUI - Paling Mudah)

**Langkah-langkah:**

1. **Buka MongoDB Compass**
2. **Connect ke:** `mongodb://localhost:27017`
3. **Pilih Database:** `BabyShop3Berlian`
4. **Klik salah satu collection** (misalnya "Mainan Edukatif")
5. **Lihat documents** yang ada di collection tersebut

**Tampilan Data Produk:**
```json
{
  "_id": "ObjectId('...')",
  "name": "Baju Bayi Cussons Baby Pink",
  "description": "Baju bayi lembut dan nyaman",
  "price": 125000,
  "discountPrice": 99000,
  "stock": 50,
  "imageUrl": "/images/products/cussons-baju-pink.svg",
  "categoryId": "1",
  "categoryName": "Pakaian Bayi",
  "isActive": true,
  "createdAt": "2024-01-01T10:00:00Z"
}
```

**Catatan Penting tentang Gambar:**
- ❌ **Gambar TIDAK disimpan di MongoDB**
- ✅ **Hanya path/URL gambar yang disimpan** (contoh: `/images/products/cussons-baju-pink.svg`)
- ✅ **File gambar asli ada di:** `C:\BabyShopWeb2\wwwroot\images\products\`

---

### 2️⃣ Web Browser (Via Admin Interface)

**URL:** http://localhost:5055/MongoAdmin

**Fitur yang tersedia:**

1. **Check MongoDB** - Lihat statistik dan sample data
   ```
   http://localhost:5055/MongoAdmin/CheckMongoDB
   ```
   
   Output:
   ```
   📊 Collections Count:
   - Products: 15
   - Categories: 4
   - Orders: 0
   - Admin Users: 1
   
   📦 Sample Products (first 5):
   - Baju Bayi Cussons Baby Pink
     Price: Rp 125,000
     Stock: 50
     Category: Pakaian Bayi
   ```

2. **Seed MongoDB** - Isi data awal (jika kosong)
   ```
   http://localhost:5055/MongoAdmin/SeedMongoDB
   ```

3. **Migrate Data** - Copy dari SQLite ke MongoDB
   ```
   http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB
   ```

---

### 3️⃣ MongoDB Shell (Command Line)

**Buka Command Prompt:**

```cmd
mongosh
```

**Pilih Database:**
```javascript
use BabyShop3Berlian
```

**Lihat Collections:**
```javascript
show collections
```

**Lihat Semua Produk:**
```javascript
db.products.find().pretty()
```

**Lihat 5 Produk Pertama:**
```javascript
db.products.find().limit(5).pretty()
```

**Hitung Jumlah Produk:**
```javascript
db.products.countDocuments()
```

**Cari Produk Tertentu:**
```javascript
db.products.find({ name: "Baju Bayi Cussons Baby Pink" }).pretty()
```

---

## 🚀 Cara Mengisi Data ke MongoDB

### Opsi 1: Seed Data Awal (Recommended untuk Testing)

**Via Browser:**
```
http://localhost:5055/MongoAdmin/SeedMongoDB
```

**Hasil:**
- 4 Categories
- 15 Products (sample data)
- 1 Admin User

---

### Opsi 2: Migrate dari SQLite (Recommended untuk Production)

**Via Browser:**
```
http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB
```

**Hasil:**
- Semua data dari SQLite di-copy ke MongoDB
- Data SQLite tetap aman (tidak dihapus)

---

### Opsi 3: Auto-Sync (Otomatis)

**Status:** ✅ Sudah Aktif!

**Cara Kerja:**
1. Tambah produk via Admin Dashboard
2. Data otomatis tersimpan ke SQLite
3. Data otomatis ter-sync ke MongoDB

**Test Auto-Sync:**
1. Login sebagai admin: http://localhost:5055/Auth/Login
2. Tambah produk baru
3. Cek console output: `✅ Product 'Nama Produk' synced to MongoDB`
4. Refresh MongoDB Compass
5. Produk baru harus muncul

---

## 🖼️ Tentang Gambar Produk

### ❓ Apakah Gambar Disimpan di MongoDB?

**Jawaban:** ❌ TIDAK

### 📁 Dimana Gambar Disimpan?

**Lokasi File Gambar:**
```
C:\BabyShopWeb2\wwwroot\images\products\
```

**Contoh file:**
- `cussons-baju-pink.svg`
- `pampers-premium.svg`
- `pigeon-botol-susu.svg`

### 💾 Apa yang Disimpan di MongoDB?

**Hanya path/URL gambar:**
```json
{
  "imageUrl": "/images/products/cussons-baju-pink.svg"
}
```

### 🌐 Cara Akses Gambar di Browser:

```
http://localhost:5055/images/products/cussons-baju-pink.svg
```

### 📊 Struktur Data Lengkap:

```
SQLite (babyshop.db)
├── Products Table
│   ├── Id, Name, Price, Stock, etc.
│   └── ImageUrl: "/images/products/cussons-baju-pink.svg"
│
MongoDB (BabyShop3Berlian)
├── products Collection
│   ├── _id, name, price, stock, etc.
│   └── imageUrl: "/images/products/cussons-baju-pink.svg"
│
File System (wwwroot)
└── images/products/
    ├── cussons-baju-pink.svg ← File gambar asli
    ├── pampers-premium.svg
    └── pigeon-botol-susu.svg
```

---

## ✅ Checklist Verifikasi

### Step 1: Cek MongoDB Connection
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```
✅ Harus muncul "MongoDB Connection: OK"

### Step 2: Isi Data (Pilih salah satu)
- [ ] Seed data: http://localhost:5055/MongoAdmin/SeedMongoDB
- [ ] Migrate data: http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB

### Step 3: Verify di MongoDB Compass
- [ ] Buka MongoDB Compass
- [ ] Connect ke localhost:27017
- [ ] Pilih database BabyShop3Berlian
- [ ] Klik collection "products"
- [ ] Harus ada documents (tidak 0 lagi)

### Step 4: Test Auto-Sync
- [ ] Login admin
- [ ] Tambah produk baru
- [ ] Cek console: `✅ Product synced to MongoDB`
- [ ] Refresh MongoDB Compass
- [ ] Produk baru harus muncul

---

## 🔧 Troubleshooting

### ❌ Collections Masih Kosong (0 documents)

**Penyebab:**
- Data belum di-seed atau di-migrate
- Auto-sync belum jalan karena belum ada produk baru

**Solusi:**
1. Seed data: http://localhost:5055/MongoAdmin/SeedMongoDB
2. Atau migrate: http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB

---

### ❌ Gambar Tidak Muncul

**Penyebab:**
- File gambar tidak ada di folder `wwwroot/images/products/`

**Solusi:**
1. Cek folder: `C:\BabyShopWeb2\wwwroot\images\products\`
2. Pastikan file gambar ada
3. Upload gambar via Admin Dashboard

---

### ❌ MongoDB Connection Failed

**Penyebab:**
- MongoDB service tidak berjalan

**Solusi:**
```cmd
net start MongoDB
```

Atau buka Services (services.msc) dan start MongoDB service.

---

## 📖 Dokumentasi Terkait

- `MONGODB_SIAP_DIGUNAKAN.md` - Setup guide
- `MONGODB_AUTO_SYNC.md` - Auto-sync feature
- `HYBRID_DATABASE_SYSTEM.md` - Database architecture
- `PRODUCT_IMAGE_UPLOAD_GUIDE.md` - Upload gambar produk

---

## 🎯 Quick Reference

| Kebutuhan | URL/Command |
|-----------|-------------|
| 🔍 Cek MongoDB | http://localhost:5055/MongoAdmin/CheckMongoDB |
| 🌱 Seed Data | http://localhost:5055/MongoAdmin/SeedMongoDB |
| 🔄 Migrate Data | http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB |
| 🖥️ MongoDB Compass | mongodb://localhost:27017 |
| 📁 Folder Gambar | C:\BabyShopWeb2\wwwroot\images\products\ |
| 🌐 Akses Gambar | http://localhost:5055/images/products/[filename] |

---

## ✅ Kesimpulan

**Collections di MongoDB:**
- ✅ Sudah ada: Mainan Edukatif, Pakaian Bayi, Perawatan Bayi, Perlengkapan Makanan
- ⚠️ Masih kosong (0 documents)
- 🔄 Perlu di-seed atau di-migrate untuk mengisi data

**Gambar Produk:**
- ❌ TIDAK disimpan di MongoDB
- ✅ Disimpan di: `wwwroot/images/products/`
- ✅ MongoDB hanya simpan path: `/images/products/filename.svg`

**Next Steps:**
1. Seed atau migrate data ke MongoDB
2. Verify di MongoDB Compass
3. Test auto-sync dengan tambah produk baru

**Selamat! Sekarang Anda tahu cara melihat data di MongoDB! 🎉**
