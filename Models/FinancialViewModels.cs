namespace BabyShopWeb2.Models
{
    public class FinancialDashboardViewModel
    {
        public DateTime SelectedDate { get; set; } = DateTime.Today;
        public int SelectedMonth { get; set; } = DateTime.Today.Month;
        public int SelectedYear { get; set; } = DateTime.Today.Year;
        
        // Daily Summary
        public decimal DailyIncome { get; set; }
        public decimal DailyExpense { get; set; }
        public decimal DailyBalance { get; set; }
        public List<FinancialTransaction> DailyTransactions { get; set; } = new();
        
        // Monthly Summary
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpense { get; set; }
        public decimal MonthlyBalance { get; set; }
        public List<FinancialTransaction> MonthlyTransactions { get; set; } = new();
        
        // Statistics
        public int TotalTransactionsToday { get; set; }
        public int TotalTransactionsMonth { get; set; }
        public Dictionary<TransactionCategory, decimal> CategoryBreakdown { get; set; } = new();
        
        // Charts data
        public List<DailyFinancialSummary> MonthlyChart { get; set; } = new();
    }
    
    public class DailyFinancialSummary
    {
        public DateTime Date { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance { get; set; }
    }
    
    public class CreateTransactionViewModel
    {
        public TransactionType Type { get; set; }
        public TransactionCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
    }
    
    public class MonthlyReportViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; } = string.Empty;
        
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetBalance { get; set; }
        
        public decimal SalesIncome { get; set; }
        public decimal OtherIncome { get; set; }
        
        public Dictionary<TransactionCategory, decimal> ExpenseByCategory { get; set; } = new();
        public List<FinancialTransaction> Transactions { get; set; } = new();
        public List<DailyFinancialSummary> DailySummaries { get; set; } = new();
    }
}
