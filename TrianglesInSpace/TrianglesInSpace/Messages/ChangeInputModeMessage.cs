using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Primitives;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class ChangeInputModeMessage : IMessage
    {
        public readonly InputMode Mode;

        public ChangeInputModeMessage(InputMode mode)
        {
            Mode = mode;
        }
    }
}
