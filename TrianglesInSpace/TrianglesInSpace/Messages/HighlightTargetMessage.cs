using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Primitives;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    class HighlightTargetMessage : IMessage
    {
        public readonly Vector WorldPosition;
        public readonly ulong Time;

        public HighlightTargetMessage(Vector worldPosition, ulong time)
        {
            WorldPosition = worldPosition;
            Time = time;
        }
    }
}
