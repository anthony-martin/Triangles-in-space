using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;

namespace TrianglesInSpace.Messages
{
    public class AttackTargetMessage : IMessage
    {
        public readonly Vector WorldPosition;
        public readonly ulong Time;

        public AttackTargetMessage(Vector worldPosition, ulong time)
        {
            WorldPosition = worldPosition;
            Time = time;
        }
    }
}
