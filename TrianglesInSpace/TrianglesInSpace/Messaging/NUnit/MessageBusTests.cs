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

        private void HandleMessage(TestMessage message)
        {

        }

        private class TestMessage : IMessage
        {

        }
    }

   
}
