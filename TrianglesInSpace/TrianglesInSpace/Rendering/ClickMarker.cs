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
            m_Bus.Subscribe<SetPathToTarget>(MoveMarker).AddTo(m_Disposer);
        }

        private void MoveMarker(SetPathToTarget message)
        {
            m_Marker.Position = new Vector3(message.WorldPosition.X, 0.0, message.WorldPosition.Y);
        }

        public void Dispose()
        {
            m_Disposer.Dispose();
        }
    }
}
