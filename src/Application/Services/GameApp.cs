using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application;

internal class GameApp : IGameApp
{
    private readonly Game _game;
    private readonly IGameNotificationsService _gameNotificationsService;

    public GameApp(IGameNotificationsService gameNotificationsService)
    {
        _game = new Game();
        _gameNotificationsService = gameNotificationsService;
    }

    public async Task AddPlayerAsync(string playerId, string playerName)
    {
        try
        {
            _game.AddPlayer(playerId, playerName);
        }
        catch (System.Exception e)
        {
            await _gameNotificationsService.NotifyPlayerJoinFailed(playerId, e.Message);
            return;
        }

        if (!_game.IsReady)
            return;


        var chosenTyper = _game.Start();

        await _gameNotificationsService.NotifyGameStarted();

        await _gameNotificationsService.NotifyPlayerChosen(chosenTyper.Id);
    }

    public async Task PlayerTypingAsync(string typingPlayerId)
    {
        var player = _game.Players.Where(o => o.Id == typingPlayerId);


        var currentPlayerId = Context.ConnectionId;
        var chosenTyperId = _game..Id;

        if (currentPlayerId != chosenTyperId)
            return;

        await Clients.Others.SomeoneTyping();

        int secondsCountdown = 10;
        using (var stopWatc = new PeriodicTimer(TimeSpan.FromSeconds(1)))
        {
            while (await stopWatc.WaitForNextTickAsync())
            {
                await _gameNotificationsService.NotifyCountdown(secondsCountdown);
                secondsCountdown--;
                if (secondsCountdown < 0)
                {
                    break;
                }
            }
        }

        var winners = _game.Winners;
        var whoWasTyping = _game.ChosenTyper;
        var guesses = _game.Guesses;

        await Clients.All.GameEnded(

        _game.Reset();
    }
}