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

        public static Vector operator +(Vector leftSide, Vector rightSide)
        {
            return new Vector(leftSide.X + rightSide.X, leftSide.Y + rightSide.Y);
        }

        public static Vector operator -(Vector leftSide, Vector rightSide)
        {
            return new Vector(leftSide.X - rightSide.X, leftSide.Y - rightSide.Y);
        }
    }
}
