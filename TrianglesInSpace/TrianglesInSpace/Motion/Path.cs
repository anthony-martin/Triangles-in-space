using System;
using Mogre;
using TrianglesInSpace.Primitives;
using Angle = TrianglesInSpace.Primitives.Angle;
using Math = System.Math;

namespace TrianglesInSpace.Motion
{
	public class Path
	{
		
		public void CreatePathTo()
		{
			
		}

		public void DetermineTurningCircles(Vector2 initialVelocity, double acceleration, out Vector2 circleOne, out Vector2 circleTwo)
		{
			Angle velocityAngle;

			// calculate the angle of the current velocity
			velocityAngle = new Angle(initialVelocity);
			velocityAngle.ReduceAngle();

			double vesselSpeed = initialVelocity.Length;
			// calculate the radius if the turning circle based on the acceleration and current speed
			double radius = CalcRadius(vesselSpeed, acceleration);

			// calcualte both of the turning circles
			circleOne = new Vector2((radius * Math.Cos(velocityAngle.Value + (Math.PI / 2)))
								   , (radius * Math.Sin(velocityAngle.Value + (Math.PI / 2))));

			circleTwo = new Vector2((radius * Math.Cos(velocityAngle.Value - (Math.PI / 2)))
								   , (radius * Math.Sin(velocityAngle.Value - (Math.PI / 2))));
		}

		public static double CalcRadius(double velocity, double accel)
		{
			double radius = 0.0;
			// dont divide by 0
			if (accel > 0)
			{
				radius = (Math.Pow(velocity, 2.0) / accel);
			}

			return radius;
		}

		public Vector2 SelectTuriningCircle(Vector2 circleCentreOne, Vector2 circleCentreTwo, Vector2 targetPosition, double turningRadius)
		{
			double displacementOne = (circleCentreOne - targetPosition).Length;
			double displacementTwo = (circleCentreTwo - targetPosition).Length;

			Vector2 selectedCircle;

			if(displacementOne < displacementTwo && displacementOne >= turningRadius )
			{
				selectedCircle = circleCentreOne;
			}
			else
			{
				selectedCircle = circleCentreTwo;
			}

			return selectedCircle;
		}

		public TurnDirection DetermineTurnDirection(Vector2 velocity, Vector2 turiningCircleOffset)
		{
			int velocityFacing = Angle.FacingNumber(velocity);
			int offsetFacing = Angle.FacingNumber(turiningCircleOffset);

			TurnDirection turnDirection = TurnDirection.Unknown;

			if ((velocityFacing == (offsetFacing + 3)) || (velocityFacing == (offsetFacing - 1)))
			{
				turnDirection = TurnDirection.AntiClockwise;
			}
			else if ((velocityFacing == (offsetFacing - 3)) || (velocityFacing == (offsetFacing + 1)))
			{
				turnDirection = TurnDirection.Clockwise;
			}

			return turnDirection;
		}
	}
}
