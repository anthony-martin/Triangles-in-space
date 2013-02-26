using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages
{
    public class SelectObjectAtMessage : IMessage
    {
        public readonly Vector WorldPosition;
        public readonly ulong Time;

        public SelectObjectAtMessage(Vector worldPosition, ulong time)
        {
            WorldPosition = worldPosition;
            Time = time;
        }
    }
}
