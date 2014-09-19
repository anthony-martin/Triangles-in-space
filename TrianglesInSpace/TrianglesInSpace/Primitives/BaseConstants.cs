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

        public const double TimeBlock =  5;

        public const double UnitRange = EscortSpeed * TimeBlock * 2;
        /// <summary>
        /// 90 degrees for turn per time unit r = v/theta
        /// a = v^2 / r 
        /// a = v^2 / (v / theta)
        /// </summary>
        public const double EscortAcceleration = (EscortSpeed * EscortSpeed) / ((EscortSpeed* TimeBlock)  / (Math.PI / 2));
        public const double EscortAcceleration2 = (EscortSpeed ) / (( TimeBlock) / (Math.PI / 2));
        /// <summary>
        /// 45 degrees for turn per time unit r = v/theta
        /// a = v^2 / r 
        /// a = v^2 / (v / theta)
        /// </summary>
        public const double BasetAcceleration = (BaseSpeed * BaseSpeed) / ((BaseSpeed * TimeBlock) / (Math.PI / 4));
    }
}
