using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Events;
using Application.Hubs;
using Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace Infra.Services;

internal class GameEventsServiceSignalR : IGameEventsService
{
    private readonly IHubContext<GameHub, IGameServer> _gameServer;

    public GameEventsServiceSignalR(IHubContext<GameHub, IGameServer> gameServer)
    {
        _gameServer = gameServer;
    }

    public Task PublishAsync<TGameEvent>(TGameEvent @event, params string[]? pids) where TGameEvent : GameEvent
    {
        if (PublishingToSpecificPlayers(pids))
            return _gameServer.Clients.Clients(pids!).Event(@event);

        return _gameServer.Clients.All.Event(@event);
    }

    private static bool PublishingToSpecificPlayers(string[]? pids) => pids != null && pids.Any();
}

