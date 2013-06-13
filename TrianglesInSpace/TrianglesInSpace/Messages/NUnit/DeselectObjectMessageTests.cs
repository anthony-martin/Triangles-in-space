using NUnit.Framework;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages.NUnit
{
    internal class DeselectObjectMessageTests : TestSpecification
    {
        [Test]
        public void RoundTrip()
        {
            var original = new DeselectedObjectMessage("Louie");

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(DeselectedObjectMessage));
            var text = serialiser.Serialise(original);

            var deserialised = (DeselectedObjectMessage)serialiser.Deserialise(text);

            Assert.AreEqual(original.DeselectedName, deserialised.DeselectedName);
        }
    }
}
