using BudgetR.Server.Handlers.PipelineBehaviors;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BudgetR.Server.Handlers;

public static class ConfigureServices
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddRequestPreProcessor<PreRequestBehavior<IRequest>>();
        });

        services.AddScoped(typeof(IRequestPreProcessor<>), typeof(PreRequestBehavior<>));
    }
}