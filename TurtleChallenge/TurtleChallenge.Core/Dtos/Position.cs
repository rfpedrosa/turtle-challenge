using Ardalis.GuardClauses;

namespace TurtleChallenge.Core.Dtos
{
    public class Position
    {
        public Position(Tile tile, Direction direction)
        {
            Guard.Against.Null(tile, nameof(tile));
            Guard.Against.Null(direction, nameof(direction));
            
            Tile = tile;
            Direction = direction;
        }

        public Tile Tile { get; }
        
        public Direction Direction { get; }
        
        public override string ToString()
        {
            return $"Tile:{Tile} - Direction:{Direction}";
        }
    }
}