namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class IncomeDetailConfiguration : IEntityTypeConfiguration<IncomeDetail>
{
    public void Configure(EntityTypeBuilder<IncomeDetail> builder)
    {
        builder.ToTable("IncomeDetails",
           a => a.IsTemporal
           (
               a =>
               {
                   a.UseHistoryTable("IncomeDetailHistory");
                   a.HasPeriodStart(DomainConstants.CreatedAt);
                   a.HasPeriodEnd(DomainConstants.ModifiedAt);
               }));
    }
}

