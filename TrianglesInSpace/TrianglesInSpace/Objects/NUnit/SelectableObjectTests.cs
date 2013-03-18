using NUnit.Framework;
using TrianglesInSpace.Motion;
using NSubstitute;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Objects.NUnit
{
    internal class SelectableObjectTests : TestSpecification
    {
        private IPath m_Path;
        private readonly string m_TestObjectName = "Sofia";
        private SelectableObject m_SelectableObject;
        private IMotion m_Motion;

        [SetUp]
        public void BecauseOf()
        {
            m_Path = Get<IPath>();
            m_Motion = Get<IMotion>();

            m_Path.GetCurrentMotion(Arg.Any<ulong>()).Returns(m_Motion);

            m_SelectableObject = new SelectableObject(m_TestObjectName, m_Path);
        }
        [Test]
        public void Name()
        {
            Assert.AreEqual(m_TestObjectName, m_SelectableObject.Name);
        }

        [Test]
        public void IntersectsPointLooksUpCurrentMotion()
        {
            const ulong time = 1500;

            m_SelectableObject.IntersectsPoint(Vector.Zero, time);

            m_Path.Received().GetCurrentMotion(time);
        }

        [Test]
        public void IntersectsPointLooksUpCurrentPosition()
        {
            const ulong time = 1500;
            m_SelectableObject.IntersectsPoint(Vector.Zero, time);

            m_Motion.Received().GetCurrentPosition(time);
        }

        [Test]
        public void IntersectsPointFalseIfMoreThan10UnitsFromPosition()
        {
            const ulong time = 1500;
            m_Motion.GetCurrentPosition(time).Returns(new Vector(5,-7));
            
            var intersects = m_SelectableObject.IntersectsPoint(new Vector(25, -7), time);

            Assert.False(intersects);
        }

        [Test]
        public void IntersectPointTrueIfWithin10Units()
        {
            const ulong time = 1500;
            m_Motion.GetCurrentPosition(time).Returns(new Vector(5, -7));

            var intersects = m_SelectableObject.IntersectsPoint(new Vector(-3, 1), time);

            Assert.True(intersects);
        }

        [Test]
        public void PathReturnsSameInstanceObjectWasCreatedWith()
        {
            IPath path = Get<IPath>();
            var selectableObject = new SelectableObject("wizard", path);

            Assert.AreEqual(path, selectableObject.Path);
        }
    }
}
