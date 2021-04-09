using System;
using System.Text;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.Console.Observers
{
    // Observable pattern -> Observer implementation based on
    // https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-an-observer
    public sealed class GameReporter : IObserver<MovementEvent>
    {
        private IDisposable _unsubscriber;

        public void Subscribe(IObservable<MovementEvent> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }
        
        /*protected virtual void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }*/
        
        public void OnCompleted()
        {
            System.Console.WriteLine("Game ended.");
        }

        public void OnError(Exception error)
        {
            System.Console.WriteLine($"Oops. An error happen:\n{error}");
        }

        public void OnNext(MovementEvent e)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Sequence {e.SequenceNumber}: ");
            if (e.GameResult != null)
            {
                stringBuilder.Append(e.GameResult == GameResult.Died ? "Turtle died :(" : "Turtle escaped! :)");
            }
            else
            {
                stringBuilder.Append($"success. Turtle is now at position: {e.CurrentPosition}");
                stringBuilder.Append(e.IsInDanger.HasValue && e.IsInDanger.Value ? " but it is in danger!" : " and is not in danger :)");
            }
            System.Console.WriteLine(stringBuilder.ToString());
        }
    }
}