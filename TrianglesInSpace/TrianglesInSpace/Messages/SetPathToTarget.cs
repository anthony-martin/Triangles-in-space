using Mogre;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class SetPathToTarget : IMessage
    {
        public readonly Vector2 WorldPosition;
        public readonly ulong Time;

        public SetPathToTarget(Vector2 worldPosition, ulong time)
        {
            WorldPosition = worldPosition;
            Time = time;
        }
    }
}
