using System;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.UnitTests.Observers
{
    // Observable pattern -> Observer implementation based on
    // https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-an-observer
    public sealed class LastMovementObserver : IObserver<MovementEvent>
    {
        private IDisposable _unsubscriber;
        
        public MovementEvent LastMovement { get; set; }

        public void Subscribe(IObservable<MovementEvent> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(MovementEvent e)
        {
            LastMovement = e;
        }
    }
}