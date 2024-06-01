namespace BudgetR.Server.Domain.Entities;
public class User
{
    [Key]
    [Column(Order = 0)]
    public long UserId { get; set; }

    [Column(Order = 1)]
    public string? AuthenticationId { get; set; }

    [Column(Order = 2)]
    public string? FirstName { get; set; }

    [Column(Order = 3)]
    public string? LastName { get; set; }

    [Column(Order = 4)]
    public long? HouseholdId { get; set; }

    public Household? Household { get; set; }

    [Column(Order = 5)]
    public UserType UserType { get; set; }

    [Column(Order = 6)]
    public bool IsActive { get; set; }

    [Column(Order = 7)]
    public long? BusinessTransactionActivityId { get; set; }

    [Column(Order = 8)]
    public long? ModifiedBy { get; set; }
}
