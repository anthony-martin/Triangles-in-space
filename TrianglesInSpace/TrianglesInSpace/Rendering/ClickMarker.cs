using System;
using Mogre;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Rendering
{
    public class ClickMarker : IDisposable
    {
        private readonly SceneNode m_Marker;
        private readonly IBus m_Bus;
        private readonly Disposer m_Disposer;

        public ClickMarker(SceneNode marker, IBus bus)
        {
            m_Marker = marker;
            m_Bus = bus;

            m_Disposer = new Disposer();
            m_Bus.Subscribe<SetPathToTargetMessage>(MoveMarker).AddTo(m_Disposer);
            m_Bus.Subscribe<SelectObjectAtMessage>(MoveMarker).AddTo(m_Disposer);
        }

        private void MoveMarker(SelectObjectAtMessage message)
        {
            m_Marker.Position = VectorConversions.ToOgreVector(message.WorldPosition); 
        }

        private void MoveMarker(SetPathToTargetMessage message)
        {
            m_Marker.Position = VectorConversions.ToOgreVector(message.WorldPosition);
        }

        public void Dispose()
        {
            m_Disposer.Dispose();
        }
    }
}
