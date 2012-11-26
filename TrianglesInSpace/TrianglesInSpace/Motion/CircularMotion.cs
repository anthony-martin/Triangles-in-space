using Mogre;
using TrianglesInSpace.Primitives;
using Angle = TrianglesInSpace.Primitives.Angle;

namespace TrianglesInSpace.Motion
{
	public class CircularMotion : IMotion
	{
		private readonly ulong m_StartTime;
		private readonly double m_Radius;
        private readonly Angle m_StartAngle;
        private readonly Angle m_TurnRate;
        private readonly double m_InitialSpeed;
        private readonly Vector m_CircleOffset;
        private readonly Vector m_InitialPosition;


		/// <summary>
		/// primary constructor for circular motion objects
		/// </summary>
		/// <param name="startTime">The begining time for the motion</param>
		/// <param name="radius">The radius of the turning circle</param>
		/// <param name="startAngle">The angle to the start point on the circle</param>
		/// <param name="turnRate">The turn rate in radians</param>
		/// <param name="initialSpeed">Speed used to determine the current velocity</param>
		/// <param name="initialPosition">The position when this motion started</param>
		public CircularMotion(ulong startTime, double radius, Angle startAngle, Angle turnRate, double initialSpeed, Vector initialPosition)
		{
			m_StartTime = startTime;
			m_Radius = radius;
			m_StartAngle = startAngle;
			m_TurnRate = turnRate;
			m_InitialSpeed = initialSpeed;

			m_CircleOffset = CoordinateConversions.RadialToVector(startAngle, m_Radius);
			m_InitialPosition = initialPosition;
		}

		public ulong StartTime
		{
			get
			{
				return m_StartTime;
			}
		}

		public Vector GetVelocity(ulong currentTime)
		{
			double vectorOffset;
			// the vector is 90degrees from the angle of acceleration
			// the angle to the current position
			if (m_TurnRate >= new Angle(0.0))
			{
				vectorOffset = Math.PI / 2;
			}
			else
			{
				vectorOffset = (-1 * (Math.PI / 2));
			}

			// set the elapsed time to get an accurate resutlt
            double timeElapsed = (currentTime - m_StartTime);
			timeElapsed = timeElapsed / 1000.0;

			Angle angle = new Angle(m_StartAngle.Value + (m_TurnRate.Value * timeElapsed) + vectorOffset);
			return CoordinateConversions.RadialToVector(angle, m_InitialSpeed);
		}

		public Vector GetMotion(ulong currentTime)
		{
			double timeElapsed = (currentTime - m_StartTime);
			timeElapsed = timeElapsed / 1000.0;

			Angle angle =new Angle( m_StartAngle.Value + (m_TurnRate.Value*timeElapsed));
			var positionOnCirlce = CoordinateConversions.RadialToVector(angle, m_Radius);

			return positionOnCirlce - m_CircleOffset;
		}

		public Vector GetCurrentPosition(ulong currentTime)
		{
			return m_InitialPosition + GetMotion(currentTime);
		}

	    public bool Equals(CircularMotion other)
	    {
	        if (ReferenceEquals(null, other))
	        {
	            return false;
	        }
	        if (ReferenceEquals(this, other))
	        {
	            return true;
	        }
	        return other.m_StartTime == m_StartTime 
                    && other.m_StartAngle.Equals(m_StartAngle) 
                    && other.m_Radius.Equals(m_Radius) 
                    && other.m_TurnRate.Equals(m_TurnRate) 
                    && other.m_InitialSpeed.Equals(m_InitialSpeed) 
                    && other.m_CircleOffset.Equals(m_CircleOffset) 
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
	        if (obj.GetType() != typeof(CircularMotion))
	        {
	            return false;
	        }
	        return Equals((CircularMotion) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            int result = m_StartTime.GetHashCode();
	            result = (result * 397) ^ m_StartAngle.GetHashCode();
	            result = (result * 397) ^ m_Radius.GetHashCode();
	            result = (result * 397) ^ m_TurnRate.GetHashCode();
	            result = (result * 397) ^ m_InitialSpeed.GetHashCode();
	            result = (result * 397) ^ m_CircleOffset.GetHashCode();
	            result = (result * 397) ^ m_InitialPosition.GetHashCode();
	            return result;
	        }
	    }

	    public static bool operator ==(CircularMotion left, CircularMotion right)
	    {
	        return Equals(left, right);
	    }

	    public static bool operator !=(CircularMotion left, CircularMotion right)
	    {
	        return !Equals(left, right);
	    }
	}
}
