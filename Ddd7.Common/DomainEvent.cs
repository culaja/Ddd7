using Ddd7.Common.Messaging;

namespace Ddd7.Common
{
    public abstract class DomainEvent : IEvent
    {
        protected DomainEvent(Id aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }
        
        public Id AggregateRootId { get; }
    }
}