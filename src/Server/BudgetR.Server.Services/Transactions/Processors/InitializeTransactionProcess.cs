﻿using BudgetR.Server.Domain.Entities;

namespace BudgetR.Server.Services.Transactions.Processors;
public class InitializeTransactionProcess
{
    private readonly BudgetRDbContext _context;
    public InitializeTransactionProcess(BudgetRDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        transactionProcessor.BTA_ID = await CreateBta(transactionProcessor.UserId.Value, transactionProcessor.TransactionBatchDto.HouseholdId);

        transactionProcessor.TransactionBatchDto.StartedAt = DateTime.Now;
        //get record count for bath dto
        transactionProcessor.TransactionBatchDto.RecordCount = transactionProcessor.TransactionBatchDto.Transactions.Count;

        return transactionProcessor;
    }

    private async Task<long> CreateBta(long UserId, long HouseHoldId)
    {
        var bta = new BusinessTransactionActivity
        {
            ProcessName = "Process Transactions",
            UserId = UserId,
            CreatedAt = DateTime.Now
        };

        await _context.BusinessTransactionActivities.AddAsync(bta);
        await _context.SaveChangesAsync();

        return bta.BusinessTransactionActivityId;
    }
}
