using NUnit.Framework;
using NSubstitute;

namespace TrianglesInSpace.Messaging.NUnit
{
    class MessageBusTests : TestSpecification
    {
        private MessageBus m_Bus;

        [SetUp]
        public void CreateBus()
        {
            m_Bus = new MessageBus(Substitute.For<IMessageSender>(),
                                    Substitute.For<IMessageReceiver>());
        }


        [Test]
        public void SubscribeReturnsUnsubscribeAction()
        {
            var unsubscribe = m_Bus.Subscribe<TestMessage>(HandleMessage);

            unsubscribe();
        }

        [Test]
        public void RemoveCanBeCalledTwiceSafely()
        {
            var unsubscribe = m_Bus.Subscribe<TestMessage>(HandleMessage);

            unsubscribe();
            unsubscribe();
        }

        [Test]
        public void SendQueuesMessageDoesNotHandle()
        {
            bool handled = false;
            m_Bus.Subscribe<TestMessage>(x => { handled = true; });
            m_Bus.Send(new TestMessage());

            Assert.False(handled);
        }

        [Test]
        public void ProcessMessagesCallsSubscribers()
        {
            bool handled = false;
            m_Bus.Subscribe<TestMessage>(x => { handled = true; });
            m_Bus.Send(new TestMessage());

            m_Bus.ProcessMessages();

            Assert.True(handled);
        }

        [Test]
        public void ProcessMessageProcessesAllMessages()
        {
            int handled = 0;
            m_Bus.Subscribe<TestMessage>(x => { handled ++; });
            m_Bus.Send(new TestMessage());
            m_Bus.Send(new TestMessage());

            m_Bus.ProcessMessages();

            Assert.AreEqual(2, handled);
        }


        private void HandleMessage(TestMessage message)
        {

        }

        private class TestMessage : IMessage
        {

        }
    }

   
}
