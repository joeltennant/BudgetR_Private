namespace BudgetR.Server.Domain.Entities;

public class BaseEntity
{
    public long? BusinessTransactionActivityId { get; set; }
    public BusinessTransactionActivity? BusinessTransactionActivity { get; set; }
}