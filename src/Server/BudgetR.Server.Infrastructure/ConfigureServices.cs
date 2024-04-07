using BudgetR.Server.Infrastructure.Data.Authentication;
using BudgetR.Server.Infrastructure.Data.BudgetR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetR.Server.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BudgetRDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        var connectionString = configuration.GetConnectionString("AuthConnection") ?? throw new InvalidOperationException("Connection string 'AuthConnection' not found.");
        services.AddDbContext<AuthenticationDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
