using System.Threading.Tasks;
using Application.Abstractions;
using Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Application;

internal class GameNotificationsServiceSignalR : IGameNotificationsService
{
    private readonly IHubContext<GameHub, IGameServer> _gameServer;

    public GameNotificationsServiceSignalR(IHubContext<GameHub, IGameServer> gameServer)
    {
        _gameServer = gameServer;
    }

    public async Task NotifyGameStarted()
    {
        await _gameServer.Clients.All.GameStarted();
    }

    public async Task NotifyPlayerJoinFailed(string message, string pid)
    {
        await _gameServer.Clients.Client(pid).JoinFailed(message);
    }

    public async Task NotifyPlayerChosen(string pid)
    {
        await _gameServer.Clients.Client(pid).Chosen();
    }

    public async Task NotifyPlayerTyping(string typingPlayerId)
    {
        await _gameServer.Clients.AllExcept(typingPlayerId).SomeoneTyping();
    }

    public async Task NotifyCountdown(int count)
    {
        await _gameServer.Clients.All.Countdown(count);
    }
}

