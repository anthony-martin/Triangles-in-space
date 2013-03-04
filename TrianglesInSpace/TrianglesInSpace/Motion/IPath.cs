namespace TrianglesInSpace.Motion
{
    public interface IPath
    {
        IMotion GetCurrentMotion(ulong currentTime);
    }
}
