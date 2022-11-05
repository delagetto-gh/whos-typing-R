using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IGameClient
{
    Task Guess(string pId, string guessedPId);
    Task Join(string pId, string name);
    Task Type(string pId);
}