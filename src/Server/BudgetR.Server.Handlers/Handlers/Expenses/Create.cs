using System.Data;

namespace BudgetR.Server.Application.Handlers.Expenses;

public static class Create
{

    public class Request : IRequest<Result<long?>>
    {
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public IEnumerable<long>? BudgetMonths { get; set; }
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

    public class Handler : BaseHandler<long?>, IRequestHandler<Request, Result<long?>>
    {
        private readonly Validator _validator = new();

        public Handler(BudgetRDbContext context, StateContainer stateContainer)
            : base(context, stateContainer)
        {
        }

        public async Task<Result<long?>> Handle(Request request, CancellationToken cancellationToken)
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

                var expense = new Expense
                {
                    Name = request.Name,
                    Amount = request.Amount.Value,
                    HouseholdId = _stateContainer.HouseholdId.Value,
                    IsActive = true,
                    BusinessTransactionActivityId = BtaId,
                };

                if (request.BudgetMonths.IsPopulated())
                {
                    expense.ExpenseDetails = BuildExpenseDetails(request.BudgetMonths.ToList());
                }

                await _context.Expenses.AddAsync(expense);
                await _context.SaveChangesAsync();

                if (request.BudgetMonths.IsPopulated())
                {
                    await UpdateBudgetMonthExpenseTotals(request.BudgetMonths.ToList(), request.Amount, BtaId);
                }

                await _context.CommitTransactionContext(transaction);
                return Result.Success(expense.ExpenseId);
            }
            catch (Exception ex)
            {
                return Result.SystemError(ex.Message);
            }
        }

        /// <summary>
        /// Each month that the expense is applied to needs to have the expense total updated.
        /// </summary>
        /// <param name="budgetMonths"></param>
        /// <param name="amount"></param>
        /// <param name="BtaId"></param>
        /// <returns></returns>
        private async Task UpdateBudgetMonthExpenseTotals(IList<long> budgetMonths, decimal? amount, long BtaId)
        {
            await _context.BudgetMonths
                    .Where(x => budgetMonths.Contains(x.BudgetMonthId))
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(b => b.BusinessTransactionActivityId, BtaId)
                        .SetProperty(b => b.ExpenseTotal, b => b.ExpenseTotal + amount));
        }

        /// <summary>
        /// Build the expense details for each month that the expense is applied too.
        /// </summary>
        /// <param name="budgetMonths"></param>
        /// <returns></returns>
        private List<ExpenseDetail>? BuildExpenseDetails(IList<long>? budgetMonths)
        {
            var expenseDetails = new List<ExpenseDetail>();

            foreach (var month in budgetMonths)
            {
                expenseDetails.Add(new ExpenseDetail
                {
                    BudgetMonthId = month,
                    BusinessTransactionActivityId = _stateContainer.BtaId,
                    IsActive = true
                });
            }

            return expenseDetails;
        }
    }
}
