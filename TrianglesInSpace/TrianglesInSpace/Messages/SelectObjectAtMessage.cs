using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages
{
    public class SelectObjectAtMessage : IMessage
    {
        public readonly Vector Position;
        public readonly ulong Time;

        public SelectObjectAtMessage(Vector position, ulong time)
        {
            Position = position;
            Time = time;
        }
    }
}
