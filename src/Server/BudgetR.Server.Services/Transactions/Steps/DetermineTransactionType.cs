using BudgetR.Core;

namespace BudgetR.Server.Services.Transactions.Steps;
public class DetermineTransactionType : TransactionStepBase
{
    public DetermineTransactionType(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        return transactionProcessor;
    }
}
