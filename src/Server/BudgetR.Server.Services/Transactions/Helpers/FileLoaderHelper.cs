using BudgetR.Server.Domain.Entities.Transactions;

namespace BudgetR.Server.Services.Transactions.Helpers;
public class FileLoaderHelper
{
    public List<string>? FileNames { get; set; }
    public List<TransactionBatch>? TransactionBatches { get; set; }
    private readonly BudgetRDbContext _context;

    public FileLoaderHelper(BudgetRDbContext context)
    {
        FileNames = new();
        TransactionBatches = new();
        _context = context;
    }

    /// <summary>
    /// Load files for transactions
    /// </summary>
    /// <returns></returns>
    public FileLoaderHelper LoadFiles(long HouseholdId)
    {
        string incomingDirectory = "C:\\Users\\joel_\\OneDrive\\Desktop\\TransactionAnalysis\\Incoming";
        var directory = new DirectoryInfo("C:\\Users\\joel_\\Downloads");
        foreach (var fileName in FileNames)
        {
            //var transactionBatch = new TransactionBatch();
            //transactionBatch.LoadTransactions(fileName);
            //TransactionBatches.Add(transactionBatch);
        }

        return this;
    }
}
