using System;

namespace Domain.Exceptions;

public class GameAlreadyStartedException : Exception
{
    public GameAlreadyStartedException() : base("Game has already started.") { }
}