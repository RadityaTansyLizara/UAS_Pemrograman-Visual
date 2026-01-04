using Microsoft.AspNetCore.Mvc;
using BabyShopWeb2.Models;
using BabyShopWeb2.Services;

namespace BabyShopWeb2.Controllers
{
    public class FinancialController : Controller
    {
        private readonly IFinancialService _financialService;
        
        public FinancialController(IFinancialService financialService)
        {
            _financialService = financialService;
        }
        
        public async Task<IActionResult> Index(DateTime? date, int? month, int? year)
        {
            var selectedDate = date ?? DateTime.Today;
            var selectedMonth = month ?? DateTime.Today.Month;
            var selectedYear = year ?? DateTime.Today.Year;
            
            var viewModel = await _financialService.GetDashboardDataAsync(selectedDate, selectedMonth, selectedYear);
            
            return View(viewModel);
        }
        
        [HttpGet]
        public IActionResult CreateTransaction()
        {
            var model = new CreateTransactionViewModel
            {
                TransactionDate = DateTime.Now
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            try
            {
                await _financialService.CreateTransactionAsync(model);
                TempData["SuccessMessage"] = "Transaksi berhasil ditambahkan!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Terjadi kesalahan: {ex.Message}");
                return View(model);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var success = await _financialService.DeleteTransactionAsync(id);
            
            if (success)
            {
                TempData["SuccessMessage"] = "Transaksi berhasil dihapus!";
            }
            else
            {
                TempData["ErrorMessage"] = "Transaksi tidak dapat dihapus (transaksi otomatis atau tidak ditemukan)";
            }
            
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> MonthlyReport(int? month, int? year)
        {
            var selectedMonth = month ?? DateTime.Today.Month;
            var selectedYear = year ?? DateTime.Today.Year;
            
            var viewModel = await _financialService.GetMonthlyReportAsync(selectedMonth, selectedYear);
            
            return View(viewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDailyData(DateTime date)
        {
            var viewModel = await _financialService.GetDashboardDataAsync(date, date.Month, date.Year);
            
            return Json(new
            {
                income = viewModel.DailyIncome,
                expense = viewModel.DailyExpense,
                balance = viewModel.DailyBalance,
                transactions = viewModel.DailyTransactions.Select(t => new
                {
                    id = t.Id,
                    time = t.TransactionDate.ToString("HH:mm"),
                    type = t.Type.ToString(),
                    category = GetCategoryName(t.Category),
                    description = t.Description,
                    amount = t.Amount,
                    isAutomatic = t.IsAutomatic
                })
            });
        }
        
        private string GetCategoryName(TransactionCategory category)
        {
            return category switch
            {
                TransactionCategory.Sales => "Penjualan",
                TransactionCategory.OtherIncome => "Pemasukan Lain",
                TransactionCategory.ProductPurchase => "Pembelian Produk",
                TransactionCategory.OperationalCost => "Biaya Operasional",
                TransactionCategory.Salary => "Gaji",
                TransactionCategory.Rent => "Sewa",
                TransactionCategory.Utilities => "Utilitas",
                TransactionCategory.Marketing => "Marketing",
                TransactionCategory.OtherExpense => "Pengeluaran Lain",
                _ => category.ToString()
            };
        }
        
        // Seed dummy data for testing charts
        [HttpGet]
        public async Task<IActionResult> SeedDummyData()
        {
            try
            {
                var transactions = new List<CreateTransactionViewModel>();
                
                // Income - Sales
                transactions.AddRange(new[] {
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 1, 10, 30, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD001", Amount = 450000, Notes = "Penjualan 3 produk bayi" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 3, 14, 20, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD002", Amount = 320000, Notes = "Penjualan mainan edukatif" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 5, 9, 15, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD003", Amount = 580000, Notes = "Penjualan perlengkapan makan" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 8, 16, 45, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD004", Amount = 275000, Notes = "Penjualan produk perawatan" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 10, 11, 30, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD005", Amount = 650000, Notes = "Penjualan pakaian bayi" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 12, 13, 20, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD006", Amount = 420000, Notes = "Penjualan berbagai produk" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 15, 10, 0, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD007", Amount = 380000, Notes = "Penjualan mainan" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 18, 15, 30, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD008", Amount = 520000, Notes = "Penjualan produk premium" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 20, 9, 45, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD009", Amount = 290000, Notes = "Penjualan perawatan bayi" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 22, 14, 15, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD010", Amount = 480000, Notes = "Penjualan paket lengkap" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 25, 11, 0, 0), Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD011", Amount = 720000, Notes = "Penjualan natal" },
                    new CreateTransactionViewModel { TransactionDate = DateTime.Now, Type = TransactionType.Income, Category = TransactionCategory.Sales, Description = "Penjualan Produk - Order #ORD012", Amount = 350000, Notes = "Penjualan hari ini" }
                });
                
                // Income - Other
                transactions.AddRange(new[] {
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 5, 15, 0, 0), Type = TransactionType.Income, Category = TransactionCategory.OtherIncome, Description = "Pendapatan Lain-lain", Amount = 150000, Notes = "Bonus dari supplier" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 15, 16, 0, 0), Type = TransactionType.Income, Category = TransactionCategory.OtherIncome, Description = "Pendapatan Konsultasi", Amount = 200000, Notes = "Konsultasi produk bayi" }
                });
                
                // Expenses
                transactions.AddRange(new[] {
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 2, 9, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.ProductPurchase, Description = "Pembelian Stok Produk", Amount = 1500000, Notes = "Restock produk dari supplier" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 10, 10, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.ProductPurchase, Description = "Pembelian Produk Baru", Amount = 800000, Notes = "Produk mainan edukatif baru" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 20, 9, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.ProductPurchase, Description = "Restock Produk Laris", Amount = 1200000, Notes = "Restock produk best seller" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 1, 8, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.OperationalCost, Description = "Biaya Operasional", Amount = 250000, Notes = "Biaya kebersihan dan maintenance" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 15, 8, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.OperationalCost, Description = "Biaya Operasional", Amount = 180000, Notes = "Supplies kantor" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 1, 17, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.Salary, Description = "Gaji Karyawan", Amount = 3000000, Notes = "Gaji bulan Desember" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 1, 9, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.Rent, Description = "Sewa Toko", Amount = 2000000, Notes = "Sewa toko bulan Desember" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 5, 10, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.Utilities, Description = "Listrik & Air", Amount = 350000, Notes = "Tagihan listrik dan air" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 10, 10, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.Utilities, Description = "Internet & Telepon", Amount = 200000, Notes = "Tagihan internet dan telepon" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 3, 11, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.Marketing, Description = "Iklan Facebook", Amount = 300000, Notes = "Biaya iklan Facebook Ads" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 12, 11, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.Marketing, Description = "Promosi Instagram", Amount = 250000, Notes = "Instagram sponsored posts" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 18, 11, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.Marketing, Description = "Brosur & Banner", Amount = 150000, Notes = "Cetak brosur promosi" },
                    new CreateTransactionViewModel { TransactionDate = new DateTime(2024, 12, 8, 14, 0, 0), Type = TransactionType.Expense, Category = TransactionCategory.OtherExpense, Description = "Pengeluaran Lain-lain", Amount = 100000, Notes = "Biaya tak terduga" }
                });
                
                foreach (var transaction in transactions)
                {
                    await _financialService.CreateTransactionAsync(transaction);
                }
                
                TempData["SuccessMessage"] = $"Berhasil menambahkan {transactions.Count} transaksi dummy untuk testing!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Gagal seed data: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
