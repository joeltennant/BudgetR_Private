namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts",
            a => a.IsTemporal
            (
                a =>
                {
                    a.UseHistoryTable("AccountHistory");
                    a.HasPeriodStart(DomainConstants.CreatedAt);
                    a.HasPeriodEnd(DomainConstants.ModifiedAt);
                })
            .HasCheckConstraint("CK_Account_Balance", "[Balance] >= 0"));
    }
}
