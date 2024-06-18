namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class BusinessTransactionActivityConfiguration : IEntityTypeConfiguration<BusinessTransactionActivity>
{
    public void Configure(EntityTypeBuilder<BusinessTransactionActivity> builder)
    {
        builder
            .HasOne(b => b.User)
            .WithOne(u => u.BusinessTransactionActivity)
            .HasForeignKey<BusinessTransactionActivity>(f => f.UserId)
            .IsRequired(false);

        builder.HasData
            (
                new BusinessTransactionActivity
                {
                    BusinessTransactionActivityId = 1,
                    ProcessName = "Initial Seeding",
                    CreatedAt = DateTime.Now,
                    UserId = 1
                }
            );
    }
}
