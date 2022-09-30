using System;

namespace DomainArchitecture.Infrastructure.Data.Entities {

    /// <summary>
    /// Base domain entity which can publish events
    /// </summary>
    public abstract class Entity : HasId {
        public int Id { get; set; }
        private List<object> _events { get; }

        protected Entity() {
            _events = new List<object>();
        }

        protected internal void Publish(object domainEvent) {
            _events.Add(domainEvent);
        }

        internal IEnumerable<object> GetEvents() {
            return _events.ToList();
        }
    }
}