
using System.Threading.Tasks;

namespace Application;

internal interface IGameApplication
{
    Task AddPlayerAsync(string playerId, string playerName);
}

