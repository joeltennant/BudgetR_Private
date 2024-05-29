namespace BudgetR.Server.Domain.Entities;

public class IncomeDetail : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long IncomeDetailId { get; set; }

    [Column(Order = 1)]
    public long IncomeId { get; set; }
    public Income? Income { get; set; }

    [Column(Order = 2)]
    public long BudgetMonthId { get; set; }
    public BudgetMonth? BudgetMonth { get; set; }

    [Column(Order = 3)]
    public bool IsActive { get; set; }
}