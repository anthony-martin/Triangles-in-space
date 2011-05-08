using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phase1
{
    class Position
    {
        private double xPosition;
        private double yPosition;

        // default constructor
        public Position()
        {
            xPosition = 0.0;
            yPosition = 0.0;
        }

        // normal constructor use most
        public Position(double xVal, double yVal)
        {
            xPosition = xVal;
            yPosition = yVal;
        }

        public Position(Position inPosition)
        {
            xPosition = inPosition.xPos;
            yPosition = inPosition.yPos;
        }

        // handles x 
        public double xPos
        {
            get { return xPosition; }
            set {xPosition = value;}
        }
        // handles y
        public double yPos
        {
            get { return yPosition; }
            set {yPosition = value;}
        }

        // gets both in 1 call if desired
        public void get(out double xOut, out double yOut)
        {
            xOut = xPosition;
            yOut = yPosition;
        }

        // reset class values
        public void set(double xIn, double yIn)
        {
            xPosition = xIn;
            yPosition = yIn;
        }

        // adds an offset to the current position using a positionObject
        public void addOffset(Position offset)
        {
            addOffset(offset.xPos, offset.yPos);
        }
        // adds an offset to the current position using two doubles
        public void addOffset(double xOffset, double yOffset)
        {
            xPosition = xPosition + xOffset;
            yPosition = yPosition + yOffset;
        }

        public void subtractOffset(double xOffset, double yOffset)
        {
            xPosition = xPosition - xOffset;
            yPosition = yPosition - yOffset;
        }

        public Position getSelf()
        {
            return this;
        }
    }
}
