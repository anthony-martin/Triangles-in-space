using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using NUnit.Framework;

namespace TrianglesInSpace.Motion.Nunit
{
	public class PathTests : TestSpecification
	{
		[Test]
		public void CreateTurningCircles()
		{
			var path = new Path();
			Vector2 initialVelocity = new Vector2(0,1);
			double acceleration = 1;
			Vector2 circleOne;
			Vector2 circleTwo;
			path.DetermineTurningCircles( initialVelocity, acceleration, out circleOne, out circleTwo);

			var circleOneResultShouldBe = new Vector2(-1, 0);
			var circleTwoResultShouldBe = new Vector2(1, 0);


			Assert.AreEqual(circleOneResultShouldBe.x, circleOne.x, 0.000000001);
			Assert.AreEqual(circleOneResultShouldBe.y, circleOne.y, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.x, circleTwo.x, 0.000000001);
			Assert.AreEqual(circleTwoResultShouldBe.y, circleTwo.y, 0.000000001);
		} 
	}
}
