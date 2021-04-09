using System;
using System.Collections.Generic;
using TurtleChallenge.Core.Dtos;

namespace TurtleChallenge.Core
{
    public class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<MovementEvent>> _observers;
        private readonly IObserver<MovementEvent> _observer;

        protected internal Unsubscriber(
            List<IObserver<MovementEvent>> observers,
            IObserver<MovementEvent> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null)
            {
                _observers.Remove(_observer);
            }
        }
    }
}