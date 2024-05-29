namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Incomes",
                       a => a.IsTemporal(a =>
                       {
                           a.UseHistoryTable("IncomeHistory");
                           a.HasPeriodStart(DomainConstants.CreatedAt);
                           a.HasPeriodEnd(DomainConstants.ModifiedAt);
                       }));
    }
}
