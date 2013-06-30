using NSubstitute;
using NUnit.Framework;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Time.NUnit
{
    internal class SynchronizedClockTests : TestSpecification
    {
        private SynchronizedClock m_Clock;
        private IBus m_Bus;

        [SetUp]
        public void SetUp()
        {
            m_Bus = Get<IBus>();

            m_Clock = new SynchronizedClock(m_Bus);
        }

        [Test]
        public void UpdateTimeIncrementsTimeBy1000PerSecond()
        {
            m_Clock.UpdateTime(1.0);

            Assert.AreEqual(m_Clock.SimulationSpeed, m_Clock.Time);
        }

        [Test]
        public void UpdateTimeHandlesFractions()
        {
            m_Clock.UpdateTime(0.333);

            Assert.AreEqual(333, m_Clock.Time);
        }

        [Test]
        public void SendsSyncMessageIfTimeIsOverUpdateInterval()
        {
            m_Clock.SetMaster(true);
            m_Clock.UpdateTime(31);

            m_Bus.Received(1).Send(Arg.Any<TimeUpdateMessage>());
        }

        [Test]
        public void SendsSyncMessageRepeatedly()
        {
            m_Clock.SetMaster(true);
            m_Clock.UpdateTime(31);
            m_Clock.UpdateTime(10);
            m_Clock.UpdateTime(21);

            m_Bus.Received(2).Send(Arg.Any<TimeUpdateMessage>());
        }

        [Test]
        public void DoesNotSendSyncMessagesIfNotMaster()
        {
            m_Clock.SetMaster(false);
            m_Clock.UpdateTime(31);

            m_Bus.DidNotReceive().Send(Arg.Any<TimeUpdateMessage>());
        }
    }
}
