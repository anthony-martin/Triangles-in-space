using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrianglesInSpace.Primitives
{
    [Flags()]
    public enum Facing
    {
        None = 0,
        Bow  = 1,
        Port  = 2,
        Starboard = 4,
        Stern = 8
    }

    public static class FacingExtensions
    {
        private const double PiOnFour = Math.PI/ 4;
        public static Facing ToFacing(this Angle heading,  Vector relativePosition)
        {
            var azimuth = Math.Atan2(relativePosition.Y, relativePosition.X);
            var relativeAzimuth = azimuth - heading.Value;
            relativeAzimuth = Angle.ReduceAngle(relativeAzimuth);

            Facing facing = Facing.None;

            // err on the side of gnerosity and consider all edge cases in both facings
            if (relativeAzimuth >= -PiOnFour && relativeAzimuth <= PiOnFour)
            {
                facing =  facing | Facing.Bow;
            }
            if (relativeAzimuth <= -PiOnFour && relativeAzimuth >= -3 * PiOnFour)
            {
                facing = facing | Facing.Starboard;
            }
            if (relativeAzimuth >= PiOnFour && relativeAzimuth <= 3 * PiOnFour)
            {
                facing = facing | Facing.Port;
            }
            if (relativeAzimuth >= 3* PiOnFour || relativeAzimuth <= -3 * PiOnFour)
            {
                facing = facing | Facing.Stern;
            }

            return facing;
        }
    }
}
