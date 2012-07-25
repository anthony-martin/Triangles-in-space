using NUnit.Framework;

namespace TrianglesInSpace.Messaging.NUnit
{
    internal class MessageSerialiserTests : TestSpecification
    {
        [Test]
        public void MessageName()
        {
            var serialiser = new MessageSerialiser();

            var messageString = serialiser.Serialise(new TestSerialiserMessage());

            var message = serialiser.Deserialise(messageString);

            Assert.NotNull(message);
        }

        private class TestSerialiserMessage : IMessage
        {
            public readonly string Name;

            public TestSerialiserMessage()
            {
                Name = "Freddy";
            }
        }
    }

    
}
