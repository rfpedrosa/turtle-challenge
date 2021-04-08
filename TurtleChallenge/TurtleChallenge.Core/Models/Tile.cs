using Ardalis.GuardClauses;

namespace TurtleChallenge.Core.Models
{
    public class Tile
    {
        public Tile(int x, int y)
        {
            Guard.Against.Negative(x, nameof(x));
            Guard.Against.Negative(y, nameof(y));
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }
    }
}