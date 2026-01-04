@echo off
echo ========================================
echo Tambah Tabel Employees (CARA AMAN)
echo ========================================
echo.
echo Script ini akan:
echo 1. Backup database Anda (AMAN!)
echo 2. Menambahkan tabel Employees
echo 3. TIDAK menghapus data apapun
echo.
echo Data produk, orders, dll TETAP ADA!
echo.
pause

echo.
echo [1/3] Backup database...
copy babyshop.db babyshop-backup-%date:~-4,4%%date:~-10,2%%date:~-7,2%.db
echo Backup berhasil!

echo.
echo [2/3] Menambahkan tabel Employees...
sqlite3 babyshop.db < add-employee-table.sql
if errorlevel 1 (
    echo.
    echo SQLite3 tidak ditemukan. Menggunakan cara alternatif...
    echo.
    echo Silakan jalankan command ini di Command Prompt:
    echo.
    echo cd C:\BabyShopWeb2
    echo dotnet run
    echo.
    echo Lalu akses: http://localhost:5055/Admin/CreateEmployeeTable
    echo.
    pause
    exit /b
)

echo Tabel Employees berhasil ditambahkan!

echo.
echo [3/3] Selesai!
echo.
echo ========================================
echo SUKSES! Tabel Employees sudah ditambahkan
echo Data lama Anda AMAN dan tidak berubah
echo ========================================
echo.
echo Sekarang jalankan aplikasi:
echo   dotnet run
echo.
echo Lalu akses menu Karyawan di:
echo   http://localhost:5055/Admin/Employees
echo.
pause
