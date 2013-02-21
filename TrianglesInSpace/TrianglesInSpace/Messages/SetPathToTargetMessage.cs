using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages
{
    public class SetPathToTargetMessage : IMessage
    {
        public readonly Vector WorldPosition = Vector.Zero;
        public readonly ulong Time = 0;

        public SetPathToTargetMessage(Vector worldPosition, ulong time)
        {
            WorldPosition = worldPosition;
            Time = time;
        }
    }
}
