using System;
using System.Collections.Generic;
using System.Linq;

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
        State = State.WaitingForPlayers;
    }

    public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

    public State State { get; private set; }

    public void AddPlayer(string playerId, string playerName)
    {
        EnsureGameNotEnded();

        if (State == State.Started)
            throw new Exception("Game has already started.");

        if (State == State.Ready)
            throw new Exception("Game already has enough players.");

        lock (padlock)
        {
            if (State != State.WaitingForPlayers)
                throw new Exception("Game already has enough players.");

            _players.Add(new Player(playerId, playerName));
            if (_players.Count == MAX_PLAYERS_COUNT)
                State = State.Ready;
        }
    }

    public void AddGuess(Player guessingPlayer, Player guessedPlayer)
    {
        EnsureGameNotEnded();

        if (State != State.Started)
            throw new Exception("Game has not started.");

        if (!_players.Contains(guessingPlayer) || !_players.Contains(guessedPlayer))
            throw new Exception("Invalid player(s).");

        _playersGuesses.TryAdd(guessingPlayer, guessedPlayer); //only taking into account player's first guess.
    }

    public Player Start()
    {
        EnsureGameNotEnded();

        if (State == State.Started)
            throw new Exception("Game has already started.");

        if (State != State.Ready)
            throw new Exception("Game is not ready. There are not enough players yet.");

        lock (padlock)
        {
            if (State == State.Started)
                throw new Exception("Game has already started.");

            var rndIdx = _random.Next(MAX_PLAYERS_COUNT + 1);
            _chosenTyper = _players[rndIdx];
            State = State.Started;
        }

        return _chosenTyper;
    }

    public Outcome End()
    {
        if (State != State.Started)
            throw new Exception("Game must started in order to end it.");

        lock (padlock)
        {
            EnsureGameNotEnded();

            var typer = _chosenTyper!;

            var playersWhoGuessedCorrectly = _playersGuesses
            .Where(o => o.Value == typer) //not making a guess is equiv to making an incorrect guess
            .Select(o => o.Key);

            var winners = playersWhoGuessedCorrectly.Any() ?
            playersWhoGuessedCorrectly.ToArray() :
            new[] { typer };

            State = State.Ended;

            return new Outcome(winners);
        }
    }

    private void EnsureGameNotEnded()
    {
        if (State == State.Ended)
            throw new Exception("Game has already ended.");
    }
}