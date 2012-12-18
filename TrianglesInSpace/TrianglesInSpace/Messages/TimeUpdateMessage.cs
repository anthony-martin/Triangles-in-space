using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class TimeUpdateMessage : IMessage
    {
        public readonly ulong Time;

        public TimeUpdateMessage(ulong time)
        {
            Time = time;
        }
    }
}
