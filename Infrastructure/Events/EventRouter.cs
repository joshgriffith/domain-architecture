using System.Reflection;

namespace DomainArchitecture.Infrastructure.Events
{
    public class EventRouter
    {
        private readonly IsObserver[] _observers;
        private readonly MethodInfo _internalPublish;

        public EventRouter(params IsObserver[] observers)
        {
            _observers = observers;
            _internalPublish = GetType().GetMethod(nameof(InternalPublish), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public void Publish(object domainEvent)
        {
            _internalPublish.MakeGenericMethod(domainEvent.GetType()).Invoke(this, new[] { domainEvent });
        }

        private void InternalPublish<T>(T domainEvent)
        {
            foreach (var observer in _observers)
            {
                if (observer is IsObserver<T> eventObserver)
                {
                    eventObserver.Handle(domainEvent);
                }
            }
        }
    }
}