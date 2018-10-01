namespace Ddd7.Common.Messaging
{
    public interface IMessageHandler<in T>
        where T : IMessage
    {
        Result Handle(T message);
    }

    public interface IMessageHandler
    {
        Result Handle(IMessage @message);
    }

    public abstract class MessageHandler<T> : IMessageHandler<T>, IMessageHandler
        where T : IMessage
    {
        public abstract Result Handle(T @message);

        public Result Handle(IMessage message) => Handle((T)message);
    }
}