using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mogre;
using MOIS;
using phase1;

namespace Mogre.Tutorials
{
	public class TutorialMain 
	{
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

		private static GameObject m_Object;

		private static double m_time;

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
			m_Camera.SetPosition(-100, 2350, 0);
			m_Camera.NearClipDistance = 5;
			m_Camera.FarClipDistance = 2501;
			m_Camera.LookAt(Vector3.ZERO);
			


			Viewport viewport = mRenderWindow.AddViewport(m_Camera);
			viewport.BackgroundColour = ColourValue.Black;
			m_Camera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;

			m_Object = new GameObject(m_SceneManager);

			//m_SceneManager.AmbientLight = new ColourValue(1, 1, 1);

			//mNinjaEntity = m_SceneManager.CreateEntity("Ninja", "ninja.mesh");

			//mNinjaNode = m_SceneManager.RootSceneNode.CreateChildSceneNode("NinjaNode");
			//mNinjaNode.AttachObject(mNinjaEntity);
			//mNinjaNode.Vector2 += Vector3.ZERO;

			Entity ent2 = m_SceneManager.CreateEntity("Head2", "ogrehead.mesh");
			SceneNode node2 = m_SceneManager.RootSceneNode.CreateChildSceneNode("HeadNode2");
			node2.AttachObject(ent2);
			//m_Camera.LookAt(node2.Position);


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
			mInputMgr = MOIS.InputManager.CreateInputSystem((uint)windowHandle);

			mNinjaKeyboard = (MOIS.Keyboard)mInputMgr.CreateInputObject(MOIS.Type.OISKeyboard, false);
			mNinjaMouse = (MOIS.Mouse)mInputMgr.CreateInputObject(MOIS.Type.OISMouse, false);

		}

		private static bool ProcessUnbufferedInput(FrameEvent evt)
		{
			m_Object.draw(m_time);
			mNinjaKeyboard.Capture();
			mNinjaMouse.Capture();

			m_time += 0.1;

			Vector3 ninjaMove = Vector3.ZERO;

			if (mNinjaKeyboard.IsKeyDown(MOIS.KeyCode.KC_I))
				ninjaMove.z -= 1;

			if (mNinjaKeyboard.IsKeyDown(MOIS.KeyCode.KC_K))
				ninjaMove.z += 1;

			if (mNinjaKeyboard.IsKeyDown(MOIS.KeyCode.KC_J))
				ninjaMove.x -= 1;

			if (mNinjaKeyboard.IsKeyDown(MOIS.KeyCode.KC_L))
				ninjaMove.x += 1;

			//mNinjaNode.Translate(ninjaMove, Node.TransformSpace.TS_LOCAL);

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
	}
}
