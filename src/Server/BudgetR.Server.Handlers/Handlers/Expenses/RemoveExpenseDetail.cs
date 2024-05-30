namespace BudgetR.Server.Application.Handlers.Expenses;
public class RemoveExpenseDetail
{

    public record Request(long ExpenseDetailId) : IRequest<Result<NoValue>>;

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        private int result;
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            ExpenseDetail? expenseDetail = await _context.ExpenseDetails
                .Where(e => e.ExpenseDetailId == request.ExpenseDetailId
                            && e.Expense.HouseholdId == _stateContainer.HouseholdId)
                .Select(e => new ExpenseDetail
                {
                    ExpenseDetailId = e.ExpenseDetailId,
                    BudgetMonthId = e.BudgetMonthId,
                    Expense = new Expense
                    {
                        Amount = e.Expense.Amount
                    }
                })
                .FirstOrDefaultAsync();

            if (expenseDetail is not null)
            {
                long BtaId = await CreateBta();

                await _context.ExpenseDetails
                    .Where(x => x.ExpenseDetailId == expenseDetail.ExpenseDetailId)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(e => e.BusinessTransactionActivityId, BtaId));

                result = await _context.ExpenseDetails
                    .Where(x => x.ExpenseDetailId == expenseDetail.ExpenseDetailId)
                    .ExecuteDeleteAsync();

                await _context.BudgetMonths
                    .Where(x => x.BudgetMonthId == expenseDetail.BudgetMonthId)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(b => b.BusinessTransactionActivityId, BtaId)
                        .SetProperty(b => b.ExpenseTotal, b => b.ExpenseTotal - expenseDetail.Expense.Amount));
            }

            return result == 1 ? Result.Success() : Result.Error(null);
        }
    }
}
