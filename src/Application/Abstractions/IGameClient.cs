using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IGameClient
{
    Task Guess(string pId);
    Task Join(string pId, string pNamee);
    Task Type(string pId);
}