using BudgetR.Core;
using BudgetR.Core.Extensions;
using BudgetR.Server.Domain.Entities;
using BudgetR.Server.Services.Transactions.Helpers;
using BudgetR.Server.Services.Transactions.Steps;

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
            var steps = PopulateProcessTransactionsSteps();

            foreach (var item in steps.OrderBy(s => s.StepOrder))
            {
                var step = item.Step.Compile().Invoke();
                transactionProcessorDto = await step.Execute(transactionProcessorDto);

                if (transactionProcessorDto.HasErrors)
                {
                    throw new Exception($"{transactionProcessorDto.ErrorMessage}");
                }
            }

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

    public List<TransactionStep> PopulateProcessTransactionsSteps()
    {
        return new List<TransactionStep>
            {
                new() { Step = () => new InitializeTransactionProcess(_context, _stateContainer), StepOrder = 1 },
                new() { Step = () => new DetermineAccountId(_context, _stateContainer), StepOrder = 2 },
                new() { Step = () => new DuplicateBatchChecker(_context, _stateContainer), StepOrder = 3 },
            };
    }
}
