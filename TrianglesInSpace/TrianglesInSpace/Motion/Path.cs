using System;
using System.Collections.Generic;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;
using Angle = TrianglesInSpace.Primitives.Angle;
using Math = System.Math;

namespace TrianglesInSpace.Motion
{
    public class Path : IPath, IDisposable
    {
        private readonly string m_Name;
        private readonly double m_Acceleration;
        private IBus m_Bus;
        private Disposer m_Disposer;

        private CombinedMotion m_Motion;

        public Path()
        {
            m_Acceleration = 0;
        }

        public Path(double maximumAcceleration, IMotion startingMotion, IBus bus)
        {
            m_Name = "triangle";
            m_Bus = bus;
            m_Disposer = new Disposer();
            m_Bus.Subscribe<SetPathToTargetMessage>(OnSetPathToTarget).AddTo(m_Disposer);
            m_Bus.Subscribe<RequestPathMessage>(OnPathRequest).AddTo(m_Disposer);
            m_Acceleration = maximumAcceleration;

            m_Motion = new CombinedMotion(new List<IMotion>
           {
               startingMotion
           });
        }

        public void OnPathRequest(RequestPathMessage message)
        {
            m_Bus.Send(new PathMessage(m_Name, m_Motion.Path));
        }

        public void OnSetPathToTarget(SetPathToTargetMessage message)
        {
            var vector = message.WorldPosition;
            MoveToDestination(vector, message.Time);
            m_Bus.Send(new PathMessage(m_Name, m_Motion.Path));
        }

        public void MoveToDestination(Vector destination, ulong currentTime)
        {
            var currentMotion = m_Motion.GetCurrentMotion(currentTime);
            var initialPosition = currentMotion.GetCurrentPosition(currentTime);
            var initialVelocity = currentMotion.GetVelocity(currentTime);

            var newPath = CreatePathTo(destination, initialVelocity, initialPosition, currentTime);

            if (newPath[0].GetCurrentPosition(currentTime) != initialPosition)
            {
                throw new InvalidOperationException("The positions do not match up");
            }

            m_Motion = new CombinedMotion( new List<IMotion>(newPath));
        }

        public IMotion GetCurrentMotion(ulong currentTime)
        {
            return m_Motion.GetCurrentMotion(currentTime);
        }


        public List<IMotion> CreatePathTo(Vector destination, Vector initialVelocity, Vector initialPosition, ulong startTime)
        {
            // get initial velocity
            //determine turning circles
            Vector circleOnePosition, circleTwoPosition;

            double circleRadius = CalcRadius(initialVelocity.Length, m_Acceleration);

            DetermineTurningCircles(initialVelocity, circleRadius, out circleOnePosition, out circleTwoPosition);

            //select a turning circle

            Vector destinationRelativeToInitialPosition = destination - initialPosition;

            Vector selectedTuringCicle = SelectTuriningCircle(circleOnePosition, circleTwoPosition, destinationRelativeToInitialPosition, circleRadius);

            //determine turn direction
            TurnDirection turnDirection = DetermineTurnDirection(initialVelocity, selectedTuringCicle);

            //determine turn end
            Angle turnStart = new Angle(-selectedTuringCicle);
            // zero the destination around the turning circle
            var destinationRelativeToTurningCircle = destination - (initialPosition + selectedTuringCicle);

            //use the relative destination to pick an end point for the turn
            Angle turnEnd = DetermineTurnEnd(destinationRelativeToTurningCircle, circleRadius, turnDirection);

            Angle turnRate = DetermineTurnRate(initialVelocity.Length, circleRadius, turnDirection);

            var turnDuration = DetermineDurationOfTurn(turnStart, turnEnd, turnRate, turnDirection);
            turnDuration += startTime;
            // create circular motion

            var circle = new CircularMotion(startTime, circleRadius, turnStart, turnRate, initialVelocity.Length, initialPosition);

            Vector startOfLine = initialPosition + selectedTuringCicle + CoordinateConversions.RadialToVector(turnEnd, circleRadius);
            //Vector startOfLine = circle.GetCurrentPosition(turnDuration);
            // create linear motion

            var velocity = (destination - startOfLine);
            ulong destinationTime = (ulong)((velocity.Length / initialVelocity.Length) * 1000.0);
            velocity = velocity.Normalise();
            velocity = velocity * initialVelocity.Length;
            var linear = new LinearMotion(turnDuration, velocity, startOfLine);
            var linear2 = new LinearMotion(turnDuration + destinationTime, velocity, destination);

            //Assert.AreEqual(circle.GetVelocity(turnDuration), linear.GetVelocity(turnDuration));
            List<IMotion> path = new List<IMotion>
            {
                circle,
                linear,
                linear2,
            };
            return path;
        }
        /// <summary>
        /// Determines the positions of the two possible turning circles 
        /// Relative to the initial position which is not passed into the function
        /// </summary>
        /// <param name="initialVelocity">The current velocity of the object</param>
        /// <param name="radius">The desired radius of the object</param>
        /// <param name="circleOne">The first of the two possible turning circles</param>
        /// <param name="circleTwo">The second of the two possible turning circles</param>
        public void DetermineTurningCircles(Vector initialVelocity, double radius, out Vector circleOne, out Vector circleTwo)
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
        public Vector SelectTuriningCircle(Vector circleCentreOne, Vector circleCentreTwo, Vector targetPosition, double turningRadius)
        {
            double displacementOne = (circleCentreOne - targetPosition).Length;
            double displacementTwo = (circleCentreTwo - targetPosition).Length;

            Vector selectedCircle;

            if (displacementOne < displacementTwo && displacementOne >= turningRadius)
            {
                selectedCircle = circleCentreOne;
            }
            else
            {
                selectedCircle = displacementTwo < turningRadius ? circleCentreOne : circleCentreTwo;
            }

            return selectedCircle;
        }

        /// <summary>
        /// Determines if a turn is clockwise or anti clockwise
        /// </summary>
        /// <param name="velocity">The current velocity of the object</param>
        /// <param name="turiningCircleOffset">The position of the turning circle relative to the initial position</param>
        /// <returns>Clockwise or anti clockwise</returns>
        public TurnDirection DetermineTurnDirection(Vector velocity, Vector turiningCircleOffset)
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
        public Angle DetermineTurnEnd(Vector destination, double radius, TurnDirection turnDirection)
        {
            Angle angleTowardsDestination = new Angle(destination);

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

        public Angle DetermineTurnRate(double speed, double radius, TurnDirection turnDirection)
        {
            var turnTime = (Math.PI * radius) / speed;

            double turnRate = Math.PI / turnTime;

            if (turnDirection == TurnDirection.Clockwise)
            {
                turnRate = -turnRate;
            }

            return new Angle(turnRate);
        }

        public ulong DetermineDurationOfTurn(Angle start, Angle end, Angle turnRate, TurnDirection turnDirection)
        {
            if (turnDirection == TurnDirection.Clockwise)
            {
                if (end > start)
                {
                    end = new Angle(end.Value - (Math.PI * 2));
                }
            }

            if (turnDirection == TurnDirection.AntiClockwise)
            {
                if (end < start)
                {
                    end = new Angle(end.Value + (Math.PI * 2));
                }
            }
            var turnAngle = start - end;
            var stuff = (turnAngle.Value / turnRate.Value) * -1000.0;
            return (ulong)(Math.Round(stuff));
        }

        public void Dispose()
        {
            m_Disposer.Dispose();
        }
    }
}
