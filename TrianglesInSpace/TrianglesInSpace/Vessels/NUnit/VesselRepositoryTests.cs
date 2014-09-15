using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace TrianglesInSpace.Vessels.NUnit
{
    [TestFixture]
    internal class VesselRepositoryTests
    {
        private VesselRepository m_VesselRepository;
        [SetUp]
        public void SetUp()
        {
            m_VesselRepository = new VesselRepository();
        }

        [Test]
        public void GetVesselReturnsNullIfNoMatch()
        {
            Assert.Null(m_VesselRepository.GetByName("does not exist"));    
        }
    }
}
