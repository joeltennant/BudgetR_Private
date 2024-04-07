namespace BudgetR.Core.Models;

public class ExpenseDetailModel
{
    public long ExpenseDetailId { get; set; }

    public long ExpenseId { get; set; }

    public long BudgetMonthId { get; set; }

    public int Month { get; set; }
    public int Year { get; set; }
    public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM");
}