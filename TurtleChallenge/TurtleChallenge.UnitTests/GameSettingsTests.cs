using System;
using System.Collections.Generic;
using TurtleChallenge.Core.Dtos;
using TurtleChallenge.UnitTests.Builders;
using Xunit;

namespace TurtleChallenge.UnitTests
{
    // this class can be extended with more unit tests similar to the one I implemented as an example
    public class GameSettingsTests
    {
        [Fact]
        public void StartingPositionMustBeInsideOfBoard()
        {
            // prepare
            var builder = new GameSettingsBuilder()
                .WithBoardWidth(5)
                .WithBoardHeight(4)
                .WithStartPosition(new Position(new Tile(0, 5), Direction.North))
                .WithExitPoint(new Tile(4, 2))
                .WithMines(new List<Tile>
                {
                    new(1, 1),
                    new(3, 1),
                    new(3, 3)
                });

            // act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => builder.Build());
            
            // assert
            Assert.NotNull(exception);
        }
    }
}