using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageReceiver
    {
        void Listen(Action<string> onMessageReceived);
    }
    public class MessageReceiver : IMessageReceiver, IDisposable
    {
        private readonly ZmqContext m_Context;
        private Action<string> m_MessageReceived;
        private ZmqSocket m_SubscribeSocket;

        private readonly Thread m_MessageThread;

        public MessageReceiver(ZmqContext context)
        {
            m_Context = context;
            
            m_MessageThread = new Thread(CreateReceiveSocket);
        }

        public void Listen(Action<string> onMessageReceived)
        {
            m_MessageReceived = onMessageReceived;
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
            m_SubscribeSocket.Connect("inproc://Local");
        }

        public void ReceiveLoop()
        {
            string message = string.Empty;
            while (message != "quit")
            {
                message = Receive();
                m_MessageReceived(message);
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
            m_MessageThread.Abort();
        }
    }
}
