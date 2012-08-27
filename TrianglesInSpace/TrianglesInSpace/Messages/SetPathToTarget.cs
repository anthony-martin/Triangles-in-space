using Mogre;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class SetPathToTarget : IMessage
    {
        public readonly Vector2 WorldPosition = Vector2.ZERO;
        public readonly ulong Time = 0;

        public SetPathToTarget(Vector2 worldPosition, ulong time)
        {
            WorldPosition = worldPosition;
            Time = time;
        }
    }
}
