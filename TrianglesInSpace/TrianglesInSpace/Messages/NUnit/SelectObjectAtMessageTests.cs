using NUnit.Framework;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages.NUnit
{
    internal class SelectObjectAtMessageTests : TestSpecification
    {
        [Test]
        public void RoundTrip()
        {
            var original = new SelectObjectAtMessage(new Vector(5.3, 7), 457);

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(SelectObjectAtMessage));
            var text = serialiser.Serialise(original);

            var deserialised = (SelectObjectAtMessage)serialiser.Deserialise(text);

            Assert.AreEqual(original.WorldPosition, deserialised.WorldPosition);
            Assert.AreEqual(original.Time, deserialised.Time);
        }
    }
}
