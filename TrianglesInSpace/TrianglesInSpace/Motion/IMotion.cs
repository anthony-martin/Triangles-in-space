using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Motion
{
	public interface IMotion
	{
		ulong StartTime
		{
			get;
		}

		Vector GetVelocity(ulong currentTime);
		Vector GetMotion(ulong currentTime);
		Vector GetCurrentPosition(ulong currentTime);
	}
}
