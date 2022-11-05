using System.Threading.Tasks;
using Application.Events;

namespace Application.Abstractions;

public interface IGameServer
{
    Task Event<TGameEvent>(TGameEvent @event) where TGameEvent : GameEvent;
}
