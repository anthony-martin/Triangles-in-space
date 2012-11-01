using NUnit.Framework;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Motion.Nunit
{
	class LinearMotionTests : TestSpecification
	{

		private LinearMotion CreateMotionAtZero()
		{
			Vector vector = new Vector(1, 1);
			return new LinearMotion(0, vector, Vector.Zero);
		}

		[Test]
		public void Create_LinearMotion()
		{
			CreateMotionAtZero();
		}

		[Test]
		public void Test_Get_Velocity()
		{
			Vector vector = new Vector(1, 1);
			ulong startTime = 0;
			LinearMotion motion = new LinearMotion(startTime, vector, Vector.Zero);

			Assert.AreEqual(vector , motion.GetVelocity(0));
		}

		[Test]
		public void Test_Get_StartTime()
		{
			Vector vector = new Vector(1, 1);
			ulong startTime = 0;
			LinearMotion motion = new LinearMotion(startTime, vector, Vector.Zero);

			Assert.AreEqual(startTime, motion.StartTime);
		}

		[Test]
		public void Test_Get_Movement_Time_Zero()
		{
			LinearMotion motion = CreateMotionAtZero();

			Vector distanceMoved = motion.GetMotion(1000);

			Assert.AreEqual(new Vector(1, 1), distanceMoved);
		}

		[Test]
		public void Test_Get_Movement_Time_Ten()
		{
			LinearMotion motion = CreateMotionAtZero();

			Vector distanceMoved = motion.GetMotion(10000);

			Assert.AreEqual(new Vector(10, 10), distanceMoved);
		}
	}
}
