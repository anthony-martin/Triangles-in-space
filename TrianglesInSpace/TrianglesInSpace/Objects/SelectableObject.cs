using TrianglesInSpace.Motion;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Objects
{
    public class SelectableObject
    {
        private readonly string m_Name;
        private readonly IPath m_Path;
        private readonly Box m_Bounds;

        public SelectableObject(string name, IPath path)
        {
            m_Bounds = new Box(20.0, 20.0);
            m_Name = name;
            m_Path = path;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public bool IntersectsPoint(Vector worldPosition, ulong time)
        {
            IMotion motion = m_Path.GetCurrentMotion(time);

            var objectPosition = motion.GetCurrentPosition(time);

            return m_Bounds.Contains(worldPosition, objectPosition);
        }
    }
}
