//using BudgetR.Core.Models;

//namespace BudgetR.Server.Application;
//public class BuildMonthListFromSelection
//{
//    add db context
//    private readonly BudgetRDbContext _context;
//    public BuildMonthListFromSelection(BudgetRDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<List<long>?> BuildBudgetMonthListFromSelection(MonthSelection monthSelection)
//    {
//        if (monthSelection == null)
//        {
//            return null;
//        }

//        List<long> months = new();
//        long startingMonthYearId = 0;
//        (int Month, int Year) StartingMonthYear;
//        StartingMonthYear.Year = monthSelection.StartYear.Value;
//        StartingMonthYear.Month = monthSelection.StartMonth.Value;

//        switch (monthSelection.SelectionType)
//        {
//            case SelectionType.All:
//                months = await _context.BudgetMonths.Select(x => x.BudgetMonthId).ToListAsync();
//                break;
//            case SelectionType.Selected:
//                if (!monthSelection.SelectedMonths.IsPopulated()) return null;
//                months = monthSelection.SelectedMonths.ToList();
//                break;
//            case SelectionType.Once:
//                long month = await _context.BudgetMonths.Select(x => x.BudgetMonthId).FirstOrDefaultAsync();

//                months.Add(month);
//                break;
//            case SelectionType.Monthly:
//                startingMonthYearId = await GetStartingMonthYearId(StartingMonthYear);

//                months = await _context.BudgetMonths
//                    .Where(x => x.MonthYear.MonthYearId >= startingMonthYearId)
//                    .OrderBy(x => x.MonthYear.MonthYearId)
//                    .Select(x => x.BudgetMonthId)
//                    .Take(monthSelection.Duration)
//                    .ToListAsync();
//                break;
//            case SelectionType.Yearly:
//                months = await _context.BudgetMonths
//                    .Where(x => x.MonthYear.Year >= StartingMonthYear.Year
//                                && x.MonthYear.Month == StartingMonthYear.Month)
//                    .OrderBy(x => x.MonthYear.Year)
//                    .Select(x => x.BudgetMonthId)
//                    .Take(monthSelection.Duration)
//                    .ToListAsync();
//                break;
//            case SelectionType.EveryOtherMonth:
//                startingMonthYearId = await GetStartingMonthYearId(StartingMonthYear);

//                months = await BuildMonthListByPattern(startingMonthYearId, monthSelection.Duration, 2);
//                break;
//            case SelectionType.EveryThreeMonths:
//                months = await BuildMonthListByPattern(startingMonthYearId, monthSelection.Duration, 3);
//                break;
//            default:
//                break;
//        }


//        return months.IsPopulated() ? months : null;
//    }

//    private async Task<List<long>> BuildMonthListByPattern(long startingMonthYearId, int duration, int monthPattern)
//    {
//        var monthYearIds = new List<long>
//        {
//            startingMonthYearId
//        };

//        duration--;

//        if (duration > 0)
//        {
//            long lastUsedId = startingMonthYearId;
//            for (int i = 1; i <= duration; i++)
//            {
//                lastUsedId += monthPattern;
//                monthYearIds.Add(lastUsedId);
//            }
//        }

//        return await _context.BudgetMonths
//            .Where(x => monthYearIds.Contains(x.MonthYearId))
//            .Select(x => x.BudgetMonthId)
//            .ToListAsync();
//    }

//    private async Task<long> GetStartingMonthYearId((int Month, int Year) startingMonthYear)
//    {
//        return await _context.MonthYears
//                             .Where(x => x.Year == startingMonthYear.Year
//                                         && x.Month == startingMonthYear.Month)
//                             .Select(x => x.MonthYearId)
//                             .FirstOrDefaultAsync();
//    }
//}
