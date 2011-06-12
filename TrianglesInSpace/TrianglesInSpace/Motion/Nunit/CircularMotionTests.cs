using NUnit.Framework;

namespace TrianglesInSpace.Motion.Nunit
{
	public class CircularMotionTests: TestSpecification
	{
		private CircularMotion CreateMotionAtZero()
		{
			ulong startTime = 0;
			return new CircularMotion(startTime, 0, 0, 0, 0);
		}
		
		[Test]
		public void Create_CircularMotion()
		{
			CreateMotionAtZero();
		}
	}
}
