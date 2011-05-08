using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phase1
{
    class Motion
    {
        private Position displacement; // represents the initial position

        private Velocity velocity;// represents any motion in the object

        private double validTill = -1; // the max period that this motion is valid till

        // default constructor
        public Motion()
        {
            displacement = new Position();
        }

        // complete constructor expected for general use
        public Motion(double xPos, double yPos, double endTime, Velocity movement)
        {
            // set the inital positions
            displacement = new Position(xPos, yPos);
            // make sure the time is valid
            // catch the return just to be proper
            bool success = setValidTill( endTime);
            
            // set the input velocity as the movement
            velocity = movement;

        }

        // complete constructor using a postion object
        public Motion(Position initial, double endTime, Velocity movement)
        {
            // set the inital positions
            displacement = initial;
            // make sure the time is valid
            // catch the return just to be proper
            bool success = setValidTill(endTime);

            // set the input velocity as the movement
            velocity = movement;

        }


        // secodary constructor for movements without an end time
        public Motion(double xPos, double yPos, Velocity movement)
        {
            // set the inital positions
            displacement = new Position(xPos, yPos);

            // set the input velocity as the movement
            velocity = movement;
        }
        // secondary constructor using a postion object
        public Motion(Position initial, Velocity movement)
        {
            // set the inital position
            displacement = initial;
            // set the initial velocity
            velocity = movement;
        }


        public bool getCurrentPosition(double time, out double xComponent, out double yComponent)
        {
            bool success = false;
            // if the time is before the end time for this motion
            if (validTill < time)
            {
                // get the movement from the velocity
                velocity.getMovement(time, out xComponent, out yComponent);

                // add the motion to the initial position
                xComponent += displacement.xPos;
                yComponent += displacement.yPos;
                // valid time return a result
                success = true;
            }
            else
            {
                // make sure they have been initialised to something
                xComponent = 0;
                yComponent = 0;
                // invalid time 
                success = false;
            }
            // return true if a valid request false if invalid
            return success;
        }


        // get current position using a position object
        public bool getCurrentPosition(double time, out Position currentPos)
        {
            bool success = false;
            // if the time is before the end time for this motion
            if (validTill < time)
            {
                // get the movement from the velocity
                currentPos = velocity.getMovement(time);

                // add the motion to the initial position
                currentPos.addOffset(displacement);
                // valid time return a result
                success = true;
            }
            else
            {
                // make sure they have been initialised to something
                currentPos = new Position();
                // invalid time 
                success = false;
            }
            // return true if a valid request false if invalid
            return success;
        }

        public bool getCurrentVelocity(double time, out double xComponent, out double yComponent)
        {
            bool success = false;
            // if the time is before the end time for this motion
            if (validTill < time)
            {
                // get the movement from the velocity
                velocity.getVelocity(time, out xComponent, out yComponent);

                // valid time return a result
                success = true;
            }
            else
            {
                // make sure they have been initialised to something
                xComponent = 0;
                yComponent = 0;
                // invalid time 
                success = false;
            }
            // return true if a valid request false if invalid
            return success;
        }

        // get current velocity using a Position object
        public bool getCurrentVelocity(double time, out Position currentVelocity)
        {
            bool success = false;
            // if the time is before the end time for this motion
            if (validTill < time)
            {
                // get the movement from the velocity
                currentVelocity = velocity.getVelocity(time);

                // valid time return a result
                success = true;
            }
            else
            {
                // make sure they have been initialised to something
                currentVelocity = new Position();
                // invalid time 
                success = false;
            }
            // return true if a valid request false if invalid
            return success;
        }

        public Velocity getVelocity()
        {
            return velocity;
        }

        public bool setValidTill(double endTime)
        {
            bool success = false;

            // if the end time is in the valid range 
            if (endTime > 0)
            {
                // set validTill to the new time
                validTill = endTime;
                // update was successful
                success = true;
            }
            // return if validTill was updated
            return success;
        }
    }


}
