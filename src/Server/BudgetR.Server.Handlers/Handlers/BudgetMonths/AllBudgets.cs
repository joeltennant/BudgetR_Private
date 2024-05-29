namespace BudgetR.Server.Application.Handlers.BudgetMonths;
public class AllBudgets
{
    public record Request : IRequest<Result<List<BudgetMonthModel>>>;

    public class Handler : BaseHandler<List<BudgetMonthModel>>, IRequestHandler<Request, Result<List<BudgetMonthModel>>>
    {
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<List<BudgetMonthModel>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var expenses = await _context.BudgetMonths
                .Where(x => x.HouseholdId == _stateContainer.HouseholdId)
                .Select(x => new BudgetMonthModel
                {
                    BudgetMonthId = x.BudgetMonthId,
                    ExpenseTotal = x.ExpenseTotal,
                    IncomeTotal = x.IncomeTotal,
                    Month = x.MonthYear.Month,
                    Year = x.MonthYear.Year,
                    IsActive = x.MonthYear.IsActive,
                })
                .Take(36)
                .OrderBy(x => x.Year)
                    .ThenBy(x => x.Month)
                .ToListAsync();

            return Result.Success(expenses);
        }
    }
}
