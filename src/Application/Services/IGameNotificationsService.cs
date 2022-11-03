using System.Threading.Tasks;

namespace Application
{
    internal interface IGameNotificationsService
    {
        Task NotifyPlayerJoinFailed(string playerId, string reason);
        Task NotifyPlayerChosen(string playerId);
        Task NotifyPlayerTyping(string typingPlayerId);
        Task NotifyGameStarted();
        Task NotifyCountdown(int count);
    }
}