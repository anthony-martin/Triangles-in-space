using System;
using System.Text;
using ZeroMQ;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageTransmission
    {
            
    }

    public class MessageTransmission : IMessageTransmission, IDisposable
    {
        private readonly ZmqContext m_Context;
        private ZmqSocket m_PublishSocket;
        private ZmqSocket m_SubscribeSocket;

        public MessageTransmission()
        {
            m_Context = ZmqContext.Create();

            m_PublishSocket = m_Context.CreateSocket(SocketType.PUB);
            m_SubscribeSocket = m_Context.CreateSocket(SocketType.SUB);
            //var inPoll = m_SubscribeSocket.CreatePollItem(IOMultiPlex.POLLIN);
            //inPoll.PollInHandler += OnMessageIn;
        }

        public void Connect()
        {
            m_PublishSocket.Connect("epgm://127.0.0.1:9500");
            m_SubscribeSocket.Bind("epgm://127.0.0.1:9500");
        }

        public void Dispose()
        {
            m_PublishSocket.Dispose();
            m_SubscribeSocket.Dispose();
            bool sf = true;
        }
    }
}
