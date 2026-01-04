-- Script untuk menambahkan user admin default
-- Username: admin
-- Password: admin123

-- Hash dari password "admin123" menggunakan SHA256
-- jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=

INSERT INTO AdminUsers (Username, PasswordHash, FullName, CreatedAt)
VALUES ('admin', 'jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=', 'Administrator', datetime('now'));

-- Untuk menambahkan user lain, gunakan format yang sama
-- Contoh user kedua:
-- Username: kasir
-- Password: kasir123
-- INSERT INTO AdminUsers (Username, PasswordHash, FullName, CreatedAt)
-- VALUES ('kasir', 'hash_password_kasir123', 'Kasir BabyShop', datetime('now'));
