namespace BudgetR.RegressionTests.Comparers;
public class ExpenseComparer
{
    public Expense? Expense { get; set; }
    public BudgetRDbContext? Context { get; set; }

    public ExpenseComparer(BudgetRDbContext context)
    {
        Context = context;
    }

    public async Task<ExpenseComparer> GetExpense(long? expenseId)
    {
        Expense = Context?.Expenses
            .Include(e => e.ExpenseDetails)
            .FirstOrDefault(e => e.ExpenseId == expenseId);

        return this;
    }
}
