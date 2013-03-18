using System;
using System.Collections.Generic;
using System.Linq;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Objects
{
    public class SelectableObjectRepository : IDisposable
    {
        private readonly IBus m_Bus;
        private readonly List<SelectableObject> m_Objects;
        private IList<SelectableObject> m_SelctedObject; 
        private readonly Disposer m_Disposer;

        public SelectableObjectRepository(IBus bus)
        {
            m_Bus = bus;

            m_Objects = new List< SelectableObject>();

            m_Disposer = new Disposer();

            m_Bus.Subscribe<SelectObjectAtMessage>(OnSelectObject).AddTo(m_Disposer);
            m_Bus.Subscribe<RequestPathMessage>(OnPathRequest).AddTo(m_Disposer);
            m_Bus.Subscribe<SetPathToTargetMessage>(OnSetPath).AddTo(m_Disposer);
        }

        public void AddObject(SelectableObject newObject)
        {
            m_Objects.Add(newObject);
        }

        public void OnPathRequest(RequestPathMessage message)
        {
            foreach (var selectableObject in m_Objects)
            {
                if (selectableObject.Name == message.Name)
                {
                    m_Bus.Send(new PathMessage(selectableObject.Name,
                                               selectableObject.Path.Motion));
                }
            }
        }

        private void OnSetPath(SetPathToTargetMessage message)
        {
            foreach (var selectableObject in m_SelctedObject)
            {
                selectableObject.Path.MoveToDestination(message.WorldPosition, message.Time);
                m_Bus.Send(new PathMessage(selectableObject.Name,
                                               selectableObject.Path.Motion));
            }
        }

        public IList<SelectableObject> GetObjectAt(Vector worldPosition, ulong time)
        {
            var selected = new List<SelectableObject>();
            foreach (var selectableObject in m_Objects)
            {
                if (selectableObject.IntersectsPoint(worldPosition, time))
                {
                    selected.Add(selectableObject);
                }
            }
            return selected;
        }

        public void OnSelectObject(SelectObjectAtMessage message)
        {
            m_SelctedObject = GetObjectAt(message.WorldPosition, message.Time);

            if (m_SelctedObject.Count == 1)
            {
                m_Bus.Send(new SelectedObjectMessage(m_SelctedObject.First().Name));
            }
        }

        public void Dispose()
        {
            m_Disposer.Dispose();
        }
    }
}
