namespace Ddd7.Common.Messaging
{
    public abstract class EventHandler<T> : MessageHandler<T> where T : IEvent
    {
    }
}