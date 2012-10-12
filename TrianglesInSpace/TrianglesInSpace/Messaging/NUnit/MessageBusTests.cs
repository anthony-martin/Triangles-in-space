using NUnit.Framework;
using ZeroMQ;

namespace TrianglesInSpace.Messaging.NUnit
{
    class MessageBusTests : TestSpecification
    {
        private MessageBus CreateBus()
        {
            return new MessageBus(ZmqContext.Create());
        }

        [Test]
        public void SubscribeReturnsUnsubscribeAction()
        {
            var bus = CreateBus();

            var unsubscribe = bus.Subscribe<TestMessage>(HandleMessage);

            unsubscribe();
        }

        [Test]
        public void RemoveCanBeCalledTwiceSafely()
        {
            var bus = CreateBus();

            var unsubscribe = bus.Subscribe<TestMessage>(HandleMessage);

            unsubscribe();
            unsubscribe();
        }

        private void HandleMessage(TestMessage message)
        {

        }

        private class TestMessage : IMessage
        {

        }

        [Test]
        public void SendCallHandler()
        {
            var bus = CreateBus();

            bool handled = false;

            bus.Subscribe<TestMessage>(x => { handled = true; });

            bus.Send(new TestMessage());

            Assert.True(handled);
        }
    }

   
}
