using BudgetR.Server.Domain.Entities.Transactions;
using BudgetR.Server.Services.Transactions.Helpers;

namespace BudgetR.Server.Services.Transactions;
public class TransactionProcessorDto
{
    public TransactionBatch? TransactionBatch { get; set; }
    public TransactionBatchDto? TransactionBatchDto { get; set; }
    public long? BTA_ID { get; set; }
    public long? UserId { get; set; }
    public long? HouseholdId { get; set; }
    public bool HasErrors { get; set; }
    public string ErrorMessage { get; set; }
    public TransactionProcessorDto()
    {
        HasErrors = false;
    }
}
