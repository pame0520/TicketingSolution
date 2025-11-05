
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ticketing.Domain.Patterns
{
    public class Notifier : INotifier
    {
        private readonly List<IEventObserver> _observers = new();
        private readonly object _sync = new();

        public void Subscribe(IEventObserver observer)
        {
            if (observer == null) return;
            lock (_sync)
            {
                if (!_observers.Contains(observer))
                    _observers.Add(observer);
            }
        }

        public void Unsubscribe(IEventObserver observer)
        {
            if (observer == null) return;
            lock (_sync)
            {
                _observers.Remove(observer);
            }
        }

        public void Notify(string message)
        {
            List<IEventObserver> copy;
            lock (_sync) { copy = _observers.ToList(); }

            foreach (var obs in copy)
            {
                try { obs.OnEventUpdated(message); }
                catch { /* ignorar observer con errores */ }
            }
        }
    }
}