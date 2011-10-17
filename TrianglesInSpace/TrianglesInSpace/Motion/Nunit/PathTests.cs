using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using NUnit.Framework;
using TrianglesInSpace.Primitives;
using Angle = TrianglesInSpace.Primitives.Angle;
using Math = System.Math;

namespace TrianglesInSpace.Motion.Nunit
{
	public class PathTests : TestSpecification
	{
		[Test]
		public void CreateTurningCirclesBasicPositiveY()
		{
			var path = new Path();
			Vector2 initialVelocity = new Vector2(0,1);
			double acceleration = 1;
			Vector2 circleOne;
			Vector2 circleTwo;
			path.DetermineTurningCircles( initialVelocity, acceleration, out circleOne, out circleTwo);

			var circleOneResultShouldBe = new Vector2(-1, 0);
			var circleTwoResultShouldBe = new Vector2(1, 0);


			Assert.AreEqual(circleOneResultShouldBe.x, circleOne.x, 0.000000001);
			Assert.AreEqual(circleOneResultShouldBe.y, circleOne.y, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.y, circleTwo.y, 0.000000001);
		}


		[Test]
		public void CreateTurningCirclesBasicPositiveX()
		{
			var path = new Path();
			Vector2 initialVelocity = new Vector2(1, 0);
			double acceleration = 1;
			Vector2 circleOne;
			Vector2 circleTwo;
			path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

			var circleOneResultShouldBe = new Vector2(0, 1);
			var circleTwoResultShouldBe = new Vector2(0, -1);


			Assert.AreEqual(circleOneResultShouldBe.x, circleOne.x, 0.000000001);
			Assert.AreEqual(circleOneResultShouldBe.y, circleOne.y, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.y, circleTwo.y, 0.000000001);
		}

		[Test]
		public void CreateTurningCirclesBasicNegativeY()
		{
			var path = new Path();
			Vector2 initialVelocity = new Vector2(0, -1);
			double acceleration = 1;
			Vector2 circleOne;
			Vector2 circleTwo;
			path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

			var circleOneResultShouldBe = new Vector2(1, 0);
			var circleTwoResultShouldBe = new Vector2(-1, 0);


			Assert.AreEqual(circleOneResultShouldBe.x, circleOne.x, 0.000000001);
			Assert.AreEqual(circleOneResultShouldBe.y, circleOne.y, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.y, circleTwo.y, 0.000000001);
		}


		[Test]
		public void CreateTurningCirclesBasicNegativeX()
		{
			var path = new Path();
			Vector2 initialVelocity = new Vector2(-1, 0);
			double acceleration = 1;
			Vector2 circleOne;
			Vector2 circleTwo;
			path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

			var circleOneResultShouldBe = new Vector2(0, -1);
			var circleTwoResultShouldBe = new Vector2(0, 1);


			Assert.AreEqual(circleOneResultShouldBe.x, circleOne.x, 0.000000001);
			Assert.AreEqual(circleOneResultShouldBe.y, circleOne.y, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.y, circleTwo.y, 0.000000001);
		}

		[Test]
		public void CreateTurningCirclesOnAngle()
		{
			var path = new Path();
			Vector2 initialVelocity = new Vector2(3, 4);
			double acceleration = 5;
			Vector2 circleOne;
			Vector2 circleTwo;
			path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

			var circleOneResultShouldBe = new Vector2(-4, 3);
			var circleTwoResultShouldBe = new Vector2(4, -3);


			Assert.AreEqual(circleOneResultShouldBe.x, circleOne.x, 0.000000001);
			Assert.AreEqual(circleOneResultShouldBe.y, circleOne.y, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.y, circleTwo.y, 0.000000001);
		} 

		[Test]
		public void SelectTuringCircleFirstCircleAbove()
		{
			var path = new Path();
			Vector2 circleOne = new Vector2(1,0);
			Vector2 circleTwo = new Vector2(-1, 0);
			Vector2 targetPosition = new Vector2(1,1);
			double turningRadius = 0.1;


			Vector2 selectedCircle = path.SelectTuriningCircle(circleOne, circleTwo, targetPosition, turningRadius);

			Assert.AreEqual(selectedCircle.x, circleOne.x, 0.000000001);
			Assert.AreEqual(selectedCircle.y, circleOne.y, 0.000000001);
		}

		[Test]
		public void SelectTuringCircleCloserToFirstButInsideRadius()
		{
			var path = new Path();
			Vector2 circleOne = new Vector2(1, 0);
			Vector2 circleTwo = new Vector2(-1, 0);
			Vector2 targetPosition = new Vector2(1, 1);
			double turningRadius = 3;


			Vector2 selectedCircle = path.SelectTuriningCircle(circleOne, circleTwo, targetPosition, turningRadius);

			Assert.AreEqual(selectedCircle.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(selectedCircle.y, circleTwo.y, 0.000000001);
		}

		[Test]
		public void SelectTuringCircleCloserToSecondPoint()
		{
			var path = new Path();
			Vector2 circleOne = new Vector2(1, 0);
			Vector2 circleTwo = new Vector2(-1, 0);
			Vector2 targetPosition = new Vector2(-1, 1);
			double turningRadius = 0.1;


			Vector2 selectedCircle = path.SelectTuriningCircle(circleOne, circleTwo, targetPosition, turningRadius);

			Assert.AreEqual(selectedCircle.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(selectedCircle.y, circleTwo.y, 0.000000001);
		}


		[Test]
		public void DetermineTurnDirectionClockwiseSimple()
		{
			var path = new Path();
			Vector2 velocity = new Vector2(0, 1);
			Vector2 turningCircleOffset = new Vector2(1, 0);


			var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.Clockwise, turnDirection);
		}


		[Test]
		public void DetermineTurnDirectionAntiClockwiseSimple()
		{
			var path = new Path();
			Vector2 velocity = new Vector2(0, 1);
			Vector2 turningCircleOffset = new Vector2(-1, 0);


			var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);
		}

		[Test]
		public void DetermineTurnDirectionOpositeClockwiseSimple()
		{
			var path = new Path();
			Vector2 velocity = new Vector2(0, -1);
			Vector2 turningCircleOffset = new Vector2(-1, 0);


			var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.Clockwise, turnDirection);
		}

		[Test]
		public void DetermineTurnDirectionOpositeAntiClockwiseSimple()
		{
			var path = new Path();
			Vector2 velocity = new Vector2(0, -1);
			Vector2 turningCircleOffset = new Vector2(1, 0);


			var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);
		}

		[Test]
		public void DetermineTurnDirectionrRoughly45DegreesClockwise()
		{
			var path = new Path();
			Vector2 velocity = new Vector2(1, 1);
			Vector2 turningCircleOffset = new Vector2(1, -1);

			var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.Clockwise, turnDirection);

			velocity = new Vector2(-1, -1);
			turningCircleOffset = new Vector2(-1, 1);

			turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.Clockwise, turnDirection);
		}

		[Test]
		public void DetermineTurnDirectionrRoughly45DegreesAntiClockwise()
		{
			var path = new Path();
			Vector2 velocity = new Vector2(1, 1);
			Vector2 turningCircleOffset = new Vector2(-1, 1);

			var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);

			velocity = new Vector2(-1, -1);
			turningCircleOffset = new Vector2(1, -1);

			turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

			Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);
		}

		[Test]
		public void DetermineTurnEndSimpleClockwise()
		{
			Vector2 turningCircle = new Vector2(2,0);
			Vector2 destination = new Vector2(6,3);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle + cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.Clockwise));

		}

		[Test]
		public void DetermineTurnEndSimpleAntiClockwise()
		{
			Vector2 turningCircle = new Vector2(2, 0);
			Vector2 destination = new Vector2(6, 3);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle - cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.AntiClockwise));

		}

		[Test]
		public void DetermineTurnEndInverseClockwise()
		{
			Vector2 turningCircle = new Vector2(2, 0);
			Vector2 destination = new Vector2(-2, -3);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle + cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.Clockwise));

		}

		[Test]
		public void DetermineTurnEndInverseAntiClockwise()
		{
			Vector2 turningCircle = new Vector2(2, 0);
			Vector2 destination = new Vector2(-2, -3);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle - cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.AntiClockwise));

		}


		[Test]
		public void DetermineTurnEndEastClockwise()
		{
			Vector2 turningCircle = new Vector2(2, 0);
			Vector2 destination = new Vector2(7, 0);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle + cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.Clockwise));
		}

		[Test]
		public void DetermineTurnEndEastAntiClockwise()
		{
			Vector2 turningCircle = new Vector2(2, 0);
			Vector2 destination = new Vector2(7, 0);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle - cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.AntiClockwise));
		}

		[Test]
		public void DetermineTurnEndWestClockwise()
		{
			Vector2 turningCircle = new Vector2(2, 0);
			Vector2 destination = new Vector2(-3, 0);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle + cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.Clockwise));

		}

		[Test]
		public void DetermineTurnEndWestAntiClockwise()
		{
			Vector2 turningCircle = new Vector2(2, 0);
			Vector2 destination = new Vector2(-3, 0);
			double turningCircleRadius = 2;

			Vector2 turnPointToDestinationOffset = destination - turningCircle;
			Angle tangle = new Angle(turnPointToDestinationOffset);
			Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
			Angle expectedAngle = tangle - cosangle;
			expectedAngle.ReduceAngle();
			var path = new Path();


			Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.AntiClockwise));

		}
	}
}
