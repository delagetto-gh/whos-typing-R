using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain;

public class Game
{
    private object padlock = new object();
    private const int MAX_PLAYERS_COUNT = 2;
    private readonly Random _random;
    private readonly List<Player> _players;
    private readonly Dictionary<Player, Player> _playersGuesses;
    private bool _chosenTyperHasTyped;
    private Player? _chosenTyper;
    private Outcome? _outcome;
    private State _state;

    public Game()
    {
        _random = new Random();
        _players = new();
        _playersGuesses = new();
        _chosenTyperHasTyped = false;
        _state = State.WaitingForPlayers;
    }

    public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

    public void AddPlayer(string playerId, string playerName)
    {
        EnsureGameNotEnded();

        if (_state == State.Started)
            throw new GameAlreadyStartedException();

        if (_state == State.Ready)
            throw new GameFullException();

        lock (padlock)
        {
            if (_state == State.Ready)
                throw new GameFullException();

            _players.Add(new Player(playerId, playerName));
            if (_players.Count == MAX_PLAYERS_COUNT)
                _state = State.Ready;
        }
    }

    public Player Start()
    {
        EnsureGameNotEnded();

        if (_state == State.WaitingForPlayers)
            throw new GameNotStartedException();

        if (_state == State.Started)
            throw new GameAlreadyStartedException();

        lock (padlock)
        {
            if (_state == State.Started)
                throw new GameAlreadyStartedException();

            var rndIdx = _random.Next(MAX_PLAYERS_COUNT);
            _chosenTyper = _players[rndIdx];
            _state = State.Started;
        }

        return _chosenTyper;
    }

    public void Type(Player typer)
    {
        EnsureGameNotEnded();

        if (_state == State.WaitingForPlayers)
            throw new GameNotStartedException();

        if (_state == State.Ready)
            throw new GameNotStartedException();

        if (typer != _chosenTyper)
            throw new UnchosenPlayerTypedException();

        if (_chosenTyperHasTyped)
            return;

        lock (padlock)
        {
            if (_chosenTyperHasTyped)
                return;

            _chosenTyperHasTyped = true;
        }
    }

    public void AddGuess(Player guessingPlayer, Player guessedPlayer)
    {
        EnsureGameNotEnded();

        if (_state == State.WaitingForPlayers)
            throw new GameNotStartedException();

        if (_state == State.Ready)
            throw new GameNotStartedException();

        _playersGuesses.TryAdd(guessingPlayer, guessedPlayer); //only taking into account player's first guess.
    }

    public Outcome End()
    {
        if (_state == State.Ended)
            return _outcome!;

        if (_state != State.Started)
            throw new GameNotStartedException();

        lock (padlock)
        {
            if (_state == State.Ended)
                return _outcome!;

            var typer = _chosenTyper!;

            var playersWhoGuessedCorrectly = _playersGuesses
            .Where(o => o.Value == typer) //not making a guess is equiv to making an incorrect guess
            .Select(o => o.Key);

            var winners = playersWhoGuessedCorrectly.Any() ?
            playersWhoGuessedCorrectly.ToArray() :
            new[] { typer };

            _state = State.Ended;

            _outcome = new Outcome(winners);
            return _outcome;
        }
    }

    private void EnsureGameNotEnded()
    {
        if (_state == State.Ended)
            throw new GameAlreadyEndedException();
    }
}