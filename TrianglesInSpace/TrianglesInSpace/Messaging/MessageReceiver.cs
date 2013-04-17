using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageReceiver
    {
        void Listen();
        Action AddListener(Action<string> onMessageReceived);

    }
    public class MessageReceiver : IMessageReceiver, IDisposable
    {
        private readonly IMessageContext m_Context;
        private Action<string> m_MessageHandlers;
        private ZmqSocket m_SubscribeSocket;
        private readonly object m_Lock;

        private readonly Thread m_MessageThread;

        public MessageReceiver(IMessageContext context)
        {
            m_Context = context;

            m_Lock = new object();

            m_MessageThread = new Thread(SubscribeAndWait);
            m_MessageThread.Start();
        }

        public Action AddListener(Action<string> onMessageReceived)
        {
            lock (m_Lock)
            {
                m_MessageHandlers += onMessageReceived;
            }
            return () => RemoveListener( onMessageReceived);
        }

        public void RemoveListener(Action<string> onMessageReceived)
        {
            lock (m_Lock)
            {
                m_MessageHandlers -= onMessageReceived;
            }
        }

        public void Listen()
        {
            
            m_MessageThread.Start();
        }

        public void SubscribeAndWait()
        {
            CreateReceiveSocket();
            ReceiveLoop();
        }

        public void CreateReceiveSocket()
        {
            m_SubscribeSocket = m_Context.CreateSocket(SocketType.SUB);

            m_SubscribeSocket.SubscribeAll();
            m_SubscribeSocket.Connect("epgm://239.1.1.1:9500");
            m_SubscribeSocket.Bind("tcp://*:9501");
            m_SubscribeSocket.Connect("inproc://Local");
        }

        public void ReceiveLoop()
        {
            string message = string.Empty;
            while (message != "quit")
            {
                message = Receive();
                Action<string> messageHandlers;
                lock (m_Lock)
                {
                    messageHandlers = m_MessageHandlers;
                }
                messageHandlers(message);
            }
        }

        public string Receive()
        {
            return m_SubscribeSocket.Receive(Encoding.UTF8);
        }

        public void Dispose()
        {
            if (m_SubscribeSocket != null)
            {
                m_SubscribeSocket.Dispose();
            }
            //m_MessageThread.Abort();
        }
    }
}
