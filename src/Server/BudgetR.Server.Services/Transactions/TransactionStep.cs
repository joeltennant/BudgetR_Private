using System.Linq.Expressions;

namespace BudgetR.Server.Services.Transactions.Steps;
public class TransactionStep
{
    public Expression<Func<TransactionStepBase>> Step { get; set; }
    public int StepOrder { get; set; }
}
