
using System;
using Mogre;

namespace TrianglesInSpace.Motion
{
	public class LinearMotion
	{
		private Vector2 m_Velocity;
		private ulong m_StartTime;

		/// <summary>
		/// Time based constant velocity linear motion
		/// </summary>
		/// <param name="startTime">The begining time of this motion</param>
		/// <param name="velocity">The velocity per second or 1000 time units</param>
		public LinearMotion(ulong startTime, Vector2 velocity)
		{
			// the starting time for this set of motion
			m_StartTime = startTime;
			// the velocity per second or 1000 time units
			m_Velocity = velocity;
		}

		public Vector2 GetVelocity()
		{
			return m_Velocity;
		}

		public ulong StartTime
		{
			get
			{
				return m_StartTime;
			}
		}

		public Vector2 GetMotion(ulong currentTime)
		{
			double timeDIfference = (currentTime - m_StartTime);
			timeDIfference = timeDIfference/1000;
			return m_Velocity * timeDIfference;
		}

	}
}
