namespace BudgetR.Server.Domain.Entities.Transactions;
public class AccountTransactionRecord : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long AccountingTransactionRecordId { get; set; }

    [Column(Order = 1)]
    public long TransactionId { get; set; }

    [Column(Order = 2)]
    public long MonthYearId { get; set; }
    public MonthYear? MonthYear { get; set; }

    [Column(Order = 3)]
    public long? AccountId { get; set; }

    [Column(Order = 4)]
    public TransactionType TransactionType { get; set; }
}
