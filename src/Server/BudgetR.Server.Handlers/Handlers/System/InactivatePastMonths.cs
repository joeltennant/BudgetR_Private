namespace BudgetR.Server.Application.Handlers.System;
public class InactivatePastMonths
{
    public record Request() : IRequest<Result<int>>;

    public class Handler : BaseHandler<int>, IRequestHandler<Request, Result<int>>
    {
        private BusinessTransactionActivity bta;

        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<int>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                IList<long> budgetMonthIds = new List<long>();

                var monthYears = await _context.MonthYears
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.Year)
                        .ThenBy(x => x.Month)
                    .Take(36)
                    .ToListAsync();

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

                    await _context.BusinessTransactionActivities.AddAsync(bta);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Result.Success();
                }

                int monthsChanged = await _context.MonthYears
                    .Where(x => pastMonthYears.Contains(x.MonthYearId))
                    .ExecuteUpdateAsync(a => a.SetProperty(p => p.IsActive, false));

                budgetMonthIds = await _context.BudgetMonths
                    .Where(x => pastMonthYears.Contains(x.MonthYearId))
                    .Select(x => x.BudgetMonthId)
                    .ToListAsync();

                await _context.ExpenseDetails
                    .Where(x => budgetMonthIds.Contains(x.BudgetMonthId))
                    .ExecuteUpdateAsync(a => a
                        .SetProperty(p => p.BusinessTransactionActivityId, bta.BusinessTransactionActivityId)
                        .SetProperty(p => p.IsActive, true));

                await _context.IncomeDetails
                    .Where(x => budgetMonthIds.Contains(x.BudgetMonthId))
                    .ExecuteUpdateAsync(a => a
                        .SetProperty(p => p.BusinessTransactionActivityId, bta.BusinessTransactionActivityId)
                        .SetProperty(p => p.IsActive, true));

                return Result.Success(monthsChanged);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
