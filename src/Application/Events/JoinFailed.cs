namespace Application.Events;

internal record JoinFailed(string PlayerId, string Reason) : GameEvent(PlayerId);
