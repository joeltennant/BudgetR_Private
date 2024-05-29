namespace BudgetR.Server.Application.Handlers.Incomes;
public class Detail
{
    public record Request(long IncomeId) : IRequest<Result<IncomeModel>>;

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

    public class Handler : BaseHandler<IncomeModel>, IRequestHandler<Request, Result<IncomeModel>>
    {
        private readonly Validator _validator = new();

        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<IncomeModel>> Handle(Request request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Result.Error(validation.Errors);
            }

            //get incomes by household id

            var incomes = await _context.Incomes
                .Where(x => x.IncomeId == request.IncomeId
                                           && x.HouseholdId == _stateContainer.HouseholdId.Value)
                .Select(x => new IncomeModel
                {
                    IncomeId = x.IncomeId,
                    Amount = x.Amount,
                    IncomeDetails = x.IncomeDetails.Select(y => new IncomeDetailModel
                    {
                        IncomeDetailId = y.IncomeDetailId,
                        BudgetMonthId = y.BudgetMonthId,
                        Month = y.BudgetMonth.MonthYear.Month,
                        Year = y.BudgetMonth.MonthYear.Year,
                        IsActive = y.IsActive,
                    })
                    .OrderBy(y => y.Year)
                    .ThenBy(y => y.Month)
                    .ToList()
                })
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            return incomes is null ? Result.NotFound() : Result.Success(incomes);
        }
    }
}
