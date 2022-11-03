using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Hubs;

[ApiController]
internal class RegistrationController : ControllerBase
{
    private readonly Game _game;

    public RegistrationController(Game game)
    {
        _game = game;
    }





    /// <summary>
    /// </summary>
    public Task GuessTyper(string playerName)
    {
        var currentPlayerId = Context.ConnectionId;
        var chosenTyperId = _game.ChosenTyper.Id;

        if (currentPlayerId != chosenTyperId)
        {
            var currPlayer = _game.Players.First(o => o.Id == currentPlayerId);
            var guessedPlayer = _game.Players.First(o => o.Name == playerName);
            _game.AddGuess(currPlayer, guessedPlayer);
        }

        return Task.CompletedTask;
    }
}


