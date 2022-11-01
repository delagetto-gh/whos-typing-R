using System;

namespace Domain
{
    [Flags]
    public enum GameState
    {
        WaitingForPlayers = 0,
        Ready = 1,
        Started = 2,
        Ended = 4
    }
}