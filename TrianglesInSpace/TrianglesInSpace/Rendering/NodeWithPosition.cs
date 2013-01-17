using Mogre;
using TrianglesInSpace.Motion;

namespace TrianglesInSpace.Rendering
{
    public class NodeWithPosition
    {
        private readonly SceneNode m_SceneNode;
        private CombinedMotion m_Motion;

        public NodeWithPosition(SceneNode sceneNode, CombinedMotion startingMotion)
        {
            m_SceneNode = sceneNode;
            m_Motion = startingMotion;
        }

        public string Name
        {
            get
            {
                return m_SceneNode.Name;
            }
        }

        public CombinedMotion Motion
        {
            get { return m_Motion; }
            set { m_Motion = value; }
        }
    }
}
