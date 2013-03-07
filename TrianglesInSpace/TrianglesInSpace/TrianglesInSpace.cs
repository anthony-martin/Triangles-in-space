using System;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Objects;
using TrianglesInSpace.Primitives;
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

			    var selectableObjectRepository = new SelectableObjectRepository(bus);

                var path = new Path(4, new CircularMotion(0, 50, new Primitives.Angle(0), new Primitives.Angle(Math.PI / 10), 20, Vector.Zero), bus);
                var selectableObject = new SelectableObject("triangle", path);
                selectableObjectRepository.AddObject(selectableObject);

                var renderer = new Renderer(bus);
                renderer.StartRendering();
			}
			catch (OperationCanceledException) { }
		}
	}
}
