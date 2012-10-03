using NUnit.Framework;

namespace TrianglesInSpace.Messaging.NUnit
{
    internal class MessageTransmissionTests : TestSpecification
    {
        [Test]
        public void DisposesCleanly()
        {
            var trasmitter = new MessageTransmission();
            trasmitter.Dispose();

        }

        [Test]
        public void RoundTrip()
        {
            var transmitter = new MessageTransmission();

            string message = "Hello world";
            transmitter.Connect();
            transmitter.Send(message);

            string recieved = transmitter.Recieve();

            Assert.AreEqual(message, recieved);
        }
    }
}
