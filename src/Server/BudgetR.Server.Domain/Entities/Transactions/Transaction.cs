namespace BudgetR.Server.Domain.Entities.Transactions;

[Index(nameof(TransactionMonth), nameof(TransactionYear))]
public class Transaction
{
    [Key]
    [Column(Order = 0)]
    public long TransactionId { get; set; }

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
    public bool PreventReprocessing { get; set; }
}
