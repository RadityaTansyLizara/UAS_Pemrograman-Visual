# 🔄 Hybrid Database System - BabyShop3Berlian

## 🎯 Sistem Terbaik: SQLite + MongoDB

Saya sudah implementasikan **Hybrid System** yang memberikan keamanan dan fleksibilitas maksimal:

### 📊 Arsitektur

```
┌─────────────────────────────────────────┐
│         BabyShop3Berlian Web App        │
├─────────────────────────────────────────┤
│                                         │
│  ┌──────────────┐    ┌──────────────┐  │
│  │   SQLite     │    │   MongoDB    │  │
│  │  (Primary)   │◄──►│  (Backup/    │  │
│  │              │    │   Cloud)     │  │
│  └──────────────┘    └──────────────┘  │
│                                         │
└─────────────────────────────────────────┘
```

## ✅ Keuntungan Hybrid System

### SQLite (Primary - Development/Local)
- ✅ **Zero Setup** - Langsung jalan
- ✅ **Fast** - Akses lokal super cepat
- ✅ **Simple** - Satu file database
- ✅ **Reliable** - Proven technology
- ✅ **Portable** - Copy file = backup

### MongoDB (Secondary - Production/Cloud)
- ✅ **Scalable** - Handle jutaan records
- ✅ **Cloud Ready** - Deploy ke Atlas gratis
- ✅ **Distributed** - Multi-server support
- ✅ **Backup** - Otomatis di cloud
- ✅ **Analytics** - Advanced querying

### Hybrid Benefits
- ✅ **Best of Both Worlds**
- ✅ **Gradual Migration** - Pindah kapan saja
- ✅ **Disaster Recovery** - Double backup
- ✅ **Flexibility** - Switch sesuai kebutuhan
- ✅ **Zero Downtime** - Migrasi tanpa stop aplikasi

## 🚀 Quick Start

### Step 1: Install MongoDB (5 Menit)

#### Windows:
1. Download: https://www.mongodb.com/try/download/community
2. Install dengan default settings
3. MongoDB akan auto-start sebagai Windows Service

#### Verify Installation:
```cmd
mongod --version
```

### Step 2: Install Packages
```cmd
dotnet restore
```

### Step 3: Jalankan Aplikasi
```cmd
dotnet run
```

### Step 4: Setup MongoDB
Buka browser dan akses endpoint berikut:

#### A. Seed Data Awal
```
http://localhost:5055/MongoAdmin/SeedMongoDB
```
Ini akan membuat:
- 4 Categories
- 15 Products
- 1 Admin User

#### B. Atau Migrasi dari SQLite
```
http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB
```
Ini akan copy semua data dari SQLite ke MongoDB.

### Step 5: Verify
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```

## 📋 Endpoints MongoDB Admin

| Endpoint | Fungsi |
|----------|--------|
| `/MongoAdmin/SeedMongoDB` | Seed data awal ke MongoDB |
| `/MongoAdmin/CheckMongoDB` | Cek status dan data MongoDB |
| `/MongoAdmin/MigrateSQLiteToMongoDB` | Copy data dari SQLite ke MongoDB |
| `/MongoAdmin/ClearMongoDB` | Hapus semua data MongoDB |

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
  },
  "DatabaseSettings": {
    "ActiveDatabase": "SQLite",
    "EnableMongoDBBackup": false
  }
}
```

### Untuk MongoDB Atlas (Cloud):
```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb+srv://username:password@cluster.mongodb.net/?retryWrites=true&w=majority"
  }
}
```

## 📊 Strategi Penggunaan

### Scenario 1: Development (Saat Ini)
```
Primary: SQLite
Backup: MongoDB (optional)
```
- Develop dengan SQLite (cepat, simple)
- Sync ke MongoDB sesekali untuk backup
- Test di MongoDB sebelum production

### Scenario 2: Production (Masa Depan)
```
Primary: MongoDB Atlas (Cloud)
Backup: SQLite (local snapshot)
```
- Production pakai MongoDB Atlas
- SQLite untuk local testing
- Automatic cloud backup

### Scenario 3: Hybrid (Recommended)
```
Primary: SQLite (local)
Sync: MongoDB Atlas (cloud backup)
```
- Aplikasi pakai SQLite (fast)
- Auto-sync ke MongoDB setiap malam
- Best performance + cloud backup

## 🔄 Workflow Migrasi

### Phase 1: Setup (Sekarang)
1. ✅ Install MongoDB
2. ✅ Seed atau migrate data
3. ✅ Verify data di MongoDB Compass
4. ✅ Test CRUD operations

### Phase 2: Testing (1-2 Minggu)
1. Jalankan aplikasi dengan SQLite
2. Sync data ke MongoDB secara manual
3. Compare performance
4. Test backup/restore

### Phase 3: Production (Kapan Siap)
1. Setup MongoDB Atlas
2. Migrate data ke cloud
3. Update connection string
4. Switch ActiveDatabase ke MongoDB
5. Monitor performance

## 💾 Backup Strategy

### Daily Backup (Recommended)
```
1. SQLite → MongoDB (setiap malam)
2. MongoDB → Atlas (auto-sync)
3. SQLite file → External drive (manual)
```

### Disaster Recovery
```
Jika SQLite corrupt:
→ Restore dari MongoDB

Jika MongoDB down:
→ Aplikasi tetap jalan dengan SQLite

Jika keduanya bermasalah:
→ Restore dari backup file
```

## 🛠️ Tools

### MongoDB Compass (GUI)
Download: https://www.mongodb.com/try/download/compass

Features:
- Visual database browser
- Query builder
- Performance monitoring
- Import/Export data

### MongoDB Atlas (Cloud)
URL: https://www.mongodb.com/cloud/atlas

Features:
- Free tier 512MB
- Automatic backups
- Global deployment
- Monitoring dashboard

## 📈 Performance Comparison

### SQLite
- Read: ⚡⚡⚡⚡⚡ (Excellent)
- Write: ⚡⚡⚡⚡ (Very Good)
- Concurrent: ⚡⚡ (Limited)
- Scalability: ⚡⚡⚡ (Good for < 1GB)

### MongoDB
- Read: ⚡⚡⚡⚡ (Very Good)
- Write: ⚡⚡⚡⚡⚡ (Excellent)
- Concurrent: ⚡⚡⚡⚡⚡ (Excellent)
- Scalability: ⚡⚡⚡⚡⚡ (Unlimited)

## 🔒 Security

### SQLite
- File-based: Protect file permissions
- Encryption: Use SQLCipher for encryption
- Backup: Regular file copies

### MongoDB
- Authentication: Username/password
- Encryption: TLS/SSL in transit
- Authorization: Role-based access
- Audit: Logging all operations

## 📖 Dokumentasi

- `MONGODB_QUICK_START.md` - Setup cepat 5 menit
- `MONGODB_SETUP_GUIDE.md` - Dokumentasi lengkap
- `PENJELASAN_DATABASE.md` - Perbandingan database

## ✅ Checklist Setup

- [ ] Install MongoDB
- [ ] Run `dotnet restore`
- [ ] Run `dotnet run`
- [ ] Seed MongoDB: `/MongoAdmin/SeedMongoDB`
- [ ] Verify: `/MongoAdmin/CheckMongoDB`
- [ ] Install MongoDB Compass
- [ ] Connect Compass ke localhost:27017
- [ ] Lihat database "BabyShop3Berlian"
- [ ] Test add product via Admin
- [ ] Verify data di kedua database

## 🎯 Rekomendasi

### Untuk Saat Ini (Development):
```
✅ Pakai SQLite sebagai primary
✅ Setup MongoDB sebagai backup
✅ Sync data sesekali
✅ Test MongoDB features
```

### Untuk Production (Nanti):
```
✅ Setup MongoDB Atlas (cloud)
✅ Migrate data ke Atlas
✅ Switch ke MongoDB primary
✅ Keep SQLite untuk local dev
```

## 💡 Tips

1. **Backup Rutin**: Copy `babyshop.db` setiap hari
2. **Test MongoDB**: Seed data dan test CRUD
3. **Monitor**: Cek MongoDB Compass untuk verify data
4. **Gradual**: Tidak perlu buru-buru switch ke MongoDB
5. **Documentation**: Baca semua .md files untuk detail

## 🚨 Troubleshooting

### MongoDB Connection Failed
```
Error: Unable to connect to MongoDB

Solution:
1. Cek MongoDB service running
2. Verify connection string
3. Check firewall settings
```

### Data Not Syncing
```
Error: Data tidak muncul di MongoDB

Solution:
1. Run seed: /MongoAdmin/SeedMongoDB
2. Or migrate: /MongoAdmin/MigrateSQLiteToMongoDB
3. Verify: /MongoAdmin/CheckMongoDB
```

### Performance Issues
```
Issue: Aplikasi lambat

Solution:
1. Check database size
2. Add indexes di MongoDB
3. Optimize queries
4. Consider switching to MongoDB
```

---

## 🎉 Kesimpulan

Dengan Hybrid System ini, Anda mendapatkan:
- ✅ **Keamanan Maksimal** - Double backup
- ✅ **Fleksibilitas** - Switch kapan saja
- ✅ **Performance** - Best of both worlds
- ✅ **Scalability** - Ready untuk growth
- ✅ **Zero Risk** - Gradual migration

**Data Anda AMAN dengan sistem ini!** 🔒
