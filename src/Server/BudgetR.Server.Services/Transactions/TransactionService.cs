using BudgetR.Core;
using BudgetR.Core.Extensions;
using BudgetR.Server.Domain.Entities;
using BudgetR.Server.Services.Transactions.Helpers;

namespace BudgetR.Server.Services.Transactions;
public class TransactionService
{
    private readonly BudgetRDbContext _context;
    protected readonly StateContainer _stateContainer;

    public TransactionService(BudgetRDbContext context, StateContainer stateContainer)
    {
        _context = context;
        _stateContainer = stateContainer;
    }

    public async Task LoadAndProcessTransactions()
    {
        long householdId = _stateContainer.HouseholdId.Value;
        var _fileLoader = await new FileLoaderHelper(_context).LoadFiles(householdId);

        foreach (var batch in _fileLoader.TransactionBatches)
        {
            if (batch.Transactions.IsNotPopulated())
            {
                await _fileLoader.FailFile(batch.FileName, householdId);
            }

            var result = await ProcessTransactions(batch);

            if (!result)
            {
                await _fileLoader.FailFile(batch.FileName, householdId);
            }
        }
    }

    public async Task<bool> ProcessTransactions(TransactionBatchDto batch)
    {
        var transaction = await _context.BeginTransactionContext();

        if (batch.Transactions.IsNotPopulated())
        {
            return false;
        }

        TransactionProcessorDto transactionProcessorDto = new()
        {
            TransactionBatchDto = batch,
        };

        transactionProcessorDto.UserId = _stateContainer.UserId.Value;
        transactionProcessorDto.HouseholdId = batch.HouseholdId;

        try
        {
            /* Step 1: Initialize Transaction Batch and create Bta
             * Step 2: Add Transactions Type
            * Step 2 : Connect Bank Account ID's
            * Step 3 : Add Categories
            */
            await _context.CommitTransactionContext(transaction);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.DisposeAsync();

            await _context.Logs.AddAsync(new Log
            {
                Message = $"ProcessTransactions - {ex.Message}",
                LogType = LogType.Error,
                HouseholdId = batch.HouseholdId,
                Created = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return false;
        }



    }

    public void ReProcessTransactions()
    {

    }


}
