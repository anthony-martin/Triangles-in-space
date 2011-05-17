using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phase1
{
    class GameMath
    {

        public static double calcAccel(double velocity, double radius)
        {
            double acceleration = 0.0;
            // dont divide by 0
            if(radius > 0)
            {
                acceleration = (Math.Pow(velocity, 2.0) / radius);
            }

            return acceleration;
        }

        public static double calcRadius(double velocity, double accel)
        {

            double radius = 0.0;
            // dont divide by 0
            if (accel > 0)
            {
                radius = (Math.Pow(velocity, 2.0) / accel);
            }

            return radius;

        }

        public static double calcMagnitude(Vector2 vector)
        {
            return calcMagnitude(vector.xPos,vector.yPos);
        }

        public static double calcMagnitude(double xVelocity, double yVelocity)
        {
            double speed = 0.0;
            // speed = sqrt(x^2+y^2)
            speed = Math.Sqrt((Math.Pow(xVelocity, 2.0) + Math.Pow(yVelocity, 2.0)));

            return speed;
        }

        public static int getFacingNumber(Vector2 vector)
        {
            return getFacingNumber(vector.xPos, vector.yPos);
        }


        public static int getFacingNumber(double xValue, double yValue)
        {
            int facing = 0;
                // positive x and y
            if ((xValue > 0) && (yValue > 0))
            {
                facing = 1;
            }
                // x negative y positive
            else if ((xValue < 0)&&(yValue > 0))
            {
                facing = 2;
            }
                // negative x and y
            else if((xValue < 0)&&(yValue < 0))
            {
                facing = 3;
            }
                // positive x, negative y
            else if ((xValue > 0)&&(yValue < 0))
            {
                facing = 4;
            }
                // positive x axis
            else if((xValue > 0) && (yValue == 0))
            {
                facing = 5;
            }
                // positive y axis
            else if ((xValue == 0) && (yValue > 0))
            {
                facing = 6;
            }
                // negative x axis
            else if((xValue < 0) && (yValue == 0))
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


        public static double getTanAngle(Vector2 vector)
        {
            return getTanAngle(vector.xPos,vector.yPos);
        }


        public static double getTanAngle(double xComponent, double yComponent)
        {
            double angle = 0.0;

            // get the facing to make sure we get the actual angle
            int facing = GameMath.getFacingNumber(xComponent, yComponent);

            switch (facing)
            {
                    // if the velocity is 0;
                case -1 : 
                    {
                        angle = 0.0;
                        break;
                    }
                    // positive x and y
                case 1:
                    {
                        angle = Math.Atan(yComponent / xComponent);
                        break;
                    }
                    // negative x positive y
                case 2:
                    {
                        angle = Math.Atan(yComponent / xComponent) +Math.PI;
                        break;
                    }
                    // posative x negative y
                case 3:
                    {
                        angle = Math.Atan(yComponent / xComponent) - Math.PI;
                        break;
                    }
                    // negative x and y
                case 4:
                    {
                        angle = Math.Atan(yComponent / xComponent);
                        break;
                    }
                    // positive x y =0
                case 5:
                    {
                        angle = 0.0;
                        break;
                    }
                    // positive y x=0
                case 6 : 
                    {
                        angle = Math.PI / 2;
                        break;
                    }
                    // negative x y=0
                case 7 :
                    {
                        angle = Math.PI;
                        break;
                    }
                    // negative y x=0
                case 8 : 
                    {
                        angle = -(Math.PI / 2);
                        break;
                    }
                    // just in case should never get in here
                default:
                    {
                        angle = 0.0;
                        break;
                    }
            }
            // make sure the angle between -Pi and Pi not including -Pi
            angle = reduceAngle(angle);

            return angle;
        }

        public static double reduceAngle(double angle)
        {
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

        // calculates the distance between point one and two
        public static double getDistance(Vector2 pointOne, Vector2 pointTwo)
        {
            double distance, xDiff, yDiff;

            xDiff = pointOne.xPos - pointTwo.xPos;
            yDiff = pointOne.yPos - pointTwo.yPos;

            distance = calcMagnitude(xDiff,yDiff);

            return distance;

        }
    }
}
