using System;
using Mogre;
using MOIS;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Motion;
using TrianglesInSpace.Primitives;
using ZeroMQ;
using Angle = TrianglesInSpace.Primitives.Angle;
using Math = System.Math;
using Vector3 = Mogre.Vector3;
using TrianglesInSpace.Rendering;

namespace TrianglesInSpace
{
	public class TrianglesInSpace
	{
		/// <summary>
		/// Non unit tested graphics engine setup stuff
		/// May need to migrate into a class later to encapsulate the setup
		/// </summary>


	    private static MessageBus m_Bus;

		[STAThread]
		public static void Main()
		{
			try
			{

                var context = ZmqContext.Create();
		        var receiver = new MessageReceiver(context);
                receiver.Listen();
                m_Bus = new MessageBus(new MessageSender(context), receiver);

			    var renderer = new Renderer(m_Bus);
                renderer.StartRendering();
			}
			catch (OperationCanceledException) { }
		}
	}
}
