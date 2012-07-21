using System;

namespace TrianglesInSpace.Messaging
{
    public interface IBus
    {
        Action Subscribe<T>(Action<T> handler);
        void Send(IMessage message);
    }
}
