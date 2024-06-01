
namespace BudgetR.Server.Application.Handlers.Accounts;
public class Add
{
    public record Request(string? AccountName, decimal? Balance, long AccountTypeId) : IRequest<Result<NoValue>>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.Balance)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.AccountTypeId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        private readonly Validator _validator = new();

        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Result.Error(validation.Errors);
            }

            Account account = new()
            {
                Name = request.AccountName,
                Balance = request.Balance.Value,
                AccountTypeId = request.AccountTypeId,
                HouseholdId = _stateContainer.HouseholdId.Value,
                ModifiedBy = _stateContainer.UserId.Value,
                BusinessTransactionActivity = new BusinessTransactionActivity
                {
                    ProcessName = "Accounts.Add",
                    UserId = _stateContainer.UserId.Value,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
