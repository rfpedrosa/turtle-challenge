using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.Core
{
    // Observable pattern -> Provider implementation based on
    // https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-a-provider
    public class TurtleGame : ITurtleGame, IObservable<MovementEvent>
    {
        public static TurtleGame NewGame(GameSettings gameSettings)
        {
            return new(gameSettings);
        }

        private readonly GameSettings _gameSettings;

        private Position _currentPosition;

        private readonly List<IObserver<MovementEvent>> _observers;

        private TurtleGame(GameSettings gameSettings)
        {
            Guard.Against.Null(gameSettings, nameof(gameSettings));
            _gameSettings = gameSettings;

            _currentPosition = gameSettings.StartingPosition;

            _observers = new List<IObserver<MovementEvent>>();
        }

        public IDisposable Subscribe(IObserver<MovementEvent> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        /// <summary>
        /// Play a turtle game
        /// </summary>
        /// <returns>true if turtle can escape; false otherwise</returns>
        public void Play(IList<MoveType> moves)
        {
            Guard.Against.Null(moves, nameof(moves));
            for (var i = 0; i < moves.Count; i++)
            {
                switch (moves[i])
                {
                    case MoveType.Forward:
                        Move(i);
                        break;
                    case MoveType.Rotate:
                        Rotate(i);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Move(int sequenceNumber)
        {
            var nextTile = GetNextTile();
            var position = new Position(nextTile, _currentPosition.Direction);

            CheckMovement(sequenceNumber, position);
        }

        private void Rotate(int sequenceNumber)
        {
            var nextDirection = GetNextDirection();
            var position = new Position(_currentPosition.Tile, nextDirection);

            CheckMovement(sequenceNumber, position);
        }

        private void CheckMovement(int sequenceNumber, Position position)
        {
            var isMine = IsMine(position.Tile);
            if (isMine)
            {
                _currentPosition = position;
                OnNewMovement(new MovementEvent(sequenceNumber, _currentPosition, GameResult.Died));
                OnGameComplete();
                return;
            }

            var isOutOfBounds = IsOutOfBounds(position.Tile);
            
            if (isOutOfBounds && IsAtExitPosition(_currentPosition.Tile))
            {
                _currentPosition = position;
                OnNewMovement(new MovementEvent(sequenceNumber, _currentPosition, GameResult.Escaped));
                OnGameComplete();
                return;
            }

            if(isOutOfBounds)
            {
                OnError(
                    new ArgumentOutOfRangeException(nameof(position), $"Invalid movement: {position.Tile} is out of bounds"));
                return;
            }

            // ok. we have a valid movement
            _currentPosition = position;
            OnNewMovement(new MovementEvent(sequenceNumber, _currentPosition, null, IsInDanger()));
        }

        private bool IsMine(Tile tile)
        {
            return _gameSettings.Mines.Any(m => m.X == tile.X && m.Y == tile.Y);
        }

        private bool IsOutOfBounds(Tile tile)
        {
            return tile.X < 0 || tile.X >= _gameSettings.BoardWidth
                                  || tile.Y < 0 || tile.Y >= _gameSettings.BoardHeight;
        }
        
        private bool IsAtExitPosition(Tile tile)
        {
            return tile.X == _gameSettings.ExitPoint.X && tile.Y == _gameSettings.ExitPoint.Y;
        }
        
        private bool IsInDanger()
        {
            var nextTile = GetNextTile();
            return IsMine(nextTile);
        }

        private Direction GetNextDirection()
        {
            return _currentPosition.Direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Tile GetNextTile()
        {
            return _currentPosition.Direction switch
            {
                Direction.North => new Tile(_currentPosition.Tile.X, _currentPosition.Tile.Y - 1),
                Direction.South => new Tile(_currentPosition.Tile.X, _currentPosition.Tile.Y + 1),
                Direction.East => new Tile(_currentPosition.Tile.X + 1, _currentPosition.Tile.Y),
                Direction.West => new Tile(_currentPosition.Tile.X - 1, _currentPosition.Tile.Y),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void OnNewMovement(MovementEvent movementEvent)
        {
            foreach (var observer in _observers.ToArray())
            {
                observer?.OnNext(movementEvent);
            }
        }
        
        private void OnError(Exception ex)
        {
            foreach (var observer in _observers.ToArray())
            {
                observer?.OnError(ex);
            }
        }

        private void OnGameComplete()
        {
            foreach (var observer in _observers.ToArray())
            {
                observer?.OnCompleted();
            }

            _observers.Clear();
        }
    }
}