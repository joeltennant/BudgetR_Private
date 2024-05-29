namespace BudgetR.Server.Domain.Entities.Transactions;
public class TransactionTypeRule : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long TransactionTypeRuleId { get; set; }

    [Column(Order = 1)]
    public string? TransactionTypeRuleName { get; set; }

    [Column(Order = 2)]
    public long? NumericRule { get; set; }

    [Column(Order = 3)]
    public string? StringRule { get; set; }

    [Column(Order = 4)]
    public ComparisonType ComparisonType { get; set; }

    [Column(Order = 5)]
    public int RuleLevel { get; set; }

    [Column(Order = 6)]
    public TransactionType? AssignTransactionType { get; set; }

    [Column(Order = 7)]
    public TransactionType? RuleOnTransactionType { get; set; }
    [Column(Order = 8)]
    public long HouseholdId { get; set; }
}
