using System.Linq;
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
            Vector initialVelocity = new Vector(0, 1);
            double acceleration = 1;
            Vector circleOne;
            Vector circleTwo;
            path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

            var circleOneResultShouldBe = new Vector(-1, 0);
            var circleTwoResultShouldBe = new Vector(1, 0);


            Assert.AreEqual(circleOneResultShouldBe.X, circleOne.X, 0.000000001);
            Assert.AreEqual(circleOneResultShouldBe.Y, circleOne.Y, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.X, circleTwo.X, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.Y, circleTwo.Y, 0.000000001);
        }


        [Test]
        public void CreateTurningCirclesBasicPositiveX()
        {
            var path = new Path();
            Vector initialVelocity = new Vector(1, 0);
            double acceleration = 1;
            Vector circleOne;
            Vector circleTwo;
            path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

            var circleOneResultShouldBe = new Vector(0, 1);
            var circleTwoResultShouldBe = new Vector(0, -1);


            Assert.AreEqual(circleOneResultShouldBe.X, circleOne.X, 0.000000001);
            Assert.AreEqual(circleOneResultShouldBe.Y, circleOne.Y, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.X, circleTwo.X, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.Y, circleTwo.Y, 0.000000001);
        }

        [Test]
        public void CreateTurningCirclesBasicNegativeY()
        {
            var path = new Path();
            Vector initialVelocity = new Vector(0, -1);
            double acceleration = 1;
            Vector circleOne;
            Vector circleTwo;
            path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

            var circleOneResultShouldBe = new Vector(1, 0);
            var circleTwoResultShouldBe = new Vector(-1, 0);


            Assert.AreEqual(circleOneResultShouldBe.X, circleOne.X, 0.000000001);
            Assert.AreEqual(circleOneResultShouldBe.Y, circleOne.Y, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.X, circleTwo.X, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.Y, circleTwo.Y, 0.000000001);
        }


        [Test]
        public void CreateTurningCirclesBasicNegativeX()
        {
            var path = new Path();
            Vector initialVelocity = new Vector(-1, 0);
            double acceleration = 1;
            Vector circleOne;
            Vector circleTwo;
            path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

            var circleOneResultShouldBe = new Vector(0, -1);
            var circleTwoResultShouldBe = new Vector(0, 1);


            Assert.AreEqual(circleOneResultShouldBe.X, circleOne.X, 0.000000001);
            Assert.AreEqual(circleOneResultShouldBe.Y, circleOne.Y, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.X, circleTwo.X, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.Y, circleTwo.Y, 0.000000001);
        }

        [Test]
        public void CreateTurningCirclesOnAngle()
        {
            var path = new Path();
            Vector initialVelocity = new Vector(3, 4);
            double acceleration = 5;
            Vector circleOne;
            Vector circleTwo;
            path.DetermineTurningCircles(initialVelocity, acceleration, out circleOne, out circleTwo);

            var circleOneResultShouldBe = new Vector(-4, 3);
            var circleTwoResultShouldBe = new Vector(4, -3);


            Assert.AreEqual(circleOneResultShouldBe.X, circleOne.X, 0.000000001);
            Assert.AreEqual(circleOneResultShouldBe.Y, circleOne.Y, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.X, circleTwo.X, 0.000000001);
            Assert.AreEqual(circleTwoResultShouldBe.Y, circleTwo.Y, 0.000000001);
        }

        [Test]
        public void SelectTuringCircleFirstCircleAbove()
        {
            var path = new Path();
            Vector circleOne = new Vector(1, 0);
            Vector circleTwo = new Vector(-1, 0);
            Vector targetPosition = new Vector(1, 1);
            double turningRadius = 0.1;


            Vector selectedCircle = path.SelectTuriningCircle(circleOne, circleTwo, targetPosition, turningRadius);

            Assert.AreEqual(selectedCircle.X, circleOne.X, 0.000000001);
            Assert.AreEqual(selectedCircle.Y, circleOne.Y, 0.000000001);
        }

        [Test]
        public void SelectTuringCircleCloserToFirstButInsideRadius()
        {
            var path = new Path();
            Vector circleOne = new Vector(1, 0);
            Vector circleTwo = new Vector(-1, 0);
            Vector targetPosition = new Vector(1, 1);
            double turningRadius = 1.5;


            Vector selectedCircle = path.SelectTuriningCircle(circleOne, circleTwo, targetPosition, turningRadius);

            Assert.AreEqual(selectedCircle.X, circleTwo.X, 0.000000001);
            Assert.AreEqual(selectedCircle.Y, circleTwo.Y, 0.000000001);
        }

        [Test]
        public void SelectTuringCircleCloserToSecondPoint()
        {
            var path = new Path();
            Vector circleOne = new Vector(1, 0);
            Vector circleTwo = new Vector(-1, 0);
            Vector targetPosition = new Vector(-1, 1);
            double turningRadius = 0.1;


            Vector selectedCircle = path.SelectTuriningCircle(circleOne, circleTwo, targetPosition, turningRadius);

            Assert.AreEqual(selectedCircle.X, circleTwo.X, 0.000000001);
            Assert.AreEqual(selectedCircle.Y, circleTwo.Y, 0.000000001);
        }

        [Test]
        public void SelectTurningCircleCloserToSecondPointWithinRadius()
        {
            var path = new Path();
            Vector circleOne = new Vector(1, 0);
            Vector circleTwo = new Vector(-1, 0);
            Vector targetPosition = new Vector(-1, 1);
            double turningRadius = 1.5;


            Vector selectedCircle = path.SelectTuriningCircle(circleOne, circleTwo, targetPosition, turningRadius);

            Assert.AreEqual(selectedCircle.X, circleOne.X, 0.000000001);
            Assert.AreEqual(selectedCircle.Y, circleOne.Y, 0.000000001);
        }

        [Test]
        public void DetermineTurnDirectionClockwiseSimple()
        {
            var path = new Path();
            Vector velocity = new Vector(0, 1);
            Vector turningCircleOffset = new Vector(1, 0);


            var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.Clockwise, turnDirection);
        }


        [Test]
        public void DetermineTurnDirectionAntiClockwiseSimple()
        {
            var path = new Path();
            Vector velocity = new Vector(0, 1);
            Vector turningCircleOffset = new Vector(-1, 0);


            var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);
        }

        [Test]
        public void DetermineTurnDirectionOpositeClockwiseSimple()
        {
            var path = new Path();
            Vector velocity = new Vector(0, -1);
            Vector turningCircleOffset = new Vector(-1, 0);


            var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.Clockwise, turnDirection);
        }

        [Test]
        public void DetermineTurnDirectionOpositeAntiClockwiseSimple()
        {
            var path = new Path();
            Vector velocity = new Vector(0, -1);
            Vector turningCircleOffset = new Vector(1, 0);


            var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);
        }

        [Test]
        public void DetermineTurnDirectionrRoughly45DegreesClockwise()
        {
            var path = new Path();
            Vector velocity = new Vector(1, 1);
            Vector turningCircleOffset = new Vector(1, -1);

            var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.Clockwise, turnDirection);

            velocity = new Vector(-1, -1);
            turningCircleOffset = new Vector(-1, 1);

            turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.Clockwise, turnDirection);
        }

        [Test]
        public void DetermineTurnDirectionrRoughly45DegreesAntiClockwise()
        {
            var path = new Path();
            Vector velocity = new Vector(1, 1);
            Vector turningCircleOffset = new Vector(-1, 1);

            var turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);

            velocity = new Vector(-1, -1);
            turningCircleOffset = new Vector(1, -1);

            turnDirection = path.DetermineTurnDirection(velocity, turningCircleOffset);

            Assert.AreEqual(TurnDirection.AntiClockwise, turnDirection);
        }

        [Test]
        public void DetermineTurnEndSimpleClockwise()
        {
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(6, 3);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
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
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(6, 3);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
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
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(-2, -3);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
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
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(-2, -3);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
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
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(7, 0);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
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
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(7, 0);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
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
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(-3, 0);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
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
            Vector turningCircle = new Vector(2, 0);
            Vector destination = new Vector(-3, 0);
            double turningCircleRadius = 2;

            Vector turnPointToDestinationOffset = destination - turningCircle;
            Angle tangle = new Angle(turnPointToDestinationOffset);
            Angle cosangle = new Angle(Math.Acos(2.0 / 5.0));
            Angle expectedAngle = tangle - cosangle;
            expectedAngle.ReduceAngle();
            var path = new Path();


            Assert.AreEqual(expectedAngle, path.DetermineTurnEnd(turnPointToDestinationOffset, turningCircleRadius, TurnDirection.AntiClockwise));

        }

        [Test]
        public void DetermineTunrRateClockwise()
        {
            TurnDirection turnDirection = TurnDirection.Clockwise;
            double speed = 1.0;
            double radius = 2.0;

            var path = new Path();

            var turnRate = path.DetermineTurnRate(speed, radius, turnDirection);

            Assert.AreEqual(new Angle(-(Math.PI / 2) / Math.PI), turnRate);
        }

        [Test]
        public void DetermineTunrRateAntiClockwise()
        {
            TurnDirection turnDirection = TurnDirection.AntiClockwise;
            double speed = 1.0;
            double radius = 2.0;

            var path = new Path();

            var turnRate = path.DetermineTurnRate(speed, radius, turnDirection);

            Assert.AreEqual(new Angle((Math.PI / 2) / Math.PI), turnRate);
        }

        [Test]
        public void DetermineDurationOfTurnClockwiseSimple()
        {
            Angle start = new Angle(Math.PI / 2);
            Angle end = new Angle();
            Angle rate = new Angle(-Math.PI / 10);
            TurnDirection turnDirection = TurnDirection.Clockwise;

            var path = new Path();

            var turnDuration = path.DetermineDurationOfTurn(start, end, rate, turnDirection);

            Assert.AreEqual(5000, turnDuration);
        }


        [Test]
        public void DetermineDurationOfTurnClockwiseComplex()
        {
            Angle start = new Angle(Math.PI - 0.1);
            Angle end = new Angle((Math.PI * 2) - 0.1);
            Angle rate = new Angle(-Math.PI / 10);
            TurnDirection turnDirection = TurnDirection.Clockwise;

            var path = new Path();

            var turnDuration = path.DetermineDurationOfTurn(start, end, rate, turnDirection);

            Assert.AreEqual(10000, turnDuration);
        }
        [Test]
        public void DetermineDurationOfTurnAntiClockwiseComplex()
        {
            Angle end = new Angle(Math.PI + 0.1);
            Angle start = new Angle((Math.PI * 2) + 0.1);
            Angle rate = new Angle(Math.PI / 10);
            TurnDirection turnDirection = TurnDirection.AntiClockwise;

            var path = new Path();

            var turnDuration = path.DetermineDurationOfTurn(start, end, rate, turnDirection);

            Assert.AreEqual(10000, turnDuration);
        }

        [Test]
        public void DetermineDurationOfTurnAntiClockwiseSimple()
        {
            Angle end = new Angle(Math.PI / 2);
            Angle start = new Angle((Math.PI * 2));
            Angle rate = new Angle(Math.PI / 10);
            TurnDirection turnDirection = TurnDirection.AntiClockwise;

            var path = new Path();

            var turnDuration = path.DetermineDurationOfTurn(start, end, rate, turnDirection);

            Assert.AreEqual(5000, turnDuration);
        }

        [Test]
        public void MotionReturnsList()
        {
            var path = new Path(3, new LinearMotion(0, new Vector(1, 0), Vector.Zero));

            path.MoveToDestination(new Vector(60,100), 1000 );

            Assert.AreEqual(3, path.Motion.Count());
        }
    }
}
