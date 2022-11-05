using System;

namespace Domain.Exceptions;

public class GameNotStartedException : Exception
{
    public GameNotStartedException() : base("Game has not started.") { }
}