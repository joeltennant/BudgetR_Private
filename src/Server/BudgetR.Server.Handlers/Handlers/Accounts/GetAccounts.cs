namespace BudgetR.Server.Application.Handlers.Accounts;
public class GetAccounts
{
    public class Request : IRequest<Result<List<AccountModel>>>
    {
    }

    public class Handler : BaseHandler<List<AccountModel>>, IRequestHandler<Request, Result<List<AccountModel>>>
    {
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<List<AccountModel>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var accounts = await _context.Accounts
                .Where(x => x.HouseholdId == _stateContainer.HouseholdId)
                .Select(x => new AccountModel
                {
                    AccountId = x.AccountId,
                    Name = x.Name,
                    Balance = x.Balance,
                    BalanceType = x.AccountType.BalanceType,
                    AccountTypeId = x.AccountTypeId,
                    AccountType = x.AccountType.Name
                })
                .OrderBy(x => x.BalanceType)
                .ThenBy(x => x.AccountType)
                .ToListAsync();

            return Result.Success(accounts);
        }
    }
}
