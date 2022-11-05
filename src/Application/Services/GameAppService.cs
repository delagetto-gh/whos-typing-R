using System;
using System.Threading.Tasks;
using Application.Events;
using Domain;

namespace Application.Services;

internal class GameAppService : IGameAppService
{
    private readonly Game _game;
    private readonly IGameEventsService _gameEventsService;

    public GameAppService(Game game, IGameEventsService gameEventsService)
    {
        _game = game;
        _gameEventsService = gameEventsService;
    }

    public async Task AddPlayerAsync(string playerId, string playerName)
    {
        try
        {
            _game.AddPlayer(playerId, playerName);
        }
        catch (Exception e)
        {
            await _gameEventsService.PublishAsync(new JoinFailed(e.Message), playerId);
            return;
        }

        if (!_game.IsReady)
            return;

        var chosenTyper = _game.Start();

        await _gameEventsService.PublishAsync(new GameStarted());

        await _gameEventsService.PublishAsync(new Chosen(), chosenTyper.Id);
    }

    public Task PlayerTypingAsync(string typingPlayerId)
    {
        throw new NotImplementedException();
        // var player = _game.Players.Where(o => o.Id == typingPlayerId);


        // var currentPlayerId = Context.ConnectionId;
        // var chosenTyperId = _game..Id;

        // if (currentPlayerId != chosenTyperId)
        //     return;

        // await Clients.Others.SomeoneTyping();

        // int secondsCountdown = 10;
        // using (var stopWatc = new PeriodicTimer(TimeSpan.FromSeconds(1)))
        // {
        //     while (await stopWatc.WaitForNextTickAsync())
        //     {
        //         await _gameEventsService.NotifyCountdown(secondsCountdown);
        //         secondsCountdown--;
        //         if (secondsCountdown < 0)
        //         {
        //             break;
        //         }
        //     }
        // }

        // var winners = _game.Winners;
        // var whoWasTyping = _game.ChosenTyper;
        // var guesses = _game.Guesses;

        // await Clients.All.GameEnded(

        // _game.Reset();
    }
}