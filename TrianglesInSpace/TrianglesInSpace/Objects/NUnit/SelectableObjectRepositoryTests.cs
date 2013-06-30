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
        private SelectableObject m_SelectableObject;
        private Vector m_Position;
        private ulong m_TestTime;

        [SetUp]
        public void SetUp()
        {
            m_Bus = Substitute.For<IBus>();
            m_Repository = new SelectableObjectRepository(m_Bus);

            m_Position = new Vector(5, 7);
            m_TestTime = 500;

            m_SelectableObject = CreateSelectableObjectAt(m_Position);

            m_Repository.AddObject(m_SelectableObject);
        }

        [Test]
        public void SubscribesToRequestPathMessage()
        {
            m_Bus.Received().Subscribe(Arg.Any<Action<RequestPathMessage>>());
        }

        [Test]
        public void SubscribesToSetPathToTargetMessage()
        {
            m_Bus.Received().Subscribe(Arg.Any<Action<SetPathToTargetMessage>>());
        }
        [Test]
        public void SubscribesToSelectObjectAtMessage()
        {
            m_Bus.Received().Subscribe(Arg.Any<Action<SelectObjectAtMessage>>());
        }

        [Test]
        public void SendsSelectedObjectMessageIfOneObjectIntersects()
        {
            var message = new SelectObjectAtMessage(m_Position, m_TestTime);

            m_Repository.OnSelectObject(message);

            m_Bus.Received().Send(Arg.Any<SelectedObjectMessage>());
        }

        [Test]
        public void PathRequestSendsPathForNamedObject()
        {
            m_Repository.OnPathRequest(new RequestPathMessage(m_SelectableObject.Name));

            m_Bus.Received().Send(Arg.Any<PathMessage>());
        }

        [Test]
        public void PathRequestDoesNotSendForOtherObjects()
        {
            m_Repository.OnPathRequest(new RequestPathMessage("Completely random string"));

            m_Bus.DidNotReceive().Send(Arg.Any<PathMessage>());
        }

        public void SetPathToTargetOnlyWorksForSelectedObjects()
        {
            m_Repository.OnSetPath(new SetPathToTargetMessage(m_Position, m_TestTime));

            m_Bus.DidNotReceive().Send(Arg.Any<PathMessage>());
        }


        public void SetPathSendsForSelectedObjects()
        {
            var message = new SelectObjectAtMessage(m_Position, m_TestTime);

            m_Repository.OnSelectObject(message);

            m_Repository.OnSetPath(new SetPathToTargetMessage(m_Position, m_TestTime));

            m_Bus.Received().Send(Arg.Any<PathMessage>());
        }


        [Test]
        public void ClearsOldSelectedObject()
        {
            var message = new SelectObjectAtMessage(m_Position, m_TestTime);

            var newPosition = new Vector(50, 5);

            var secondObject = CreateSelectableObjectAt(newPosition);
            m_Repository.AddObject(secondObject);
            m_Repository.OnSelectObject(message);

            m_Bus.DidNotReceive().Send(Arg.Any<DeselectedObjectMessage>());

            m_Repository.OnSelectObject(new SelectObjectAtMessage(newPosition, m_TestTime));

            m_Bus.Received().Send(Arg.Any<DeselectedObjectMessage>());
        }

        
        private SelectableObject CreateSelectableObjectAt(Vector position)
        {
            var path = Get<IPath>();
            var motion = Get<IMotion>();

            motion.GetCurrentPosition(m_TestTime).Returns(position);
            path.GetCurrentMotion(m_TestTime).Returns(motion);
            return new SelectableObject("fred", path);
        }
    }
}
