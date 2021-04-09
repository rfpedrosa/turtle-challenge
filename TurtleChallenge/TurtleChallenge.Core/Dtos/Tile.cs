using Ardalis.GuardClauses;

namespace TurtleChallenge.Core.Dtos
{
    public class Tile
    {
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}