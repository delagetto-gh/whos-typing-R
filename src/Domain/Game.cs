using System;
using System.Collections.Generic;

namespace Domain;

public class Game
{
    private object padlock = new object();
    private const int MAX_PLAYERS_COUNT = 2;
    private readonly Random _random;
    private readonly List<Player> _players;
    private readonly Dictionary<Player, Player> _playersGuesses;
    private Player? _chosenTyper;

    public Game()
    {
        _random = new Random();
        _players = new();
        _playersGuesses = new();
        State = GameState.WaitingForPlayers;
    }

    public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

    public GameState State { get; private set; }

    public void AddPlayer(string playerId, string playerName)
    {
        EnsureGameNotEnded();

        if (State == GameState.Started)
            throw new Exception("Game has already started.");

        if (State == GameState.Ready)
            throw new Exception("Game already has enough players.");

        lock (padlock)
        {
            if (State != GameState.WaitingForPlayers)
                throw new Exception("Game already has enough players.");

            _players.Add(new Player(playerId, playerName));
            if (_players.Count == MAX_PLAYERS_COUNT)
                State = GameState.Ready;
        }
    }

    public void AddGuess(Player guessingPlayer, Player guessedPlayer)
    {
        EnsureGameNotEnded();

        if (State != GameState.Started)
            throw new Exception("Game has not started.");

        if (State == GameState.WaitingForPlayers)
            throw new Exception("Game has already started.");

        if (State == GameState.Started)
            throw new Exception("Game has already started.");

        if (!_players.Contains(guessingPlayer) || !_players.Contains(guessedPlayer))
            throw new Exception("Invalid player");

        _playersGuesses.TryAdd(guessingPlayer, guessedPlayer); //only taking into account player's first guess.
    }

    public Result End()
    {
        if (State == GameState.Ended)
            throw new Exception("Game has ended.");
    }

    public Player Start()
    {
        var rndIdx = _random.Next(MAX_PLAYERS_COUNT + 1);
        _chosenTyper = _players[rndIdx];
    }

    private void EnsureGameNotEnded()
    {
        if (State == GameState.Ended)
            throw new Exception("Game has ended.");
    }

    private void EnsureGameNotStarted()
    {
        if (State == GameState.Started)
            throw new Exception("Game has already started.");
    }
}