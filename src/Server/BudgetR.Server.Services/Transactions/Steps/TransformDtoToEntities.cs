using BudgetR.Core;

namespace BudgetR.Server.Services.Transactions.Steps;
public class TransformDtoToEntities : TransactionStepBase
{
    public TransformDtoToEntities(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        return transactionProcessor;
    }
}
