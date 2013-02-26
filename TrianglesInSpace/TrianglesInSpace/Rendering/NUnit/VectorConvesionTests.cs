using NUnit.Framework;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Rendering.NUnit
{
    internal class VectorConvesionTests : TestSpecification
    {
        [Test]
        public void ConvertToAndFromVector3()
        {
            var vector = new Vector(5.7, 6.6);

            var vector3 = VectorConversions.ToOgreVector(vector);

            var returnedVector = VectorConversions.FromOgreVector(vector3);

            Assert.AreEqual(vector, returnedVector);
        }
    }
}
