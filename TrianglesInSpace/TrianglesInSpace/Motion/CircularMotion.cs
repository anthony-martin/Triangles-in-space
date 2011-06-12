using Mogre;

namespace TrianglesInSpace.Motion
{
	public class CircularMotion
	{
		private ulong m_StartTime;


		/// <summary>
		/// primary constructor for circular motion objects
		/// </summary>
		/// <param name="startTime">The begining time for the motion</param>
		/// <param name="radius">The radius of the turning circle</param>
		/// <param name="startAngle">The angle to the start point on the circle</param>
		/// <param name="turnRate">The turn rate in radians</param>
		/// <param name="initialSpeed"></param>
		public CircularMotion(ulong startTime, double radius, double startAngle, double turnRate, double initialSpeed)
		{
			m_StartTime = startTime;
		}
	}
}
