using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace phase1
{
    class GameObject
    {
        private double acceleration = 1;
        private LinkedList<Motion> movement;

        public GameObject()
        {
            CircularMotion line = new CircularMotion(0.0, 2.0, 0.0,1.0,2.0);
            Motion startMotion = new Motion(0.0,-5.0,line);
            LinkedListNode<Motion> firstNode = new LinkedListNode<Motion>(startMotion);
            movement = new LinkedList<Motion>();
            movement.AddFirst(firstNode);
        }

        public void draw()
        {
            double xPosition = 0.0;
            double yPosition = 0.0;

            addWaypoint(-10.0, 15.0);

        }

        public int addWaypoint(Position destination)
        {
            int tempSuccess = addWaypoint(destination.xPos, destination.yPos);
            return tempSuccess;
        }


        public int addWaypoint(double xPos, double yPos)
        {
            
            Position destination = new Position(xPos, yPos);
            Position initialPos;
            // get the current position
            double xPosition, yPosition;
            movement.First.Value.getCurrentPosition(0.0, out xPosition, out yPosition);
            initialPos = new Position(xPosition, yPosition);

            // get the current velocity
            Position vesselVelocity;
            movement.First.Value.getCurrentVelocity(0.0, out vesselVelocity);

            double velocityAngle;

            // calculate the angle of the current velocity
            velocityAngle = GameMath.getTanAngle(vesselVelocity.xPos, vesselVelocity.yPos);

            Position circleOne, circleTwo;

            double vesselSpeed = GameMath.calcMagnitude(vesselVelocity);
            // calculate the radius if the turning circle based on the acceleration and current speed
            double radius = GameMath.calcRadius(vesselSpeed, acceleration);

            // calcualte both of the turning circles
            circleOne = new Position((radius * Math.Cos(velocityAngle + (Math.PI / 2)))
                                   , (radius * Math.Sin(velocityAngle + (Math.PI / 2))));

            circleTwo = new Position((radius * Math.Cos(velocityAngle - (Math.PI / 2)))
                                   , (radius * Math.Sin(velocityAngle - (Math.PI / 2))));
            //test code

            circleOne.addOffset(xPosition, yPosition);

            circleTwo.addOffset(xPosition, yPosition);


            circleOne = selectTurningCircle(radius,destination, circleOne,circleTwo);

            circleTwo.xPos = xPosition - circleOne.xPos;
            circleTwo.yPos = yPosition - circleOne.yPos;
            int turnDirection = getTurnDirection(vesselVelocity, circleTwo);

            double turnEnd, turnStart;
            turnEnd = determineTurnEnd(radius, initialPos, destination, circleOne, turnDirection,out turnStart);

            double turnTime = 0.0;
            double turnRate = determineTurnRate(turnDirection, radius, vesselSpeed
                                                , turnStart, turnEnd, out turnTime);

            CircularMotion turn = new CircularMotion(0.0, radius, turnStart, turnRate, vesselSpeed);
            Motion nextTurn = new Motion(initialPos, turnEnd, turn);

            movement.AddLast(nextTurn);

            //initialPos =  new Position((circleOne.xPos+(turnEnd*)),)

            //// end test code

            return 1;
        }

        private Position selectTurningCircle(double radius, Position destination, Position circleOne, Position circleTwo)
        {
            double distanceOne, distanceTwo;

            Position selectedCircle;

            distanceOne = GameMath.getDistance(destination, circleOne);

            distanceTwo = GameMath.getDistance(destination, circleTwo);
            // select the closest turning point to the destination
            // that where the destination is not inside the turning circle
            if ((distanceOne <= distanceTwo) && (distanceOne >= radius))
            {
                selectedCircle = new Position(circleOne.xPos,circleOne.yPos);
            }
            else
            {
                selectedCircle = new Position(circleTwo.xPos, circleTwo.yPos);
            }

            return selectedCircle;
        }

        private double determineTurnEnd(double radius, Position inStartPosition, 
            Position inDestination, Position turningCircle, int turnDirection, out double initialAngle)
        {
            Position destination = new Position(inDestination);
            Position startPosition = new Position(inStartPosition);

            // zero the positions around the turning circle
            destination.subtractOffset(turningCircle.xPos,turningCircle.yPos);
            startPosition.subtractOffset(turningCircle.xPos, turningCircle.yPos);


            initialAngle = GameMath.getTanAngle(startPosition);

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

            double selectedPoint = 0.0;

            // determine the two points on the circle that match the determined angle
            Position endPointOne = new Position((radius*Math.Cos(pointOneAngle)),
                                    (radius*Math.Sin(pointOneAngle)));

            Position endPointTwo = new Position((radius * Math.Cos(pointTwoAngle)),
                                    (radius * Math.Sin(pointTwoAngle)));
            // change before completion
            if (turnDirection == 1)
            {
                selectedPoint = selectEndPointClockwise(pointOneAngle,pointTwoAngle,initialAngle);
            }
            else
            {
                selectedPoint = selectEndPointCounter(pointOneAngle, pointTwoAngle, initialAngle);
            }

            return selectedPoint;
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



        private int getTurnDirection(Position velocity,Position turnOffset)
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
