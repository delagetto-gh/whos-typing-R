
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IGameApp
{
    Task AddPlayerAsync(string playerId, string playerName);
    Task PlayerTypingAsync(string pid);
}

