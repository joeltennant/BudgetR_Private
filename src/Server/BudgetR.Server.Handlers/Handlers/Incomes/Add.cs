namespace BudgetR.Server.Application.Handlers.Incomes;
public class Add
{
    public class Request : IRequest<Result<NoValue>>
    {
        public string Name { get; init; }
        public decimal Amount { get; init; }
        public MonthSelection MonthSelection { get; set; }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.Amount)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        private readonly Validator _validator = new();
        private BuildMonthListFromSelection _buildMonthListFromSelection { get; set; }

        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
            _buildMonthListFromSelection = new BuildMonthListFromSelection(dbContext);
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            transaction = await _context.BeginTransactionContext();

            try
            {

                var validation = await _validator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    return Result.Error(validation.Errors);
                }

                long BtaId = await CreateBta();

                var income = new Income
                {
                    HouseholdId = (long)_stateContainer.HouseholdId,
                    Name = request.Name,
                    Amount = request.Amount,
                    IsActive = true,
                    BusinessTransactionActivityId = BtaId
                };

                List<long>? budgetMonths = await _buildMonthListFromSelection
                        .BuildBudgetMonthListFromSelection(request.MonthSelection);

                if (budgetMonths == null)
                {
                    return Result.Error();
                }

                income.IncomeDetails = BuildIncomeDetails(budgetMonths);

                await _context.Incomes.AddAsync(income);
                await _context.SaveChangesAsync(cancellationToken);

                if (budgetMonths.IsPopulated())
                {
                    await UpdateBudgetMonthIncomeTotals(budgetMonths, request.Amount, BtaId);
                }

                await _context.CommitTransactionContext(transaction);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.SystemError(ex.Message);
            }
        }

        private List<IncomeDetail>? BuildIncomeDetails(IList<long>? budgetMonths)
        {
            var incomeDetails = new List<IncomeDetail>();

            foreach (var month in budgetMonths)
            {
                incomeDetails.Add(new IncomeDetail
                {
                    BudgetMonthId = month,
                    BusinessTransactionActivityId = _stateContainer.BtaId,
                    IsActive = true
                });
            }

            return incomeDetails;
        }

        /// <summary>
        /// Each month that the income is applied to needs to have the income total updated.
        /// </summary>
        /// <param name="budgetMonths"></param>
        /// <param name="amount"></param>
        /// <param name="BtaId"></param>
        /// <returns></returns>
        private async Task UpdateBudgetMonthIncomeTotals(IList<long> budgetMonths, decimal? amount, long BtaId)
        {
            await _context.BudgetMonths
                    .Where(x => budgetMonths.Contains(x.BudgetMonthId))
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(b => b.BusinessTransactionActivityId, BtaId)
                        .SetProperty(b => b.IncomeTotal, b => b.IncomeTotal + amount));
        }
    }
}
