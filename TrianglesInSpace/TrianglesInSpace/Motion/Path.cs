using System;
using Mogre;
using TrianglesInSpace.Primitives;
using Angle = TrianglesInSpace.Primitives.Angle;
using Math = System.Math;

namespace TrianglesInSpace.Motion
{
	public class Path
	{
		private double m_Acceleration;

		public Path()
		{
			m_Acceleration = 0;
		}

		public Path(double maximumAcceleration)
		{
			m_Acceleration = maximumAcceleration;
		}


		public void CreatePathTo(Vector2 destination, Vector2 initialVelocity)
		{
			// get initial velocity
			//determine turning circles
			Vector2 circleOnePosition, circleTwoPosition;

			double circleRadius = CalcRadius(initialVelocity.Length, m_Acceleration);

			DetermineTurningCircles(initialVelocity, circleRadius, out circleOnePosition, out circleTwoPosition);

			//select a turning circle
			Vector2 selectedTuringCicle =  SelectTuriningCircle(circleOnePosition, circleTwoPosition, destination, circleRadius);

			//determine turn direction
			TurnDirection turnDirection = DetermineTurnDirection(initialVelocity, selectedTuringCicle);

			//determine turn end
			Angle turnStart = new Angle(-selectedTuringCicle);
			// zero the destination arou
			Angle turnEnd = DetermineTurnEnd(destination, circleRadius, turnDirection);

			// create circular motion

			// create linear motion

			// add motion to list
		}
		/// <summary>
		/// Determines the positions of the two possible turning circles 
		/// Relative to the initial position which is not passed into the function
		/// </summary>
		/// <param name="initialVelocity">The current velocity of the object</param>
		/// <param name="acceleration">The desired acceleration of the object</param>
		/// <param name="circleOne">The first of the two possible turning circles</param>
		/// <param name="circleTwo">The second of the two possible turning circles</param>
		public void DetermineTurningCircles(Vector2 initialVelocity, double radius, out Vector2 circleOne, out Vector2 circleTwo)
		{
			Angle velocityAngle;

			// calculate the angle of the current velocity
			velocityAngle = new Angle(initialVelocity);
			velocityAngle.ReduceAngle();

			//double vesselSpeed = initialVelocity.Length;
			// calculate the radius if the turning circle based on the acceleration and current speed
			//double radius = CalcRadius(vesselSpeed, acceleration);

			// calcualte both of the turning circles
			Angle angleOne = new Angle(velocityAngle.Value + (Math.PI / 2));
			circleOne = CoordinateConversions.RadialToVector(angleOne, radius);

			Angle angleTwo = new Angle(velocityAngle.Value - (Math.PI / 2));
			circleTwo = CoordinateConversions.RadialToVector(angleTwo, radius);
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
		/// <summary>
		/// Select the closest turning circle to the desitnation unless the closest circle is within the 
		/// radius of the turning circle.
		/// All positions need to be in the same reference frame
		/// </summary>
		/// <param name="circleCentreOne"></param>
		/// <param name="circleCentreTwo"></param>
		/// <param name="targetPosition">The destination</param>
		/// <param name="turningRadius">The radius of the circles</param>
		/// <returns></returns>
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

		/// <summary>
		/// Determines if a turn is clockwise or anti clockwise
		/// </summary>
		/// <param name="velocity">The current velocity of the object</param>
		/// <param name="turiningCircleOffset">The position of the turning circle relative to the initial position</param>
		/// <returns>Clockwise or anti clockwise</returns>
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
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="destination">The position of the destination relative to the turning point</param>
		/// <param name="radius">The radius of the turning circle</param>
		/// <param name="turnDirection">If the turn is clockwise or anti clockwise</param>
		/// <returns></returns>
		public Angle DetermineTurnEnd(Vector2 destination, double radius, TurnDirection turnDirection)
		{
			Angle angleTowardsDestination = new Angle( destination);

			Angle tangentAngles = new Angle(Math.Acos(radius / destination.Length));

			Angle desiredEndPoint;
			if (turnDirection == TurnDirection.Clockwise)
			{
				desiredEndPoint = angleTowardsDestination + tangentAngles;
			}
			else
			{
				desiredEndPoint = angleTowardsDestination - tangentAngles;
			}
			desiredEndPoint.ReduceAngle();
			return desiredEndPoint;
		}

		
	}
}
