@echo off
echo ========================================
echo   RESTART APLIKASI DAN BUKA KEUANGAN
echo ========================================
echo.
echo Membunuh proses aplikasi yang sedang berjalan...
taskkill /F /IM BabyShopWeb2.exe 2>nul
timeout /t 2 /nobreak >nul

echo.
echo Menjalankan aplikasi...
start cmd /k "cd /d %~dp0 && dotnet run"

echo.
echo Menunggu aplikasi siap (10 detik)...
timeout /t 10 /nobreak

echo.
echo Membuka browser Incognito dengan halaman Keuangan...
start chrome --incognito http://localhost:5055/Admin/Financial

echo.
echo ========================================
echo   SELESAI!
echo ========================================
echo.
echo Halaman Keuangan akan terbuka di browser Incognito.
echo.
echo Jika Chrome tidak terbuka otomatis, buka manual:
echo http://localhost:5055/Admin/Financial
echo.
echo Tekan tombol apa saja untuk menutup...
pause >nul
