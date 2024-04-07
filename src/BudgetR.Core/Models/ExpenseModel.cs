namespace BudgetR.Core.Models;
public class ExpenseModel
{
    public long ExpenseId { get; set; }

    public string? Name { get; set; }

    public decimal Amount { get; set; }
    public bool IsActive { get; set; }

    public IEnumerable<ExpenseDetailModel> ExpenseDetails { get; set; }
}
