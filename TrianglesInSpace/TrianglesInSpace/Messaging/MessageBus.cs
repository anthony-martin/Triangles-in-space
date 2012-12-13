using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;

namespace TrianglesInSpace.Messaging
{
    public class MessageBus : IBus, IDisposable
    {
        private readonly Dictionary<Type, Delegate> m_Subscribers;
        private readonly object m_Lock;

        private readonly MessageSerialiser m_MessageSerialiser;
        private readonly IMessageSender m_MessageSender;
        private readonly IMessageReceiver m_MessageReceiver;

        private readonly ConcurrentQueue<IMessage> m_Messages;

        private readonly Disposer m_Disposer;

        /// <summary>
        /// Create a new bus that will use the sender and reciever provided communication outside of the buses context
        /// </summary>
        /// <param name="messageSender">Shared isntance of the sender</param>
        /// <param name="messageReceiver">Shared instance of the receiver</param>
        public MessageBus(IMessageSender messageSender,
                          IMessageReceiver messageReceiver)
        {
            m_Lock = new object();
            m_Subscribers = new Dictionary<Type, Delegate>();

            m_MessageSerialiser = new MessageSerialiser();
            m_MessageSerialiser.Register(typeof(SetPathToTarget));
            m_MessageSerialiser.Register(typeof(PathMessage));
            m_MessageSerialiser.Register(typeof(RequestPathMessage));
            m_MessageSender = messageSender;
            m_MessageReceiver = messageReceiver;

            m_Disposer = new Disposer();

            m_MessageReceiver.AddListener(ReceiveFromRemote).AddTo(m_Disposer);

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

            return () => RemoveHandler(typeof(T), handler); 
        }

        private void RemoveHandler(Type target, Delegate handler)
        {
            lock (m_Lock)
            {
                Delegate handlers;
                m_Subscribers.TryGetValue(target, out handlers);

                m_Subscribers[target] = Delegate.Remove(handlers, handler);
                
            }
        }

        public void Send(IMessage message)
        {
            SendRemote(message);
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
            m_Disposer.Dispose();
        }
    }
}