using BudgetR.Server.Services;
using System.Data.Common;

namespace BudgetR.RegressionTests;

public class BudgetRDbMockContext
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<BudgetRDbContext> _contextOptions;
    private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BudgetR.Test.Data;Trusted_Connection=True;MultipleActiveResultSets=true";

    public BudgetRDbContext? Context;

    public BudgetRDbContext CreateContext()
    {
        try
        {
            var context = new BudgetRDbContext(
            new DbContextOptionsBuilder<BudgetRDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);

            context.IsTestMode = true;

            Context = context;

            return Context;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void UpdateSystemMonths()
    {
        new UpdateMonthsService(Context).Execute();
    }
}
