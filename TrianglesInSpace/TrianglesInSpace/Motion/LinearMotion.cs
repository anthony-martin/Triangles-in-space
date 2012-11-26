using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Motion
{
	public class LinearMotion : IMotion
	{
		private readonly Vector m_Velocity;
        private readonly ulong m_StartTime;
        private readonly Vector m_InitialPosition;

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

	    public bool Equals(LinearMotion other)
	    {
	        if (ReferenceEquals(null, other))
	        {
	            return false;
	        }
	        if (ReferenceEquals(this, other))
	        {
	            return true;
	        }
	        return other.m_Velocity.Equals(m_Velocity) 
                    && other.m_StartTime == m_StartTime 
                    && other.m_InitialPosition.Equals(m_InitialPosition);
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj))
	        {
	            return false;
	        }
	        if (ReferenceEquals(this, obj))
	        {
	            return true;
	        }
	        if (obj.GetType() != typeof(LinearMotion))
	        {
	            return false;
	        }
	        return Equals((LinearMotion) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            int result = m_Velocity.GetHashCode();
	            result = (result * 397) ^ m_StartTime.GetHashCode();
	            result = (result * 397) ^ m_InitialPosition.GetHashCode();
	            return result;
	        }
	    }

	    public static bool operator ==(LinearMotion left, LinearMotion right)
	    {
	        return Equals(left, right);
	    }

	    public static bool operator !=(LinearMotion left, LinearMotion right)
	    {
	        return !Equals(left, right);
	    }
	}
}
