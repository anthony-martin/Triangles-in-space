using System.Collections.Generic;
using System.Linq;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;

namespace TrianglesInSpace.Messages
{
    public class PathMessage : IMessage
    {
        public readonly string Name;
        private readonly LinearMotion[] m_LinearMotion;
        private readonly CircularMotion[] m_CircularMotion;

        public PathMessage(string name, IEnumerable<IMotion> motions)
        {
            Name = name;
    
            var linearMotions = new List<LinearMotion>();
            var circularMotions = new List<CircularMotion>();
            foreach (var motion in motions)
            {
                var linear = motion as LinearMotion;
                if (linear != null)
                {
                    linearMotions.Add(linear);
                }

                var circular = motion as CircularMotion;
                if (circular != null)
                {
                    circularMotions.Add(circular);
                }
            }
            m_LinearMotion = linearMotions.ToArray();
            m_CircularMotion = circularMotions.ToArray();
        }

        public CombinedMotion Motion
        {
            get
            {
                var motions = m_LinearMotion.Concat<IMotion>(m_CircularMotion);
                return new CombinedMotion(motions.OrderBy(x => x.StartTime).ToList());
            }
        }
    }
}
