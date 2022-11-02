
using System.Threading.Tasks;
using Application.Events;
using Application.Services;
using Domain;

namespace Application;

internal class GameApplication : IGameApplication
{
    private readonly Game _game;
    private readonly IGameEventsService _gameEventsService;

    public GameApplication(IGameEventsService gameEventsService)
    {
        _game = new Game();
        _gameEventsService = gameEventsService;
    }

    public async Task AddPlayerAsync(string playerId, string playerName)
    {
        try
        {
            _game.AddPlayer(playerId, playerName);
        }
        catch (System.Exception e)
        {
            await _gameEventsService.PublishAsync(new JoinFailed(playerId, e.Message));
            return;
        }
        
        if (_game.State == State.Ready)
            await _gameEventsService.PublishAsync(new GameReady());
    }
}