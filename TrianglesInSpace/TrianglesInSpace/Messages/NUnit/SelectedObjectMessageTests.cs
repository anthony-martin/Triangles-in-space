using NUnit.Framework;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages.NUnit
{
    internal class SelectedObjectMessageTests : TestSpecification
    {
        [Test]
        public void RoundTrip()
        {
            var original = new SelectedObjectMessage("Louie");

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(SelectedObjectMessage));
            var text = serialiser.Serialise(original);

            var deserialised = (SelectedObjectMessage)serialiser.Deserialise(text);

            Assert.AreEqual(original.SelectedName, deserialised.SelectedName);
        }
    }
}
