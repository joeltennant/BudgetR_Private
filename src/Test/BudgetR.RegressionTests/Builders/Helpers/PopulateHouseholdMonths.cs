namespace BudgetR.RegressionTests.Builders.Helpers;
public static class PopulateHouseholdMonths
{
    public static async Task Populate(BudgetRDbContext context, long HouseHoldId)
    {
        try
        {
            var count = await context.BudgetMonths
    .Where(bm => bm.HouseholdId == HouseHoldId)
    .CountAsync();

            if (count > 0) return;

            await context.AddRangeAsync(await BuildMonthBudgetList(HouseHoldId, context));
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private static async Task<List<BudgetMonth>> BuildMonthBudgetList(long HouseholdId, BudgetRDbContext context)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        List<MonthYear> monthYears = await context.MonthYears
            .Where(m => m.IsActive)
            .OrderBy(m => m.MonthYearId)
            .ToListAsync();

        List<BudgetMonth> monthBudgets = [];

        foreach (var monthYear in monthYears)
        {
            monthBudgets.Add(new BudgetMonth
            {
                MonthYearId = monthYear.MonthYearId,
                HouseholdId = HouseholdId,
            });
        }

        return monthBudgets;
    }
}
