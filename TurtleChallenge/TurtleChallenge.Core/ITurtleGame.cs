using System.Collections.Generic;
using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Core
{
    public interface ITurtleGame
    {
        void Play(IList<MoveType> moves);
    }
}