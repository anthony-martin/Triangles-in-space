using System;
using NSubstitute;
using NUnit.Framework;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Objects.NUnit
{
    internal class SelectableObjectRepositoryTests : TestSpecification
    {
        private IBus m_Bus;
        private SelectableObjectRepository m_Repository;

        [SetUp]
        public void SetUp()
        {
            m_Bus = Substitute.For<IBus>();
            m_Repository = new SelectableObjectRepository(m_Bus);
        }

        [Test]
        public void SubscribesToSelectObjectAtMessage()
        {
            m_Bus.Received().Subscribe(Arg.Any<Action<SelectObjectAtMessage>>());
        }

        [Test]
        public void SendsSelectedObjectMessageIfOneObjectIntersects()
        {
            var position = new Vector(5,7);
            var path = Get<IPath>();
            var motion = Get<IMotion>();
            const ulong currentTime = 500;
            motion.GetCurrentPosition(currentTime).Returns(position);
            path.GetCurrentMotion(currentTime).Returns(motion);
            var selectableObject = new SelectableObject("fred", path);

            var message = new SelectObjectAtMessage(position, currentTime);

            m_Repository.AddObject(selectableObject);

            m_Repository.OnSelectObject(message);

            m_Bus.Received().Send(Arg.Any<SelectedObjectMessage>());
        }
    }
}
