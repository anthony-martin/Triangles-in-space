using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Vessels
{
    public interface IWeaponSystem
    {
        Facing FireArc {get;}
        double Range {get;}
        
    }
}
