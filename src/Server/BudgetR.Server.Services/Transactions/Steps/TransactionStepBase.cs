using BudgetR.Core;

namespace BudgetR.Server.Services.Transactions.Steps;
public abstract class TransactionStepBase
{
    protected readonly BudgetRDbContext _context;
    protected readonly StateContainer _stateContainer;

    protected TransactionStepBase(BudgetRDbContext context, StateContainer stateContainer)
    {
        _context = context;
        _stateContainer = stateContainer;
    }

    public abstract Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor);
}
