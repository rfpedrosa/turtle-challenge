using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace TurtleChallenge.Core.Models
{
    public class GameSettings
    {
        public GameSettings(
            int boardWidth,
            int boardHeight,
            Position startingPosition,
            Tile exitPoint,
            IList<Tile> mines)
        {
            Guard.Against.Negative(boardWidth, nameof(boardWidth));
            Guard.Against.Negative(boardHeight, nameof(boardHeight));
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            
            Guard.Against.Null(startingPosition, nameof(startingPosition));
            Guard.Against.OutOfRange(
                startingPosition.Tile.X, 
                $"{nameof(startingPosition)}.{nameof(startingPosition.Tile.X)}", 
                0, 
                boardWidth);
            Guard.Against.OutOfRange(
                startingPosition.Tile.Y, 
                $"{nameof(startingPosition)}.{nameof(startingPosition.Tile.Y)}", 
                0, 
                boardHeight);
            StartingPosition = startingPosition;
            
            Guard.Against.Null(exitPoint, nameof(exitPoint));
            Guard.Against.OutOfRange(
                exitPoint.X, 
                $"{nameof(exitPoint)}.{nameof(exitPoint.X)}", 
                0, 
                boardWidth);
            Guard.Against.OutOfRange(
                exitPoint.Y, 
                $"{nameof(exitPoint)}.{nameof(exitPoint.Y)}", 
                0, 
                boardHeight);
            ExitPoint = exitPoint;
            
            Guard.Against.Null(mines, nameof(mines));
            foreach (var mine in mines)
            {
                Guard.Against.OutOfRange(
                    mine.X, 
                    $"{nameof(mine)}.{nameof(mine.X)}", 
                    0, 
                    boardWidth);
                Guard.Against.OutOfRange(
                    mine.Y, 
                    $"{nameof(mine)}.{nameof(mine.Y)}", 
                    0, 
                    boardHeight);
            }
            Mines = mines;
        }

        /// <summary>
        /// Represent the number of tiles
        /// </summary>
        public int BoardWidth  { get; }
        
        /// <summary>
        ///  Represent the number of tiles
        /// </summary>
        public int BoardHeight  { get; }
        
        public Position StartingPosition { get; }
        
        public Tile ExitPoint { get; }
        
        public IList<Tile> Mines { get; }
    }
}