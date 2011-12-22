using Mogre;
using NUnit.Framework;

namespace TrianglesInSpace.Motion.Nunit
{
	class LinearMotionTests : TestSpecification
	{

		private LinearMotion CreateMotionAtZero()
		{
			Vector2 vector = new Vector2(1, 1);
			return new LinearMotion(0, vector, Vector2.ZERO);
		}

		[Test]
		public void Create_LinearMotion()
		{
			CreateMotionAtZero();
		}

		[Test]
		public void Test_Get_Velocity()
		{
			Vector2 vector = new Vector2(1, 1);
			ulong startTime = 0;
			LinearMotion motion = new LinearMotion(startTime, vector, Vector2.ZERO);

			Assert.AreEqual(vector , motion.GetVelocity(0));
		}

		[Test]
		public void Test_Get_StartTime()
		{
			Vector2 vector = new Vector2(1, 1);
			ulong startTime = 0;
			LinearMotion motion = new LinearMotion(startTime, vector, Vector2.ZERO);

			Assert.AreEqual(startTime, motion.StartTime);
		}

		[Test]
		public void Test_Get_Movement_Time_Zero()
		{
			LinearMotion motion = CreateMotionAtZero();

			Vector2 distanceMoved = motion.GetMotion(1000);

			Assert.AreEqual(new Vector2(1, 1), distanceMoved);
		}

		[Test]
		public void Test_Get_Movement_Time_Ten()
		{
			LinearMotion motion = CreateMotionAtZero();

			Vector2 distanceMoved = motion.GetMotion(10000);

			Assert.AreEqual(new Vector2(10, 10), distanceMoved);
		}
	}
}
