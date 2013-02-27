using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class SelectedObjectMessage : IMessage
    {
        public readonly string SelectedName;

        public SelectedObjectMessage(string objectName)
        {
            SelectedName = objectName;
        }
    }
}
