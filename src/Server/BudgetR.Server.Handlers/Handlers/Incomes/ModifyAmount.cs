namespace BudgetR.Server.Application.Handlers.Incomes;
public class ModifyAmount
{
    public record Request(long IncomeId, long NewAmount) : IRequest<Result<NoValue>>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.IncomeId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.NewAmount)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
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

            long BtaId = await CreateBta();

            //get amount from Income by income id and household id as decimal
            var currentIncome = await _context.Incomes
                .Where(x => x.IncomeId == request.IncomeId
                            && x.HouseholdId == _stateContainer.HouseholdId.Value)
                .Select(x => new Income
                {
                    IncomeId = x.IncomeId,
                    Amount = x.Amount
                })
                .FirstOrDefaultAsync();

            if (currentIncome is null)
            {
                return Result.NotFound();
            }
            if (currentIncome.Amount == request.NewAmount)
            {
                return Result.Success();
            }

            IList<long> BudgetMonthIds = await _context.IncomeDetails
                 .Where(x => x.IncomeId == currentIncome.IncomeId
                             && x.Income.HouseholdId == _stateContainer.HouseholdId.Value)
                 .Select(x => x.BudgetMonthId)
                 .ToListAsync();

            await _context.Incomes
                .Where(x => x.IncomeId == currentIncome.IncomeId)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(e => e.Amount, request.NewAmount)
                    .SetProperty(e => e.BusinessTransactionActivityId, BtaId));

            decimal difference;

            //add difference if new amount is greater than current amount
            if (currentIncome.Amount < request.NewAmount)
            {
                difference = request.NewAmount - currentIncome.Amount;
            }

            //subtract difference if new amount is less than current amount
            else
            {
                difference = (currentIncome.Amount - request.NewAmount) * -1;
            }

            await _context.BudgetMonths.Where(x => BudgetMonthIds.Contains(x.BudgetMonthId))
                .ExecuteUpdateAsync(x => x
                    .SetProperty(b => b.IncomeTotal, b => b.IncomeTotal + difference)
                    .SetProperty(b => b.BusinessTransactionActivityId, BtaId));


            return Result.Success();
        }
    }
}
