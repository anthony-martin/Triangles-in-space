using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Objects;
using TrianglesInSpace.Disposers;

namespace TrianglesInSpace.Rendering
{

    public class TargetedVesselRenderer : IDisposable
    {
        private readonly IBus m_Bus;
        private readonly ISelectableObjectRepository m_Targets;

        private Disposer m_Disposer; 


        public TargetedVesselRenderer(IBus bus,
                                      ISelectableObjectRepository targets)
        {
            m_Disposer = new Disposer();
            m_Bus = bus;
            m_Targets = targets;


        }

        public virtual void Dispose()
        {
        }
    }
}
