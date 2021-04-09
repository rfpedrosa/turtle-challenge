using System.Collections.Generic;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Dtos;
using TurtleChallenge.UnitTests.Builders;
using TurtleChallenge.UnitTests.Observers;
using Xunit;

namespace TurtleChallenge.UnitTests
{
    public class TurtleGameTests
    {
        private readonly GameSettings _gameSettings = new GameSettingsBuilder().WithExampleInput();

        [Fact]
        public void TurtleIsSafe()
        {
            // prepare
            var game = TurtleGame.NewGame(_gameSettings);
            var observer = new LastMovementObserver();
            observer.Subscribe(game);
            
            // act
            game.Play(new List<MoveType>
            {
                MoveType.Forward
            });
            
            // assert
            Assert.NotNull(observer.LastMovement);
            Assert.False(observer.LastMovement.IsInDanger);
        }
        
        [Fact]
        public void TurtleIsInDanger()
        {
            // prepare
            var game = TurtleGame.NewGame(_gameSettings);
            var observer = new LastMovementObserver();
            observer.Subscribe(game);
            
            // act
            game.Play(new List<MoveType>
            {
                MoveType.Rotate
            });
            
            // assert
            Assert.NotNull(observer.LastMovement);
            Assert.True(observer.LastMovement.IsInDanger);
        }
        
        [Fact]
        public void TurtleDied()
        {
            // prepare
            var game = TurtleGame.NewGame(_gameSettings);
            var observer = new LastMovementObserver();
            observer.Subscribe(game);
            
            // act
            game.Play(new List<MoveType>
            {
                MoveType.Rotate,
                MoveType.Forward
            });
            
            // assert
            Assert.NotNull(observer.LastMovement);
            Assert.NotNull(observer.LastMovement.GameResult);
            Assert.Equal(GameResult.Died, observer.LastMovement.GameResult);
        }
        
        [Fact]
        public void TurtleEscaped()
        {
            // prepare
            var game = TurtleGame.NewGame(_gameSettings);
            var observer = new LastMovementObserver();
            observer.Subscribe(game);
            
            // act
            game.Play(new List<MoveType>
            {
                MoveType.Forward,
                MoveType.Rotate,
                MoveType.Forward,
                MoveType.Forward,
                MoveType.Forward,
                MoveType.Forward,
                MoveType.Rotate,
                MoveType.Forward,
                MoveType.Forward,
                MoveType.Rotate,
                MoveType.Rotate,
                MoveType.Rotate,
                MoveType.Forward
            });
            
            // assert
            Assert.NotNull(observer.LastMovement);
            Assert.NotNull(observer.LastMovement.GameResult);
            Assert.Equal(GameResult.Escaped, observer.LastMovement.GameResult);
        }
    }
}