namespace BudgetR.Core.Models;
public class IncomeModel
{
    public long IncomeId { get; set; }

    public string? Name { get; set; }

    public decimal Amount { get; set; }
    public bool IsActive { get; set; }

    public IEnumerable<IncomeDetailModel> IncomeDetails { get; set; }
}
