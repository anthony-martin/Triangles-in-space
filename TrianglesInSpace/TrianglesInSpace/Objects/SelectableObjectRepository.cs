using System.Collections.Generic;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;

namespace TrianglesInSpace.Objects
{
    public class SelectableObjectRepository
    {
        private readonly IBus m_Bus;
        private readonly Dictionary<string, Path> m_Paths;

        public SelectableObjectRepository(IBus bus)
        {
            m_Bus = bus;

            m_Paths = new Dictionary<string, Path>();

            m_Bus.Subscribe<SelectObjectAtMessage>(OnSelectObject);
        }

        private void OnSelectObject(SelectObjectAtMessage message)
        {

        }
    }
}
