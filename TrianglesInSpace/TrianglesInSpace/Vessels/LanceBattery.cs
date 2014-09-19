using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Vessels
{
    public class LanceBattery : IWeaponSystem
    {

        public Facing FireArc
        {
            get 
            {
                return Facing.Bow | Facing.Port | Facing.Starboard;
            }
        }

        public double Range
        {
            get 
            { 
                return BaseConstants.UnitRange; 
            }
        }
    }
}
