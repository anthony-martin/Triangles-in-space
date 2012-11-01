using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Motion
{
	public class LinearMotion : IMotion
	{
		private Vector m_Velocity;
		private ulong m_StartTime;
		private Vector m_InitialPosition;

		/// <summary>
		/// Time based constant velocity linear motion
		/// </summary>
		/// <param name="startTime">The begining time of this motion</param>
		/// <param name="velocity">The velocity per second or 1000 time units</param>
		/// <param name="initialPosition">The starting point of the line</param>
		public LinearMotion(ulong startTime, Vector velocity, Vector initialPosition)
		{
			// the starting time for this set of motion
			m_StartTime = startTime;
			// the velocity per second or 1000 time units
			m_Velocity = velocity;
			m_InitialPosition = initialPosition;
		}

		public Vector GetVelocity(ulong currentTime)
		{
			//to match the interface and incase acceleration is added
			return m_Velocity;
		}

		public ulong StartTime
		{
			get
			{
				return m_StartTime;
			}
		}

		public Vector GetMotion(ulong currentTime)
		{
			double timeDIfference = (currentTime - m_StartTime);
			timeDIfference = timeDIfference/1000.0;
			return m_Velocity * timeDIfference;
		}

		public Vector GetCurrentPosition(ulong currentTime)
		{
			return m_InitialPosition + GetMotion(currentTime);
		}
	}
}
