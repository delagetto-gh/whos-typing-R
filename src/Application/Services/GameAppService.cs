using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Events;
using Domain;
using Domain.Exceptions;

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

        try
        {
            var chosenTyper = _game.Start();

            var allPlayers = _game.Players.ToArray();

            await _gameEventsService.PublishAsync(new GameStarted(allPlayers));
            await _gameEventsService.PublishAsync(new Chosen(), chosenTyper.Id);
        }
        catch (Exception) { }
    }

    public async Task PlayerTypingAsync(string typingPlayerId)
    {
        var player = _game.Players.SingleOrDefault(o => o.Id == typingPlayerId);
        if (player == default)
            return;

        try
        {
            _game.Type(player);
        }
        catch (Exception)
        {
            return;
        }

        var pidsOfAllPlayersExceptTyper = _game.Players
        .Except(new[] { player })
        .Select(o => o.Id)
        .ToArray();

        await _gameEventsService.PublishAsync(new SomeoneTyping(), pidsOfAllPlayersExceptTyper);

        int secondsCountdown = 10;
        using (var stopWatc = new PeriodicTimer(TimeSpan.FromSeconds(1)))
        {
            while (await stopWatc.WaitForNextTickAsync())
            {
                await _gameEventsService.PublishAsync(new Countdown(secondsCountdown));
                secondsCountdown--;
                if (secondsCountdown < 0)
                {
                    break;
                }
            }
        }

        // var winners = _game.Winners;
        // var whoWasTyping = _game.ChosenTyper;
        // var guesses = _game.Guesses;

        // await Clients.All.GameEnded(

        // _game.Reset();
    }
}