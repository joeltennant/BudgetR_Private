using BudgetR.Core;

namespace BudgetR.Server.Services.Transactions.Steps;
public class CompleteTransactions : TransactionStepBase
{
    public CompleteTransactions(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        var batch = transactionProcessor.TransactionBatch;

        if (true)
        {

        }

        await _context.TransactionBatches.AddAsync(batch);

        return transactionProcessor;
    }
}
