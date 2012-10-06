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
            
            
            m_PublishSocket.Bind("epgm://239.1.1.1:9500");
            m_PublishSocket.Bind("tcp://*:9501");
            m_SubscribeSocket.SubscribeAll();
            m_SubscribeSocket.Connect("epgm://239.1.1.1:9500");
        }

        public void Send(string message)
        {
            m_PublishSocket.Send(message, Encoding.UTF8);
        }

        public string Recieve()
        {
            var bytes = new byte[100];
            var size = m_SubscribeSocket.Receive(bytes, TimeSpan.FromSeconds(1));
            if(size > 0)
            {
                return Encoding.UTF8.GetString(bytes, 0, size);
            }
            return string.Empty;
        }

        public void Dispose()
        {
            m_PublishSocket.Dispose();
            m_SubscribeSocket.Dispose();
            m_Context.Dispose();
        }
    }
}
