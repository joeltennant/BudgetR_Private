namespace BudgetR.Server.Domain.Entities.Transactions;
public class Allocation : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long AccountingTransactionRecordId { get; set; }

    [Column(Order = 0)]
    public long TransactionId { get; set; }

    [Column(Order = 1)]
    public AllocationType AllocationType { get; set; }

    [Column(Order = 2)]
    [Precision(19, 2)]
    public decimal Amount { get; set; }

    [Column(Order = 3)]
    public long? ExpenseDetailId { get; set; }

    [Column(Order = 4)]
    public long? IncomeDetailId { get; set; }
}
