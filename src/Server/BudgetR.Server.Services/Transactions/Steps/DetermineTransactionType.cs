using BudgetR.Core;
using BudgetR.Core.Enums;

namespace BudgetR.Server.Services.Transactions.Steps;
public class DetermineTransactionType : TransactionStepBase
{
    public DetermineTransactionType(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        foreach (var item in transactionProcessor.TransactionBatchDto.Transactions)
        {
            if (IsTransferRecord(item.OriginalDescription))
            {
                if (item.Amount > 0)
                {
                    item.TransactionType = TransactionType.TransferIn;
                }
                else
                {
                    item.TransactionType = TransactionType.TransferOut;
                }
            }
            else if (IsInvestmentTransaction(item.OriginalDescription))
            {
                item.TransactionType = TransactionType.Investment;
            }
            else if (item.Amount > 0)
            {
                item.TransactionType = TransactionType.Income;
            }
            else if (item.Amount < 0)
            {
                item.TransactionType = TransactionType.Expense;
            }
        }
        return transactionProcessor;
    }

    private bool IsInvestmentTransaction(string originalDescription)
    {
        return originalDescription.Contains("PURCHASE: ") || originalDescription.Contains("SALE: ");
    }

    private bool IsTransferRecord(string name)
    {
        name = name.ToLower();
        return name.Contains("transfer from") || name.Contains("transfer to") || name.Contains("payment from") || name.Contains("payment to");
    }
}
