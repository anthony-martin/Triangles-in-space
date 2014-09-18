using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrianglesInSpace.Primitives
{
    /// <summary>
    /// Central place for storing convesion values to allow 
    /// the changing of constants while keeping the correct baseline
    /// </summary>
    public static class BaseConstants
    {
        // 750 km/s
        public const double BaseSpeed = 5;

        //eqivelant to 1 500km/s
        public const double EscortSpeed = BaseSpeed * 2.0;

        // 30 000 km/s
        public const double WeaponSpeed = EscortSpeed * 20.0;
    }
}
