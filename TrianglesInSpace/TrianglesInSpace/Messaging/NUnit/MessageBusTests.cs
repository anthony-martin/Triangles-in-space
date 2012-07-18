using System;
using NUnit.Framework;

namespace TrianglesInSpace.Messaging.NUnit
{
    class MessageBusTests : TestSpecification
    {
        [Test]
        public void SubscribeReturnsUnsubscribeAction()
        {
            var bus = new MessageBus();

            var unsubscribe = bus.Subscribe<TestMessage>(HandleMessage);

            unsubscribe();
        }

        [Test]
        public void RemoveCanBeCalledTwiceSafely()
        {
            var bus = new MessageBus();

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
            var bus = new MessageBus();

            bool handled = false;

            bus.Subscribe<TestMessage>(x => { handled = true; });

            bus.Send(new TestMessage());

            Assert.True(handled);
        }
    }

   
}
