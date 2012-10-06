using NUnit.Framework;

namespace TrianglesInSpace.Messaging.NUnit
{
    internal class MessageTransmissionTests : TestSpecification
    {
        [Test]
        public void DisposesCleanly()
        {
            var transmitter = new MessageTransmission();
            transmitter.Dispose();

        }

        //[Test]
        //note the ability for this to function internally depends on what type of connection is created
        public void RoundTrip()
        {
            var transmitter = new MessageTransmission();

            string message = "Hello world";
            transmitter.Connect();
            transmitter.Send(message);

            string recieved = transmitter.Recieve();

            Assert.AreEqual(message, recieved);
            transmitter.Dispose();
        }
    }
}
