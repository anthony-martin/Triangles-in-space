
using System;
using Mogre;

namespace TrianglesInSpace.Motion
{
	public class LinearMotion
	{
		private Vector2 m_Velocity;

		public LinearMotion(Vector2 velocity)
		{
			m_Velocity = velocity;
		}

		public Vector2 GetVelocity()
		{
			return m_Velocity;
		}

	}
}
