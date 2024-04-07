namespace BudgetR.Core.Models;
public class BalanceSheetModel
{
    public IList<AccountModel>? Accounts { get; set; }

    public IList<AccountModel>? DebitAccounts => Accounts?.Where(x => x.BalanceType == BalanceType.Debit).ToList();

    public IList<AccountModel>? CreditAccounts => Accounts?.Where(x => x.BalanceType == BalanceType.Credit).ToList();

    //Helpers

    public decimal TotalDebit()
    {
        return DebitAccounts?.Sum(x => x.BalanceWithSign) ?? 0;
    }

    public decimal TotalCredit()
    {
        return CreditAccounts?.Sum(x => x.BalanceWithSign) ?? 0;
    }

    public decimal Total()
    {
        return TotalDebit() - TotalCredit();
    }
}
