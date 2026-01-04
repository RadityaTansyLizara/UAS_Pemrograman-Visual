# 🚀 MongoDB Quick Start - 5 Menit Setup

## 🎯 Tujuan
Menyimpan data produk secara permanen di MongoDB agar tidak hilang saat restart aplikasi.

## ⚡ Quick Setup (Pilih Salah Satu)

### Opsi A: MongoDB Local (Paling Mudah)

#### 1. Download & Install MongoDB
```
https://www.mongodb.com/try/download/community
```
- Download "MongoDB Community Server"
- Install dengan default settings
- MongoDB akan otomatis berjalan di background

#### 2. Install Package
```cmd
dotnet restore
```

#### 3. Jalankan Aplikasi
```cmd
dotnet run
```

#### 4. Seed Data MongoDB
Buka browser dan akses:
```
http://localhost:5055/Admin/SeedMongoDB
```

✅ **Selesai!** Data sekarang tersimpan di MongoDB.

---

### Opsi B: MongoDB Atlas (Cloud - Gratis)

#### 1. Daftar MongoDB Atlas
```
https://www.mongodb.com/cloud/atlas/register
```
- Daftar dengan email
- Pilih "Free" tier (512MB gratis)

#### 2. Buat Cluster
- Klik "Build a Database"
- Pilih "Free" tier
- Pilih region: Singapore
- Klik "Create"

#### 3. Setup User & Network
**Database Access:**
- Klik "Database Access"
- Add user: `admin` / `admin123` (atau password lain)
- Role: "Read and write to any database"

**Network Access:**
- Klik "Network Access"
- Add IP: "Allow Access from Anywhere" (0.0.0.0/0)

#### 4. Get Connection String
- Klik "Database" → "Connect"
- Pilih "Connect your application"
- Copy connection string
- Ganti `<password>` dengan password Anda

#### 5. Update appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb+srv://admin:admin123@cluster.xxxxx.mongodb.net/?retryWrites=true&w=majority"
  }
}
```

#### 6. Install Package & Run
```cmd
dotnet restore
dotnet run
```

#### 7. Seed Data
```
http://localhost:5055/Admin/SeedMongoDB
```

✅ **Selesai!** Data tersimpan di cloud MongoDB Atlas.

---

## 🔍 Verifikasi Data

### Cara 1: MongoDB Compass (GUI)
1. Download: https://www.mongodb.com/try/download/compass
2. Install dan buka
3. Connect ke `mongodb://localhost:27017` (local) atau paste connection string (Atlas)
4. Lihat database "BabyShop3Berlian"
5. Lihat collections: products, categories, orders

### Cara 2: Via Browser
Akses endpoint check:
```
http://localhost:5055/Admin/CheckMongoDB
```

## 📊 Struktur Data

### Products Collection
```json
{
  "_id": "507f1f77bcf86cd799439011",
  "name": "Baju Bayi Cussons Baby Pink",
  "price": 65000,
  "stock": 50,
  "categoryName": "Pakaian Bayi",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z"
}
```

## 💡 Keuntungan MongoDB

✅ **Data Permanen** - Tidak hilang saat restart
✅ **Scalable** - Bisa handle jutaan produk
✅ **Cloud Ready** - Bisa deploy ke Atlas
✅ **Fast** - Query cepat
✅ **Flexible** - Mudah tambah field baru

## 🔄 Migrasi dari SQLite

Jika Anda sudah punya data di SQLite dan ingin pindah ke MongoDB:

```
http://localhost:5055/Admin/MigrateSQLiteToMongoDB
```

Endpoint ini akan:
1. Copy semua produk dari SQLite ke MongoDB
2. Copy semua kategori
3. Copy semua orders
4. Data SQLite tetap ada (tidak dihapus)

## 🎯 Next Steps

Setelah setup MongoDB:
1. ✅ Tambah produk via Admin Dashboard
2. ✅ Data tersimpan di MongoDB
3. ✅ Restart aplikasi - data tetap ada
4. ✅ Bisa akses dari mana saja (jika pakai Atlas)

## 🚨 Troubleshooting

### "Unable to connect to MongoDB"
- **Local**: Pastikan MongoDB service berjalan
- **Atlas**: Cek connection string dan network access

### "Authentication failed"
- Cek username/password di connection string
- Pastikan user punya permission "readWrite"

### Data tidak muncul
- Pastikan sudah seed data: `/Admin/SeedMongoDB`
- Cek di MongoDB Compass
- Restart aplikasi

## 📖 Dokumentasi Lengkap

Lihat `MONGODB_SETUP_GUIDE.md` untuk dokumentasi detail.

---

## ✅ Checklist

- [ ] Install MongoDB (Local atau Atlas)
- [ ] Run `dotnet restore`
- [ ] Update `appsettings.json` (jika pakai Atlas)
- [ ] Run `dotnet run`
- [ ] Seed data: `http://localhost:5055/Admin/SeedMongoDB`
- [ ] Verify: `http://localhost:5055/Admin/CheckMongoDB`
- [ ] Test: Tambah produk via Admin Dashboard
- [ ] Restart aplikasi - cek data masih ada

**Selamat! Data Anda sekarang tersimpan permanen di MongoDB! 🎉**
