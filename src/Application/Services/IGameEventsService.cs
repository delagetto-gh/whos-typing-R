using System.Threading.Tasks;
using Application.Events;

namespace Application.Services;

public interface IGameEventsService
{
    Task PublishAsync<TGameEvent>(TGameEvent @event, params string[]? pids) where TGameEvent : GameEvent;
}