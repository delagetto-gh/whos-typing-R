using System.Threading.Tasks;
using Application.Abstractions;
using Application.Events;
using Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public class GameHub : Hub<IGameServer>, IGameClient
{
    public static readonly string Route = "/whostyping";

    private readonly IGameAppService _gameApp;

    public GameHub(IGameAppService gameApp)
    {
        _gameApp = gameApp;
    }

    public override async Task OnConnectedAsync()
    {
        var pid = Context.ConnectionId;
        await Clients.Caller.Event(new PlayerConnected(pid));
    }

    public Task Guess(string guessedPlayer)
    {
        throw new System.NotImplementedException();
    }

    public async Task Join(string pId, string playerName)
    {
        await _gameApp.AddPlayerAsync(pId, playerName);
    }

    public async Task Type(string pId)
    {
        await _gameApp.PlayerTypingAsync(pId);
    }

    // /// <summary>
    // /// Allows a player to participate in a game session
    // /// by registering their name.
    // /// </summary>
    // public async Task Register(string playerName)
    // {
    //     try
    //     {
    //         var pId = Context.ConnectionId; //our "player id" for singlarR purposes
    //         var pNm = playerName;
    //         _game.RegisterPlayer(pId, pNm);
    //     }
    //     catch (Exception e) when (e.Message.Contains("Game already has enough players"))
    //     {
    //         await Clients.Caller.RegisterFailed("A game is already in progress.");
    //         return;
    //     }

    //     if (_game.IsStarted)
    //     {
    //         var players = _game.Players.Select(o => o.Name).ToArray();

    //         var chosenTyper = _game.ChosenTyper;

    //         await Clients.All.GameStarted(players);

    //         await Clients
    //         .Client(chosenTyper.Id)
    //         .Chosen();
    //     }
    //     else
    //     {
    //         await Clients.Caller.Waiting();
    //     }
    // }

    // /// <summary>
    // /// Allows the chosen player to begin typing to
    // /// which intiates the game round.
    // /// </summary>
    // public async Task Type()
    // {
    //     var currentPlayerId = Context.ConnectionId;
    //     var chosenTyperId = _game.ChosenTyper.Id;

    //     if (currentPlayerId != chosenTyperId)
    //         return;

    //     await Clients.Others.SomeoneTyping();

    //     int secondsCountdown = 10;
    //     using (var stopWatc = new PeriodicTimer(TimeSpan.FromSeconds(1)))
    //     {
    //         while (await stopWatc.WaitForNextTickAsync())
    //         {
    //             await Clients.All.CountdownTick(secondsCountdown);
    //             secondsCountdown--;
    //             if (secondsCountdown < 0)
    //             {
    //                 break;
    //             }
    //         }
    //     }

    //     var winners = _game.Winners;
    //     var whoWasTyping = _game.ChosenTyper;
    //     var guesses = _game.Guesses;

    //     await Clients.All.GameEnded(

    //     _game.Reset();
    // }


    // /// <summary>
    // /// </summary>
    // public Task GuessTyper(string playerName)
    // {
    //     var currentPlayerId = Context.ConnectionId;
    //     var chosenTyperId = _game.ChosenTyper.Id;

    //     if (currentPlayerId != chosenTyperId)
    //     {
    //         var currPlayer = _game.Players.First(o => o.Id == currentPlayerId);
    //         var guessedPlayer = _game.Players.First(o => o.Name == playerName);
    //         _game.AddGuess(currPlayer, guessedPlayer);
    //     }

    //     return Task.CompletedTask;
    // }
}


