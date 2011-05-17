using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phase1
{
    public class Vector2
    {
        private double xPosition;
        private double yPosition;

        // default constructor
        public Vector2()
        {
            xPosition = 0.0;
            yPosition = 0.0;
        }

        // normal constructor use most
        public Vector2(double xVal, double yVal)
        {
            xPosition = xVal;
            yPosition = yVal;
        }

        public Vector2(Vector2 inVector2)
        {
            xPosition = inVector2.xPos;
            yPosition = inVector2.yPos;
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
        public void addOffset(Vector2 offset)
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

        public Vector2 getSelf()
        {
            return this;
        }
    }
}
