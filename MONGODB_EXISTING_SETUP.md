# 🍃 Menggunakan MongoDB yang Sudah Ada

## ✅ Status

Anda sudah punya MongoDB yang berjalan dengan:
- **Host**: localhost:27017
- **Database**: BabyShop3Berlian
- **Collections**: 
  - Mainan Edukatif
  - Pakaian Bayi
  - Perawatan Bayi
  - Perlengkapan Makanan

## 🔗 Integrasi dengan Aplikasi (Port 5055)

Aplikasi BabyShopWeb2 sekarang sudah terintegrasi dengan MongoDB Anda!

### Endpoints MongoDB Admin

Semua endpoint menggunakan port **5055**:

| Endpoint | Fungsi | URL |
|----------|--------|-----|
| **Check MongoDB** | Cek koneksi & data | http://localhost:5055/MongoAdmin/CheckMongoDB |
| **Seed MongoDB** | Tambah data awal (jika kosong) | http://localhost:5055/MongoAdmin/SeedMongoDB |
| **Migrate SQLite→MongoDB** | Copy data dari SQLite | http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB |
| **Clear MongoDB** | Hapus semua data | http://localhost:5055/MongoAdmin/ClearMongoDB |

## 🚀 Cara Menggunakan

### 1. Jalankan Aplikasi
```cmd
dotnet run
```

Aplikasi akan berjalan di: **http://localhost:5055**

### 2. Cek Koneksi MongoDB
Buka browser:
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```

Ini akan menampilkan:
- Status koneksi MongoDB
- Jumlah data di setiap collection
- Sample products (5 produk pertama)

### 3. Sync Data (Opsional)

#### Jika MongoDB Kosong:
```
http://localhost:5055/MongoAdmin/SeedMongoDB
```
Ini akan menambahkan:
- 4 Categories
- 15 Products
- 1 Admin User

#### Jika Ingin Copy dari SQLite:
```
http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB
```
Ini akan copy semua data dari SQLite ke MongoDB.

## 📊 Struktur Data

### Collections yang Digunakan

#### 1. categories (atau nama collection Anda)
```json
{
  "_id": "ObjectId",
  "name": "Pakaian Bayi",
  "description": "Baju, celana, dan aksesoris bayi",
  "iconClass": "fas fa-tshirt",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z"
}
```

#### 2. products
```json
{
  "_id": "ObjectId",
  "name": "Baju Bayi Cussons Baby Pink",
  "description": "Baju bayi 100% katun organik",
  "price": 65000,
  "discountPrice": null,
  "stock": 50,
  "imageUrl": "/images/products/cussons-baju-pink.svg",
  "categoryId": "ObjectId",
  "categoryName": "Pakaian Bayi",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

#### 3. orders
```json
{
  "_id": "ObjectId",
  "orderNumber": "ORD-20240101-001",
  "customerName": "Nama Customer",
  "customerEmail": "email@example.com",
  "totalAmount": 500000,
  "paymentStatus": "Paid",
  "orderStatus": "Processing",
  "items": [
    {
      "productId": "ObjectId",
      "productName": "Nama Produk",
      "quantity": 2,
      "price": 100000,
      "subtotal": 200000
    }
  ],
  "createdAt": "2024-01-01T00:00:00Z"
}
```

#### 4. adminUsers
```json
{
  "_id": "ObjectId",
  "username": "admin",
  "passwordHash": "hash...",
  "fullName": "Administrator",
  "createdAt": "2024-01-01T00:00:00Z",
  "lastLogin": null
}
```

## 🔄 Hybrid Mode (SQLite + MongoDB)

Saat ini aplikasi menggunakan **SQLite sebagai primary database**, tapi Anda bisa:

### Opsi 1: Tetap SQLite + MongoDB Backup
```json
// appsettings.json
{
  "DatabaseSettings": {
    "ActiveDatabase": "SQLite",
    "EnableMongoDBBackup": false
  }
}
```
- Aplikasi pakai SQLite
- MongoDB untuk backup manual
- Sync data sesekali via endpoint

### Opsi 2: Switch ke MongoDB
```json
// appsettings.json (future)
{
  "DatabaseSettings": {
    "ActiveDatabase": "MongoDB",
    "EnableMongoDBBackup": false
  }
}
```
- Aplikasi pakai MongoDB
- SQLite untuk local dev

## 🎯 Workflow Recommended

### Development (Sekarang):
1. ✅ Develop dengan SQLite (fast, simple)
2. ✅ Test fitur baru
3. ✅ Sync ke MongoDB sesekali untuk backup
4. ✅ Verify di MongoDB Compass

### Production (Nanti):
1. ✅ Migrate semua data ke MongoDB
2. ✅ Deploy MongoDB ke Atlas (cloud)
3. ✅ Switch ActiveDatabase ke MongoDB
4. ✅ Automatic backup di cloud

## 🔍 Monitoring

### Via MongoDB Compass
1. Buka MongoDB Compass
2. Connect: `mongodb://localhost:27017`
3. Database: `BabyShop3Berlian`
4. Lihat collections dan data

### Via Browser (Port 5055)
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```

## 💡 Tips

### 1. Backup Data
MongoDB Anda sudah punya data, pastikan backup:
```
mongodump --db BabyShop3Berlian --out C:\backup\mongodb
```

### 2. Restore Data
Jika perlu restore:
```
mongorestore --db BabyShop3Berlian C:\backup\mongodb\BabyShop3Berlian
```

### 3. Export Collection
Export ke JSON:
```
mongoexport --db BabyShop3Berlian --collection products --out products.json
```

### 4. Import Collection
Import dari JSON:
```
mongoimport --db BabyShop3Berlian --collection products --file products.json
```

## 🚨 Troubleshooting

### "Unable to connect to MongoDB"
**Cek MongoDB Service:**
```cmd
net start MongoDB
```

**Atau via Services:**
1. Win + R → `services.msc`
2. Cari "MongoDB"
3. Start service

### Data Tidak Muncul
**Cek via Compass:**
1. Buka MongoDB Compass
2. Connect ke localhost:27017
3. Verify data ada

**Cek via Endpoint:**
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```

### Port 5055 Tidak Bisa Diakses
**Pastikan aplikasi running:**
```cmd
dotnet run
```

**Cek output console:**
```
Now listening on: http://localhost:5055
```

## 📖 Dokumentasi Lengkap

- `HYBRID_DATABASE_SYSTEM.md` - Arsitektur hybrid
- `MONGODB_QUICK_START.md` - Quick setup guide
- `MONGODB_SETUP_GUIDE.md` - Dokumentasi detail

## ✅ Quick Test

### 1. Jalankan Aplikasi
```cmd
cd C:\BabyShopWeb2
dotnet run
```

### 2. Test Koneksi MongoDB
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```

### 3. Lihat Data di Compass
- Buka MongoDB Compass
- Connect: mongodb://localhost:27017
- Database: BabyShop3Berlian
- Verify collections

### 4. Test Admin Dashboard
```
http://localhost:5055/Auth/Login
Username: admin
Password: admin123
```

### 5. Test Produk
```
http://localhost:5055/Product
```

## 🎉 Selesai!

Aplikasi Anda sekarang terintegrasi dengan MongoDB yang sudah ada!

**Semua endpoint menggunakan port 5055** ✅
**MongoDB connection: localhost:27017** ✅
**Database: BabyShop3Berlian** ✅
**Data Anda aman!** ✅
