using System;

namespace Domain.Exceptions;

public class GameAlreadyEndedException : Exception
{
    public GameAlreadyEndedException() : base("Game has ended.") { }
}