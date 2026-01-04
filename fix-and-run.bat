@echo off
echo ========================================
echo Fix Employee Table Error
echo ========================================
echo.
echo Langkah yang akan dilakukan:
echo 1. Stop aplikasi yang sedang berjalan
echo 2. Hapus database lama (babyshop.db)
echo 3. Jalankan aplikasi dengan database baru
echo.
echo CATATAN: Gambar produk Anda AMAN di folder wwwroot/images/products/
echo.
pause

echo.
echo [1/3] Mencoba stop aplikasi yang sedang berjalan...
taskkill /F /IM BabyShopWeb2.exe 2>nul
taskkill /F /IM dotnet.exe /FI "WINDOWTITLE eq *BabyShopWeb2*" 2>nul
timeout /t 2 /nobreak >nul
echo Done!

echo.
echo [2/3] Menghapus database lama...
if exist babyshop.db (
    del /F babyshop.db
    echo Database lama berhasil dihapus!
) else (
    echo Database tidak ditemukan (mungkin sudah dihapus)
)

echo.
echo [3/3] Menjalankan aplikasi...
echo Database baru akan dibuat dengan tabel Employees
echo.
echo ========================================
echo Aplikasi akan berjalan di: http://localhost:5055
echo Login: admin / admin123
echo ========================================
echo.

dotnet run
