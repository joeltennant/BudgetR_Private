namespace BudgetR.Server.Application.Handlers.Accounts;
public class GetAccountTypes
{
    public record Request : IRequest<Result<IList<AccountTypeModel>>>;

    public class Handler
        : BaseHandler<IList<AccountTypeModel>>, IRequestHandler<Request, Result<IList<AccountTypeModel>>>
    {
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<IList<AccountTypeModel>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var accountTypes = await _context.AccountTypes
                .Select(x => new AccountTypeModel
                {
                    AccountTypeId = x.AccountTypeId,
                    Name = x.Name
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Result.Success(accountTypes);
        }
    }
}
