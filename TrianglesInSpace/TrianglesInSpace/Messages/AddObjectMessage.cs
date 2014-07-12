using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrianglesInSpace.Messaging;

namespace TrianglesInSpace.Messages
{
    public class AddObjectMessage : IMessage
    {
        public readonly string Name;
        public readonly string Shape = "triangle";

        public AddObjectMessage(string name)
        {
            Name = name;
        }
    }
}
