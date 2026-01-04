# 🚀 Quick Access Guide - Port 5055

## 📍 Your Application Port: **5055**

## 🔗 Quick Links

### Main Pages
- 🏠 **Homepage**: http://localhost:5055
- 🛍️ **Products**: http://localhost:5055/Product
- 🛒 **Cart**: http://localhost:5055/Cart
- 💳 **Checkout**: http://localhost:5055/Order/Checkout

### Admin Pages
- 📊 **Admin Dashboard**: http://localhost:5055/Admin
- 📦 **Manage Products**: http://localhost:5055/Admin/Products
- 📋 **Manage Orders**: http://localhost:5055/Admin/Orders
- ➕ **Add Product**: http://localhost:5055/Admin/CreateProduct

### Financial Pages
- 💰 **Financial Dashboard**: http://localhost:5055/Financial
- 📈 **Monthly Report**: http://localhost:5055/Financial/MonthlyReport
- ➕ **Add Transaction**: http://localhost:5055/Financial/CreateTransaction

## 🧪 Test Features

### Calendar Feature (Admin Dashboard)
```
http://localhost:5055/Admin
```
- Klik card tanggal di "Statistik Bisnis"
- Pilih tanggal untuk filter data

### Calendar with Date Filter
```
http://localhost:5055/Admin?date=2025-12-25&month=12&year=2025
```

### Cute Hero Section
```
http://localhost:5055
```
- Lihat hero section dengan ilustrasi bayi kartun
- Animasi dan decorations

## 🚀 Start Application

```bash
# Open terminal in project directory
cd C:\BabyShopWeb2

# Run application
dotnet run

# Wait for message:
# Now listening on: http://localhost:5055

# Open browser
start http://localhost:5055
```

## 🔍 Verify Application Running

### Method 1: Browser
```
Open: http://localhost:5055
Should show: BabyShop3Berlian homepage
```

### Method 2: Command Line
```bash
# Check if port 5055 is in use
netstat -ano | findstr :5055
```

### Method 3: PowerShell
```powershell
# Test connection
Test-NetConnection -ComputerName localhost -Port 5055
```

## 📱 Access from Mobile

If testing on mobile device (same network):

1. Find your computer's IP address:
```bash
ipconfig
# Look for IPv4 Address (e.g., 192.168.1.100)
```

2. Access from mobile:
```
http://192.168.1.100:5055
```

## ⚡ Quick Commands

### Start Application
```bash
dotnet run
```

### Build Application
```bash
dotnet build
```

### Clean Build
```bash
dotnet clean
dotnet build
```

### Restore Packages
```bash
dotnet restore
```

## 🎯 Common Tasks

### View Homepage
```
http://localhost:5055
```

### Access Admin Dashboard
```
http://localhost:5055/Admin
```

### Test Calendar Feature
```
1. Go to: http://localhost:5055/Admin
2. Scroll to "Statistik Bisnis"
3. Click date card (with 📅 icon)
4. Select date from calendar
```

### View Financial Reports
```
http://localhost:5055/Financial
```

### Add New Product
```
http://localhost:5055/Admin/CreateProduct
```

### View All Orders
```
http://localhost:5055/Admin/Orders
```

## 🔧 Troubleshooting

### Port Already in Use
```bash
# Find process using port 5055
netstat -ano | findstr :5055

# Kill process (replace PID with actual process ID)
taskkill /PID <PID> /F
```

### Application Not Starting
```bash
# Check for errors
dotnet run --verbosity detailed

# Or check logs
# Look in bin/Debug/net8.0/ folder
```

### Cannot Access from Browser
1. Check if application is running
2. Verify port 5055 is correct
3. Try hard refresh (Ctrl + F5)
4. Clear browser cache
5. Try different browser

## 📋 Checklist

Before testing features:
- [ ] Application running on port 5055
- [ ] Browser open
- [ ] Navigate to correct URL (with 5055)
- [ ] Hard refresh if needed (Ctrl + F5)

## 💡 Tips

1. **Bookmark URLs**: Save frequently used URLs with port 5055
2. **Use Ctrl + F5**: Hard refresh to see latest changes
3. **Check Console**: F12 → Console for JavaScript errors
4. **Check Network**: F12 → Network to verify requests
5. **Use Incognito**: Test without cache interference

## 🎨 Feature Testing

### Hero Section (Cute Baby Illustration)
```
URL: http://localhost:5055
Location: Top of homepage
Features: SVG baby, animations, floating toys
```

### Calendar Feature (Admin Dashboard)
```
URL: http://localhost:5055/Admin
Location: Statistik Bisnis section
Features: Date picker, historical data, filter
```

### Financial Dashboard
```
URL: http://localhost:5055/Financial
Features: Daily/monthly reports, charts, transactions
```

## 📞 Support

If you encounter issues:
1. Check PORT_CONFIGURATION.md
2. Verify application running on 5055
3. Check browser console for errors
4. Restart application if needed

---

**Port**: 5055  
**Protocol**: HTTP  
**Host**: localhost  
**Full URL**: http://localhost:5055  
**Status**: ✅ ACTIVE
