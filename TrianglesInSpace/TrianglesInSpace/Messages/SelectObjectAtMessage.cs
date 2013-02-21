using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages
{
    public class SelectObjectAtMessage : IMessage
    {
        public readonly Vector Position;

        public SelectObjectAtMessage(Vector position)
        {
            Position = position;
        }
    }
}
