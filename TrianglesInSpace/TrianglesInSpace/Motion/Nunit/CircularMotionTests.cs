using System.Runtime.Serialization.Formatters;
using Mogre;
using NUnit.Framework;
using Newtonsoft.Json;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;
using Angle = TrianglesInSpace.Primitives.Angle;

namespace TrianglesInSpace.Motion.Nunit
{
	public class CircularMotionTests: TestSpecification
	{
		
		[Test]
		public void Create_CircularMotion()
		{
			new CircularMotion(0, 0, new Angle(0), new Angle(0), 1, Vector.Zero);
		}

		[Test]
		public void Test_Get_Velocity_Initial_North()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(1), 1, Vector.Zero);
			Vector velocity = new Vector(0, 1);
			Vector motionVelocity = motion.GetVelocity(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Velocity_Initial_South()
		{
			var motion = new CircularMotion(0, 0, new Angle(Math.PI), new Angle(1), 1, Vector.Zero);
			Vector velocity = new Vector(0, -1);
			Vector motionVelocity = motion.GetVelocity(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Velocity_Initial_East()
		{
			var motion = new CircularMotion(0, 0, new Angle(-Math.PI / 2), new Angle(1), 1, Vector.Zero);
			Vector velocity = new Vector(1, 0);
			Vector motionVelocity = motion.GetVelocity(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Velocity_Initial_West()
		{
			var motion = new CircularMotion(0, 0, new Angle(Math.PI / 2), new Angle(1), 1, Vector.Zero);
			Vector velocity = new Vector(-1, 0);
			Vector motionVelocity = motion.GetVelocity(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Velocity_Initial_North_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI / 2), 1, Vector.Zero);
			Vector velocity = new Vector(0, 1);
			Vector motionVelocity = motion.GetVelocity(2000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Velocity_Initial_South_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI), 1, Vector.Zero);
			Vector velocity = new Vector(0, -1);
			Vector motionVelocity = motion.GetVelocity(2000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Velocity_Initial_East_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI / 2), 1, Vector.Zero);
			Vector velocity = new Vector(1, 0);
			Vector motionVelocity = motion.GetVelocity(3000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Velocity_Initial_West_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI / 2), 1, Vector.Zero);
			Vector velocity = new Vector(-1, 0);
			Vector motionVelocity = motion.GetVelocity(1000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_North()
		{
			var motion = new CircularMotion(0, 1, new Angle(0), new Angle(0), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_South()
		{
			var motion = new CircularMotion(0, 0, new Angle(Math.PI), new Angle(0), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_East()
		{
			var motion = new CircularMotion(0, 0, new Angle(-Math.PI / 2), new Angle(0), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_West()
		{
			var motion = new CircularMotion(0, 0, new Angle(Math.PI / 2), new Angle(0), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(0);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_North_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI / 2), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(2000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_South_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(2000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_East_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI / 2), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(3000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}

		[Test]
		public void Test_Get_Motion_Initial_West_Clockwise()
		{
			var motion = new CircularMotion(0, 0, new Angle(0), new Angle(-Math.PI / 2), 1, Vector.Zero);
			Vector velocity = new Vector(0, 0);
			Vector motionVelocity = motion.GetMotion(1000);

			Assert.AreEqual(velocity.X, motionVelocity.X, 0.000000000001);
			Assert.AreEqual(velocity.Y, motionVelocity.Y, 0.000000000001);
		}
	}
}
