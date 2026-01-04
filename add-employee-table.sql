-- Script untuk menambahkan tabel Employees ke database yang sudah ada
-- AMAN: Tidak menghapus data apapun!

-- Cek apakah tabel sudah ada, jika belum buat tabel baru
CREATE TABLE IF NOT EXISTS Employees (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeId TEXT NOT NULL UNIQUE,
    FullName TEXT NOT NULL,
    Position INTEGER NOT NULL,
    PhoneNumber TEXT NOT NULL,
    Email TEXT,
    Address TEXT NOT NULL,
    JoinDate TEXT NOT NULL,
    Status INTEGER NOT NULL,
    Shift INTEGER NOT NULL,
    Salary REAL,
    Notes TEXT,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT
);

-- Buat index untuk performa lebih baik
CREATE INDEX IF NOT EXISTS IX_Employees_EmployeeId ON Employees(EmployeeId);
CREATE INDEX IF NOT EXISTS IX_Employees_Status ON Employees(Status);

-- Selesai! Tabel Employees berhasil ditambahkan tanpa menghapus data lain
