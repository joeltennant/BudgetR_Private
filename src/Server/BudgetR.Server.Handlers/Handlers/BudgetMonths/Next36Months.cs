namespace BudgetR.Server.Application.Handlers.BudgetMonths;
public class Next36Months
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
                .Where(x => x.MonthYear.IsActive
                            && x.HouseholdId == _stateContainer.HouseholdId)
                .Select(x => new BudgetMonthModel
                {
                    BudgetMonthId = x.BudgetMonthId,
                    Month = x.MonthYear.Month,
                    Year = x.MonthYear.Year,
                })
                .Take(36)
                .OrderBy(x => x.Year)
                    .ThenBy(x => x.Month)
                .ToListAsync();

            return Result.Success(expenses);
        }
    }
}
