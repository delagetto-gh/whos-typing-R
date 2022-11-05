using Application.Hubs;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services
       .AddSignalR();

        services
        .AddSingleton<Game>()
        .AddScoped<IGameAppService, GameAppService>();


        services.AddCors();

        return services;
    }

    public static TApp UseApp<TApp>(this TApp app) where TApp : IApplicationBuilder, IEndpointRouteBuilder
    {
        app.UseCors();
        app.MapHub<GameHub>(GameHub.Route);

        return app;
    }
}

