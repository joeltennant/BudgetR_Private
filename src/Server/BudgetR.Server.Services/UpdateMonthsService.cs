using BudgetR.Core.Extensions;
using BudgetR.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetR.Server.Services;
public class UpdateMonthsService
{
    private readonly BudgetRDbContext _context;
    public UpdateMonthsService(BudgetRDbContext context)
    {
        _context = context;
    }

    public void Execute()
    {
        try
        {
            BusinessTransactionActivity bta = new();
            var today = DateOnly.FromDateTime(DateTime.Now);
            IList<long> budgetMonthIds = new List<long>();

            var monthYears = _context.MonthYears
                .Where(x => x.IsActive)
                .OrderBy(x => x.Year)
                    .ThenBy(x => x.Month)
                .Take(36)
                .ToList();

            List<long> pastMonthYears = new();

            foreach (var monthYear in monthYears)
            {
                if (new DateOnly(monthYear.Year, monthYear.Month, monthYear.NumberOfDays) < today)
                {
                    pastMonthYears.Add(monthYear.MonthYearId);
                }
            }

            if (pastMonthYears.IsPopulated())
            {
                bta = new BusinessTransactionActivity
                {
                    ProcessName = "System.InactivatePastMonths",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow
                };

                _context.BusinessTransactionActivities.Add(bta);
                _context.SaveChanges();
            }
            //else
            //{
            //    return Result.Success();
            //}

            int monthsChanged = _context.MonthYears
                .Where(x => pastMonthYears.Contains(x.MonthYearId))
                .ExecuteUpdate(a => a.SetProperty(p => p.IsActive, false));

            budgetMonthIds = _context.BudgetMonths
                .Where(x => pastMonthYears.Contains(x.MonthYearId))
                .Select(x => x.BudgetMonthId)
                .ToList();

            _context.ExpenseDetails
               .Where(x => budgetMonthIds.Contains(x.BudgetMonthId))
               .ExecuteUpdateAsync(a => a
                   .SetProperty(p => p.BusinessTransactionActivityId, bta.BusinessTransactionActivityId)
                   .SetProperty(p => p.IsActive, true));

            _context.IncomeDetails
               .Where(x => budgetMonthIds.Contains(x.BudgetMonthId))
               .ExecuteUpdateAsync(a => a
                   .SetProperty(p => p.BusinessTransactionActivityId, bta.BusinessTransactionActivityId)
                   .SetProperty(p => p.IsActive, true));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
