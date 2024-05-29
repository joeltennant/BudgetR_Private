namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.ToTable("AccountTypes");

        builder.HasData(
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.Cash,
                Name = "Cash",
                BalanceType = BalanceType.Debit
            },
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.Savings,
                Name = "Savings",
                BalanceType = BalanceType.Debit
            },
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.Checking,
                Name = "Checking",
                BalanceType = BalanceType.Debit
            },
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.CreditCard,
                Name = "Credit Card",
                BalanceType = BalanceType.Credit
            },
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.Investment,
                Name = "Investment",
                BalanceType = BalanceType.Debit
            },
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.Retirement,
                Name = "Retirement",
                BalanceType = BalanceType.Debit
            },
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.Loan,
                Name = "Loan",
                BalanceType = BalanceType.Credit
            },
            new AccountType
            {
                AccountTypeId = AppConstants.AccountTypes.Other,
                Name = "Other",
                BalanceType = BalanceType.Debit
            }
        );
    }
}
