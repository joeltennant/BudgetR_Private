namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses",
        a => a.IsTemporal(a =>
        {
            a.UseHistoryTable("ExpenseHistory");
            a.HasPeriodStart(DomainConstants.CreatedAt);
            a.HasPeriodEnd(DomainConstants.ModifiedAt);
        }));
    }
}
