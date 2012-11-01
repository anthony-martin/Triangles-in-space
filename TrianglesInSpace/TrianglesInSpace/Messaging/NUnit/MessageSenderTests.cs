using NUnit.Framework;
using ZeroMQ;

namespace TrianglesInSpace.Messaging.NUnit
{
    internal class MessageSenderTests : TestSpecification
    {
        private ZmqContext m_Context;

        [TestFixtureSetUp]
        public void CreateContext()
        {
            m_Context = ZmqContext.Create();
        }


        [TestFixtureTearDown]
        public void DiposeContext()
        {
            m_Context.Dispose();
        }

        [Test]
        public void DisposesCleanly()
        {
            var transmitter = new MessageSender(m_Context);
            transmitter.Dispose();
        }

        //[Test] not hangs the unit tests due to threads
        //note the ability for this to function internally depends on what type of connection is created
        public void RoundTrip()
        {
            var transmitter = new MessageSender(m_Context);
            var receiver = new MessageReceiver(m_Context);
            receiver.CreateReceiveSocket();

            string message = "Hello world";
            //transmitter.Connect();
            transmitter.Send(message);

            string recieved = receiver.Receive();

            Assert.AreEqual(message, recieved);
            receiver.Dispose();
            transmitter.Dispose();
        }
    }
}
