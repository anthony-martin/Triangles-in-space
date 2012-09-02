using NUnit.Framework;

namespace TrianglesInSpace.Messaging.NUnit
{
    internal class MessageTransmissionTests : TestSpecification
    {
        [Test]
        public void MakeOne()
        {
            var trasmitter = new MessageTransmission();
            trasmitter.Dispose();

        }
    }
}
