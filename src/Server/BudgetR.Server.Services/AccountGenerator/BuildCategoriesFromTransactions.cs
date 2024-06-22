using BudgetR.Core.Enums;
using BudgetR.Core.Extensions;
using BudgetR.Server.Domain.Entities;
using BudgetR.Server.Domain.Entities.Transactions;
using BudgetR.Server.Services.Transactions.Helpers;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


public class BuildCategoriesFromTransactions
{
    private readonly BudgetRDbContext _context;
    public BuildCategoriesFromTransactions(BudgetRDbContext context)
    {
        _context = context;
    }

    public async Task Build(long householdId)
    {
        try
        {
            var transaction = await _context.BeginTransactionContext();

            var TransactionBatches = new List<TransactionBatchDto>();

            string? incomingDirectoryPath = await _context.HouseholdParameters
                    .Where(p => p.HouseholdId == householdId && p.HouseholdParameterType == HouseholdParameterType.IncomingPath)
                    .Select(p => p.Value)
                    .SingleAsync();

            List<string> incomingFiles = new DirectoryInfo(incomingDirectoryPath)
                .GetFiles()
                .Where(f => f.Extension == ".csv")
                .Select(f => f.FullName)
                .ToList();

            if (incomingFiles.IsPopulated())
            {
                foreach (var fileName in incomingFiles)
                {
                    var transactionBatch = new TransactionBatchDto
                    {
                        FileName = fileName,
                        HouseholdId = householdId,
                        Source = BatchSource.Batch
                    };

                    TransactionBatches.Add(transactionBatch);
                }
            }

            List<TransactionCSVData> transactions = new();

            foreach (var batch in TransactionBatches)
            {
                transactions.AddRange(GetTransactionData(batch.FileName));
            }

            //Now find accounts 
            var count = _context.TransactionCategories
                .Where(a => a.HouseholdId == householdId)
                .Count();

            List<string> names = new();

            foreach (var t in transactions)
            {
                if (!names.Contains(t.Category))
                {
                    names.Add(t.Category);
                }
            }

            foreach (var name in names)
            {
                if (_context.TransactionCategories.Any(t => t.HouseholdId == householdId && t.CategoryName == name))
                {
                    continue;
                }
                else
                {
                    var category = new TransactionCategory
                    {
                        CategoryName = name,
                        HouseholdId = householdId
                    };

                    _context.TransactionCategories.Add(category);
                }
            }

            await _context.SaveChangesAsync();

            await _context.CommitTransactionContext(transaction);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<TransactionCSVData> GetTransactionData(string fileName)
    {
        var transactionData = new List<TransactionCSVData>();
        CsvConfiguration? csvConfig = new(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.Replace(" ", ""),
        };

        using (var reader = new StreamReader(fileName))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            var result = csv.GetRecords<TransactionCSVData>();
            transactionData.AddRange(result);
        }

        return transactionData;
    }
}
