namespace BudgetR.Server.Domain.Entities;
public class Expense : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long ExpenseId { get; set; }

    [Column(Order = 1)]
    [MaxLength(125)]
    public string? Name { get; set; }

    [Column(Order = 2)]
    [Precision(19, 2)]
    public decimal Amount { get; set; }

    [Column(Order = 3)]
    public bool IsActive { get; set; }

    [Column(Order = 4)]
    public long HouseholdId { get; set; }

    public List<ExpenseDetail>? ExpenseDetails { get; set; }
}
