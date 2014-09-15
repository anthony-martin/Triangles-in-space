using System;
using System.Collections.Generic;
using System.Linq;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Vessels;

namespace TrianglesInSpace.Objects
{
    public interface ISelectableObjectRepository
    {
        void AddObject(SelectableObject newObject);
        IList<SelectableObject> GetObjectAt(Vector worldPosition, ulong time);
    }
    public class SelectableObjectRepository : IDisposable, ISelectableObjectRepository
    {
        private readonly IBus m_Bus;
        private readonly List<SelectableObject> m_Objects;
        private IList<SelectableObject> m_SelctedObject; 
        private readonly Disposer m_Disposer;
        private readonly IPlayerId m_Id;
        private readonly IVesselRepository m_VesselRepository;


        public SelectableObjectRepository(IBus bus,
                                          IPlayerId id,
                                          IVesselRepository vesselRepository)
        {
            m_Bus = bus;
            m_Id = id;

            m_VesselRepository = vesselRepository;

            m_Objects = new List< SelectableObject>();

            m_Disposer = new Disposer();

            m_Bus.Subscribe<SelectObjectAtMessage>(OnSelectObject).AddTo(m_Disposer);
            m_Bus.Subscribe<RequestPathMessage>(OnPathRequest).AddTo(m_Disposer);
            m_Bus.Subscribe<SetPathToTargetMessage>(OnSetPath).AddTo(m_Disposer);
        }

        private void OnAdd(AddObjectMessage message)
        {
            var path = new Path(4, new CircularMotion(0, 50, new Angle(0), new Angle(Math.PI / 10), 20, Vector.Zero));
            var selectableObject = new SelectableObject(m_Id.Id ,message.Name, path);
            m_Objects.Add(selectableObject);

            m_VesselRepository.Add(new Vessel(selectableObject));
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

        public void OnSetPath(SetPathToTargetMessage message)
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
            IList<SelectableObject> selectedObjects = GetObjectAt(message.WorldPosition, message.Time);
            IList<SelectableObject> newlySelectedObjects;
            IList<SelectableObject> deselectedObjects;
            if (m_SelctedObject != null)
            {
                newlySelectedObjects = selectedObjects.Where(x => !m_SelctedObject.Contains(x)).ToList();
                deselectedObjects = m_SelctedObject.Where(x => !selectedObjects.Contains(x)).ToList();
            }
            else
            {
                newlySelectedObjects = selectedObjects;
                deselectedObjects = new List<SelectableObject>();
            }
            m_SelctedObject = selectedObjects;

            if (newlySelectedObjects.Any())
            {
                m_Bus.Send(new SelectedObjectMessage(newlySelectedObjects.First().Name, newlySelectedObjects.First().Owner == m_Id.Id));
            }

            if (deselectedObjects.Any())
            {
                m_Bus.Send(new DeselectedObjectMessage(deselectedObjects.First().Name));
            }
            
        }

        public void Dispose()
        {
            m_Disposer.Dispose();
        }
    }
}
