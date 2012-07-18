using System;
using System.Collections.Generic;
namespace TrianglesInSpace.Messaging
{
    public class MessageBus
    {
        private readonly Dictionary<Type, List<Delegate>> m_Subscribers; 

        public MessageBus()
        {
            m_Subscribers = new Dictionary<Type, List<Delegate>>();
        }


        public Action Subscribe<T>(Action<T> handler) 
            where T : IMessage
        {
            List<Delegate> handlers;

            m_Subscribers.TryGetValue(typeof(T),out handlers);

            if(handlers == null)
            {
                handlers = new List<Delegate>();
                m_Subscribers.Add(typeof(T), handlers);
            }

            handlers.Add(handler);

            return () => RemoveHandler(handlers, handler); 
        }

        private void RemoveHandler(List<Delegate> handlers, Delegate handler)
        {
            handlers.Remove(handler);
        }
    }
}