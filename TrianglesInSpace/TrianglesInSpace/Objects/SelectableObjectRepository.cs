using System.Collections.Generic;
using System.Linq;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;

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

        public void AddObject(SelectableObject newObject)
        {
            m_Objects.Add(newObject);
        }

        public void OnSelectObject(SelectObjectAtMessage message)
        {
            var selected = new List<SelectableObject>();
            int count = 0;
            foreach (var selectableObject in m_Objects)
            {
                if(selectableObject.IntersectsPoint(message.WorldPosition, message.Time))
                {
                    selected.Add(selectableObject);
                    count++;
                }
            }

            if(count == 1)
            {
                m_Bus.Send(new SelectedObjectMessage(selected.First().Name));
            }
            

        }
    }
}
