using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrianglesInSpace.Objects
{
    public class PlayerId : IPlayerId
    {
        private Guid m_Id;

        public PlayerId()
        {
            m_Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get
            {
                return m_Id;
            }
            set
            {
                m_Id = value;
            }
        }
    }
}
