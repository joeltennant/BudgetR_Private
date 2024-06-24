using BudgetR.Core;

namespace BudgetR.Server.Services.Transactions.Steps;
public class DetermineAccountId : TransactionStepBase
{
    public DetermineAccountId(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        return transactionProcessor;
    }
}
