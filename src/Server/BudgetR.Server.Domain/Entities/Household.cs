namespace BudgetR.Server.Domain.Entities;
public class Household : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long HouseholdId { get; set; }
    [Column(Order = 1)]
    public string? Name { get; set; }

    public ICollection<User>? Users { get; set; }
}
