using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TrianglesInSpace.Primitives.NUnit
{
    internal class FacingTests : TestSpecification
    {
        [Test]
        public void BowIsBow()
        {
            Angle heading = new Angle(new Vector(0, 1));

            Vector relativePosition = new Vector(0, 1);

            var facing = heading.ToFacing(relativePosition);

            Assert.AreEqual(Facing.Bow, facing);
        } 

        [Test]
        public void Starboard()
        {
            Angle heading = new Angle(new Vector(1, 0));

            Vector relativePosition = new Vector(0, -1);

            var facing = heading.ToFacing(relativePosition);

            Assert.AreEqual(Facing.Starboard, facing);
        }

        [Test]
        public void Port()
        {
            Angle heading = new Angle(new Vector(-1, 0));

            Vector relativePosition = new Vector(0, -1);

            var facing = heading.ToFacing(relativePosition);

            Assert.AreEqual(Facing.Port, facing);
        }

        [Test]
        public void Stern()
        {
            Angle heading = new Angle(new Vector(-1, 0));

            Vector relativePosition = new Vector(1, 0);

            var facing = heading.ToFacing(relativePosition);

            Assert.AreEqual(Facing.Stern, facing);
        }

        [Test]
        public void Combined()
        {
            var facing = Facing.Bow | Facing.Port;

            Assert.True(facing.HasFlag(Facing.Bow));
            Assert.True(facing.HasFlag(Facing.Port));
        }
    }
}
