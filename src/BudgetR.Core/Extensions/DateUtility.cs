namespace BudgetR.Core.Extensions;
public static class DateUtility
{
    public static DateOnly LastDayFromParts(int Month, int Year, int Day = 1)
    {
        return DateOnly.FromDateTime(new DateTime(Month, Year, Day).AddMonths(1).AddMinutes(-1));
    }

    public static bool IsMonthPast(int Year, int Month)
    {
        return LastDayFromParts(Month, Year) < DateOnly.FromDateTime(DateTime.Now);
    }
}

