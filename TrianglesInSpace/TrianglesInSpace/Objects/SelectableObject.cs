using TrianglesInSpace.Messages;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Primitives;
using System;

namespace TrianglesInSpace.Objects
{
    public class SelectableObject
    {
        private readonly Guid m_Owner;
        private readonly string m_Name;
        private readonly IPath m_Path;
        private readonly Box m_Bounds;


        public SelectableObject(string name, IPath path)
        : this(Guid.NewGuid(), name, path)
        {
        }

        public SelectableObject(Guid owner ,string name, IPath path)
        {
            m_Owner = owner;
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

        public IPath Path
        {
            get
            {
                return m_Path;
            }
        }

        public Guid Owner
        {
            get
            {
                return m_Owner;
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
