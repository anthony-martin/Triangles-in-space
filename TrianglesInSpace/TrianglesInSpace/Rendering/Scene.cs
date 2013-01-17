using System.Collections.Generic;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Rendering
{
    public class Scene
    {
        private readonly IBus m_Bus;
        public readonly List<NodeWithPosition> m_SceneNodes;

        public Scene(IBus bus)
        {
            m_Bus = bus;
            m_SceneNodes = new List<NodeWithPosition>();
        }
    }
}
