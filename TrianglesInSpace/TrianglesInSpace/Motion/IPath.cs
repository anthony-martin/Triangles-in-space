using System.Collections.Generic;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Motion
{
    public interface IPath
    {
        void MoveToDestination(Vector destination, ulong currentTime);
        IMotion GetCurrentMotion(ulong currentTime);
        IEnumerable<IMotion> Motion { get; }
    }
}
