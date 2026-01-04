# 🔄 MongoDB Auto-Sync Feature

## ✅ Fitur Auto-Sync Sudah Aktif!

Sistem sekarang akan **otomatis menyimpan data ke MongoDB** setiap kali ada perubahan di SQLite.

## 🎯 Apa yang Di-Sync Otomatis?

### 1. Products (Produk)
- ✅ **Create**: Saat tambah produk baru via Admin Dashboard
- ✅ **Update**: Saat edit produk (nama, harga, stock, gambar, dll)
- ✅ **Delete**: Saat hapus produk (soft delete di MongoDB)

### 2. Orders (Pesanan)
- ✅ **Create**: Saat customer checkout dan buat order baru
- ✅ Termasuk semua order items (detail produk yang dibeli)

## 🔧 Cara Kerja

### Workflow Auto-Sync:
```
User Action (Admin/Customer)
        ↓
   Save to SQLite ✅
        ↓
   Auto-Sync to MongoDB ✅
        ↓
   Data Tersimpan di Kedua Database
```

### Contoh: Tambah Produk Baru
```
1. Admin login → Admin Dashboard
2. Klik "Tambah Produk"
3. Isi form (nama, harga, upload gambar, dll)
4. Klik "Simpan"
   ↓
5. ✅ Produk tersimpan di SQLite
6. ✅ Produk otomatis ter-sync ke MongoDB
7. ✅ Gambar tersimpan di wwwroot/images/products/
```

## ⚙️ Konfigurasi

### Enable/Disable Auto-Sync

File: `appsettings.json`
```json
{
  "DatabaseSettings": {
    "ActiveDatabase": "SQLite",
    "EnableMongoDBBackup": true  // ← Set true untuk enable auto-sync
  }
}
```

**Enable Auto-Sync:**
```json
"EnableMongoDBBackup": true
```

**Disable Auto-Sync:**
```json
"EnableMongoDBBackup": false
```

## 📊 Monitoring Auto-Sync

### Via Console Log
Saat aplikasi running, perhatikan console output:

**Saat Tambah Produk:**
```
✅ Product 'Baju Bayi Cussons Baby Pink' synced to MongoDB
```

**Saat Update Produk:**
```
✅ Product 'Baju Bayi Cussons Baby Pink' updated in MongoDB
```

**Saat Delete Produk:**
```
✅ Product ID 5 deleted in MongoDB
```

**Saat Create Order:**
```
✅ Order 'ORD-20240101-001' synced to MongoDB
```

### Via MongoDB Compass
1. Buka MongoDB Compass
2. Connect: `mongodb://localhost:27017`
3. Database: `BabyShop3Berlian`
4. Refresh collections untuk melihat data baru

## 🔍 Verifikasi Auto-Sync

### Test 1: Tambah Produk
1. Login sebagai admin
2. Tambah produk baru
3. Cek MongoDB Compass → collection "products"
4. Produk baru harus muncul

### Test 2: Edit Produk
1. Edit produk yang sudah ada
2. Ubah harga atau stock
3. Cek MongoDB Compass
4. Data harus ter-update

### Test 3: Hapus Produk
1. Hapus produk
2. Cek MongoDB Compass
3. Produk masih ada tapi `isActive: false`

### Test 4: Create Order
1. Sebagai customer, checkout produk
2. Cek MongoDB Compass → collection "orders"
3. Order baru harus muncul

## 🛡️ Keamanan & Reliability

### Fail-Safe Mechanism
Jika MongoDB tidak tersedia (offline/error):
- ✅ Data tetap tersimpan di SQLite
- ✅ Aplikasi tetap berjalan normal
- ✅ Error hanya di-log, tidak crash
- ⚠️ Sync akan di-skip untuk transaksi tersebut

### Error Handling
```csharp
try {
    // Sync to MongoDB
} catch (Exception ex) {
    // Log error tapi tidak throw
    // Aplikasi tetap jalan
}
```

## 📈 Performance

### Impact Minimal
- Sync berjalan **asynchronous** (tidak blocking)
- User tidak merasakan delay
- Jika MongoDB lambat, tidak affect SQLite operation

### Recommended Setup
- **Development**: Auto-sync ON (untuk testing)
- **Production**: Auto-sync ON (untuk backup real-time)

## 🔄 Manual Sync (Jika Diperlukan)

Jika auto-sync sempat off atau ada data yang miss, bisa manual sync:

### Via Web Interface:
```
http://localhost:5055/MongoAdmin
```
Klik button **"Migrate Data"**

### Via Endpoint:
```
http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB
```

## 📝 Technical Details

### Files Modified:
1. ✅ `Services/MongoSyncService.cs` - Service untuk auto-sync
2. ✅ `Controllers/AdminController.cs` - Sync saat CRUD products
3. ✅ `Controllers/OrderController.cs` - Sync saat create order
4. ✅ `Program.cs` - Register MongoSyncService
5. ✅ `appsettings.json` - Enable auto-sync config

### Methods:
- `SyncProductAsync()` - Sync new product
- `SyncProductUpdateAsync()` - Sync product update
- `SyncProductDeleteAsync()` - Sync product delete (soft)
- `SyncOrderAsync()` - Sync new order
- `IsMongoDBAvailable()` - Check MongoDB connection

## 🎯 Benefits

### 1. Real-Time Backup
- Data langsung ter-backup ke MongoDB
- Tidak perlu manual sync
- Tidak ada data loss

### 2. Disaster Recovery
- Jika SQLite corrupt → restore dari MongoDB
- Jika MongoDB down → aplikasi tetap jalan dengan SQLite

### 3. Cloud Ready
- Data sudah di MongoDB
- Tinggal switch connection string ke MongoDB Atlas
- Ready untuk production deployment

### 4. Zero Maintenance
- Tidak perlu schedule backup job
- Tidak perlu manual intervention
- Automatic & transparent

## 🚨 Troubleshooting

### Auto-Sync Tidak Jalan

**Check 1: Config**
```json
"EnableMongoDBBackup": true  // ← Harus true
```

**Check 2: MongoDB Service**
```cmd
net start MongoDB
```

**Check 3: Console Log**
Lihat apakah ada error message di console

**Check 4: MongoDB Connection**
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```

### Data Tidak Muncul di MongoDB

**Solusi 1: Restart Aplikasi**
```cmd
Ctrl+C
dotnet run
```

**Solusi 2: Manual Sync**
```
http://localhost:5055/MongoAdmin/MigrateSQLiteToMongoDB
```

**Solusi 3: Check MongoDB Compass**
- Refresh collections
- Check connection

## 💡 Tips

### 1. Monitor Console
Selalu perhatikan console output untuk melihat sync status

### 2. Periodic Check
Sesekali cek MongoDB Compass untuk verify data

### 3. Backup Strategy
Meskipun auto-sync aktif, tetap backup SQLite file secara manual:
```cmd
copy babyshop.db backup\babyshop-2024-01-01.db
```

### 4. Production Deployment
Untuk production, gunakan MongoDB Atlas:
- Free tier 512MB
- Automatic backup
- Global deployment
- Update connection string di appsettings.json

## 📖 Related Documentation

- `MONGODB_SIAP_DIGUNAKAN.md` - MongoDB setup guide
- `HYBRID_DATABASE_SYSTEM.md` - Hybrid system architecture
- `MONGODB_EXISTING_SETUP.md` - Using existing MongoDB

---

## ✅ Summary

**Auto-Sync Status:** ✅ ACTIVE

**What's Synced:**
- ✅ Products (Create, Update, Delete)
- ✅ Orders (Create)

**Configuration:**
- ✅ Enabled in appsettings.json
- ✅ Fail-safe mechanism
- ✅ Asynchronous operation

**Benefits:**
- ✅ Real-time backup
- ✅ Zero maintenance
- ✅ Cloud ready
- ✅ Disaster recovery

**Selamat! Data Anda sekarang otomatis ter-backup ke MongoDB! 🎉**
