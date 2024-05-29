namespace BudgetR.Server.Domain.Entities.Transactions;
public class FlaggedTransaction : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long FlaggedTransactionId { get; set; }

    [Column(Order = 1)]
    public string? Description { get; set; }

    [Column(Order = 2)]
    public long? AccountId { get; set; }

    [Column(Order = 3)]
    public TransactionType? TransactionType { get; set; }

    [Precision(19, 2)]
    [Column(Order = 4)]
    public decimal Amount { get; set; }

    [Column(Order = 5)]
    public DateOnly? TransactionDate { get; set; }

    [Column(Order = 6)]
    [MaxLength(2)]
    public int TransactionMonth { get; set; }

    [MaxLength(4)]
    [Column(Order = 7)]
    public int TransactionYear { get; set; }

    [Column(Order = 8)]
    public long? TransactionCategoryId { get; set; }
    public TransactionCategory? TransactionCategory { get; set; }

    [Column(Order = 9)]
    public long? TransactionBatchId { get; set; }

    [Column(Order = 10)]
    public long HouseholdId { get; set; }
    public Household? Household { get; set; }

    [Column(Order = 11)]
    public FlagType FlagType { get; set; }
    [Column(Order = 12)]
    public bool Complete { get; set; }
}

public enum FlagType
{
    Duplicate,
    Uncategorized,
    ErrorProcessing
}