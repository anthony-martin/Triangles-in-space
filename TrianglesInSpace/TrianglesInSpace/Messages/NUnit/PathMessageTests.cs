using System;
using System.Collections.Generic;
using NUnit.Framework;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages.NUnit
{
    internal class PathMessageTests : TestSpecification
    {
        [Test]
        public void MessageRoundTrip()
        {
            List<IMotion> motions = new List<IMotion>
            {
                new LinearMotion(1, new Vector(5,10),new Vector(3,5.5)),
                new CircularMotion(30, 1, new Angle(0), new Angle(0), 1, Vector.Zero),
                new LinearMotion(500, new Vector(7,20),new Vector(5,100)),
                new CircularMotion(670, 0, new Angle(0), new Angle(-Math.PI), 1, Vector.Zero),
                new CircularMotion(1500, 0, new Angle(-Math.PI / 2), new Angle(1), 1, Vector.Zero)
            };

            var original = new PathMessage(motions);

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(PathMessage));

            var text = serialiser.Serialise(original);

            var deserialised = (PathMessage)serialiser.Deserialise(text);

            Assert.AreEqual(motions, deserialised.Motion);
        }
    }
}
