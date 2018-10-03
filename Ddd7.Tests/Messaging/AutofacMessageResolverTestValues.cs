using Ddd7.Common;
using Ddd7.Common.Messaging;
using static Ddd7.Common.Result;

namespace Ddd7.Tests.Messaging
{
    public static class AutofacMessageResolverTestValues
    {
        public abstract class BaseMessage : IMessage
        {
        }

        public sealed class BaseMessageHandler1 : MessageHandler<BaseMessage>
        {
            public override Result Handle(BaseMessage message) => Ok();
        }

        public sealed class BaseMessageHandler2 : MessageHandler<BaseMessage>
        {
            public override Result Handle(BaseMessage message) => Ok();
        }

        public sealed class FirstMessage : BaseMessage
        {
        }

        public sealed class FirstMessageHandler1 : MessageHandler<FirstMessage>
        {
            public override Result Handle(FirstMessage message) => Ok();
        }

        public sealed class FirstMessageHandler2 : MessageHandler<FirstMessage>
        {
            public override Result Handle(FirstMessage message) => Ok();
        }

        public sealed class SecondMessage : BaseMessage
        {
        }

        public sealed class SecondMessageHandler : MessageHandler<SecondMessage>
        {
            public override Result Handle(SecondMessage message) => Ok();
        }
    }
}