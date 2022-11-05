using System;

namespace Domain.Exceptions;

public class GameFullException : Exception
{
    public GameFullException() : base("Game already has enough players.") { }
}