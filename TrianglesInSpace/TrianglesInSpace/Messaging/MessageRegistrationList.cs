using System;
using System.Collections.Generic;
using TrianglesInSpace.Messages;

namespace TrianglesInSpace.Messaging
{
    public class MessageRegistrationList : IMessageRegistrationList
    {
        public List<Type> Messages
        {
            get
            {
                return new List<Type>
                {
                    typeof(SelectObjectAtMessage),
                    typeof(SelectedObjectMessage),
                    typeof(SetPathToTargetMessage),
                    typeof(PathMessage),
                    typeof(RequestPathMessage),
                    typeof(TimeUpdateMessage),
                };
            }
        }
    }

}
