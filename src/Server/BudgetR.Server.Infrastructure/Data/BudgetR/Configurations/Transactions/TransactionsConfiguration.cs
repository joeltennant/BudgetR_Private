
namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations.Transactions;
internal class TransactionsConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions",
            a => a.IsTemporal
            (
                a =>
                {
                    a.UseHistoryTable("TransactionHistory");
                    a.HasPeriodStart(DomainConstants.CreatedAt);
                    a.HasPeriodEnd(DomainConstants.ModifiedAt);
                }));
    }
}
