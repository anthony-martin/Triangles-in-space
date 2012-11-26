using NUnit.Framework;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages.NUnit
{
    internal class SetPathToTargetTests : TestSpecification
    {
        [Test]
        public void RoundTrip()
        {
            var original = new SetPathToTarget(new Vector(5.3, 7), 15000);

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(SetPathToTarget));
            var text = serialiser.Serialise(original);

            var deserialised = (SetPathToTarget)serialiser.Deserialise(text) ;

            Assert.AreEqual(original.WorldPosition, deserialised.WorldPosition);
            Assert.AreEqual(original.Time, deserialised.Time);

        }
    }
}
