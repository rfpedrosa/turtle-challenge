using System.Collections.Generic;

namespace TurtleChallenge.Core.Models
{
    public class GameSettings
    {
        /// <summary>
        /// Represent the number of tiles
        /// </summary>
        public int BoardWidth  { get; set; }
        
        /// <summary>
        ///  Represent the number of tiles
        /// </summary>
        public int BoardHeight  { get; set; }
        
        public Position StartingPosition { get; set; }
        
        public Tile ExitPoint { get; set; }
        
        public IList<Tile> Mines { get; set; }
    }
}