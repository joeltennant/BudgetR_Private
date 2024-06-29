using BudgetR.Core;
using BudgetR.Core.Enums;
using BudgetR.Core.Extensions;
using BudgetR.Server.Domain.Entities;
using BudgetR.Server.Services.Transactions.Helpers;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BudgetR.Server.Services.AccountGenerator;
public class BuildAccountsFromTransactions
{
    private readonly BudgetRDbContext _context;
    public BuildAccountsFromTransactions(BudgetRDbContext context)
    {
        _context = context;
    }

    public async Task Build(long householdId)
    {
        try
        {
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
            else
            {
                return;
            }

            List<TransactionCSVData> transactions = new();

            foreach (var batch in TransactionBatches)
            {
                transactions.AddRange(GetTransactionData(batch.FileName));
            }

            //Now find accounts 
            var accountsCount = _context.Accounts
                .Where(a => a.HouseholdId == householdId)
                .Count();

            List<string> names = new();

            foreach (var t in transactions)
            {
                if (!names.Contains(t.AccountName))
                {
                    names.Add(t.AccountName);
                }
            }

            //create Account entity objects from list of names
            foreach (var name in names)
            {
                (BalanceType balanceType, long AccountTypeId) types = DetermineAccountTypeAndBalanceType(name);
                var account = new Account
                {
                    Name = name,
                    LongName = name,
                    AccountTypeId = types.AccountTypeId,
                    Balance = 0,
                    HouseholdId = householdId
                };

                await _context.Accounts.AddAsync(account);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private (BalanceType balanceType, long AccountTypeId) DetermineAccountTypeAndBalanceType(string name)
    {
        (BalanceType balanceType, long AccountTypeId) types = new();

        if (name.Contains("Checking"))
        {
            types.balanceType = BalanceType.Debit;
            types.AccountTypeId = AppConstants.AccountTypes.Checking;
        }
        else if (name.Contains("Savings"))
        {
            types.balanceType = BalanceType.Debit;
            types.AccountTypeId = AppConstants.AccountTypes.Savings;
        }
        else if (name.Contains("Credit"))
        {
            types.balanceType = BalanceType.Credit;
            types.AccountTypeId = AppConstants.AccountTypes.CreditCard;
        }
        else if (name.Contains("Loan"))
        {
            types.balanceType = BalanceType.Credit;
            types.AccountTypeId = AppConstants.AccountTypes.Loan;
        }
        else if (name.Contains("Investment"))
        {
            types.balanceType = BalanceType.Debit;
            types.AccountTypeId = AppConstants.AccountTypes.Investment;
        }
        else
        {
            types.balanceType = BalanceType.Debit;
            types.AccountTypeId = AppConstants.AccountTypes.Other;
        }

        return types;
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
