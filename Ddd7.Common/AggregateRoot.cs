using System.Collections.Generic;
using Ddd7.Common.Messaging;

namespace Ddd7.Common
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyList<IEvent> DomainEvents => _domainEvents;

        protected DomainEvent AddDomainEvent(DomainEvent newDomainEvent)
        {
            _domainEvents.Add(newDomainEvent);
            return newDomainEvent;
        }

        public virtual void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}