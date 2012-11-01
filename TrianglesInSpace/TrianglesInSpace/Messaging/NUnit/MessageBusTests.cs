using System;
using NUnit.Framework;
using NSubstitute;

namespace TrianglesInSpace.Messaging.NUnit
{
    class MessageBusTests : TestSpecification
    {
        private MessageBus m_Bus;
        private IMessageSender m_Sender;
        private IMessageReceiver m_Receiver;

        [SetUp]
        public void CreateBus()
        {
            m_Sender = Substitute.For<IMessageSender>();
            m_Receiver = Substitute.For<IMessageReceiver>();
            m_Bus = new MessageBus(m_Sender,
                                   m_Receiver);
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
            bool handled = false;
            Action<TestMessage> handler = x => { handled = true; };
            var unsubscribe = m_Bus.Subscribe<TestMessage>(handler);

            unsubscribe();
            Assert.DoesNotThrow( () => unsubscribe());
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
        public void SendPutsTheMessageIntoTheSocket()
        {
            bool handled = false;
            m_Bus.Subscribe<TestMessage>(x => { handled = true; });
            m_Bus.Send(new TestMessage());

            m_Sender.Received().Send(Arg.Any<string>());
            Assert.False(handled);
        }

        [Test]
        public void ProcessMessagesCallsSubscribers()
        {
            bool handled = false;
            m_Bus.Subscribe<TestMessage>(x => { handled = true; });
            m_Bus.SendLocal(new TestMessage());

            m_Bus.ProcessMessages();

            Assert.True(handled);
        }

        [Test]
        public void ProcessMessageProcessesAllMessages()
        {
            int handled = 0;
            m_Bus.Subscribe<TestMessage>(x => { handled ++; });
            m_Bus.SendLocal(new TestMessage());
            m_Bus.SendLocal(new TestMessage());

            Assert.AreEqual(2, handled);
        }

        [Test]
        public void SubscriberIsNotLost()
        {
            bool handledOne = false;
            bool handledTwo = false;

            var unsubscribeOne = m_Bus.Subscribe<TestMessage>(x => { handledOne = true; });
            m_Bus.Subscribe<TestMessage>(x => { handledTwo = true; });

            unsubscribeOne();

            m_Bus.SendLocal(new TestMessage());

            Assert.False(handledOne);
            Assert.True(handledTwo);

        }


        private void HandleMessage(TestMessage message)
        {

        }

        private class TestMessage : IMessage
        {

        }
    }

   
}
