using System;
using System.Collections.Generic;
namespace TrianglesInSpace.Messaging
{
    public class MessageBus : IBus
    {
        private readonly Dictionary<Type, List<Delegate>> m_Subscribers;
        private readonly object m_Lock;

        public MessageBus()
        {
            m_Lock = new object();
            m_Subscribers = new Dictionary<Type, List<Delegate>>();
        }

        public Action Subscribe<T>(Action<T> handler) 
            where T : IMessage
        {
            List<Delegate> handlers = GetHandlersForType(typeof(T));

            handlers.Add(handler);

            return () => RemoveHandler(handlers, handler); 
        }

        private void RemoveHandler(List<Delegate> handlers, Delegate handler)
        {
            handlers.Remove(handler);
        }

        public void Send(IMessage message)
        {
            List<Delegate> handlers = GetHandlersForType(message.GetType());

            foreach (var handler in handlers)
            {
                handler.DynamicInvoke(new object[] { message });
            }
        }

        private List<Delegate> GetHandlersForType(Type type)
        {
            List<Delegate> handlers;
            lock (m_Lock)
            {
                m_Subscribers.TryGetValue(type, out handlers);

                if (handlers == null)
                {
                    handlers = new List<Delegate>();
                    m_Subscribers[type] = handlers;
                }
            }
            return handlers;
        }
    }
}