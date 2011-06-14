using Mogre;
using NUnit.Framework;
using Math = System.Math;

namespace TrianglesInSpace.Primitives.NUnit
{
	public class AngleTests : TestSpecification
	{
		[Test]
		public static void Reduce_Angle_Test_Greater_Than_Two_Pi()
		{
			double angle = Angle.ReduceAngle(3 * Math.PI);

			Assert.AreEqual(Math.PI, angle,0.00001);
		}

		[Test]
		public static void Reduce_Angle_Test_Less_Than_Two_Pi()
		{
			double angle = Angle.ReduceAngle(-3 * Math.PI);

			Assert.AreEqual(Math.PI, angle, 0.00001);
		}

		[Test]
		public static void Create_Angle_Check_Value_Greater_Than_Two_Pi()
		{
			Angle angle = new Angle(3 * Math.PI);
			
			Assert.AreEqual(Math.PI, angle.Value, 0.00001);
		}

		[Test]
		public static void Create_Angle_Check_Value_Less_Than_Two_Pi()
		{
			Angle angle = new Angle(-3 * Math.PI);

			Assert.AreEqual(Math.PI, angle.Value, 0.00001);
		}

		/// <summary>
		/// Creates a number for the facing of the vector
		/// 261
		/// 7 5
		/// 384
		/// </summary>
		/// <returns></returns>
		

		[Test]
		public static void Test_First_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(1,1);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(1, facing);
		}

		[Test]
		public static void Test_Second_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(-1, 1);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(2, facing);
		}
		[Test]
		public static void Test_Third_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(-1, -1);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(3, facing);
		}
		[Test]
		public static void Test_Fourth_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(1, -1);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(4, facing);
		}
		[Test]
		public static void Test_Fifth_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(1, 0);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(5, facing);
		}
		[Test]
		public static void Test_Sixth_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(0, 1);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(6, facing);
		}
		[Test]
		public static void Test_Seventh_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(-1, 0);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(7, facing);
		}
		[Test]
		public static void Test_Eighth_Quadrant_Facing()
		{
			Vector2 vector2 = new Vector2(0, -1);

			int facing = Angle.FacingNumber(vector2);

			Assert.AreEqual(8, facing);
		}

		[Test]
		public static void Test_First_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(1, 1);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(Math.PI/4, angle);
		}

		[Test]
		public static void Test_Second_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(-1, 1);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(3 * Math.PI / 4, angle);
		}
		[Test]
		public static void Test_Third_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(-1, -1);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(-3 * Math.PI / 4, angle);
		}
		[Test]
		public static void Test_Fourth_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(1, -1);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(-Math.PI / 4, angle);
		}
		[Test]
		public static void Test_Fifth_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(1, 0);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(0.0, angle);
		}
		[Test]
		public static void Test_Sixth_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(0, 1);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(Math.PI/2, angle);
		}
		[Test]
		public static void Test_Seventh_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(-1, 0);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(Math.PI, angle);
		}
		[Test]
		public static void Test_Eighth_Quadrant_TanAngle()
		{
			Vector2 vector2 = new Vector2(0, -1);

			double angle = Angle.TanAngle(vector2);

			Assert.AreEqual(-Math.PI/2, angle);
		}
	}
}