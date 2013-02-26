using Mogre;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Rendering
{
    public static class VectorConversions
    {
        public static Vector3 ToOgreVector(Vector vector)
        {
            return new Vector3(vector.X, 0.0, vector.Y);
        }

        public static Vector FromOgreVector(Vector3 vector3)
        {
            return new Vector(vector3.x, vector3.z);
        }
    }
}
