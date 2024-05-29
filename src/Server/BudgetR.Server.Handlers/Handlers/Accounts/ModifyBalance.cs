
namespace BudgetR.Server.Application.Handlers.Accounts;
public class ModifyBalance
{
    public record Request(long AccountId, decimal Amount) : IRequest<Result<NoValue>>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.AccountId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Amount)
                .NotNull()
                .GreaterThanOrEqualTo(0);
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

            var currentAmount = await _context.Accounts
                .Where(a => a.AccountId == request.AccountId)
                .Select(a => a.Balance)
                .FirstOrDefaultAsync();

            if (currentAmount == request.Amount)
            {
                return Result.Success();
            }

            long bta_id = await CreateBta();

            await _context.Accounts
                .Where(a => a.AccountId == request.AccountId)
                .ExecuteUpdateAsync(a => a
                            .SetProperty(p => p.BusinessTransactionActivityId, bta_id)
                            .SetProperty(p => p.Balance, request.Amount));

            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
