using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phase1
{
    class CircularMotion : Velocity
    {
        private double radius;
        private double startAngle;
        private double turnRate;
        private double speed;
        private double startTime;

        // default constructor simple initialisation 
        // not reccomended for use
        public CircularMotion()
        {
            radius = 1;
            startAngle = 0;
            turnRate = 0.25;
            speed = 0;
            startTime = 0;
        }

        public CircularMotion(double start, double rad, double startAng, double turn, double speedIn)
        {
            radius = rad;

            startAngle = startAng;

            turnRate = turn;

            speed = speedIn;

            // make sure the start time is after things have started
            if (start >= 0)
            {
                startTime = start;
            }
            else
            {
                startTime = 0;
            }


        }

        public void getVelocity(double time, out double xComponent, out double yComponent)
        {
            double vectorOffset;
            double timeElapsed = 0;
            // the vector is 90degrees from the angle of acceleration
            // the angle to the current position
            if (turnRate >= 0)
            {
                vectorOffset = Math.PI / 2;
            }
            else
            {
                vectorOffset = (-1 * (Math.PI /2));
            }

            // set the elapsed time to get an accurate resutlt
            timeElapsed = time - startTime;

            xComponent = (speed * Math.Cos(startAngle + (turnRate * timeElapsed) + vectorOffset)) * speed;

            yComponent = (speed * Math.Sin(startAngle + (turnRate * timeElapsed) + vectorOffset)) * speed;
        }

        public Position getVelocity(double time)
        {
            double vectorOffset;
            double timeElapsed = 0;
            double xComponent, yComponent;
            Position tempPos;
            // the vector is 90degrees from the angle of acceleration
            // the angle to the current position
            if (turnRate >= 0)
            {
                vectorOffset = Math.PI / 2;
            }
            else
            {
                vectorOffset = (-1 * (Math.PI / 2));
            }

            // set the elapsed time to get an accurate resutlt
            timeElapsed = time - startTime;

            xComponent = (speed * Math.Cos(startAngle + (turnRate * timeElapsed) + vectorOffset));

            yComponent = (speed * Math.Sin(startAngle + (turnRate * timeElapsed) + vectorOffset));
            
            tempPos = new Position(xComponent, yComponent);

            return tempPos;
        }


        public void getMovement(double time, out double xComponent, out double yComponent)
        {
            double timeElapsed = 0;

            // set the elapsed time to get an accurate resutlt
            timeElapsed = time - startTime;

            // calcualte the xPosition at input time 
            xComponent = (radius * Math.Cos(startAngle + (turnRate * time)))* speed;
            // calculate the yPosition at input time
            yComponent = (radius * Math.Sin(startAngle + (turnRate * time))) * speed; ;

        }

        public Position getMovement(double time)
        {
            double timeElapsed = 0;
            double xComponent, yComponent;
            Position tempPos;
            // set the elapsed time to get an accurate resutlt
            timeElapsed = time - startTime;

            // calcualte the xPosition at input time 
            xComponent = (radius * Math.Cos(startAngle + (turnRate * time))) * speed;
            // calculate the yPosition at input time
            yComponent = (radius * Math.Sin(startAngle + (turnRate * time))) * speed;

            tempPos = new Position(xComponent, yComponent);

            return tempPos;

        }
    }
}
