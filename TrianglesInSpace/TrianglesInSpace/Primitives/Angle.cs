using Mogre;
using Math = System.Math;

namespace TrianglesInSpace.Primitives
{
	public struct Angle
	{
		private double m_Radians;

		public Angle(double angle)
		{
			m_Radians = angle;
		}

		public Angle(Vector2 vector)
		{
			m_Radians = TanAngle(vector);
		}

		public double Value
		{
			get
			{
				return m_Radians;
			}
		}

		public static double ReduceAngle(double angle)
		{
            //angle%(2*Math.PI)
			// make sure the angle between -Pi and Pi not including -Pi
			while ((angle > Math.PI) || (angle <= (-Math.PI)))
			{
				if (angle > Math.PI)
				{
					angle = angle - (2 * Math.PI);
				}
				else
				{
					angle = angle + (2 * Math.PI);
				}
			}

			return angle;
		}

		public void ReduceAngle()
		{
			m_Radians = ReduceAngle(m_Radians);
		}
		/// <summary>
		/// Creates a number for the facing of the vector
		/// 261
		/// 7 5
		/// 384
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static int FacingNumber(Vector2 vector)
		{
			double xValue = vector.x;
			double yValue = vector.y;

			int facing = 0;
			// positive x and y
			if ((xValue > 0) && (yValue > 0))
			{
				facing = 1;
			}
			// x negative y positive
			else if ((xValue < 0) && (yValue > 0))
			{
				facing = 2;
			}
			// negative x and y
			else if ((xValue < 0) && (yValue < 0))
			{
				facing = 3;
			}
			// positive x, negative y
			else if ((xValue > 0) && (yValue < 0))
			{
				facing = 4;
			}
			// positive x axis
			else if ((xValue > 0) && (yValue == 0))
			{
				facing = 5;
			}
			// positive y axis
			else if ((xValue == 0) && (yValue > 0))
			{
				facing = 6;
			}
			// negative x axis
			else if ((xValue < 0) && (yValue == 0))
			{
				facing = 7;
			}
			// negative y axis
			else if ((xValue == 0) && (yValue < 0))
			{
				facing = 8;
			}
			// there is no angle or something fell through
			else
			{
				facing = -1;
			}

			return facing;
		}

		public static double TanAngle(Vector2 vector)
		{
			return ReduceAngle(Math.Atan2(vector.y, vector.x));
		}

		#region mathOverloads
		public static bool operator ==(Angle leftSide , Angle rightSide)
		{
			return leftSide.m_Radians == rightSide.m_Radians;
		}

		public static bool operator !=(Angle leftSide , Angle rightSide)
		{
			return !(leftSide == rightSide);
		}

		public static bool operator >(Angle leftSide, Angle rightSide)
		{
			return leftSide.m_Radians > rightSide.m_Radians;
		}

		public static bool operator <(Angle leftSide, Angle rightSide)
		{
			return leftSide.m_Radians < rightSide.m_Radians;
		}

		public static bool operator >=(Angle leftSide, Angle rightSide)
		{
			return leftSide.m_Radians >= rightSide.m_Radians;
		}

		public static bool operator <=(Angle leftSide, Angle rightSide)
		{
			return leftSide.m_Radians <= rightSide.m_Radians;
		}

		public static Angle operator +(Angle leftSide, Angle rightSide)
		{
			return new Angle(leftSide.m_Radians + rightSide.m_Radians);
		}

		public static Angle operator -(Angle leftSide, Angle rightSide)
		{
			return new Angle(leftSide.m_Radians - rightSide.m_Radians);
		}

		public override string ToString()
		{
			return m_Radians.ToString();
		}

		#endregion 
	}
}
