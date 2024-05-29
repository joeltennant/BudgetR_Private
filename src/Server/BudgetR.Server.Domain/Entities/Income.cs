namespace BudgetR.Server.Domain.Entities;

public class Income : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long IncomeId { get; set; }

    [Column(Order = 1)]
    public long HouseholdId { get; set; }

    [Column(Order = 2)]
    public string? Name { get; set; }

    [Column(Order = 3)]
    [Precision(19, 2)]
    public decimal Amount { get; set; }
    [Column(Order = 4)]
    public bool IsActive { get; set; }

    public List<IncomeDetail>? IncomeDetails { get; set; }

}