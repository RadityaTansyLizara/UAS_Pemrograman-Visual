using BabyShopWeb2.Data;
using BabyShopWeb2.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyShopWeb2.Services
{
    public interface IFinancialService
    {
        Task<FinancialDashboardViewModel> GetDashboardDataAsync(DateTime date, int month, int year);
        Task<MonthlyReportViewModel> GetMonthlyReportAsync(int month, int year);
        Task<FinancialTransaction> CreateTransactionAsync(CreateTransactionViewModel model);
        Task<bool> DeleteTransactionAsync(int id);
        Task RecordSaleTransactionAsync(Order order);
    }
    
    public class FinancialService : IFinancialService
    {
        private readonly ApplicationDbContext _context;
        
        public FinancialService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<FinancialDashboardViewModel> GetDashboardDataAsync(DateTime date, int month, int year)
        {
            var viewModel = new FinancialDashboardViewModel
            {
                SelectedDate = date,
                SelectedMonth = month,
                SelectedYear = year
            };
            
            // Daily transactions
            var dailyStart = date.Date;
            var dailyEnd = date.Date.AddDays(1);
            
            viewModel.DailyTransactions = await _context.FinancialTransactions
                .Include(ft => ft.Order)
                .Where(ft => ft.TransactionDate >= dailyStart && ft.TransactionDate < dailyEnd)
                .OrderByDescending(ft => ft.TransactionDate)
                .ToListAsync();
            
            viewModel.DailyIncome = (decimal)viewModel.DailyTransactions
                .Where(t => t.Type == TransactionType.Income)
                .Select(t => (double)t.Amount)
                .DefaultIfEmpty(0)
                .Sum();
            
            viewModel.DailyExpense = (decimal)viewModel.DailyTransactions
                .Where(t => t.Type == TransactionType.Expense)
                .Select(t => (double)t.Amount)
                .DefaultIfEmpty(0)
                .Sum();
            
            viewModel.DailyBalance = viewModel.DailyIncome - viewModel.DailyExpense;
            viewModel.TotalTransactionsToday = viewModel.DailyTransactions.Count;
            
            // Monthly transactions
            var monthlyStart = new DateTime(year, month, 1);
            var monthlyEnd = monthlyStart.AddMonths(1);
            
            viewModel.MonthlyTransactions = await _context.FinancialTransactions
                .Include(ft => ft.Order)
                .Where(ft => ft.TransactionDate >= monthlyStart && ft.TransactionDate < monthlyEnd)
                .OrderByDescending(ft => ft.TransactionDate)
                .ToListAsync();
            
            viewModel.MonthlyIncome = (decimal)viewModel.MonthlyTransactions
                .Where(t => t.Type == TransactionType.Income)
                .Select(t => (double)t.Amount)
                .DefaultIfEmpty(0)
                .Sum();
            
            viewModel.MonthlyExpense = (decimal)viewModel.MonthlyTransactions
                .Where(t => t.Type == TransactionType.Expense)
                .Select(t => (double)t.Amount)
                .DefaultIfEmpty(0)
                .Sum();
            
            viewModel.MonthlyBalance = viewModel.MonthlyIncome - viewModel.MonthlyExpense;
            viewModel.TotalTransactionsMonth = viewModel.MonthlyTransactions.Count;
            
            // Category breakdown
            viewModel.CategoryBreakdown = viewModel.MonthlyTransactions
                .GroupBy(t => t.Category)
                .ToDictionary(
                    g => g.Key, 
                    g => (decimal)g.Select(t => t.Type == TransactionType.Income ? (double)t.Amount : -(double)t.Amount).Sum()
                );
            
            // Monthly chart data
            var daysInMonth = DateTime.DaysInMonth(year, month);
            viewModel.MonthlyChart = new List<DailyFinancialSummary>();
            
            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = new DateTime(year, month, day);
                var dayTransactions = viewModel.MonthlyTransactions
                    .Where(t => t.TransactionDate.Date == currentDate.Date)
                    .ToList();
                
                var dayIncome = dayTransactions.Where(t => t.Type == TransactionType.Income).Select(t => (double)t.Amount).DefaultIfEmpty(0).Sum();
                var dayExpense = dayTransactions.Where(t => t.Type == TransactionType.Expense).Select(t => (double)t.Amount).DefaultIfEmpty(0).Sum();
                
                viewModel.MonthlyChart.Add(new DailyFinancialSummary
                {
                    Date = currentDate,
                    Income = (decimal)dayIncome,
                    Expense = (decimal)dayExpense,
                    Balance = (decimal)(dayIncome - dayExpense)
                });
            }
            
            return viewModel;
        }
        
        public async Task<MonthlyReportViewModel> GetMonthlyReportAsync(int month, int year)
        {
            var monthlyStart = new DateTime(year, month, 1);
            var monthlyEnd = monthlyStart.AddMonths(1);
            
            var transactions = await _context.FinancialTransactions
                .Include(ft => ft.Order)
                .Where(ft => ft.TransactionDate >= monthlyStart && ft.TransactionDate < monthlyEnd)
                .OrderByDescending(ft => ft.TransactionDate)
                .ToListAsync();
            
            var viewModel = new MonthlyReportViewModel
            {
                Month = month,
                Year = year,
                MonthName = new DateTime(year, month, 1).ToString("MMMM yyyy"),
                Transactions = transactions
            };
            
            viewModel.TotalIncome = (decimal)transactions.Where(t => t.Type == TransactionType.Income).Select(t => (double)t.Amount).DefaultIfEmpty(0).Sum();
            viewModel.TotalExpense = (decimal)transactions.Where(t => t.Type == TransactionType.Expense).Select(t => (double)t.Amount).DefaultIfEmpty(0).Sum();
            viewModel.NetBalance = viewModel.TotalIncome - viewModel.TotalExpense;
            
            viewModel.SalesIncome = (decimal)transactions
                .Where(t => t.Type == TransactionType.Income && t.Category == TransactionCategory.Sales)
                .Select(t => (double)t.Amount)
                .DefaultIfEmpty(0)
                .Sum();
            
            viewModel.OtherIncome = (decimal)transactions
                .Where(t => t.Type == TransactionType.Income && t.Category != TransactionCategory.Sales)
                .Select(t => (double)t.Amount)
                .DefaultIfEmpty(0)
                .Sum();
            
            viewModel.ExpenseByCategory = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .GroupBy(t => t.Category)
                .ToDictionary(g => g.Key, g => (decimal)g.Select(t => (double)t.Amount).Sum());
            
            // Daily summaries
            var daysInMonth = DateTime.DaysInMonth(year, month);
            viewModel.DailySummaries = new List<DailyFinancialSummary>();
            
            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = new DateTime(year, month, day);
                var dayTransactions = transactions.Where(t => t.TransactionDate.Date == currentDate.Date).ToList();
                
                var dayIncome = dayTransactions.Where(t => t.Type == TransactionType.Income).Select(t => (double)t.Amount).DefaultIfEmpty(0).Sum();
                var dayExpense = dayTransactions.Where(t => t.Type == TransactionType.Expense).Select(t => (double)t.Amount).DefaultIfEmpty(0).Sum();
                
                viewModel.DailySummaries.Add(new DailyFinancialSummary
                {
                    Date = currentDate,
                    Income = (decimal)dayIncome,
                    Expense = (decimal)dayExpense,
                    Balance = (decimal)(dayIncome - dayExpense)
                });
            }
            
            return viewModel;
        }
        
        public async Task<FinancialTransaction> CreateTransactionAsync(CreateTransactionViewModel model)
        {
            var transaction = new FinancialTransaction
            {
                TransactionDate = model.TransactionDate,
                Type = model.Type,
                Category = model.Category,
                Description = model.Description,
                Amount = model.Amount,
                Notes = model.Notes,
                IsAutomatic = false,
                CreatedAt = DateTime.Now
            };
            
            _context.FinancialTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            return transaction;
        }
        
        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.FinancialTransactions.FindAsync(id);
            if (transaction == null || transaction.IsAutomatic)
            {
                return false; // Cannot delete automatic transactions
            }
            
            _context.FinancialTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
            
            return true;
        }
        
        public async Task RecordSaleTransactionAsync(Order order)
        {
            // Check if transaction already exists for this order
            var existingTransaction = await _context.FinancialTransactions
                .FirstOrDefaultAsync(ft => ft.OrderId == order.Id);
            
            if (existingTransaction != null)
            {
                return; // Already recorded
            }
            
            var transaction = new FinancialTransaction
            {
                TransactionDate = order.OrderDate,
                Type = TransactionType.Income,
                Category = TransactionCategory.Sales,
                Description = $"Penjualan - Order #{order.OrderNumber}",
                Amount = order.TotalAmount,
                Notes = $"Pembayaran dari {order.CustomerName} ({order.CustomerPhone})",
                OrderId = order.Id,
                IsAutomatic = true,
                CreatedAt = DateTime.Now
            };
            
            _context.FinancialTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
