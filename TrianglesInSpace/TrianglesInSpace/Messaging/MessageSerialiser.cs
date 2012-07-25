using System;
using System.Reflection;
using Newtonsoft.Json;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageSerialiser
    {
        
    }
    public class MessageSerialiser : IMessageSerialiser
    {
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

            //note the typename needs to be the full type name here which I am no sending at the moment
            var messageType = Type.GetType(className);

            return JsonConvert.DeserializeObject(messageContent, messageType) as IMessage;

        }
    }
}
