using System.Threading.Tasks;

namespace Application.Abstractions;

internal interface IGameClient
{
    Task Guess(string guessedPlayer);
    Task Join(string playerName);
    Task Type();
}


