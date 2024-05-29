namespace BudgetR.Server.Application.Handlers.Incomes;
public class Delete
{
    public record Request(long IncomeId) : IRequest<Result<NoValue>>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.IncomeId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        private readonly Validator _validator = new();

        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Result.Error(validation.Errors);
            }

            var income = await _context.Incomes
                .Where(x => x.IncomeId == request.IncomeId
                            && x.HouseholdId == _stateContainer.HouseholdId.Value)
                .Select(x => new Income
                {
                    IncomeId = x.IncomeId,
                    Amount = x.Amount
                })
                .FirstOrDefaultAsync();

            if (income is null)
            {
                return Result.NotFound();
            }

            long BtaId = await CreateBta();

            IList<long> BudgetMonthIds = await _context.IncomeDetails
                .Where(x => x.IncomeId == income.IncomeId)
                .Select(x => x.BudgetMonthId)
                .ToListAsync();

            //delete income details
            await _context.IncomeDetails
                .Where(x => x.IncomeId == income.IncomeId)
                .ExecuteDeleteAsync();

            //delete income
            await _context.Incomes
                .Where(x => x.IncomeId == income.IncomeId)
                .ExecuteDeleteAsync();

            //now update the budget month balances
            await _context.BudgetMonths.Where(x => BudgetMonthIds.Contains(x.BudgetMonthId))
                .ExecuteUpdateAsync(x => x
                    .SetProperty(b => b.BusinessTransactionActivityId, BtaId)
                    .SetProperty(b => b.IncomeTotal, b => b.IncomeTotal - income.Amount));

            return Result.Success();
        }
    }
}
