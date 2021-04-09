using System.Collections.Generic;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.Core
{
    public interface ITurtleGame
    {
        void Play(IList<MoveType> moves);
    }
}