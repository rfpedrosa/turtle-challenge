using System.Collections.Generic;
using System.Threading.Tasks;
using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Console
{
    /// <summary>
    /// An input parser abstraction
    /// </summary>
    /*
     * Allow code to be easily extend with other parsers in the future by decoupling the parser implementation
     * (like Json or Csv).
     * Follows 'S', 'O' & 'I' of SOLID principles 
     */
    public interface IParser
    {
        Task<GameSettings> LoadGameSettings(string filePath);

        Task<IList<MoveType>> LoadMoves(string filePath);
    }
}