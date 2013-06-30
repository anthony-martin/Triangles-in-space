namespace TrianglesInSpace.Time
{
    public class SynchronizedClock : IClock
    {
        private ulong m_Time;
        private const ulong m_SimulationSpeed = 1000;

        public ulong Time
        {
            get
            {
                return m_Time;
            }
        }
        
        public ulong SimulationSpeed
        {
            get
            {
                return m_SimulationSpeed;
            }
        }

        /// <summary>
        /// Update the time based on real time since last update
        /// </summary>
        /// <param name="timeSinceLastUpdate">in seconds</param>
        public void UpdateTime(double timeSinceLastUpdate)
        {
            m_Time += (ulong)(timeSinceLastUpdate * m_SimulationSpeed);
        }
    }
}
