namespace BudgetR.Server.Domain.Entities;
public class ExpenseAllocation : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long ExpenseAllocationId { get; set; }
    [Column(Order = 1)]
    [Precision(19, 2)]
    public decimal Amount { get; set; }

    [Column(Order = 2)]
    public long TransactionId { get; set; }

    [Column(Order = 3)]
    public long ExpenseDetailId { get; set; }
}
