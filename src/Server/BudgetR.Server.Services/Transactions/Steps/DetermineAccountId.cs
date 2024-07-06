using BudgetR.Core;
using BudgetR.Server.Domain.Entities;

namespace BudgetR.Server.Services.Transactions.Steps;
public class DetermineAccountId : TransactionStepBase
{
    public DetermineAccountId(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        var accounts = _context.Accounts
            .Where(a => a.HouseholdId == _stateContainer.HouseholdId)
            .Select(a => new Account
            {
                AccountId = a.AccountId,
                LongName = a.LongName,
            })
            .ToList();

        foreach (var transaction in transactionProcessor.TransactionBatchDto.Transactions)
        {
            long? accountId = accounts
                .Where(a => a.LongName == transaction.AccountName)
                .Select(a => a.AccountId)
                .FirstOrDefault();

            if (accountId.HasValue && accountId > 0)
            {
                transaction.AccountId = accountId.Value;
            }
        }

        return transactionProcessor;
    }
}
