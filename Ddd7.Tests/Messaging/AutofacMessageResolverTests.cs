using System;
using System.Linq;
using Autofac;
using Ddd7.Common.Messaging;
using Ddd7.Messaging.AutofacIntegration;
using FluentAssertions;
using Xunit;
using static Ddd7.Tests.Messaging.AutofacMessageResolverTestValues;

namespace Ddd7.Tests.Messaging
{
    public sealed class AutofacMessageResolverTests
    {
        private readonly AutofacMessageResolver _autofacMessageResolver;

        public AutofacMessageResolverTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IMessageHandler<BaseMessage>>().AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IMessageHandler<FirstMessage>>().AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AssignableTo<IMessageHandler<SecondMessage>>().AsImplementedInterfaces();
            _autofacMessageResolver = new AutofacMessageResolver(builder.Build());
        }

        [Fact]
        public void first_message_is_dispatched_to_first_message_handlers()
        {
            var firstMessage = new FirstMessage();

            _autofacMessageResolver.GetMessageHandlersFor(firstMessage)
                .Select(messageHandler => messageHandler.GetType().Name)
                .Should().Contain(
                    nameof(FirstMessageHandler1),
                    nameof(FirstMessageHandler2));
        }

        [Fact]
        public void first_message_is_not_dispatcher_to_second_message_handler()
        {
            var firstMessage = new FirstMessage();

            _autofacMessageResolver.GetMessageHandlersFor(firstMessage)
                .Select(messageHandler => messageHandler.GetType().Name)
                .Should().NotContain(
                    nameof(SecondMessageHandler));
        }

        [Fact]
        public void first_message_is_dispatched_to_base_message_handlers()
        {
            var firstMessage = new FirstMessage();

            _autofacMessageResolver.GetMessageHandlersFor(firstMessage)
                .Select(messageHandler => messageHandler.GetType().Name)
                .Should().Contain(
                    nameof(BaseMessageHandler1),
                    nameof(BaseMessageHandler2));
        }
    }
}