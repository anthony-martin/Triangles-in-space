using ZeroMQ;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageContext
    {
        ZmqSocket CreateSocket(SocketType socketType);
    }
}
