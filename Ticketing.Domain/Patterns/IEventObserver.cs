using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Domain.Patterns
{
    public interface IEventObserver
    {
        void OnEventUpdated(string message);
    }

    public interface INotifier
    {
        void Subscribe(IEventObserver observer);
        void Unsubscribe(IEventObserver observer);
        void Notify(string message);
    }
}
