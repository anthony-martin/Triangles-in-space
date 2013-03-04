using System.Collections.Generic;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;

namespace TrianglesInSpace.Objects
{
    public class SelectableObjectRepository
    {
        private readonly IBus m_Bus;
        private readonly List<SelectableObject> m_Objects;

        public SelectableObjectRepository(IBus bus)
        {
            m_Bus = bus;

            m_Objects = new List< SelectableObject>();

            m_Bus.Subscribe<SelectObjectAtMessage>(OnSelectObject);
        }

        private void OnSelectObject(SelectObjectAtMessage message)
        {
            foreach (var selectableObject in m_Objects)
            {
                //selectableObject;
            }
        }
    }
}
