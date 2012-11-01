using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageSender: IDisposable
    {
        void Send(string message);
    }

    public class MessageSender : IMessageSender
    {
        private readonly ZmqContext m_Context;
        private ZmqSocket m_PublishSocket;
        private ConcurrentQueue<string> m_MessageBuffer;  

        private readonly Thread m_MessageThread;

        public MessageSender(ZmqContext context)
        {
            m_Context = context;
            m_MessageBuffer = new ConcurrentQueue<string>();

            m_MessageThread = new Thread(OnThreadStarted);
            m_MessageThread.Start();
        }

        public void OnThreadStarted()
        {
            InitialiseSocket();
            SendLoop();
        }

        public void InitialiseSocket()
        {
            m_PublishSocket = m_Context.CreateSocket(SocketType.PUB);
            m_PublishSocket.Bind("epgm://239.1.1.1:9500");
            m_PublishSocket.Bind("tcp://*:9500");
            m_PublishSocket.Bind("inproc://Local");
        }

        public void SendLoop()
        {
            string message;
            do
            {
                message = string.Empty;
                m_MessageBuffer.TryDequeue(out message);
                if(!string.IsNullOrEmpty(message))
                {
                    m_PublishSocket.Send(Encoding.UTF8.GetBytes(message));
                }
            } while (message != "quit");
        }

        public void Send(string message)
        {
            m_MessageBuffer.Enqueue(message);
        }

        public void Dispose()
        {
            if (m_PublishSocket != null)
            {
                m_PublishSocket.Dispose();
            }
            m_MessageThread.Abort();
        }
    }
}
