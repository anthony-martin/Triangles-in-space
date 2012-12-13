using System;
using System.Collections.Generic;
using NUnit.Framework;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages.NUnit
{
    internal class RequestPathMessageTests : TestSpecification
    {
        [Test]
        public void MessageRoundTrip()
        {

            var original = new RequestPathMessage();

            var serialiser = new MessageSerialiser();
            serialiser.Register(typeof(RequestPathMessage));

            var text = serialiser.Serialise(original);

            var deserialised = (RequestPathMessage)serialiser.Deserialise(text);

            Assert.NotNull(deserialised);
        }
    }
}
