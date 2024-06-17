namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class MonthYearConfiguration : IEntityTypeConfiguration<MonthYear>
{
    public void Configure(EntityTypeBuilder<MonthYear> builder)
    {
        builder.ToTable("MonthYears");

        builder.HasData(BuildMonthYearList());
    }

    private IList<MonthYear> BuildMonthYearList()
    {
        int Year = 2024;
        int Month = 1;
        int numberOfMonths = 120;
        IList<MonthYear> monthYears = new List<MonthYear>();

        for (int i = 0; i < numberOfMonths; i++)
        {
            monthYears.Add(new MonthYear
            {
                MonthYearId = i + 1,
                Month = Month,
                Year = Year,
                IsActive = true,
                NumberOfDays = DateTime.DaysInMonth(Year, Month)
            });

            if (Month == 12)
            {
                Month = 1;
                Year++;
            }
            else
            {
                Month++;
            }
        }

        return monthYears;
    }
}
