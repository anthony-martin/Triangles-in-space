using System;
using NSubstitute;
using NUnit.Framework;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;

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
            m_Bus.Received().Subscribe<SelectObjectAtMessage>(Arg.Any<Action<SelectObjectAtMessage>>());
        }
    }
}
