namespace BudgetR.Server.Application.Handlers.Incomes;
public class RemoveIncomeDetail
{
    public record Request(long IncomeDetailId) : IRequest<Result<NoValue>>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.IncomeDetailId)
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

            var incomeDetail = await _context.IncomeDetails
                 .Where(x => x.IncomeDetailId == request.IncomeDetailId
                             && x.Income.HouseholdId == _stateContainer.HouseholdId.Value)
                 .Select(x => new IncomeDetail
                 {
                     IncomeDetailId = x.IncomeDetailId,
                     BudgetMonthId = x.BudgetMonthId,
                     Income = new Income
                     {
                         Amount = x.Income.Amount
                     }
                 })
                 .FirstOrDefaultAsync();

            if (incomeDetail is null)
            {
                return Result.NotFound();
            }

            long BtaId = await CreateBta();

            await _context.IncomeDetails
                    .Where(x => x.IncomeDetailId == incomeDetail.IncomeDetailId)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(e => e.BusinessTransactionActivityId, BtaId));

            await _context.IncomeDetails
                .Where(x => x.IncomeDetailId == incomeDetail.IncomeDetailId)
                .ExecuteDeleteAsync();

            await _context.BudgetMonths
                .Where(x => x.BudgetMonthId == incomeDetail.BudgetMonthId)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(b => b.BusinessTransactionActivityId, BtaId)
                    .SetProperty(b => b.IncomeTotal, b => b.IncomeTotal - incomeDetail.Income.Amount));

            return Result.Success();
        }
    }
}
