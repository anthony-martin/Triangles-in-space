using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class DeselectedObjectMessage : IMessage
    {
        public readonly string DeselectedName;

        public DeselectedObjectMessage(string deselectedName)
        {
            DeselectedName = deselectedName;
        }
    }
}
