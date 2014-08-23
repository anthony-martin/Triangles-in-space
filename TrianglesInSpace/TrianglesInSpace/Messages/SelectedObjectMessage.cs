using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class SelectedObjectMessage : IMessage
    {
        public readonly string SelectedName;
        public readonly bool Owned;

        public SelectedObjectMessage(string objectName, bool owned = false)
        {
            SelectedName = objectName;
            Owned = owned;
        }
    }
}
