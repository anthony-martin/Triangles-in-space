using NUnit.Framework;
using ZeroMQ;

namespace TrianglesInSpace.Messaging.NUnit
{
    internal class MessageSenderTests : TestSpecification
    {
        [Test]
        public void DisposesCleanly()
        {
            var context = ZmqContext.Create();
            var transmitter = new MessageSender(context);
            transmitter.Dispose();
            context.Dispose();

        }

        [Test]
        //note the ability for this to function internally depends on what type of connection is created
        public void RoundTrip()
        {
            var context = ZmqContext.Create();
            var transmitter = new MessageSender(context);
            var receiver = new MessageReceiver(context);
            receiver.CreateReceiveSocket();

            string message = "Hello world";
            //transmitter.Connect();
            transmitter.Send(message);

            string recieved = receiver.Receive();

            Assert.AreEqual(message, recieved);
            receiver.Dispose();
            transmitter.Dispose();
            context.Dispose();
        }
    }
}
