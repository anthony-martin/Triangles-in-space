using NUnit.Framework;

namespace TrianglesInSpace.Primitives.NUnit
{
    internal class VectorTests : TestSpecification
    {

        [Test]
        public void XSetsWithoutModification()
        {
            double x = 4.0;
            var vector = new Vector(x, 5);

            Assert.AreEqual(x, vector.X);
        }

        [Test]
        public void YSetsWithoutModification()
        {
            double y = 3.4;
            var vector = new Vector(1, y);

            Assert.AreEqual(y, vector.Y);
        }

        [Test]
        public void AddSumsXs()
        {
            const double xOne = 2.1;
            const double xTwo = -3.9;

            var vectorOne = new Vector(xOne, 0);
            var vectorTwo = new Vector(xTwo, 0);

            var result = vectorOne + vectorTwo;

            Assert.AreEqual(xOne + xTwo, result.X);
        }

        [Test]
        public void AddSumsYs()
        {
            const double yOne = -2.1;
            const double yTwo = 3.9;

            var vectorOne = new Vector(0, yOne);
            var vectorTwo = new Vector(0, yTwo);

            var result = vectorOne + vectorTwo;

            Assert.AreEqual(yOne + yTwo, result.Y);
        }

        [Test]
        public void AddIsCommutative()
        {
            var vectorOne = new Vector(2, 1);
            var vectorTwo = new Vector(4, 1);

            var resultOne = vectorOne + vectorTwo;
            var resultTwo = vectorTwo + vectorOne;

            Assert.AreEqual(resultOne, resultTwo);
        }

        [Test]
        public void SubtractSubtractsLeftXFromRight()
        {
            const double xOne = 2.1;
            const double xTwo = 3.1;

            var vectorOne = new Vector(xOne, 0);
            var vectorTwo = new Vector(xTwo, 0);

            var result = vectorOne - vectorTwo;

            Assert.AreEqual(xOne - xTwo, result.X);
        }

        [Test]
        public void SubtractSubtractsLeftYFromRight()
        {
            const double yOne = 2.1;
            const double yTwo = 3.1;

            var vectorOne = new Vector(0, yOne);
            var vectorTwo = new Vector(0, yTwo);

            var result = vectorOne - vectorTwo;

            Assert.AreEqual(yOne - yTwo, result.Y);
        }

        public void VectorZeroHasZeroValues()
        {
            var vector = new Vector(0, 0);

            Assert.AreEqual(vector.X, Vector.Zero.X);
            Assert.AreEqual(vector.Y, Vector.Zero.Y);
        }
    }
}
