using NUnit.Framework;

namespace TrianglesInSpace.Disposers.NUnit
{
    class DisposerTests : TestSpecification
    {
        [Test]
        public void Dispose_Calls_The_Action_Added()
        {
            var disposer = new Disposer();
            bool disposed = false;

            disposer.Add(() => { disposed = true; });

            disposer.Dispose();

            Assert.True(disposed);
        }
    }
}
