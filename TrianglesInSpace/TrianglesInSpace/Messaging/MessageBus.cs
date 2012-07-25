using System;
using System.Collections.Generic;
namespace TrianglesInSpace.Messaging
{
    public class MessageBus : IBus
    {
        private readonly Dictionary<Type, Delegate> m_Subscribers;
        private readonly object m_Lock;

        public MessageBus()
        {
            m_Lock = new object();
            m_Subscribers = new Dictionary<Type, Delegate>();
        }

        public Action Subscribe<T>(Action<T> handler)
        {
            Delegate handlers;

            lock (m_Lock)
            {
                m_Subscribers.TryGetValue(typeof(T), out handlers);

                if (handlers == null)
                {
                    handlers = new Action<T>(x => { });
                }
                m_Subscribers[typeof(T)] = Delegate.Combine(handlers,handler);
            }

            return () => RemoveHandler(typeof(T), handlers, handler); 
        }

        private void RemoveHandler(Type target, Delegate source, Delegate handler)
        {
            lock (m_Lock)
            {
                m_Subscribers[target] = Delegate.Remove(source, handler);
            }
        }

        public void Send(IMessage message)
        {
            Delegate handlers;    
            lock (m_Lock)
            {
                m_Subscribers.TryGetValue(message.GetType(), out handlers);
            }

            if (handlers != null)
            {
                handlers.DynamicInvoke(new object[] { message });
            }
            
        }

    }
}