using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using Math = System.Math;

namespace phase1
{
    class GameObject
    {
        private double m_Acceleration = 2;
        private LinkedList<Motion> m_Movement;
	private double m_LastTime;

		private static SceneManager m_SceneManager;

		protected static Entity m_NinjaEntity;
		protected static SceneNode m_NinjaNode;

		public GameObject(SceneManager sceneManager)
		{
			m_SceneManager = sceneManager;
            CircularMotion line = new CircularMotion(0.0, 2.0, 0.0, 1.0 ,20.0);
            Motion startMotion = new Motion(0.0,-5.0,line);
            LinkedListNode<Motion> firstNode = new LinkedListNode<Motion>(startMotion);
            m_Movement = new LinkedList<Motion>();
            m_Movement.AddFirst(firstNode);

			m_SceneManager.AmbientLight = new ColourValue(1, 1, 1);

			m_NinjaEntity = m_SceneManager.CreateEntity("Ninja", "ninja.mesh");

			m_NinjaNode = m_SceneManager.RootSceneNode.CreateChildSceneNode("NinjaNode");
			m_NinjaNode.AttachObject(m_NinjaEntity);
			m_NinjaNode.Position += Vector3.ZERO;

			addWaypoint(-10.0, 15.0);
        }

        public void draw(double time)
        {
	    m_LastTime = time;
            double xPosition = 0.0;
            double yPosition = 0.0;

			m_Movement.First.Value.getCurrentPosition(time, out xPosition, out yPosition);

			m_NinjaNode.SetPosition(xPosition, 0.0, yPosition);
            //addWaypoint(-10.0, 15.0);

        }

        public int addWaypoint(Vector2 destination)
        {
            int tempSuccess = addWaypoint(destination.xPos, destination.yPos);
            return tempSuccess;
        }


        public int addWaypoint(double xPos, double yPos)
        {
            double lastTime = m_LastTime;
            Vector2 destination = new Vector2(xPos, yPos);
            Vector2 initialPos;
            // get the current position
            double xPosition, yPosition;
            m_Movement.Last.Value.getCurrentPosition(lastTime, out xPosition, out yPosition);
            initialPos = new Vector2(xPosition, yPosition);

            // get the current velocity
            Vector2 vesselVelocity;
            m_Movement.First.Value.getCurrentVelocity(lastTime, out vesselVelocity);

            double velocityAngle;

            // calculate the angle of the current velocity
            velocityAngle = GameMath.getTanAngle(vesselVelocity.xPos, vesselVelocity.yPos);

            Vector2 circleOne, circleTwo;

            double vesselSpeed = GameMath.calcMagnitude(vesselVelocity);
            // calculate the radius if the turning circle based on the acceleration and current speed
            double radius = GameMath.calcRadius(vesselSpeed, m_Acceleration);

            // calcualte both of the turning circles
            circleOne = new Vector2((radius * Math.Cos(velocityAngle + (Math.PI / 2)))
                                   , (radius * Math.Sin(velocityAngle + (Math.PI / 2))));

            circleTwo = new Vector2((radius * Math.Cos(velocityAngle - (Math.PI / 2)))
                                   , (radius * Math.Sin(velocityAngle - (Math.PI / 2))));
            //test code

            circleOne.addOffset(xPosition, yPosition);

            circleTwo.addOffset(xPosition, yPosition);


            circleOne = selectTurningCircle(radius,destination, circleOne,circleTwo);

            circleTwo.xPos = xPosition - circleOne.xPos;
            circleTwo.yPos = yPosition - circleOne.yPos;
            int turnDirection = getTurnDirection(vesselVelocity, circleTwo);

            double turnStart;
			Vector2 turnEnd = determineTurnEnd(radius, initialPos, destination, circleOne, turnDirection, out turnStart);

        	double turnEndRadians = GameMath.getTanAngle(turnEnd);

            double turnTime = 0.0;
            double turnRate = determineTurnRate(turnDirection, radius, vesselSpeed
												, turnStart, turnEndRadians, out turnTime);


            turnTime = (turnStart - turnEndRadians)/turnRate;
			if(turnTime < 0)
			{
				turnTime = turnTime *(-1);
			}
			turnTime += lastTime; 
            CircularMotion turn = new CircularMotion(0.0, radius, turnStart, turnRate, vesselSpeed);
			Motion nextTurn = new Motion(initialPos, turnTime, turn);

            m_Movement.AddLast(nextTurn);

            //double distaceFromturn = 

            return 1;
        }

        private Vector2 selectTurningCircle(double radius, Vector2 destination, Vector2 circleOne, Vector2 circleTwo)
        {
            double distanceOne, distanceTwo;

            Vector2 selectedCircle;

            distanceOne = GameMath.getDistance(destination, circleOne);

            distanceTwo = GameMath.getDistance(destination, circleTwo);
            // select the closest turning point to the destination
            // that where the destination is not inside the turning circle
            if ((distanceOne <= distanceTwo) && (distanceOne >= radius))
            {
                selectedCircle = new Vector2(circleOne.xPos,circleOne.yPos);
            }
            else
            {
                selectedCircle = new Vector2(circleTwo.xPos, circleTwo.yPos);
            }

            return selectedCircle;
        }

		private Vector2 determineTurnEnd(double radius, Vector2 inStartVector2, 
            Vector2 inDestination, Vector2 turningCircle, int turnDirection, out double initialAngle)
        {
            Vector2 destination = new Vector2(inDestination);
            Vector2 startVector2 = new Vector2(inStartVector2);

            // zero the positions around the turning circle
            destination.subtractOffset(turningCircle.xPos,turningCircle.yPos);
            startVector2.subtractOffset(turningCircle.xPos, turningCircle.yPos);


            initialAngle = GameMath.getTanAngle(startVector2);

            // determine the distance between the centre of the turning circle
            // and the destination point
            double disCircToDest = GameMath.calcMagnitude(destination);

            // get the angle of the line from the centre of the turning circle
            // to the destination
            double tanAngle = GameMath.getTanAngle(destination);
            // get the angle of a triangle so that the line from the destination to the 
            //turning circle meets at 90 degrees
            double cosAngle = Math.Acos(radius/disCircToDest);

            // get the angle for both possible points on the circle
            double pointOneAngle = GameMath.reduceAngle( tanAngle + cosAngle);
            double pointTwoAngle = GameMath.reduceAngle( tanAngle - cosAngle);

			double selectedPoint;

            // change before completion
            if (turnDirection == 1)
            {
                selectedPoint = selectEndPointClockwise(pointOneAngle,pointTwoAngle,initialAngle);
            }
            else
            {
                selectedPoint = selectEndPointCounter(pointOneAngle, pointTwoAngle, initialAngle);
            }

			return new Vector2((radius * Math.Cos(selectedPoint)),
									(radius * Math.Sin(selectedPoint)));
            //return pointOneAngle; 
            //return tanAngle;
        }

        private double selectEndPointClockwise(double endOne, double endTwo, double initial)
        {
            double selection = 0.0;
            if ((endOne > initial) && (endTwo > initial))
            {
                if (endOne > endTwo)
                {
                    selection = endOne;
                }
                else
                {
                    selection = endTwo;
                }
            }
            else if ((endOne < initial) && (endTwo < initial))
            {
                if (endOne > endTwo)
                {
                    selection = endOne;
                }
                else
                {
                    selection = endTwo;
                }
            }
            else if (endOne == initial)
            {
                selection = endOne;
            }
            else if (endTwo == initial)
            {
                selection = endTwo;
            }
            else if ((endOne > initial) && (endTwo < initial))
            {
                selection = endTwo;
            }
            else if ((endOne < initial) && (endTwo > initial))
            {
                selection = endOne;
            }

            return selection;
        }

        private double selectEndPointCounter(double endOne, double endTwo, double initial)
        {
            double selection = 0.0;
            if ((endOne > initial) && (endTwo > initial))
            {
                if (endOne < endTwo)
                {
                    selection = endOne;
                }
                else
                {
                    selection = endTwo;
                }
            }
            else if ((endOne < initial) && (endTwo < initial))
            {
                if (endOne < endTwo)
                {
                    selection = endOne;
                }
                else
                {
                    selection = endTwo;
                }
            }
            else if (endOne == initial) 
            {
                selection = endOne;
            }
            else if (endTwo == initial)
            {
                selection = endTwo;
            }
            else if ((endOne > initial) && (endTwo < initial))
            {
                selection = endOne;
            }
            else if ((endOne < initial) && (endTwo > initial))
            {
                selection = endTwo;
            }

            return selection;
        }



        private int getTurnDirection(Vector2 velocity,Vector2 turnOffset)
        {
            int velocityFacing = GameMath.getFacingNumber(velocity);
            int turnFacing = GameMath.getFacingNumber(turnOffset);
            int clockwise = -1; // 1 = clockwise 0 = anticlockwise -1 = error

            if ((velocityFacing == (turnFacing + 3)) || (velocityFacing == (turnFacing - 1)))
            {
                clockwise = 1;
            }
            else if ((velocityFacing == (turnFacing - 3)) || (velocityFacing == (turnFacing + 1)))
            {
                clockwise = 0;
            }
            else
            {
                clockwise = -1;
            }

            return clockwise;
        }

        private double determineTurnRate(int turnDirection,double radius, double speed, double start, double end, out double turnTime)
        {
            double turnDist;
            double turnAngle;
            // determine the angle turned 
            // if the turn is clockwise
            if (turnDirection == 1)
            {
                // if the end angle is less then the start angle
                if (end < start)
                {
                    turnAngle = start - end;
                }
                else
                {
                    turnAngle = (2 * Math.PI) - (start - end);
                }
            }
            else
            {
                if (end > start)
                {
                    turnAngle = end - start;
                }
                else
                {
                    turnAngle = (2 * Math.PI) - (end - start);
                }
            }
            // get the disntance turned
            turnDist = turnAngle * radius;
            // divide the distance by the speed to get the time 
            // taken to complete the turn
            turnTime = turnDist / speed;
            // divide the turn angle by the time taken to turn 
            // to get the turn rate per second
            turnAngle = turnAngle / turnDist;
            // return the turn rate per second in radians
            return turnAngle;
        }
    }
}
