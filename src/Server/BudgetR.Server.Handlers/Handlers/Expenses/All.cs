namespace BudgetR.Server.Application.Handlers.Expenses;
public class All
{
    public record Request : IRequest<Result<List<ExpenseModel>>>;

    public class Handler : BaseHandler<List<ExpenseModel>>, IRequestHandler<Request, Result<List<ExpenseModel>>>
    {
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<List<ExpenseModel>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var expenses = await _context.Expenses
                .Where(x => x.HouseholdId == _stateContainer.HouseholdId)
                .Select(x => new ExpenseModel
                {
                    ExpenseId = x.ExpenseId,
                    Name = x.Name,
                    Amount = x.Amount,
                    IsActive = x.IsActive
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Result.Success(expenses);
        }
    }
}
