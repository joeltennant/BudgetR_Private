namespace BudgetR.Server.Domain.Entities.Transactions;
public class ProcessedFile : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long ProcessedFileId { get; set; }
    [Column(Order = 1)]
    public DateTime? RunOn { get; set; }
    [Column(Order = 2)]
    public string? FileName { get; set; }
    [Column(Order = 3)]
    public long? TransactionBatchId { get; set; }
    public TransactionBatch? TransactionBatch { get; set; }
}
