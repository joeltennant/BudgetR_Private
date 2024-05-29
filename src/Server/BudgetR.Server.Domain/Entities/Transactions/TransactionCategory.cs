namespace BudgetR.Server.Domain.Entities.Transactions;
public class TransactionCategory
{
    [Key]
    [Column(Order = 0)]
    public long TransactionCategoryId { get; set; }

    [Column(Order = 1)]
    public string? CategoryName { get; set; }

    [Column(Order = 2)]
    public long? HouseholdId { get; set; }
}
