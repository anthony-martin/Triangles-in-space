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

        public void UpdatePosition(ulong time)
        {
            var currenmtMotion = m_Motion.GetCurrentMotion(time);
            var currentPositon = currenmtMotion.GetCurrentPosition(time);
            m_SceneNode.Position = new Vector3(currentPositon.X, 0.0, currentPositon.Y);

            var rotation = new Primitives.Angle(currenmtMotion.GetVelocity(time));
            rotation.ReduceAngle();

            Quaternion quat = new Quaternion(new Radian(rotation.Value), new Vector3(0, -1, 0));
            m_SceneNode.Orientation = quat;
        }
    }
}
