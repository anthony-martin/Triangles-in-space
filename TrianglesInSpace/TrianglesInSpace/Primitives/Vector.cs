using System;

namespace TrianglesInSpace.Primitives
{
    public struct Vector
    {
        public readonly double X;
        public readonly double Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector Zero
        {
            get
            {
                return new Vector(0, 0);
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

            }
        }

        public Vector Normalise()
        {
            return new Vector(X/Length, Y/Length);
        }

        public static Vector operator +(Vector leftSide, Vector rightSide)
        {
            return new Vector(leftSide.X + rightSide.X, leftSide.Y + rightSide.Y);
        }

        public static Vector operator -(Vector leftSide, Vector rightSide)
        {
            return new Vector(leftSide.X - rightSide.X, leftSide.Y - rightSide.Y);
        }

        public static Vector operator -(Vector leftSide)
        {
            return new Vector(-leftSide.X , -leftSide.Y);
        }

        public static Vector operator *(Vector leftSide, double rightSide)
        {
            return new Vector(leftSide.X * rightSide, leftSide.Y * rightSide);
        }

        public static Vector operator *(double leftSide, Vector rightSide)
        {
            return new Vector(leftSide * rightSide.X, leftSide * rightSide.Y);
        }

        public static Vector operator /(Vector leftSide, double rightSide)
        {
            return new Vector(leftSide.X / rightSide, leftSide.Y / rightSide);
        }

        public static bool operator ==(Vector leftSide, Vector rightSide)
        {
            return leftSide.X.Equals(rightSide.X) && leftSide.Y.Equals(rightSide.Y);
        }

        public static bool operator !=(Vector leftSide, Vector rightSide)
        {
            return !(leftSide == rightSide);
        }

        public bool Equals(Vector other)
        {
            return other.X.Equals(X) && other.Y.Equals(Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(Vector))
            {
                return false;
            }
            return Equals((Vector)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public override string ToString()
        {
            return "x = "+ X+", y = "+Y;
        }
    }
}
