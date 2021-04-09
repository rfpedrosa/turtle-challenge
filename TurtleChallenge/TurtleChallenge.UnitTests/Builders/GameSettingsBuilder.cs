using System.Collections.Generic;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.UnitTests.Builders
{
    public class GameSettingsBuilder
    {
        public GameSettingsBuilder()
        {
        }

        private int BoardWidth { get; set; }
        
        private int BoardHeight { get; set; }
        
        private Position StartPosition { get; set; }
        
        private Tile ExitPoint { get; set; }

        private IList<Tile> Mines  { get; set; }
        
        public GameSettingsBuilder WithBoardWidth(int boardWidth)
        {
            BoardWidth = boardWidth;
            return this;
        }
        
        public GameSettingsBuilder WithBoardHeight(int boardHeight)
        {
            BoardHeight = boardHeight;
            return this;
        }
        
        public GameSettingsBuilder WithStartPosition(Position position)
        {
            StartPosition = position;
            return this;
        }
        
        public GameSettingsBuilder WithExitPoint(Tile tile)
        {
            ExitPoint = tile;
            return this;
        }
        
        public GameSettingsBuilder WithMines(IList<Tile> mines)
        {
            Mines = mines;
            return this;
        }
        
        public GameSettings Build()
        {
            return new(BoardWidth, BoardHeight, StartPosition, ExitPoint, Mines);
        }

        public GameSettings WithExampleInput()
        {
            return
                WithBoardWidth(5)
                    .WithBoardHeight(4)
                    .WithStartPosition(new Position(new Tile(0, 1), Direction.North))
                    .WithExitPoint(new Tile(4, 2))
                    .WithMines(new List<Tile>
                    {
                        new(1, 1),
                        new(3,1),
                        new(3,3)
                    })
                    .Build();
        }
    }
}