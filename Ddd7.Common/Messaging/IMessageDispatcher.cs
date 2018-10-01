using System.Collections.Generic;

namespace Ddd7.Common.Messaging
{
    public interface IMessageDispatcher
    {
        IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages);

        IMessage Dispatch(IMessage message);
    }
}