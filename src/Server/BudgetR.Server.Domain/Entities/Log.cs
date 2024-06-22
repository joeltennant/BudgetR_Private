namespace BudgetR.Server.Domain.Entities;
public class Log
{
    [Key]
    public long LogId { get; set; }
    public string? Message { get; set; }
    public LogType LogType { get; set; }
    public bool IsHidden { get; set; }
    public long HouseholdId { get; set; }
    public long? UserId { get; set; }
    public long? BusinessTransactionActivityId { get; set; }
    public DateTime Created { get; set; }
}

public enum LogType
{
    Error,
    Informational
}