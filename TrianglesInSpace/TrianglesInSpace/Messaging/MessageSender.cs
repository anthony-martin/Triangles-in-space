using System;
using System.Text;
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

        public MessageSender(ZmqContext context)
        {
            m_Context = context;

            m_PublishSocket = m_Context.CreateSocket(SocketType.PUB);
            m_PublishSocket.Bind("epgm://239.1.1.1:9500");
            m_PublishSocket.Bind("tcp://*:9501");
            m_PublishSocket.Bind("inproc://Local");
        }

        public void Send(string message)
        {
            m_PublishSocket.Send(message, Encoding.UTF8);
        }

        public void Dispose()
        {
            m_PublishSocket.Dispose();
        }
    }
}
