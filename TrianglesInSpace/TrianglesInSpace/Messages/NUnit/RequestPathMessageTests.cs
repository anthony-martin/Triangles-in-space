using NUnit.Framework;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages.NUnit
{
    internal class RequestPathMessageTests : TestSpecification
    {
        [Test]
        public void MessageRoundTrip()
        {
            const string name = "harry";
            var original = new RequestPathMessage(name);

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(RequestPathMessage));

            var text = serialiser.Serialise(original);

            var deserialised = (RequestPathMessage)serialiser.Deserialise(text);

            Assert.NotNull(deserialised);
            Assert.AreEqual(name, deserialised.Name);
        }
    }
}
