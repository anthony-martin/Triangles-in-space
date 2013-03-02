namespace TrianglesInSpace.Primitives
{
    public struct Box
    {
        private readonly Vector m_TopLeftCorner;
        private readonly Vector m_BottomRightCorner;

        /// <summary>
        /// A box around 0,0 with X being the horizontal 
        /// And Y being the vertiacal
        /// </summary>
        /// <param name="x">the width of the box</param>
        /// <param name="y">the height of the box</param>
        public Box(double x, double y)
        {
            const double two = 2.0;
            m_TopLeftCorner = new Vector(-x / two, y / two);
            m_BottomRightCorner = new Vector(x / two, -y / two);
        }

        public Vector TopLeft
        {
            get
            {
                return m_TopLeftCorner;
            }
        }

        public Vector BottomRight
        {
            get
            {
                return m_BottomRightCorner;
            }
        }

        /// <summary>
        /// Checks to see if positionOne is within the box around positionTwo
        /// </summary>
        public bool Contains(Vector positionOne, Vector positionTwo)
        {
            var relativePosition = positionOne - positionTwo;
 
            return relativePosition.X >= TopLeft.X
                   && relativePosition.X <= BottomRight.X
                   && relativePosition.Y >= BottomRight.Y
                   && relativePosition.Y <= TopLeft.Y;

        }
    }
}
