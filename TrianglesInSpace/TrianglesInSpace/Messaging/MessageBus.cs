using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ZeroMQ;

namespace TrianglesInSpace.Messaging
{
    public class MessageBus : IBus, IDisposable
    {
        private readonly Dictionary<Type, Delegate> m_Subscribers;
        private readonly object m_Lock;

        private readonly MessageSerialiser m_MessageSerialiser;
        private readonly MessageSender m_MessageSender;
        private readonly MessageReceiver m_MessageReceiver;

        private readonly ConcurrentQueue<IMessage> m_Messages; 

        public MessageBus(ZmqContext messageContext)
        {
            m_Lock = new object();
            m_Subscribers = new Dictionary<Type, Delegate>();

            m_MessageSerialiser = new MessageSerialiser();
            m_MessageSender = new MessageSender(messageContext);
            m_MessageReceiver = new MessageReceiver(messageContext, ReceiveFromRemote);

            m_Messages = new ConcurrentQueue<IMessage>();
        }

        public Action Subscribe<T>(Action<T> handler)
        {
            Delegate handlers;

            lock (m_Lock)
            {
                m_Subscribers.TryGetValue(typeof(T), out handlers);

                if (handlers == null)
                {
                    m_Subscribers[typeof (T)] = handler;
                }
                else
                {
                    m_Subscribers[typeof(T)] = Delegate.Combine(handlers, handler);
                }
                
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
            SendRemote(message);
            m_Messages.Enqueue(message);
        }

        public void SendRemote(IMessage message)
        {
            var serialisedMessage = m_MessageSerialiser.Serialise(message);
            m_MessageSender.Send(serialisedMessage);
        }

        public void ReceiveFromRemote(string jsonMessage)
        {
            m_Messages.Enqueue(m_MessageSerialiser.Deserialise(jsonMessage));
        }

        public void ProcessMessages()
        {
            IMessage message;
            do
            {
                message = null;
                m_Messages.TryDequeue(out message);

                if(message != null)
                {
                    SendLocal(message);
                }
            } while (message != null);
        }

        public void SendLocal(IMessage message)
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

        public void Dispose()
        {
            m_MessageSender.Dispose();
            m_MessageReceiver.Dispose();
        }
    }
}