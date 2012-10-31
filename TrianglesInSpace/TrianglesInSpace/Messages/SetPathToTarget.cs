using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages
{
    public class SetPathToTarget : IMessage
    {
        public readonly Vector WorldPosition = Vector.Zero;
        public readonly ulong Time = 0;

        public SetPathToTarget(Vector worldPosition, ulong time)
        {
            WorldPosition = worldPosition;
            Time = time;
        }
    }
}
