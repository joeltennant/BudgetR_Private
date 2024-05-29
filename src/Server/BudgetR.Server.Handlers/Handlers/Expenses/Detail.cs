namespace BudgetR.Server.Application.Handlers.Expenses;
public class Detail
{
    public record Request(long? ExpenseId) : IRequest<Result<ExpenseModel>>;

    public class Handler : BaseHandler<ExpenseModel>, IRequestHandler<Request, Result<ExpenseModel>>
    {
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<ExpenseModel>> Handle(Request request, CancellationToken cancellationToken)
        {
            ExpenseModel? expense = await _context.Expenses
                .Where(x => x.HouseholdId == _stateContainer.HouseholdId
                            && x.ExpenseId == request.ExpenseId)
                .Select(x => new ExpenseModel
                {
                    ExpenseId = x.ExpenseId,
                    Name = x.Name,
                    Amount = x.Amount,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();

            if (expense is not null)
            {
                IList<ExpenseDetailModel> expenseDetailModels = _context.ExpenseDetails
                    .Where(x => x.ExpenseId == expense.ExpenseId)
                    .Select(x => new ExpenseDetailModel
                    {
                        ExpenseDetailId = x.ExpenseDetailId,
                        BudgetMonthId = x.BudgetMonthId,
                        Month = x.BudgetMonth.MonthYear.Month,
                        Year = x.BudgetMonth.MonthYear.Year
                    })
                    .ToList();

                expense.ExpenseDetails = expenseDetailModels;
            }

            return Result.Success(expense);
        }
    }
}
