# 🔧 Port 5055 Configuration - SELESAI

## ✅ Status: DIKONFIGURASI

Aplikasi sekarang akan selalu berjalan di **localhost:5055**

## 🎯 Yang Dilakukan

### 1. Update `Properties/launchSettings.json`
- ✅ Set default profile ke port 5055
- ✅ Hapus HTTPS profile (hanya HTTP)
- ✅ Update IIS Express ke port 5055
- ✅ Disable SSL port

### 2. Update `Program.cs`
- ✅ Tambahkan konfigurasi Kestrel
- ✅ Force listen di localhost:5055
- ✅ Memastikan port tetap 5055 walaupun ada konfigurasi lain

## 🚀 Cara Menjalankan

### Opsi 1: Menggunakan dotnet run
```bash
dotnet run
```

### Opsi 2: Menggunakan Visual Studio
- Tekan F5 atau klik "Start"
- Aplikasi akan otomatis buka di http://localhost:5055

### Opsi 3: Menggunakan dotnet watch (auto-reload)
```bash
dotnet watch run
```

## 🌐 URL Aplikasi

Setelah aplikasi berjalan, akses di:
- **URL Utama**: http://localhost:5055
- **Admin**: http://localhost:5055/Admin
- **Produk**: http://localhost:5055/Product
- **Keranjang**: http://localhost:5055/Cart
- **Keuangan**: http://localhost:5055/Financial

## 📝 File yang Diubah

1. ✅ `Properties/launchSettings.json` - Konfigurasi port
2. ✅ `Program.cs` - Force Kestrel ke port 5055

## ⚙️ Konfigurasi Detail

### launchSettings.json
```json
{
  "profiles": {
    "BabyShopWeb2": {
      "applicationUrl": "http://localhost:5055"
    }
  }
}
```

### Program.cs
```csharp
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5055);
});
```

## 🔍 Troubleshooting

### Jika port 5055 sudah digunakan:
```bash
# Windows - Cek proses yang menggunakan port 5055
netstat -ano | findstr :5055

# Kill proses (ganti PID dengan nomor yang muncul)
taskkill /PID <PID> /F
```

### Jika aplikasi tidak buka otomatis:
- Buka browser manual
- Ketik: http://localhost:5055

## 🎉 Hasil

Sekarang aplikasi akan **SELALU** berjalan di:
```
http://localhost:5055
```

Tidak akan berubah ke port lain! 🎯
