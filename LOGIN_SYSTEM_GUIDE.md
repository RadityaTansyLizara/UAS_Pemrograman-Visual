# 🔐 Sistem Login Admin - BabyShop3Berlian

## ✅ Fitur yang Sudah Diimplementasikan

### 1. **Halaman Login**
- URL: `http://localhost:5055/Auth/Login`
- Desain cute dengan tema baby shop
- Form username & password
- Validasi input
- Error message jika login gagal

### 2. **Session Management**
- Session timeout: 2 jam
- Auto redirect ke login jika belum login
- Session menyimpan: Username, Full Name, User ID

### 3. **Proteksi Halaman Admin**
- Semua halaman admin dilindungi
- Redirect otomatis ke login jika belum login
- Tombol logout di navbar admin

### 4. **Password Security**
- Password di-hash menggunakan SHA256
- Tidak disimpan dalam bentuk plain text
- Aman dari SQL injection

---

## 🔑 Kredensial Login Default

### Admin Utama:
```
Username: admin
Password: admin123
```

---

## 📋 Cara Menggunakan

### **Login:**
1. Buka browser: `http://localhost:5055/Auth/Login`
2. Masukkan username: `admin`
3. Masukkan password: `admin123`
4. Klik tombol "Login"
5. Akan redirect ke Dashboard Admin

### **Logout:**
1. Klik tombol "Logout" di navbar admin (kanan atas)
2. Session akan dihapus
3. Redirect ke halaman login

### **Akses Admin Tanpa Login:**
- Jika coba akses `/Admin` tanpa login
- Otomatis redirect ke `/Auth/Login`

---

## 🎨 Tampilan Login

### Desain:
- ✅ Background gradient pastel animasi
- ✅ Card login dengan shadow
- ✅ Logo baby icon dengan animasi bounce
- ✅ Form input dengan border pink
- ✅ Tombol login gradient pink
- ✅ Emoji dekorasi (🍼 dan 🧸)
- ✅ Link kembali ke beranda

### Responsive:
- ✅ Mobile friendly
- ✅ Tablet friendly
- ✅ Desktop optimized

---

## 🔧 Cara Menambah User Baru

### Opsi 1: Manual via Database
```sql
-- Buka babyshop.db dengan SQLite browser
-- Jalankan query:
INSERT INTO AdminUsers (Username, PasswordHash, FullName, CreatedAt)
VALUES ('username_baru', 'hash_password', 'Nama Lengkap', datetime('now'));
```

### Opsi 2: Buat Halaman Register (Future)
- Bisa ditambahkan halaman `/Auth/Register`
- Hanya admin yang bisa register user baru
- Form input: username, password, full name

---

## 🛡️ Keamanan

### Yang Sudah Diimplementasikan:
✅ Password hashing (SHA256)
✅ Session management
✅ HttpOnly cookies
✅ Auto logout setelah 2 jam
✅ Redirect protection
✅ SQL injection prevention (Entity Framework)

### Rekomendasi untuk Production:
⚠️ Gunakan password yang lebih kuat
⚠️ Tambahkan CAPTCHA untuk prevent brute force
⚠️ Implementasikan rate limiting
⚠️ Gunakan HTTPS
⚠️ Tambahkan 2FA (Two-Factor Authentication)
⚠️ Log semua aktivitas login

---

## 📁 File yang Ditambahkan

### Models:
- `Models/AdminUser.cs` - Model user admin
- `Models/LoginViewModel.cs` - ViewModel untuk form login

### Controllers:
- `Controllers/AuthController.cs` - Handle login/logout

### Views:
- `Views/Auth/Login.cshtml` - Halaman login

### CSS:
- `wwwroot/css/login.css` - Styling halaman login

### Filters:
- `Filters/AdminAuthFilter.cs` - Middleware auth (optional)

### Database:
- Table `AdminUsers` ditambahkan ke database

---

## 🧪 Testing

### Test Login Berhasil:
1. Buka `/Auth/Login`
2. Input: admin / admin123
3. Klik Login
4. ✅ Harus redirect ke `/Admin`
5. ✅ Harus bisa akses semua halaman admin

### Test Login Gagal:
1. Buka `/Auth/Login`
2. Input: admin / wrongpassword
3. Klik Login
4. ✅ Harus muncul error "Username atau password salah"
5. ✅ Tetap di halaman login

### Test Proteksi:
1. Logout dulu
2. Coba akses `/Admin` langsung
3. ✅ Harus redirect ke `/Auth/Login`

### Test Logout:
1. Login dulu
2. Klik tombol Logout
3. ✅ Harus redirect ke `/Auth/Login`
4. ✅ Coba akses `/Admin` lagi, harus redirect ke login

---

## 🔄 Cara Ganti Password

### Untuk ganti password admin:

1. **Generate hash password baru:**
```csharp
using System.Security.Cryptography;
using System.Text;

string password = "password_baru";
using (var sha256 = SHA256.Create())
{
    var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    string hash = Convert.ToBase64String(bytes);
    Console.WriteLine(hash);
}
```

2. **Update di database:**
```sql
UPDATE AdminUsers 
SET PasswordHash = 'hash_dari_step_1' 
WHERE Username = 'admin';
```

---

## 💡 Tips

1. **Jangan lupa password default!**
   - Username: `admin`
   - Password: `admin123`

2. **Ganti password setelah deployment**
   - Password default hanya untuk development

3. **Backup database secara berkala**
   - File: `babyshop.db`

4. **Session timeout bisa diubah**
   - Edit di `Program.cs`
   - Default: 2 jam

---

## 🎉 Selesai!

Sistem login sudah siap digunakan. Semua halaman admin sekarang terlindungi dan hanya bisa diakses setelah login.

**Tidak ada perubahan pada tampilan yang sudah ada!** ✅
- Semua halaman customer tetap sama
- Semua halaman admin tetap sama
- Hanya tambah halaman login dan tombol logout
