using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Core
{
    public class TurtleGame : ITurtleGame
    {
        private List<IObserver<MovementInfo>> observers;

        private readonly GameSettings _gameSettings;

        private Position _currentPosition;

        public static TurtleGame NewGame(GameSettings gameSettings)
        {
            return new(gameSettings);
        }

        public TurtleGame(GameSettings gameSettings)
        {
            Guard.Against.Null(gameSettings, nameof(gameSettings));
            

            _gameSettings = gameSettings;
            

            _currentPosition = gameSettings.StartingPosition;

            observers = new List<IObserver<MovementInfo>>();
        }

        /// <summary>
        /// Play a turtle game
        /// </summary>
        /// <returns>true if turtle can escape; false otherwise</returns>
        public void Play(IList<MoveType> moves)
        {
            Guard.Against.Null(moves, nameof(moves));
            foreach (var move in moves)
            {
                switch (move)
                {
                    case MoveType.Forward:
                        Move();
                        break;
                    case MoveType.Rotate:
                        Rotate();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Move()
        {
            var nextTile = GetNextTile();
            Guard.Against.OutOfRange(
                nextTile.X,
                $"{nameof(nextTile)}.{nameof(nextTile.X)}",
                0,
                _gameSettings.BoardWidth);
            Guard.Against.OutOfRange(
                nextTile.Y,
                $"{nameof(nextTile)}.{nameof(nextTile.Y)}",
                0,
                _gameSettings.BoardWidth);

            // ok. we have a valid movement

            _currentPosition = new Position(nextTile, _currentPosition.Direction);
        }

        private void Rotate()
        {
            var nextDirection = GetNextDirection();
            _currentPosition = new Position(_currentPosition.Tile, nextDirection);
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
                Direction.East => new Tile(_currentPosition.Tile.X + 1, _currentPosition.Tile.Y - 1),
                Direction.West => new Tile(_currentPosition.Tile.X - 1, _currentPosition.Tile.Y - 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}