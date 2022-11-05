using Application.Services;
using Infra.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        return services
        .AddScoped<IGameEventsService, GameEventsServiceSignalR>();
    }
}

