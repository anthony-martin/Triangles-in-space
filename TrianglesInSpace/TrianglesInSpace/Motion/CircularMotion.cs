using Mogre;
using Angle = TrianglesInSpace.Primitives.Angle;

namespace TrianglesInSpace.Motion
{
	public class CircularMotion
	{
		private ulong m_StartTime;
		private double m_Radius;
		private Angle m_StartAngle;
		private Angle m_TurnRate;
		private double m_InitialSpeed;
		private Vector2 m_InitialPosition;


		/// <summary>
		/// primary constructor for circular motion objects
		/// </summary>
		/// <param name="startTime">The begining time for the motion</param>
		/// <param name="radius">The radius of the turning circle</param>
		/// <param name="startAngle">The angle to the start point on the circle</param>
		/// <param name="turnRate">The turn rate in radians</param>
		/// <param name="initialSpeed"></param>
		public CircularMotion(ulong startTime, double radius, Angle startAngle, Angle turnRate, double initialSpeed)
		{
			m_StartTime = startTime;
			m_Radius = radius;
			m_StartAngle = startAngle;
			m_TurnRate = turnRate;
			m_InitialSpeed = initialSpeed;

			m_InitialPosition = GetMotion(startTime);
		}

		public ulong StartTime
		{
			get
			{
				return m_StartTime;
			}
		}

		public Vector2 GetVelocity(ulong currentTime)
		{
			double vectorOffset;
			double timeElapsed = 0;
			double xComponent, yComponent;
			Vector2 temporyPosition;
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
			timeElapsed = (currentTime - m_StartTime);
			timeElapsed = timeElapsed / 1000;

			xComponent = (m_InitialSpeed * Math.Cos(m_StartAngle.Value + (m_TurnRate.Value * timeElapsed) + vectorOffset));

			yComponent = (m_InitialSpeed * Math.Sin(m_StartAngle.Value + (m_TurnRate.Value * timeElapsed) + vectorOffset));

			temporyPosition = new Vector2(xComponent, yComponent);

			return temporyPosition;
		}

		public Vector2 GetMotion(ulong currentTime)
		{
			double timeElapsed = 0;
			double xComponent, yComponent;

			timeElapsed = (currentTime - m_StartTime);
			timeElapsed = timeElapsed / 1000;

			xComponent = (m_Radius * Math.Cos(m_StartAngle.Value + (m_TurnRate.Value * timeElapsed)));

			yComponent = (m_Radius * Math.Sin(m_StartAngle.Value + (m_TurnRate.Value * timeElapsed)));

			return new Vector2(xComponent, yComponent) - m_InitialPosition;
		}
	}
}
