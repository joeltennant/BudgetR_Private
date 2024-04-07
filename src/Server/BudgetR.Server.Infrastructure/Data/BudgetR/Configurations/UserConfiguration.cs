namespace BudgetR.Server.Infrastructure.Data.BudgetR.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users",
                        a => a.IsTemporal(
                                  a =>
                                  {
                                      a.HasPeriodStart(DomainConstants.CreatedAt);
                                      a.HasPeriodEnd(DomainConstants.ModifiedAt);
                                      a.UseHistoryTable("UserHistory");
                                  }));

        builder.HasData
            (
                new User//system user
                {
                    UserId = 1,
                    AuthenticationId = string.Empty,
                    FirstName = "System",
                    HouseholdId = null,
                    UserType = UserType.System,
                    BtaId = 1
                }
            );
    }
}
