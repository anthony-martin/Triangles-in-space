using Mogre;

namespace TrianglesInSpace.Motion
{
	public interface IMotion
	{
		ulong StartTime
		{
			get;
		}

		Vector2 GetVelocity(ulong currentTime);
		Vector2 GetMotion(ulong currentTime);
		Vector2 GetCurrentPosition(ulong currentTime);
	}
}
