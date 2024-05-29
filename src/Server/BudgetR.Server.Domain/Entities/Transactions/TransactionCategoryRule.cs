namespace BudgetR.Server.Domain.Entities.Transactions;
public class TransactionCategoryRule : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long TransactionCategoryRuleId { get; set; }

    [Column(Order = 1)]
    public string? CategoryRuleName { get; set; }

    [Column(Order = 2)]
    public long? CategoryId { get; set; }
    public TransactionCategory? TransactionCategory { get; set; }

    [Column(Order = 3)]
    public string? Rule { get; set; }

    [Column(Order = 4)]
    public ComparisonType ComparisonType { get; set; }

    [Column(Order = 5)]
    public TransactionType? TransactionType { get; set; }

    [Column(Order = 6)]
    public long HouseholdId { get; set; }
}
