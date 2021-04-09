using Ardalis.GuardClauses;

namespace TurtleChallenge.Core.Dtos
{
    /// <summary>
    /// Represents a movement that just happen (event).
    /// Movement may lead to game ending. If that is the case, GameResult won't be null.
    /// </summary>
    public class MovementEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequenceNumber">Increment number</param>
        /// <param name="currentPosition">Current turtle position</param>
        /// <param name="gameResult">If game ended, what is the the game result?</param>
        /// <param name="isInDanger">Is turtle in dangerous? i.e., does it have a mine in the next square giving current orientation?</param>
        public MovementEvent(
            int sequenceNumber,
            Position currentPosition,
            GameResult? gameResult = null,
            bool? isInDanger = null
            )
        {
            Guard.Against.Negative(sequenceNumber, nameof(sequenceNumber));
            SequenceNumber = sequenceNumber;

            Guard.Against.Null(currentPosition, nameof(currentPosition));
            CurrentPosition = currentPosition;

            GameResult = gameResult;
            IsInDanger = isInDanger;
        }

        public int SequenceNumber { get; }
        
        public Position CurrentPosition { get; }

        public GameResult? GameResult { get; }
        
        public bool? IsInDanger { get; }
    }
}