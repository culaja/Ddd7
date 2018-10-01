using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Ddd7.Common.Messaging;
using Module = Autofac.Module;

namespace Ddd7.Messaging.AutofacIntegration
{
    public sealed class AutofacMessagingRegistrator : Module
    {
        private readonly IReadOnlyList<Assembly> _assembliesWithMessageTypes;
        private readonly Assembly[] _assembliesWithMessageHandlerTypes;

        public AutofacMessagingRegistrator(
            IReadOnlyList<Assembly> assembliesWithMessageTypes,
            IReadOnlyList<Assembly> assembliesWithMessageHandlerTypes)
        {
            _assembliesWithMessageTypes = assembliesWithMessageTypes;
            _assembliesWithMessageHandlerTypes = assembliesWithMessageHandlerTypes.ToArray();
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacMessageDispatcher>().As<IMessageDispatcher>().SingleInstance();
            RegisterMessageHandlersForAllMessages(builder);
        }

        private void RegisterMessageHandlersForAllMessages(ContainerBuilder builder)
        {
            var allMessageTypes =_assembliesWithMessageTypes
                .SelectMany(assembly => assembly.GetTypes())
                .Where(typeof(IMessage).IsAssignableFrom)
                .Select(messageType => RegisterMessageHandlersForMessageType(builder, messageType))
                .ToList();
        }

        private Type RegisterMessageHandlersForMessageType(ContainerBuilder builder, Type messageType)
        {
            builder.RegisterAssemblyTypes(_assembliesWithMessageHandlerTypes)
                .AssignableTo(typeof(IMessageHandler<>).MakeGenericType(messageType))
                .AsImplementedInterfaces();
            return messageType;
        }
    }
}