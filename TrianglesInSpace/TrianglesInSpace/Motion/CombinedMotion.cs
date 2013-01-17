using System.Collections.Generic;
using System.Linq;

namespace TrianglesInSpace.Motion
{
    public class CombinedMotion
    {
        private readonly List<IMotion> m_Path;
        public CombinedMotion(IEnumerable<IMotion> motion)
        {
            m_Path = motion.ToList();
        }

        public IMotion GetCurrentMotion(ulong currentTime)
        {
            IMotion pathSegment;
            int index = m_Path.Count;
            do
            {
                index--;
                pathSegment = m_Path[index];

            }
            while (index > 0 && pathSegment.StartTime > currentTime);

            if (index != 0)
            {
                m_Path.RemoveRange(0, index);
            }

            return pathSegment;
        }

        public List<IMotion> Path
        {
            get
            {
                return m_Path.ToList();
            }
        }

    }
}
