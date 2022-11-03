using System.Threading.Tasks;

namespace Application.Abstractions;

internal interface IGameServer
{
    Task GameStarted();
    Task JoinFailed(string reason);
    Task Chosen();
    Task SomeoneTyping();
    Task Countdown(int count);
}