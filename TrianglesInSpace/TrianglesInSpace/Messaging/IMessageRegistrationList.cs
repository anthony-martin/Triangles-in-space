using System;
using System.Collections.Generic;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageRegistrationList
    {
        List<Type> Messages { get; }
    }
}
