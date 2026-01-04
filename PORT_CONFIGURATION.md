# ⚙️ Port Configuration - localhost:5055

## 🎯 Important: Correct Port

**Your application runs on**: `localhost:5055`  
**NOT**: `localhost:5000`

## 📝 Note About Documentation

Beberapa file dokumentasi mungkin masih menyebutkan `localhost:5000`. Ini adalah kesalahan dokumentasi. **Selalu gunakan `localhost:5055`** untuk mengakses aplikasi Anda.

## ✅ Correct URLs

### Homepage
```
http://localhost:5055
```

### Admin Dashboard
```
http://localhost:5055/Admin
```

### Financial Dashboard
```
http://localhost:5055/Financial
```

### Products
```
http://localhost:5055/Product
```

### Cart
```
http://localhost:5055/Cart
```

### Checkout
```
http://localhost:5055/Order/Checkout
```

## 🔧 Configuration Files

### appsettings.json
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5055"
      }
    }
  }
}
```

### launchSettings.json (Properties folder)
```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5055",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## 🧪 Testing URLs

### Calendar Feature
```
http://localhost:5055/Admin
http://localhost:5055/Admin?date=2025-12-25&month=12&year=2025
```

### Financial Reports
```
http://localhost:5055/Financial
http://localhost:5055/Financial/MonthlyReport?month=12&year=2025
```

### Order Management
```
http://localhost:5055/Admin/Orders
http://localhost:5055/Admin/Products
```

## 🚀 How to Start Application

```bash
# Navigate to project directory
cd C:\BabyShopWeb2

# Run application
dotnet run

# Application will start on:
# http://localhost:5055
```

## 🌐 Browser Access

After starting the application, open your browser and navigate to:

```
http://localhost:5055
```

## 📱 Mobile Testing

If testing on mobile device on same network:

```
http://YOUR_IP_ADDRESS:5055
```

Replace `YOUR_IP_ADDRESS` with your computer's local IP address (e.g., `192.168.1.100`)

## 🔍 Verify Port

To check if application is running on correct port:

```bash
# Check running processes
netstat -ano | findstr :5055

# Or check in browser
# Navigate to: http://localhost:5055
# Should show homepage
```

## ⚠️ Common Mistakes

### ❌ Wrong:
```
http://localhost:5000
http://localhost:5000/Admin
```

### ✅ Correct:
```
http://localhost:5055
http://localhost:5055/Admin
```

## 📋 Quick Reference

| Service | URL |
|---------|-----|
| Homepage | `http://localhost:5055` |
| Admin | `http://localhost:5055/Admin` |
| Financial | `http://localhost:5055/Financial` |
| Products | `http://localhost:5055/Product` |
| Cart | `http://localhost:5055/Cart` |
| Orders | `http://localhost:5055/Admin/Orders` |

## 🔄 Port Change History

- **Previous**: `localhost:5000` (incorrect in some docs)
- **Current**: `localhost:5055` (correct)
- **Status**: ✅ Active

## 📝 Documentation Update Status

Files that may still reference port 5000 (use 5055 instead):
- ⚠️ TEST_CUTE_HERO.md
- ⚠️ TEST_CALENDAR_NOW.md
- ⚠️ test_calendar_feature.html
- ⚠️ QUICK_REFERENCE.md
- ⚠️ IMPLEMENTATION_SUMMARY.md
- ⚠️ CALENDAR_FEATURE_COMPLETED.md
- ⚠️ CALENDAR_ARCHITECTURE.md
- ⚠️ CALENDAR_FIX_APPLIED.md
- ⚠️ CUTE_HERO_SECTION_COMPLETED.md
- ⚠️ ADDRESS_UPDATED.md
- ⚠️ DEPLOYMENT.md

**Action**: When following instructions in these files, mentally replace `5000` with `5055`.

## ✅ Correct Configuration

Your application is correctly configured to run on port **5055**.

All URLs should use: `http://localhost:5055`

---

**Configuration**: localhost:5055  
**Status**: ✅ ACTIVE  
**Last Updated**: December 26, 2025
