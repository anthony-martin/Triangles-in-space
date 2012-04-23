using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mogre;
using MOIS;
using TrianglesInSpace.Motion;
using Angle = TrianglesInSpace.Primitives.Angle;
using Math = System.Math;
using Vector3 = Mogre.Vector3;

namespace TrianglesInSpace
{
	public class TrianglesInSpace
	{
		/// <summary>
		/// Non unit tested graphics engine setup stuff
		/// May need to migrate into a class later to encapsulate the setup
		/// </summary>


		private static Camera m_Camera;
		private static SceneManager m_SceneManager;

		protected static InputManager mInputMgr;
		protected static Keyboard mNinjaKeyboard;
		protected static Mouse mNinjaMouse;

		protected static Entity mNinjaEntity;
		protected static SceneNode mNinjaNode;
		protected static Light mLight;
		protected static double mLightToggleTimeout = 0;

		protected static Root mRoot;
		protected static RenderWindow mRenderWindow;

		//private static GameObject m_Object;
        private static Path m_Path;
		private static CircularMotion m_Circle;
		private static LinearMotion m_Linear;

		private static ulong m_time;

		[STAThread]
		public static void Main()
		{
			try
			{
				CreateRoot();

				DefineResources();

				CreateRenderSystem();

				CreateRenderWindow();

				InitializeResources();

				CreateScene();

				InitializeInput();

				CreateFrameListeners();

				EnterRenderLoop();


			}
			catch (OperationCanceledException) { }
		}


		protected static void EnterRenderLoop()
		{
			mRoot.StartRendering();
		}

		protected static void InitializeResources()
		{
			TextureManager.Singleton.DefaultNumMipmaps = 5;
			ResourceGroupManager.Singleton.InitialiseAllResourceGroups();

		}

		protected static void DefineResources()
		{
			ConfigFile cf = new ConfigFile();
			cf.Load("resources.cfg", "\t:=", true);

			var section = cf.GetSectionIterator();
			while (section.MoveNext())
			{
				foreach (var line in section.Current)
				{
					ResourceGroupManager.Singleton.AddResourceLocation(
						line.Value, line.Key, section.CurrentKey);
				}
			}
		}

		protected static void CreateRenderSystem()
		{
			RenderSystem renderSystem = mRoot.GetRenderSystemByName("Direct3D9 Rendering Subsystem");
			renderSystem.SetConfigOption("Full Screen", "No");
			renderSystem.SetConfigOption("Video Mode", "800 x 600 @ 32-bit colour");
			mRoot.RenderSystem = renderSystem;
		}

		protected static void CreateRenderWindow()
		{
			mRenderWindow = mRoot.Initialise(true, "Main Ogre Window");
		}

		protected static void CreateScene()
		{

			m_SceneManager = mRoot.CreateSceneManager(SceneType.ST_GENERIC);

			m_Camera = m_SceneManager.CreateCamera("myCamera1");
            m_Camera.ProjectionType = ProjectionType.PT_ORTHOGRAPHIC;
			m_Camera.SetPosition(-1, 2000, 0);
			m_Camera.NearClipDistance = 5;
			m_Camera.FarClipDistance = 2501;
			m_Camera.LookAt(Vector3.ZERO);



			Viewport viewport = mRenderWindow.AddViewport(m_Camera);
			viewport.BackgroundColour = ColourValue.Black;
			m_Camera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;

			//m_Object = new GameObject(m_SceneManager);
            m_Path = new Path(0.1, new CircularMotion(0, 50, new Angle(0), new Angle(Math.PI / 10), 1, Vector2.ZERO));
			//m_Circle = path.CreatePathTo(new Vector2(100, -100), new Vector2(0, 10), Vector2.ZERO);
			//m_Circle = new CircularMotion(0, 50, new Angle(0), new Angle(Math.PI/2),2);
			m_Linear = new LinearMotion(0, new Vector2(10,0), Vector2.ZERO);

			m_SceneManager.AmbientLight = new ColourValue(1, 1, 1);

			mNinjaEntity = m_SceneManager.CreateEntity("Ninja", "ninja.mesh");

			mNinjaNode = m_SceneManager.RootSceneNode.CreateChildSceneNode("NinjaNode");
			mNinjaNode.AttachObject(mNinjaEntity);
            mNinjaNode.SetPosition(500, 0, -500);
			//mNinjaNode.Vector2 += Vector3.ZERO;

			Entity ent2 = m_SceneManager.CreateEntity("Head2", "ogrehead.mesh");
			SceneNode node2 = m_SceneManager.RootSceneNode.CreateChildSceneNode("HeadNode2");
			node2.AttachObject(ent2);
			//m_Camera.LookAt(node2.Position);

            Entity ent3 = m_SceneManager.CreateEntity("Head3", "ogrehead.mesh");
            SceneNode node3 = m_SceneManager.RootSceneNode.CreateChildSceneNode("HeadNode3");
            node3.AttachObject(ent3);
            node3.SetPosition(-500, 0, 500);
            Entity ent4 = m_SceneManager.CreateEntity("Head4", "ogrehead.mesh");
            SceneNode node4 = m_SceneManager.RootSceneNode.CreateChildSceneNode("HeadNode4");
            node4.AttachObject(ent4);
            node4.SetPosition(-500, 0, -500);
            Entity ent5 = m_SceneManager.CreateEntity("Head5", "ogrehead.mesh");
            SceneNode node5 = m_SceneManager.RootSceneNode.CreateChildSceneNode("HeadNode5");
            node5.AttachObject(ent5);
            node5.SetPosition(500, 0, 500);
            Entity ent6 = m_SceneManager.CreateEntity("Head6", "ogrehead.mesh");
            SceneNode node6 = m_SceneManager.RootSceneNode.CreateChildSceneNode("HeadNode6");
            node6.AttachObject(ent6);
            node6.SetPosition(500, 0, -500);

			mLight = m_SceneManager.CreateLight("pointLight");
			mLight.Type = Light.LightTypes.LT_POINT;
			mLight.Position = new Vector3(250, 150, 250);
			mLight.DiffuseColour = ColourValue.White;
			mLight.SpecularColour = ColourValue.White;

		}

		protected static void CreateRoot()
		{
			mRoot = new Root();
			mRoot.LoadPlugin("RenderSystem_Direct3D9");
			mRoot.LoadPlugin("RenderSystem_GL");
			mRoot.LoadPlugin("Plugin_OctreeSceneManager");

			m_SceneManager = mRoot.CreateSceneManager(SceneType.ST_GENERIC);
		}


		protected static void CreateFrameListeners()
		{
			mRoot.FrameRenderingQueued += ProcessUnbufferedInput;

		}

		protected static void InitializeInput()
		{
			int windowHandle;
			mRenderWindow.GetCustomAttribute("WINDOW", out windowHandle);
            
            ParamList pl = new ParamList(); 
            pl.Insert("WINDOW", windowHandle.ToString());
            pl.Insert("w32_mouse", "DISCL_FOREGROUND");
            pl.Insert("w32_mouse", "DISCL_NONEXCLUSIVE");
            pl.Insert("w32_keyboard", "DISCL_FOREGROUND");
            pl.Insert("w32_keyboard", "DISCL_NONEXCLUSIVE");

            mInputMgr = MOIS.InputManager.CreateInputSystem( pl);

			mNinjaKeyboard = (MOIS.Keyboard)mInputMgr.CreateInputObject(MOIS.Type.OISKeyboard, false);
			mNinjaMouse = (MOIS.Mouse)mInputMgr.CreateInputObject(MOIS.Type.OISMouse, true);
		    mNinjaMouse.MousePressed += MousePressed;
            var state = mNinjaMouse.MouseState;
            state.height = 600;
            state.width = 800;

		}

		private static bool ProcessUnbufferedInput(FrameEvent evt)
		{
			
			mNinjaKeyboard.Capture();
			mNinjaMouse.Capture();

			m_time += (ulong) (evt.timeSinceLastFrame*1000);
            var currentMovement= m_Path.GetCurrentMotion(m_time);
            var motion = currentMovement.GetCurrentPosition(m_time);
			//motion.x += 50;
            var rotation = new Angle(currentMovement.GetVelocity(m_time));
			rotation.ReduceAngle();
			
            Quaternion quat = new Quaternion(new Radian(rotation.Value + Math.PI/2), new Vector3(0, -1, 0));

            mNinjaNode.Position = new Vector3(motion.x,  0.0, motion.y);
            mNinjaNode.Orientation = quat;
			//Vector3 ninjaMove = Vector3.ZERO;

			if (mNinjaMouse.MouseState.ButtonDown(MOIS.MouseButtonID.MB_Left))
			{
				Axis_NativePtr x = mNinjaMouse.MouseState.X;
				var y = mNinjaMouse.MouseState.Y;
			}

			if (mNinjaKeyboard.IsKeyDown(MOIS.KeyCode.KC_SPACE))
				mLight.Visible = !mLight.Visible;

			if (mLightToggleTimeout > 0)
			{
				mLightToggleTimeout -= evt.timeSinceLastFrame;
			}
			else
			{
				if (mNinjaKeyboard.IsKeyDown(MOIS.KeyCode.KC_SPACE))
				{
					mLight.Visible = !mLight.Visible;
					mLightToggleTimeout = 0.5f;
				}
			}

			if (mNinjaKeyboard.IsKeyDown(MOIS.KeyCode.KC_ESCAPE))
			{
				return false;
			}

			return true;
		}

		private static bool MousePressed(MouseEvent mouseEvent, MouseButtonID id)
		{
            if (mouseEvent.state.ButtonDown(MouseButtonID.MB_Left))
            {
                var windowHeight = m_Camera.OrthoWindowHeight;
                var windowWidth = m_Camera.OrthoWindowWidth;
                Vector3 cameraPosition = m_Camera.Position;
                Vector3 cornerPosition = cameraPosition - new Vector3(windowWidth/2, 0, windowHeight/2);
                cornerPosition.y = 0;
                var mouseOffset = new Vector3(windowHeight * (1.0 - (double)mouseEvent.state.Y.abs / 600.0),
                                              0,
                                              windowWidth * (double)mouseEvent.state.X.abs / 800.0);
                var desiredPosition = cornerPosition + mouseOffset;
                //mNinjaNode.SetPosition(desiredPosition.x,0,desiredPosition.z);

                m_Path.MoveToDestination(new Vector2(desiredPosition.x, desiredPosition.z), m_time);
            }
            return true;
		}
	}
}
