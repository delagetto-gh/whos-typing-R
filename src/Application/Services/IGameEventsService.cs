using System.Threading.Tasks;
using Application.Events;

namespace Application.Services
{
    internal interface IGameEventsService
    {
        Task PublishAsync(GameEvent @event);
    }
}