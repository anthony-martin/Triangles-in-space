using System.Collections.Generic;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;

namespace TrianglesInSpace.Messages
{
    public class PathMessage : IMessage
    {
        private readonly LinearMotion[] m_LinearMotion;
        private readonly CircularMotion[] m_CircularMotion;

        public PathMessage(List<LinearMotion> linearMotion, List<CircularMotion> circularMotion)
        {
            m_LinearMotion = linearMotion.ToArray();
            m_CircularMotion = circularMotion.ToArray();
        }

        public List<LinearMotion> LinearMotion
        {
            get
            {
                return new List<LinearMotion>(m_LinearMotion);
            }
        }

        public List<CircularMotion> CircularMotion
        {
            get
            {
                return new List<CircularMotion>(m_CircularMotion);
            }
        }
    }
}
