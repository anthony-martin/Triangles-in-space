using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phase1
{
    interface Velocity
    {
        // gets the current velocity from the object
        void getVelocity(double time, out double xComponent, out double yComponent);
        Position getVelocity(double time);
        // gets the current relative position of the object determined by start time.
        void getMovement(double time, out double xComponent, out double yComponent);
        Position getMovement(double time);
    }
}
