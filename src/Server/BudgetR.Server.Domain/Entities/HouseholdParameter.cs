namespace BudgetR.Server.Domain.Entities;
public class HouseholdParameter
{
    [Key]
    public long HouseholdParameterId { get; set; }
    //public string? Name { get; set; }
    public HouseholdParameterType? HouseholdParameterType { get; set; }
    public string? Value { get; set; }
    public long HouseholdId { get; set; }
    public Household? Household { get; set; }
}

public enum HouseholdParameterType
{
    DownloadPath,
    IncomingPath,
    ArchivePath,
    FailedPath
}