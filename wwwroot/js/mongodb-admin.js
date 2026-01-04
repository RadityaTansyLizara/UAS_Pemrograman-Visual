// MongoDB Admin JavaScript

// Check MongoDB on page load
document.addEventListener('DOMContentLoaded', function() {
    checkMongoDB();
});

// Check MongoDB Connection
async function checkMongoDB() {
    try {
        const response = await fetch('/MongoAdmin/CheckMongoDB');
        const text = await response.text();
        
        // Parse the text response
        const lines = text.split('\n');
        let productsCount = 0;
        let categoriesCount = 0;
        let ordersCount = 0;
        let adminsCount = 0;
        let sampleProducts = [];
        
        lines.forEach(line => {
            if (line.includes('Products:')) {
                productsCount = parseInt(line.match(/\d+/)[0]);
            } else if (line.includes('Categories:')) {
                categoriesCount = parseInt(line.match(/\d+/)[0]);
            } else if (line.includes('Orders:')) {
                ordersCount = parseInt(line.match(/\d+/)[0]);
            } else if (line.includes('Admin Users:')) {
                adminsCount = parseInt(line.match(/\d+/)[0]);
            }
        });
        
        // Update UI
        document.getElementById('connectionStatus').innerHTML = `
            <div class="status-success">
                <i class="fas fa-check-circle"></i>
                MongoDB Connection: OK
            </div>
        `;
        
        document.getElementById('productsCount').textContent = productsCount;
        document.getElementById('categoriesCount').textContent = categoriesCount;
        document.getElementById('ordersCount').textContent = ordersCount;
        document.getElementById('adminsCount').textContent = adminsCount;
        
        // Show sections
        document.getElementById('statsGrid').style.display = 'grid';
        document.getElementById('actionsSection').style.display = 'block';
        document.getElementById('databaseInfo').style.display = 'block';
        
        // Show sample products if any
        if (productsCount > 0) {
            document.getElementById('sampleProducts').style.display = 'block';
            // Parse sample products from response
            // This is simplified - you might want to enhance this
        }
        
    } catch (error) {
        document.getElementById('connectionStatus').innerHTML = `
            <div class="status-error">
                <i class="fas fa-times-circle"></i>
                MongoDB Connection Failed: ${error.message}
            </div>
        `;
    }
}

// Seed MongoDB
async function seedMongoDB() {
    if (!confirm('Seed data awal ke MongoDB?\n\nIni akan menambahkan:\n- 4 Categories\n- 15 Products\n- 1 Admin User')) {
        return;
    }
    
    showLoading('Seeding MongoDB...');
    
    try {
        const response = await fetch('/MongoAdmin/SeedMongoDB');
        const text = await response.text();
        
        hideLoading();
        
        if (text.includes('✅')) {
            showResult('success', 'Seed Berhasil!', text);
            setTimeout(() => checkMongoDB(), 2000);
        } else {
            showResult('error', 'Seed Gagal', text);
        }
    } catch (error) {
        hideLoading();
        showResult('error', 'Error', error.message);
    }
}

// Migrate from SQLite to MongoDB
async function migrateSQLiteToMongoDB() {
    if (!confirm('Migrate data dari SQLite ke MongoDB?\n\nIni akan copy semua data:\n- Products\n- Categories\n- Orders\n- Admin Users\n\nData SQLite tidak akan dihapus.')) {
        return;
    }
    
    showLoading('Migrating data from SQLite to MongoDB...');
    
    try {
        const response = await fetch('/MongoAdmin/MigrateSQLiteToMongoDB');
        const text = await response.text();
        
        hideLoading();
        
        if (text.includes('✅') || text.includes('Completed')) {
            showResult('success', 'Migration Berhasil!', text);
            setTimeout(() => checkMongoDB(), 2000);
        } else {
            showResult('error', 'Migration Gagal', text);
        }
    } catch (error) {
        hideLoading();
        showResult('error', 'Error', error.message);
    }
}

// Clear MongoDB
async function clearMongoDB() {
    if (!confirm('⚠️ PERINGATAN!\n\nHapus SEMUA data di MongoDB?\n\nIni akan menghapus:\n- Semua Products\n- Semua Categories\n- Semua Orders\n- Semua Admin Users\n\nTindakan ini TIDAK BISA dibatalkan!')) {
        return;
    }
    
    // Double confirmation
    if (!confirm('Apakah Anda YAKIN ingin menghapus semua data MongoDB?')) {
        return;
    }
    
    showLoading('Clearing MongoDB...');
    
    try {
        const response = await fetch('/MongoAdmin/ClearMongoDB');
        const text = await response.text();
        
        hideLoading();
        
        if (text.includes('✅')) {
            showResult('success', 'Clear Berhasil!', text);
            setTimeout(() => checkMongoDB(), 2000);
        } else {
            showResult('error', 'Clear Gagal', text);
        }
    } catch (error) {
        hideLoading();
        showResult('error', 'Error', error.message);
    }
}

// Show Loading Modal
function showLoading(message) {
    document.getElementById('loadingText').textContent = message;
    document.getElementById('loadingModal').style.display = 'flex';
}

// Hide Loading Modal
function hideLoading() {
    document.getElementById('loadingModal').style.display = 'none';
}

// Show Result Modal
function showResult(type, title, message) {
    const icon = type === 'success' 
        ? '<i class="fas fa-check-circle" style="color: #10b981;"></i>'
        : '<i class="fas fa-times-circle" style="color: #ef4444;"></i>';
    
    document.getElementById('resultIcon').innerHTML = icon;
    document.getElementById('resultTitle').textContent = title;
    document.getElementById('resultMessage').textContent = message;
    document.getElementById('resultModal').style.display = 'flex';
}

// Close Result Modal
function closeResultModal() {
    document.getElementById('resultModal').style.display = 'none';
}

// Close modal when clicking outside
window.onclick = function(event) {
    const resultModal = document.getElementById('resultModal');
    if (event.target === resultModal) {
        closeResultModal();
    }
}
