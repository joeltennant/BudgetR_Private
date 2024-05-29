namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class ExpenseDetailConfiguration : IEntityTypeConfiguration<ExpenseDetail>
{
    public void Configure(EntityTypeBuilder<ExpenseDetail> builder)
    {
        builder.ToTable("ExpenseDetails",
            a => a.IsTemporal(a =>
            {
                a.UseHistoryTable("ExpenseDetailHistory");
                a.HasPeriodStart(DomainConstants.CreatedAt);
                a.HasPeriodEnd(DomainConstants.ModifiedAt);
            }));
    }
}
