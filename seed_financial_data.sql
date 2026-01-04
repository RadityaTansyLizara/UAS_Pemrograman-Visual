-- Seed Financial Transactions for Testing Charts
-- Run this in SQLite browser or via command line

-- Income Transactions (Type = 0)
-- Sales (Category = 0)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-01 10:30:00', 0, 0, 'Penjualan Produk - Order #ORD001', 450000, 'Penjualan 3 produk bayi', NULL, 'System', '2024-12-01 10:30:00', 0),
('2024-12-03 14:20:00', 0, 0, 'Penjualan Produk - Order #ORD002', 320000, 'Penjualan mainan edukatif', NULL, 'System', '2024-12-03 14:20:00', 0),
('2024-12-05 09:15:00', 0, 0, 'Penjualan Produk - Order #ORD003', 580000, 'Penjualan perlengkapan makan', NULL, 'System', '2024-12-05 09:15:00', 0),
('2024-12-08 16:45:00', 0, 0, 'Penjualan Produk - Order #ORD004', 275000, 'Penjualan produk perawatan', NULL, 'System', '2024-12-08 16:45:00', 0),
('2024-12-10 11:30:00', 0, 0, 'Penjualan Produk - Order #ORD005', 650000, 'Penjualan pakaian bayi', NULL, 'System', '2024-12-10 11:30:00', 0),
('2024-12-12 13:20:00', 0, 0, 'Penjualan Produk - Order #ORD006', 420000, 'Penjualan berbagai produk', NULL, 'System', '2024-12-12 13:20:00', 0),
('2024-12-15 10:00:00', 0, 0, 'Penjualan Produk - Order #ORD007', 380000, 'Penjualan mainan', NULL, 'System', '2024-12-15 10:00:00', 0),
('2024-12-18 15:30:00', 0, 0, 'Penjualan Produk - Order #ORD008', 520000, 'Penjualan produk premium', NULL, 'System', '2024-12-18 15:30:00', 0),
('2024-12-20 09:45:00', 0, 0, 'Penjualan Produk - Order #ORD009', 290000, 'Penjualan perawatan bayi', NULL, 'System', '2024-12-20 09:45:00', 0),
('2024-12-22 14:15:00', 0, 0, 'Penjualan Produk - Order #ORD010', 480000, 'Penjualan paket lengkap', NULL, 'System', '2024-12-22 14:15:00', 0),
('2024-12-25 11:00:00', 0, 0, 'Penjualan Produk - Order #ORD011', 720000, 'Penjualan natal', NULL, 'System', '2024-12-25 11:00:00', 0),
('2024-12-27 10:30:00', 0, 0, 'Penjualan Produk - Order #ORD012', 350000, 'Penjualan hari ini', NULL, 'System', '2024-12-27 10:30:00', 0);

-- Other Income (Category = 1)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-05 15:00:00', 0, 1, 'Pendapatan Lain-lain', 150000, 'Bonus dari supplier', NULL, 'Admin', '2024-12-05 15:00:00', 0),
('2024-12-15 16:00:00', 0, 1, 'Pendapatan Konsultasi', 200000, 'Konsultasi produk bayi', NULL, 'Admin', '2024-12-15 16:00:00', 0);

-- Expense Transactions (Type = 1)
-- Product Purchase (Category = 2)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-02 09:00:00', 1, 2, 'Pembelian Stok Produk', 1500000, 'Restock produk dari supplier', NULL, 'Admin', '2024-12-02 09:00:00', 0),
('2024-12-10 10:00:00', 1, 2, 'Pembelian Produk Baru', 800000, 'Produk mainan edukatif baru', NULL, 'Admin', '2024-12-10 10:00:00', 0),
('2024-12-20 09:00:00', 1, 2, 'Restock Produk Laris', 1200000, 'Restock produk best seller', NULL, 'Admin', '2024-12-20 09:00:00', 0);

-- Operational Cost (Category = 3)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-01 08:00:00', 1, 3, 'Biaya Operasional', 250000, 'Biaya kebersihan dan maintenance', NULL, 'Admin', '2024-12-01 08:00:00', 0),
('2024-12-15 08:00:00', 1, 3, 'Biaya Operasional', 180000, 'Supplies kantor', NULL, 'Admin', '2024-12-15 08:00:00', 0);

-- Salary (Category = 4)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-01 17:00:00', 1, 4, 'Gaji Karyawan', 3000000, 'Gaji bulan Desember', NULL, 'Admin', '2024-12-01 17:00:00', 0);

-- Rent (Category = 5)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-01 09:00:00', 1, 5, 'Sewa Toko', 2000000, 'Sewa toko bulan Desember', NULL, 'Admin', '2024-12-01 09:00:00', 0);

-- Utilities (Category = 6)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-05 10:00:00', 1, 6, 'Listrik & Air', 350000, 'Tagihan listrik dan air', NULL, 'Admin', '2024-12-05 10:00:00', 0),
('2024-12-10 10:00:00', 1, 6, 'Internet & Telepon', 200000, 'Tagihan internet dan telepon', NULL, 'Admin', '2024-12-10 10:00:00', 0);

-- Marketing (Category = 7)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-03 11:00:00', 1, 7, 'Iklan Facebook', 300000, 'Biaya iklan Facebook Ads', NULL, 'Admin', '2024-12-03 11:00:00', 0),
('2024-12-12 11:00:00', 1, 7, 'Promosi Instagram', 250000, 'Instagram sponsored posts', NULL, 'Admin', '2024-12-12 11:00:00', 0),
('2024-12-18 11:00:00', 1, 7, 'Brosur & Banner', 150000, 'Cetak brosur promosi', NULL, 'Admin', '2024-12-18 11:00:00', 0);

-- Other Expense (Category = 8)
INSERT INTO FinancialTransactions (TransactionDate, Type, Category, Description, Amount, Notes, OrderId, CreatedBy, CreatedAt, IsAutomatic)
VALUES 
('2024-12-08 14:00:00', 1, 8, 'Pengeluaran Lain-lain', 100000, 'Biaya tak terduga', NULL, 'Admin', '2024-12-08 14:00:00', 0);

-- Summary:
-- Total Income: Rp 5,935,000 (Sales: 5,585,000 + Other: 350,000)
-- Total Expense: Rp 8,280,000
-- Net Balance: -Rp 2,345,000 (Normal untuk bulan pertama dengan investasi besar)
