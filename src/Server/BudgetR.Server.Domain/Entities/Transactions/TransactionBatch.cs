namespace BudgetR.Server.Domain.Entities.Transactions;
public class TransactionBatch : BaseEntity
{
    [Key]
    public long TransactionBatchId { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? RecordCount { get; set; }
    public BatchSource? Source { get; set; }
    public string? FileName { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
    public long HouseholdId { get; set; }
}
