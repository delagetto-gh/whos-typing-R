using Domain;

namespace Application.Events;

public record GameStarted(Player[] Players) : GameEvent { }
