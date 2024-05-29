namespace BudgetR.Server.Application.Handlers.Expenses;
public class Delete
{
    public record Request(long ExpenseId) : IRequest<Result<NoValue>>;

    public class Handler : BaseHandler<NoValue>, IRequestHandler<Request, Result<NoValue>>
    {
        public Handler(BudgetRDbContext dbContext, StateContainer stateContainer) : base(dbContext, stateContainer)
        {
        }

        public async Task<Result<NoValue>> Handle(Request request, CancellationToken cancellationToken)
        {
            Expense? expense = await GetExpense(request.ExpenseId);

            if (expense is not null)
            {
                long BtaId = await CreateBta();

                IList<long> BudgetMonthIds = await _context.ExpenseDetails
                    .Where(x => x.ExpenseId == expense.ExpenseId)
                    .Select(x => x.BudgetMonthId)
                    .ToListAsync();

                //delete expense details
                await _context.ExpenseDetails
                    .Where(x => x.ExpenseId == expense.ExpenseId)
                    .ExecuteDeleteAsync();

                //delete expense
                await _context.Expenses
                    .Where(x => x.ExpenseId == expense.ExpenseId)
                    .ExecuteDeleteAsync();

                //now update the budget month balances
                await _context.BudgetMonths.Where(x => BudgetMonthIds.Contains(x.BudgetMonthId))
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(b => b.BusinessTransactionActivityId, BtaId)
                        .SetProperty(b => b.ExpenseTotal, b => b.ExpenseTotal - expense.Amount));
            }

            return Result.Success();
        }

        /// <summary>
        /// Get the expense and all of BudgetMonthId's that the expense is attached too
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        private async Task<Expense?> GetExpense(long expenseId)
        {
            return await _context.Expenses
                .Where(x => x.HouseholdId == _stateContainer.HouseholdId.Value
                         && x.ExpenseId == expenseId)
                .Select(e => new Expense
                {
                    ExpenseId = e.ExpenseId,
                    Amount = e.Amount,
                })
                .FirstOrDefaultAsync();
        }
    }
}
