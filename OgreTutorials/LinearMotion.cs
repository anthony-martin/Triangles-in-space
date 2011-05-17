using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phase1
{
    class LinearMotion : Velocity
    {
        private Vector2 velocity;

        // used to determine the time on this velocty 
        // makes the overall numbers easier to handle
        private double startTime;

        //default constructor simply initialises things
        public LinearMotion()
        {
            // initialise the values
            velocity = new Vector2();
            startTime = 0;
        }

        // primary constructor
        public LinearMotion(double start, double xComponent, double yComponent)
        {
            velocity = new Vector2(xComponent, yComponent);

            // start times below 0 make no sense so disallow them
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
            // time isnt used here its just to make this consistent 
            // with circular motion where it is required
            // return the current velocity
            xComponent = velocity.xPos;
            yComponent = velocity.yPos;
        }

        public Vector2 getVelocity(double time)
        {
            // time isnt used here its just to make this consistent 
            // with circular motion where it is required
            // return the current velocity
            Vector2 outVeloc = new Vector2(velocity);

            return outVeloc;
        }

        public void getMovement(double time, out double xComponent, out double yComponent)
        {
            // if the time is before the start time something is wrong
            if (time >= startTime)
            {
                double elapsedTime;
                // get the amount of time that has passed
                elapsedTime = time - startTime;
                // return the velocity x the time
                xComponent = velocity.xPos * elapsedTime;
                yComponent = velocity.yPos * elapsedTime;
            }
            else
            {
                //movement hasnt started yet return 0
                xComponent = 0;
                yComponent = 0;
            }
        }

        public Vector2 getMovement(double time)
        {
            Vector2 tempPos;
            // if the time is before the start time something is wrong
            if (time >= startTime)
            {
                double elapsedTime;
                // get the amount of time that has passed
                elapsedTime = time - startTime;
                // return the velocity x the time
                tempPos = new Vector2( velocity.xPos * elapsedTime, velocity.yPos * elapsedTime);
            }
            else
            {
                //movement hasnt started yet return 0
                tempPos = new Vector2();
            }

            return tempPos;
        }
    }
}
