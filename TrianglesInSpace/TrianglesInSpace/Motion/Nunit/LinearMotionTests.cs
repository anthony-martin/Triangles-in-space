using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using NUnit.Framework;

namespace TrianglesInSpace.Motion.Nunit
{
	class LinearMotionTests : TestSpecification
	{
		
		[Test]
		public void Create_LinearMotion()
		{
			var vector = new Vector2(1,1);
			var motion = new LinearMotion(vector);
		}

		[Test]
		public void Test_Get_Velocity()
		{
			var vector = new Vector2(1, 1);
			var motion = new LinearMotion(vector);

			Assert.AreEqual(vector , motion.GetVelocity());
		}
	}
}
