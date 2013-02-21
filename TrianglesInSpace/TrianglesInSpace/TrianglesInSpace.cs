using System;
using TrianglesInSpace.Messaging;
using ZeroMQ;
using TrianglesInSpace.Rendering;

namespace TrianglesInSpace
{
	public class TrianglesInSpace
	{
		/// <summary>
		/// Non unit tested graphics engine setup stuff
		/// May need to migrate into a class later to encapsulate the setup
		/// </summary>

		[STAThread]
		public static void Main()
		{
			try
			{
                var context = ZmqContext.Create();
		        var receiver = new MessageReceiver(context);
                receiver.Listen();
                var bus = new MessageBus(new MessageSender(context), receiver, new MessageRegistrationList());

                var renderer = new Renderer(bus);
                renderer.StartRendering();
			}
			catch (OperationCanceledException) { }
		}
	}
}
