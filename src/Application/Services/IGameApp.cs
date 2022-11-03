
using System.Threading.Tasks;

namespace Application;

internal interface IGameApp
{
    Task AddPlayerAsync(string pId, string pName);

    Task PlayerTypingAsync(string pId);
}

