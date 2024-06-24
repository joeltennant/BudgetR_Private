using BudgetR.Core;
using BudgetR.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BudgetR.Server.Services.Transactions.Steps;
public class DuplicateBatchChecker : TransactionStepBase
{
    public DuplicateBatchChecker(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        int recordCount = transactionProcessor.TransactionBatchDto.RecordCount.Value;

        var transactionBatches = await _context.TransactionBatches
            .Where(b => b.RecordCount == recordCount
                && b.StartedAt > DateTime.Now.AddYears(-3))
            .Select(b => b.TransactionBatchId)
            .ToListAsync();

        bool isDuplicate = false;
        long? duplicateBatchId = null;

        if (transactionBatches.IsNotPopulated())
        {
            return transactionProcessor;
        }

        foreach (var batchId in transactionBatches)
        {
            var transactions = await _context.Transactions
                .Where(t => t.TransactionBatchId == batchId)
                .Select(t => new
                {
                    t.AccountId,
                    t.Description,
                    t.Amount,
                    t.TransactionDate
                })
                .ToListAsync();

            int matchCount = 0;

            foreach (var transaction in transactions)
            {
                bool hasMatch = transactionProcessor.TransactionBatchDto.Transactions
                    .Any(t =>
                        t.AccountId == transaction.AccountId
                        && t.OriginalDescription == transaction.Description
                        && t.Amount == transaction.Amount
                        && t.Date == transaction.TransactionDate);

                if (!hasMatch)
                {
                    break;
                }

                matchCount++;
            }

            if (matchCount == recordCount)
            {
                isDuplicate = true;
                duplicateBatchId = batchId;
                break;
            }
        }

        if (isDuplicate)
        {
            transactionProcessor.HasErrors = true;
            var message = $"Transaction Batch is a duplicate. File: {transactionProcessor.TransactionBatchDto.FileName}. HouseholdId: {transactionProcessor.HouseholdId}. UserId: {transactionProcessor.UserId}. Error at {DateTime.Now}.  Id of duplicate TransactionBatch: {duplicateBatchId}";
            transactionProcessor.ErrorMessage = message;
        }

        return transactionProcessor;
    }
}
