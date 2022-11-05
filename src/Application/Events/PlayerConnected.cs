namespace Application.Events;

public record PlayerConnected(string PId) : GameEvent { };