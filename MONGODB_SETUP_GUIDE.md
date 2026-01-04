# 🍃 MongoDB Setup Guide - BabyShop3Berlian

## 📋 Ringkasan

Saya sudah menambahkan dukungan MongoDB ke aplikasi BabyShopWeb2. Sekarang Anda punya 2 pilihan database:
1. **SQLite** (default, sudah berjalan) - Database lokal, file-based
2. **MongoDB** (baru) - Database NoSQL yang lebih scalable

## 🎯 Keuntungan MongoDB

- ✅ **Data Permanen** - Data tersimpan di MongoDB server, tidak akan hilang
- ✅ **Scalable** - Bisa handle data dalam jumlah besar
- ✅ **Flexible Schema** - Mudah menambah field baru
- ✅ **Cloud Ready** - Bisa deploy ke MongoDB Atlas (cloud)
- ✅ **Performance** - Cepat untuk read/write operations

## 📦 Yang Sudah Dibuat

### 1. MongoDB Models
- `Models/MongoDB/MongoProduct.cs` - Model produk untuk MongoDB
- `Models/MongoDB/MongoCategory.cs` - Model kategori
- `Models/MongoDB/MongoOrder.cs` - Model order
- `Models/MongoDB/MongoAdminUser.cs` - Model admin user

### 2. MongoDB Context
- `Data/MongoDbContext.cs` - Context untuk koneksi MongoDB

### 3. MongoDB Services
- `Services/MongoProductService.cs` - Service untuk operasi produk

### 4. Configuration
- `appsettings.json` - Ditambahkan MongoDB connection string

## 🚀 Cara Setup MongoDB

### Opsi 1: MongoDB Local (Recommended untuk Development)

#### Step 1: Install MongoDB
1. Download MongoDB Community Server dari: https://www.mongodb.com/try/download/community
2. Install dengan default settings
3. MongoDB akan berjalan di `mongodb://localhost:27017`

#### Step 2: Install MongoDB Compass (Optional - GUI Tool)
1. Download dari: https://www.mongodb.com/try/download/compass
2. Install dan buka
3. Connect ke `mongodb://localhost:27017`

#### Step 3: Restore NuGet Packages
```cmd
dotnet restore
```

#### Step 4: Jalankan Aplikasi
```cmd
dotnet run
```

### Opsi 2: MongoDB Atlas (Cloud - Free Tier)

#### Step 1: Buat Account MongoDB Atlas
1. Buka https://www.mongodb.com/cloud/atlas/register
2. Daftar gratis (Free Tier 512MB)

#### Step 2: Buat Cluster
1. Pilih "Build a Database"
2. Pilih "Free" tier
3. Pilih region terdekat (Singapore/Jakarta)
4. Klik "Create"

#### Step 3: Setup Database Access
1. Klik "Database Access" di sidebar
2. Klik "Add New Database User"
3. Buat username dan password (catat!)
4. Pilih "Read and write to any database"

#### Step 4: Setup Network Access
1. Klik "Network Access" di sidebar
2. Klik "Add IP Address"
3. Pilih "Allow Access from Anywhere" (0.0.0.0/0)
4. Klik "Confirm"

#### Step 5: Get Connection String
1. Klik "Database" di sidebar
2. Klik "Connect" pada cluster Anda
3. Pilih "Connect your application"
4. Copy connection string
5. Ganti `<password>` dengan password Anda

#### Step 6: Update appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb+srv://username:password@cluster.mongodb.net/?retryWrites=true&w=majority"
  },
  "MongoDb": {
    "DatabaseName": "BabyShop3Berlian"
  }
}
```

## 🔄 Migrasi Data dari SQLite ke MongoDB

Saya akan buatkan script untuk migrasi data otomatis dari SQLite ke MongoDB.

### Cara Migrasi:
1. Pastikan aplikasi SQLite sudah punya data
2. Jalankan endpoint migrasi (akan saya buatkan)
3. Data akan ter-copy ke MongoDB
4. Anda bisa pilih mau pakai SQLite atau MongoDB

## 📊 Struktur Database MongoDB

### Collection: products
```json
{
  "_id": "ObjectId",
  "name": "Nama Produk",
  "description": "Deskripsi",
  "price": 100000,
  "discountPrice": 85000,
  "stock": 50,
  "imageUrl": "/images/products/...",
  "categoryId": "ObjectId",
  "categoryName": "Nama Kategori",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

### Collection: categories
```json
{
  "_id": "ObjectId",
  "name": "Pakaian Bayi",
  "description": "Deskripsi kategori",
  "iconClass": "fas fa-tshirt",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z"
}
```

### Collection: orders
```json
{
  "_id": "ObjectId",
  "orderNumber": "ORD-20240101-001",
  "customerName": "Nama Customer",
  "customerEmail": "email@example.com",
  "customerPhone": "08123456789",
  "shippingAddress": "Alamat lengkap",
  "totalAmount": 500000,
  "paymentMethod": "Transfer Bank",
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
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

## 🔧 Konfigurasi

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=babyshop.db",
    "MongoDb": "mongodb://localhost:27017"
  },
  "MongoDb": {
    "DatabaseName": "BabyShop3Berlian"
  }
}
```

### appsettings.Development.json
Untuk development, bisa override connection string:
```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017"
  }
}
```

## 🎯 Next Steps

1. **Install MongoDB** (pilih Local atau Atlas)
2. **Restore packages**: `dotnet restore`
3. **Jalankan aplikasi**: `dotnet run`
4. **Seed data MongoDB** (saya akan buatkan endpoint)
5. **Test CRUD operations**

## 💡 Tips

### Untuk Development
- Gunakan MongoDB Local untuk development
- Gunakan MongoDB Compass untuk melihat data
- Data akan tersimpan permanen di MongoDB

### Untuk Production
- Gunakan MongoDB Atlas (cloud)
- Setup backup otomatis
- Monitor performance di Atlas dashboard
- Gunakan connection string dengan authentication

## 🔍 Monitoring

### MongoDB Compass
- Buka MongoDB Compass
- Connect ke `mongodb://localhost:27017`
- Pilih database "BabyShop3Berlian"
- Lihat collections: products, categories, orders, adminUsers

### MongoDB Atlas Dashboard
- Login ke https://cloud.mongodb.com
- Lihat metrics: operations, connections, storage
- Setup alerts untuk monitoring

## 🚨 Troubleshooting

### Error: "Unable to connect to MongoDB"
- Pastikan MongoDB service berjalan
- Cek connection string di appsettings.json
- Untuk Atlas: cek network access dan database user

### Error: "Authentication failed"
- Cek username dan password di connection string
- Pastikan user punya permission "readWrite"

### Data tidak muncul
- Cek apakah data sudah di-seed
- Gunakan MongoDB Compass untuk verify
- Cek collection name (case-sensitive)

## 📖 Resources

- MongoDB Documentation: https://docs.mongodb.com/
- MongoDB Atlas: https://www.mongodb.com/cloud/atlas
- MongoDB Compass: https://www.mongodb.com/products/compass
- C# MongoDB Driver: https://mongodb.github.io/mongo-csharp-driver/

## ✅ Checklist Setup

- [ ] Install MongoDB (Local atau Atlas)
- [ ] Install MongoDB Compass (optional)
- [ ] Update appsettings.json dengan connection string
- [ ] Run `dotnet restore`
- [ ] Run `dotnet run`
- [ ] Seed data MongoDB
- [ ] Test CRUD operations
- [ ] Verify data di MongoDB Compass

---

**Note:** Saat ini aplikasi masih menggunakan SQLite. Setelah MongoDB setup selesai, saya akan buatkan:
1. Script migrasi data dari SQLite ke MongoDB
2. Option untuk switch antara SQLite dan MongoDB
3. Endpoint untuk seed data MongoDB
