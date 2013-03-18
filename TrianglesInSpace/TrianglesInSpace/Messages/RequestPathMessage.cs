using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class RequestPathMessage : IMessage
    {
        public readonly string Name;

        public RequestPathMessage(string name)
        {
            Name = name;
        }
    }
}
