# 🍃 Install MongoDB di Windows - 5 Menit

## 📥 Step 1: Download MongoDB

1. Buka browser dan akses:
   ```
   https://www.mongodb.com/try/download/community
   ```

2. Pilih:
   - **Version**: 7.0.x (Latest)
   - **Platform**: Windows
   - **Package**: MSI

3. Klik **Download**

## 💿 Step 2: Install MongoDB

1. **Jalankan installer** yang sudah di-download (file .msi)

2. **Setup Type**: Pilih **Complete**

3. **Service Configuration**:
   - ✅ Install MongoDB as a Service
   - ✅ Run service as Network Service user
   - Service Name: `MongoDB`
   - Data Directory: `C:\Program Files\MongoDB\Server\7.0\data\`
   - Log Directory: `C:\Program Files\MongoDB\Server\7.0\log\`

4. **Install MongoDB Compass**: 
   - ✅ Centang "Install MongoDB Compass" (GUI tool)

5. Klik **Next** → **Install**

6. Tunggu instalasi selesai (2-3 menit)

7. Klik **Finish**

## ✅ Step 3: Verify Installation

### Cara 1: Via Command Prompt
```cmd
mongod --version
```

Output seharusnya:
```
db version v7.0.x
```

### Cara 2: Via Services
1. Tekan `Win + R`
2. Ketik `services.msc`
3. Enter
4. Cari "MongoDB" di list
5. Status harus: **Running**

### Cara 3: Via MongoDB Compass
1. Buka MongoDB Compass (sudah terinstall)
2. Connection string: `mongodb://localhost:27017`
3. Klik **Connect**
4. Jika berhasil, akan muncul list databases

## 🚀 Step 4: Setup BabyShop3Berlian

### A. Install Package
```cmd
cd C:\BabyShopWeb2
dotnet restore
```

### B. Jalankan Aplikasi
```cmd
dotnet run
```

### C. Seed Data MongoDB
Buka browser:
```
http://localhost:5055/MongoAdmin/SeedMongoDB
```

### D. Verify Data
```
http://localhost:5055/MongoAdmin/CheckMongoDB
```

## 🎯 Selesai!

MongoDB sudah terinstall dan berjalan!

### Cek Data di MongoDB Compass:
1. Buka MongoDB Compass
2. Connect ke `mongodb://localhost:27017`
3. Pilih database: **BabyShop3Berlian**
4. Lihat collections:
   - products
   - categories
   - orders
   - adminUsers

## 📊 Default Configuration

- **Host**: localhost
- **Port**: 27017
- **Connection String**: `mongodb://localhost:27017`
- **Database Name**: BabyShop3Berlian
- **Service**: Auto-start dengan Windows

## 🔧 Troubleshooting

### MongoDB Service Tidak Jalan

**Cara 1: Via Services**
1. Buka `services.msc`
2. Cari "MongoDB"
3. Klik kanan → Start

**Cara 2: Via Command Prompt (Admin)**
```cmd
net start MongoDB
```

### Port 27017 Sudah Dipakai

Cek aplikasi yang pakai port 27017:
```cmd
netstat -ano | findstr :27017
```

Kill process jika perlu:
```cmd
taskkill /PID [process_id] /F
```

### MongoDB Compass Tidak Bisa Connect

1. Pastikan MongoDB service running
2. Cek firewall tidak block port 27017
3. Restart MongoDB service
4. Restart komputer

## 💡 Tips

### Start/Stop MongoDB Service

**Stop:**
```cmd
net stop MongoDB
```

**Start:**
```cmd
net start MongoDB
```

**Restart:**
```cmd
net stop MongoDB && net start MongoDB
```

### Uninstall MongoDB

1. Stop service: `net stop MongoDB`
2. Uninstall via Control Panel
3. Delete folder: `C:\Program Files\MongoDB`
4. Delete data: `C:\data\db` (jika ada)

## 🌐 MongoDB Atlas (Cloud Alternative)

Jika tidak ingin install local, bisa pakai MongoDB Atlas (cloud):

1. Daftar gratis: https://www.mongodb.com/cloud/atlas/register
2. Buat cluster (Free tier 512MB)
3. Setup user & network access
4. Get connection string
5. Update `appsettings.json`

Lihat: `MONGODB_QUICK_START.md` untuk panduan Atlas.

## 📖 Resources

- MongoDB Docs: https://docs.mongodb.com/
- MongoDB Compass: https://www.mongodb.com/products/compass
- MongoDB University: https://university.mongodb.com/ (Free courses)

---

## ✅ Checklist

- [ ] Download MongoDB Community Server
- [ ] Install dengan Complete setup
- [ ] Install MongoDB Compass
- [ ] Verify service running
- [ ] Test connection di Compass
- [ ] Run `dotnet restore`
- [ ] Run `dotnet run`
- [ ] Seed data: `/MongoAdmin/SeedMongoDB`
- [ ] Verify: `/MongoAdmin/CheckMongoDB`
- [ ] Check data di Compass

**Selamat! MongoDB siap digunakan! 🎉**
