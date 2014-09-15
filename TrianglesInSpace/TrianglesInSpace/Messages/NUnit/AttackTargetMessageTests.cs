using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages.NUnit
{
    [TestFixture]
    internal class AttackTargetMessageTests
    {
        [Test]
        public void RoundTrip()
        {
            var original = new AttackTargetMessage(new Vector(5.3, 7), 457);

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(AttackTargetMessage));
            var text = serialiser.Serialise(original);

            var deserialised = (AttackTargetMessage)serialiser.Deserialise(text);

            Assert.AreEqual(original.WorldPosition, deserialised.WorldPosition);
            Assert.AreEqual(original.Time, deserialised.Time);
        }
    }
}
