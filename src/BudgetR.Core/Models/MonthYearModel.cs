namespace BudgetR.Core.Models;
public class MonthYearModel
{
    public long MonthYearId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public bool IsActive { get; set; }
    public int NumberOfDays { get; set; }

    public bool CheckIfMonthIsPast()
    {
        var today = DateTime.Today;
        //if the end of the object's month is less than today, then it is in the past
        return DateOnly.FromDateTime(new DateTime(Year, Month, NumberOfDays))
            < DateOnly.FromDateTime(new DateTime(today.Month, today.Year, 1));
    }
}
