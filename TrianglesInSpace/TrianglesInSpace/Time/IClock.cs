namespace TrianglesInSpace.Time
{
    public interface IClock
    {
        ulong Time { get; }
        void UpdateTime(double timeSinceLastUpdate);
    }
}
