using BudgetR.Core;

namespace BudgetR.Server.Services.Transactions.Steps;
/// <summary>
/// Flag problem transactions
/// 1. Duplicate transactions
/// 2. Transactions with no account
/// 3. Transactions with no category
/// </summary>
public class FlagTransactions : TransactionStepBase
{
    public FlagTransactions(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        return transactionProcessor;
    }
}
