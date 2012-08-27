namespace TrianglesInSpace.Messaging.Messages
{
	public class InvalidMessage : IMessage
	{
		public readonly string MessageType;
		public readonly string MessageContents;
		public readonly string ErrorDescription;

		public InvalidMessage(string messageType, string messageContents, string errorDescription)
		{
			MessageType = messageType;
			MessageContents = messageContents;
			ErrorDescription = errorDescription;
		}
	}
}
