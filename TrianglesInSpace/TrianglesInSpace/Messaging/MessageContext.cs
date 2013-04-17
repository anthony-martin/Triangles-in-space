using System;
using ZeroMQ;

namespace TrianglesInSpace.Messaging
{
    //wrapper to allow for injection of the context
    public class MessageContext : IMessageContext, IDisposable
    {
        private readonly ZmqContext m_Context;

        public MessageContext()
        {
            m_Context = ZmqContext.Create();
        }

        public ZmqSocket CreateSocket(SocketType socketType)
        {
            return m_Context.CreateSocket(socketType);
        }

        public void Dispose()
        {
            m_Context.Dispose(); 
        }
    }
}
