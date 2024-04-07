namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class HouseholdConfiguration : IEntityTypeConfiguration<Household>
{
    public void Configure(EntityTypeBuilder<Household> builder)
    {
        builder.ToTable("Households",
            a => a.IsTemporal
            (
                a =>
                {
                    a.UseHistoryTable("HouseholdHistory");
                    a.HasPeriodStart(DomainConstants.CreatedAt);
                    a.HasPeriodEnd(DomainConstants.ModifiedAt);
                }));
    }
}
