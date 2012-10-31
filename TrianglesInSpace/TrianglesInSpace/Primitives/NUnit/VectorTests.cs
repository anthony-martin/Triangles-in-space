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

        [Test]
        public void VectorZeroHasZeroValues()
        {
            var vector = new Vector(0, 0);

            Assert.AreEqual(vector.X, Vector.Zero.X);
            Assert.AreEqual(vector.Y, Vector.Zero.Y);
        }

        [Test]
        public void MultiplyByDoubleAppliesToX()
        {
            const double x = 2.0;
            const double multiple = 4;

            var vector = new Vector(x, 0) * multiple;

            Assert.AreEqual(8, vector.X);
        }

        [Test]
        public void MultiplyByDoubleAppliesToY()
        {
            const double y = 3.0;
            const double multiple = -3;

            var vector = new Vector(0, y) * multiple;

            Assert.AreEqual(-9, vector.Y);
        }

        [Test]
        public void MultiplyIsCommutative()
        {
            const double x = 2.0;
            const double y = 3.0;
            const double multiple = 4;

            var resultOne  = new Vector(x, y) * multiple;
            var resultwo = multiple * new Vector(x, y);

            Assert.AreEqual(resultOne.X, resultwo.X);
            Assert.AreEqual(resultOne.Y, resultwo.Y);
        }

        [Test]
        public void OperatorEqualIfXsAreEqual()
        {
            var vectorOne = new Vector(2.3, 0);
            var vectorTwo = new Vector(2.3, 0);

            Assert.True(vectorOne.X.Equals( vectorTwo.X));
            Assert.True(vectorOne == vectorTwo);
        }

        [Test]
        public void OperatorEqualIfYsAreEqual()
        {
            var vectorOne = new Vector(0, 3.33);
            var vectorTwo = new Vector(0, 3.33);

            Assert.True(vectorOne.Y.Equals(vectorTwo.Y));
            Assert.True(vectorOne == vectorTwo);
        }

        [Test]
        public void OperatorNotEqualIfXsAreNotEqual()
        {
            var vectorOne = new Vector(2.3, 0);
            var vectorTwo = new Vector(32, 0);

            Assert.False(vectorOne.X.Equals(vectorTwo.X));
            Assert.True(vectorOne.Y.Equals(vectorTwo.Y));
            Assert.False(vectorOne == vectorTwo);
        }

        [Test]
        public void OperatorNotEqualIfYsAreNotEqual()
        {
            var vectorOne = new Vector(0, 3.33);
            var vectorTwo = new Vector(0, 1);

            Assert.True(vectorOne.X.Equals(vectorTwo.X));
            Assert.False(vectorOne.Y.Equals(vectorTwo.Y));
            Assert.False(vectorOne == vectorTwo);
        }

        [Test]
        public void EqualIfXsAreEqual()
        {
            var vectorOne = new Vector(2.3, 0);
            var vectorTwo = new Vector(2.3, 0);

            Assert.True(vectorOne.X.Equals(vectorTwo.X));
            Assert.True(vectorOne.Equals( vectorTwo));
        }

        [Test]
        public void EqualIfYsAreEqual()
        {
            var vectorOne = new Vector(0, 3.33);
            var vectorTwo = new Vector(0, 3.33);

            Assert.True(vectorOne.Y.Equals(vectorTwo.Y));
            Assert.True(vectorOne.Equals(vectorTwo));
        }

        [Test]
        public void NotEqualIfXsAreNotEqual()
        {
            var vectorOne = new Vector(2.3, 0);
            var vectorTwo = new Vector(32, 0);

            Assert.False(vectorOne.X.Equals(vectorTwo.X));
            Assert.True(vectorOne.Y.Equals(vectorTwo.Y));
            Assert.False(vectorOne.Equals(vectorTwo));
        }

        [Test]
        public void NotEqualIfYsAreNotEqual()
        {
            var vectorOne = new Vector(0, 3.33);
            var vectorTwo = new Vector(0, 1);

            Assert.True(vectorOne.X.Equals(vectorTwo.X));
            Assert.False(vectorOne.Y.Equals(vectorTwo.Y));
            Assert.False(vectorOne.Equals(vectorTwo));
        }

        [Test]
        public void DivideByDoubleDividesX()
        {
            const int divisor = 3;
            var vector = new Vector(3, 0)/divisor;

            Assert.AreEqual(1, vector.X);
        }

        [Test]
        public void DivideByDoubleDividesY()
        {
            const int divisor = 5;
            var vector = new Vector(0, 10) / divisor;

            Assert.AreEqual(2, vector.Y);
        }
    }
}
