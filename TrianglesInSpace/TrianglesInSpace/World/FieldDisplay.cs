using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Objects;
using TrianglesInSpace.Primitives;
using TrianglesInSpace.Rendering;

namespace TrianglesInSpace.World
{
    public interface IFieldDisplay
    {
    }

    public class FieldDisplay : IFieldDisplay
    {
        private readonly IBus m_Bus;

        private readonly Disposer m_Disposer;
        private ISelectableObjectRepository m_SelectableRepo;
        private IRenderer m_Renderer;
        private Scene m_Scene;

        public FieldDisplay(IBus bus,
                            ISelectableObjectRepository selectableRepo,
                            IRenderer renderer,
                            IPlayerId id,
                            ITargetedVesselRenderer _)
        {
            m_Bus = bus;
            m_Renderer = renderer;
            m_SelectableRepo = selectableRepo;
            m_Id = id;

            m_Disposer = new Disposer();

            m_Bus.Subscribe<AddObjectMessage>(OnAdd).AddTo(m_Disposer);
        }

        private void OnAdd(AddObjectMessage message)
        {
            var path = new Path(BaseConstants.EscortAcceleration, new CircularMotion(0, 50, new Angle(0), new Angle(Math.PI / 10), 20, Vector.Zero));
            var selectableObject = new SelectableObject(m_Id.Id, message.Name, path);

            m_SelectableRepo.AddObject(selectableObject);
            m_Renderer.Scene.Add(message.Name, message.Shape);
        }

        public IPlayerId m_Id { get; set; }
    }
}
