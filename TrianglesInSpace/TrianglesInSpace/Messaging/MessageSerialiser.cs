using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using TrianglesInSpace.Messaging.Messages;

namespace TrianglesInSpace.Messaging
{
    public interface IMessageSerialiser
    {
    	void Register(Type type);
    	string Serialise(IMessage message);
    	IMessage Deserialise(string messageString);
    }

    public class MessageSerialiser : IMessageSerialiser
    {
    	internal const string InvalidTypeMessage = "Unregistered or invalid message type";
		internal const string InvalidContents = "Message contents could not be deserialised";
        private readonly Dictionary<string, Type> m_Types;
        private readonly JsonSerializerSettings m_Settings;

        public MessageSerialiser()
        {
            m_Types = new Dictionary<string, Type>();
            m_Settings = new JsonSerializerSettings
            {
                ContractResolver = new FieldContractResolver(),
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,

            };
        }

        public void Register(Type type)
        {
            m_Types.Add(type.Name, type);
        }

        public string Serialise(IMessage message)
        {
            var name = message.GetType().Name;
            return name + JsonConvert.SerializeObject(message, m_Settings);
        }

        public IMessage Deserialise(string messageString)
        {
            int lenthOfMessageName = messageString.IndexOf('{');
            string className = messageString.Substring(0, lenthOfMessageName);
            string messageContent = messageString.Substring(lenthOfMessageName);

            Type messageType;
            m_Types.TryGetValue(className, out messageType);

        	IMessage message = null;
			if (messageType != null)
			{
                message = JsonConvert.DeserializeObject(messageContent, messageType, m_Settings) as IMessage;
			}

        	if (message == null)
        	{
				string error = messageType == null ? InvalidTypeMessage : InvalidContents;

				message = new InvalidMessage(className, messageContent, error);
			}

        	return message;
        }
    }
}
