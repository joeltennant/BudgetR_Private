using BudgetR.Server.Infrastructure.Data.BudgetR;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BudgetR.RegressionTests;

public class BudgetRDbMockContext
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<BudgetRDbContext> _contextOptions;
    private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BudgetR.Test.Data;Trusted_Connection=True;MultipleActiveResultSets=true";

    //BudgetRDbContext CreateContext() => new BudgetRDbContext(_contextOptions);

    public BudgetRDbContext CreateContext()
        => new BudgetRDbContext(
            new DbContextOptionsBuilder<BudgetRDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);
}
