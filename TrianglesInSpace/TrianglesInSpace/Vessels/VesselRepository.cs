using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Ioc;

namespace TrianglesInSpace.Vessels
{
    public interface IVesselRepository
    {
        IVessel GetByName(string name);
        void Add(IVessel vessel);
    }

    public class VesselRepository : IVesselRepository
    {
        private List<IVessel> m_Vessel;

        public VesselRepository()
        {
            m_Vessel = new List<IVessel>();
        }

        public IVessel GetByName(string name)
        {
            return m_Vessel.FirstOrDefault(x => x.Name == name);
        }

        public void Add(IVessel vessel)
        {
            m_Vessel.Add(vessel);
        }
    }
}
