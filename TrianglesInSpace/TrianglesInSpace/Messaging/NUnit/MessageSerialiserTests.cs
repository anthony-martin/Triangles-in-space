using NUnit.Framework;
using TrianglesInSpace.Messaging.Messages;

namespace TrianglesInSpace.Messaging.NUnit
{
    internal class MessageSerialiserTests : TestSpecification
    {
        [Test]
        public void MessageName()
        {
            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(TestSerialiserMessage));
            var messageString = serialiser.Serialise(new TestSerialiserMessage("bob"));

			var message = (TestSerialiserMessage)serialiser.Deserialise(messageString);

            Assert.AreEqual("bob", message.Name);
        }


		[Test]
		public void ReturnsNullIfTypeNotRegistered()
		{
			var serialiser = new MessageSerialiser();

			var messageString = serialiser.Serialise(new TestSerialiserMessage("bob"));

			InvalidMessage message = serialiser.Deserialise(messageString) as InvalidMessage;

			Assert.NotNull( message);
			Assert.AreEqual(MessageSerialiser.InvalidTypeMessage, message.ErrorDescription);
		}

		[Test]
		public void ReturnsNullIfBadMessage()
		{
			var serialiser = new MessageSerialiser();
			serialiser.Register(typeof(TestSerialiserMessage));
			var messageString = serialiser.Serialise(new TestSerialiserMessage("bob"));

			InvalidMessage message = serialiser.Deserialise(messageString.Substring(20)) as InvalidMessage;

			Assert.NotNull(message);
			Assert.AreEqual(MessageSerialiser.InvalidTypeMessage, message.ErrorDescription);
		}

        private class TestSerialiserMessage : IMessage
        {
			public readonly string Name = "Freddy";

            public TestSerialiserMessage(string name)
            {
				Name = name;
            }
        }
    }

    
}
