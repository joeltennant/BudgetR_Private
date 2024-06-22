namespace BudgetR.Server.Services.Transactions.Processors;
public class DuplicateBatchChecker
{
    private readonly BudgetRDbContext _context;
    public DuplicateBatchChecker(BudgetRDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        //TODO
        // check for duplicate batch
        //determine how to check for duplicate batches
        //need to find a batch that has a similar set of date

        return transactionProcessor;
    }
}
