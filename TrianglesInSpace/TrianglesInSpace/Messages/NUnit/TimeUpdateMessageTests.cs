using NUnit.Framework;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages.NUnit
{
    public class TimeUpdateMessageTests : TestSpecification
    {
        [Test]
        public void RoundTrip()
        {
            const ulong time = 550000;
            var original = new TimeUpdateMessage(time);

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(TimeUpdateMessage));
            var text = serialiser.Serialise(original);

            var deserialised = (TimeUpdateMessage)serialiser.Deserialise(text);

            Assert.AreEqual(time, deserialised.Time);
        }
    }
}
