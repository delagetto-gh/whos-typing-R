using System.Linq;
using System.Threading.Tasks;
using Application.Events;
using Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Application.Services;

internal class GameEventService : IGameEventsService
{
    private readonly IHubContext<GameHub> _gameEventsHub;

    public GameEventService(IHubContext<GameHub> gameEventsHub)
    {
        _gameEventsHub = gameEventsHub;
    }

    public async Task PublishAsync(GameEvent @event)
    {
        var eventName = @event.GetType().Name;
        var explicitRecepients = @event.To?.ToList() ?? Enumerable.Empty<string>();

        if (explicitRecepients.Any())
            await _gameEventsHub.Clients.Clients(explicitRecepients).SendAsync(eventName, @event);
        else
            await _gameEventsHub.Clients.All.SendAsync(eventName, @event);
    }
}
