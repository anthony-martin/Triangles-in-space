using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageSerialiser
    {
        
    }
    public class MessageSerialiser : IMessageSerialiser
    {
        private readonly Dictionary<string, Type> m_Types;
 
        public MessageSerialiser()
        {
            m_Types = new Dictionary<string, Type>();
        }

        public void Register(Type type)
        {
            m_Types.Add(type.Name, type);
        }

        public string Serialise(IMessage message)
        {
            var name = message.GetType().Name;
            return name + JsonConvert.SerializeObject(message);
        }

        public IMessage Deserialise(string message)
        {
            int lenthOfMessageName = message.IndexOf('{');
            string className = message.Substring(0, lenthOfMessageName);
            string messageContent = message.Substring(lenthOfMessageName);

            Type messageType;
            m_Types.TryGetValue(className, out messageType);

            return JsonConvert.DeserializeObject(messageContent, messageType) as IMessage;

        }
    }
}
