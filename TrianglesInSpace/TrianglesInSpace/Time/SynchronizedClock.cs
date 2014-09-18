using System;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Time
{
    public class SynchronizedClock : IClock, IDisposable
    {
        private readonly            IBus m_Bus;
        private readonly            Disposer m_Disposer;
        private                     ulong m_Time;
        private const               ulong m_SimulationSpeed = 1000;
        private                     ulong m_NextSync ;
        private const               ulong m_SyncRate = 30000;
        private                     bool m_AmMaster;

        public SynchronizedClock(IBus bus)
        {
            m_Bus = bus;
            m_Disposer = new Disposer();
            m_Bus.Subscribe<TimeUpdateMessage>(OnTimeUpdate).AddTo(m_Disposer);

            m_NextSync = m_SyncRate;
        }

        private void OnTimeUpdate(TimeUpdateMessage message)
        {
            //todo adjust for ping here
            //technically safe to hanle our own message as it will just update the time to be the same. 
            //we can also adjust remote simulation speed to try keep it closer to the master here
            m_Time = message.Time;
        }

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

        public void SetMaster(bool isMaster)
        {
            m_AmMaster = isMaster;
        }

        /// <summary>
        /// Update the time based on real time since last update
        /// </summary>
        /// <param name="timeSinceLastUpdate">in seconds</param>
        public void UpdateTime(double timeSinceLastUpdate)
        {
            m_Time += (ulong)(timeSinceLastUpdate * m_SimulationSpeed);

            if (m_AmMaster && m_Time > m_NextSync)
            {
                m_NextSync = m_NextSync + m_SyncRate;
                m_Bus.Send(new TimeUpdateMessage(m_Time));
            }
            else
            {
                m_Bus.SendLocal(new TimeUpdateMessage(m_Time));
            }
        }

        public void Dispose()
        {
            m_Disposer.Dispose();
        }
    }
}
