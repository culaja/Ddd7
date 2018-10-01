namespace Ddd7.Common.Messaging
{
    public interface IEvent : IMessage
    {
        Id AggregateRootId { get; }
    }
}