using NUnit.Framework;

namespace TrianglesInSpace.Time.NUnit
{
    internal class SynchronizedClocktests : TestSpecification
    {
        private SynchronizedClock m_Clock;
        [SetUp]
        public void SetUp()
        {
            m_Clock = new SynchronizedClock();
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
    }
}
