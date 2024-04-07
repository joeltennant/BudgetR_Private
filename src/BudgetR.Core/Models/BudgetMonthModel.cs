namespace BudgetR.Core.Models;
public class BudgetMonthModel
{
    public long BudgetMonthId { get; set; }
    public long MonthYearId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public decimal IncomeTotal { get; set; }
    public decimal ExpenseTotal { get; set; }

    public bool IsActive { get; set; }

    public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM");
}
