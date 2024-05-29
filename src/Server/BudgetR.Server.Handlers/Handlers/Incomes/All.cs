namespace BudgetR.Server.Application.Handlers.Incomes;
public class All
{
    public record Request : IRequest<Result<List<IncomeModel>>>;

    public class Handler : BaseHandler<List<IncomeModel>>, IRequestHandler<Request, Result<List<IncomeModel>>>
    {
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<List<IncomeModel>>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var incomes = await _context.Incomes
                    .Where(x => x.HouseholdId == _stateContainer.HouseholdId)
                    .Select(x => new IncomeModel
                    {
                        IncomeId = x.IncomeId,
                        Name = x.Name,
                        Amount = x.Amount,
                        IsActive = x.IsActive
                    })
                    .OrderBy(x => x.Name)
                    .ToListAsync();

                return Result.Success(incomes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
