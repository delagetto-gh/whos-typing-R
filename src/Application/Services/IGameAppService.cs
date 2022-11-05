using System.Threading.Tasks;

namespace Application.Services;

public interface IGameAppService
{
    Task AddPlayerAsync(string playerId, string playerName);
    Task PlayerTypingAsync(string pid);
}

