namespace BudgetR.Server.Domain.Entities;
public class MonthYear : BaseEntity
{
    [Key]
    [Column(Order = 0)]
    public long MonthYearId { get; set; }

    [Column(Order = 1)]
    public int Month { get; set; }
    [Column(Order = 2)]
    public int Year { get; set; }
    [Column(Order = 3)]
    public bool IsActive { get; set; }

    [Column(Order = 4)]
    public int NumberOfDays { get; set; }

    [NotMapped]
    public DateOnly EndOfMonth => new(Year, Month, NumberOfDays);
}
