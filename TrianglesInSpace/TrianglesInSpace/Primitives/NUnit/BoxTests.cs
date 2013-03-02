using NUnit.Framework;

namespace TrianglesInSpace.Primitives.NUnit
{
    internal class BoxTests : TestSpecification
    {
        [Test]
        public void TopLeftCorner()
        {
            var vector = new Vector(-1, 2.75);

            var box = new Box(2, 5.5);

            Assert.AreEqual(vector, box.TopLeft);
        }

        [Test]
        public void BottomLeftCorner()
        {
            var vector = new Vector(2, -4);

            var box = new Box(4, 8);

            Assert.AreEqual(vector, box.BottomRight);
        }

        [Test]
        public void DoesNotContainIfTooFarLeft()
        {
            var posOne = new Vector(-5, 0);
            var posTwo = new Vector(5, 0);

            var box = new Box(5, 5);

            Assert.False(box.Contains(posOne, posTwo));
        }

        [Test]
        public void DoesNotContainIfTooFarRight()
        {
            var posOne = new Vector(15, 0);
            var posTwo = new Vector(5, 0);

            var box = new Box(5, 5);

            Assert.False(box.Contains(posOne, posTwo));
        }

        [Test]
        public void ContainsIfWithinXDimension()
        {
            var posOne = new Vector(2.5, 0);
            var posTwo = new Vector(5, 0);

            var box = new Box(5, 5);

            Assert.True(box.Contains(posOne, posTwo));
        }

        [Test]
        public void DoesNotContainIfTooLow()
        {
            var posOne = new Vector(0, -7);
            var posTwo = new Vector(0, 3);

            var box = new Box(5, 5);

            Assert.False(box.Contains(posOne, posTwo));
        }

        [Test]
        public void DoesNotContainIfTooHigh()
        {
            var posOne = new Vector(0, 112);
            var posTwo = new Vector(0, 3);

            var box = new Box(5, 5);

            Assert.False(box.Contains(posOne, posTwo));
        }

        [Test]
        public void ContainsIfWithinVertial()
        {
            var posOne = new Vector(0, 5.5);
            var posTwo = new Vector(0, 3);

            var box = new Box(5, 5);

            Assert.True(box.Contains(posOne, posTwo));
        }
    }
}
