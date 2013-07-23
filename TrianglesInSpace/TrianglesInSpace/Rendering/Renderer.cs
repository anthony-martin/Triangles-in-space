using System;
using Mogre;
using TrianglesInSpace.Input;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;
using TrianglesInSpace.Time;

namespace TrianglesInSpace.Rendering
{
    public interface IRendered
    {
        
    }
    public class Renderer : IDisposable
    {
        private Camera m_Camera;
        private SceneManager m_SceneManager;
        private Root m_Root;
        private RenderWindow m_RenderWindow;
        private SceneNode m_TriangleNode;
        private ClickMarker m_ClickMarker;
        private Scene m_Scene;
        private Light m_Light;

        private readonly InputController m_InputController;

        private readonly IBus m_Bus;
        private readonly IClock m_Clock;

        public Renderer( IBus bus, IClock clock)
        {
            m_Bus = bus;
            m_Clock = clock;

            CreateRoot();
            DefineResources();
            CreateRenderSystem();
            InitialiseRoot();
            
            CreateRenderWindow("");
            InitializeResources();
            CreateScene();
            CreateFrameListeners();
           // var overlays = new OverlayScene(new Vector(800, 800));
           // overlays.AddButton("hello", Vector.Zero, new Vector(50,20));
           // overlays.AddButton("world", new Vector(0, 20), new Vector(50, 20));
            int windowHandle;
            m_RenderWindow.GetCustomAttribute("WINDOW", out windowHandle);
            m_InputController = new InputController(windowHandle.ToString(), m_Camera, m_Bus, m_Clock);

        }

        private void CreateRoot()
        {
            m_Root = new Root();
            m_Root.LoadPlugin("RenderSystem_Direct3D9");
            m_Root.LoadPlugin("RenderSystem_GL");
            m_Root.LoadPlugin("Plugin_OctreeSceneManager");

            m_SceneManager = m_Root.CreateSceneManager(SceneType.ST_GENERIC);
            m_Scene = new Scene(m_Bus, m_SceneManager);
        }


        private void DefineResources()
        {
            ConfigFile configFile = new ConfigFile();
            configFile.Load("resources.cfg", "\t:=", true);

            var section = configFile.GetSectionIterator();
            while (section.MoveNext())
            {
                foreach (var line in section.Current)
                {
                    ResourceGroupManager.Singleton.AddResourceLocation(
                        line.Value, line.Key, section.CurrentKey);
                }
            }
        }

        private void CreateRenderSystem()
        {
            RenderSystem renderSystem = m_Root.GetRenderSystemByName("Direct3D9 Rendering Subsystem");
            renderSystem.SetConfigOption("Full Screen", "No");
            renderSystem.SetConfigOption("Video Mode", "800 x 800 @ 32-bit colour");
            renderSystem.SetConfigOption("FSAA", "16");
            m_Root.RenderSystem = renderSystem;
        }

        private void InitializeResources()
        {
            TextureManager.Singleton.DefaultNumMipmaps = 5;
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();

            // load the triangle and click star into the system
            var creator = new ShapeCreator(m_SceneManager);
            creator.CreateUnitTrianlge();
            creator.CreateStar();
        }
        
        private void InitialiseRoot()
        {
            m_Root.Initialise(false, "Main Ogre Window");
        }


        private void CreateScene()
        {
            
           
            m_SceneManager.AmbientLight = new ColourValue(1, 1, 1);

            CreateTriangleNode();
            CreateClickStar();
            LetThereBeLight();
        }

        public void CreateRenderWindow(string handle)
        {
            var form = new Form1();
            CreateCamera();

            NameValuePairList misc = new NameValuePairList();
            misc["externalWindowHandle"] = form.Handle.ToString();
            m_RenderWindow = m_Root.CreateRenderWindow("Main RenderWindow", 800, 600, false, misc);

            Viewport viewport = m_RenderWindow.AddViewport(m_Camera);
            viewport.BackgroundColour = ColourValue.Black;
            m_Camera.AspectRatio = (double)viewport.ActualWidth / (double)viewport.ActualHeight;
            form.Show();
        }

        private void CreateCamera()
        {
            m_Camera = m_SceneManager.CreateCamera("myCamera1");
            m_Camera.ProjectionType = ProjectionType.PT_ORTHOGRAPHIC;
            m_Camera.SetPosition(-1, 2000, 0);
            m_Camera.NearClipDistance = 5;
            m_Camera.FarClipDistance = 2501;
            m_Camera.LookAt(Vector3.ZERO);
            m_Camera.AutoAspectRatio = true;

        }

        private void CreateTriangleNode()
        {
           m_Scene.Add("triangle", "triangle");
        }

        private void CreateClickStar()
        {
            var clickStar = m_SceneManager.CreateEntity("Star", "star");
            var node = m_SceneManager.RootSceneNode.CreateChildSceneNode("ClickNode");
            node.AttachObject(clickStar);

            m_ClickMarker = new ClickMarker(node, m_Bus);
        }

        private void LetThereBeLight()
        {
            m_Light = m_SceneManager.CreateLight("pointLight");
            m_Light.Type = Light.LightTypes.LT_POINT;
            m_Light.Position = new Vector3(250, 150, 250);
            m_Light.DiffuseColour = ColourValue.White;
            m_Light.SpecularColour = ColourValue.White;
        }

        private void CreateFrameListeners()
        {
            m_Root.FrameRenderingQueued += OnRenderingCompleted;

        }

        private bool OnRenderingCompleted(FrameEvent evt)
        {
            m_Clock.UpdateTime(evt.timeSinceLastFrame);

            m_InputController.Capture();
            ((MessageBus)m_Bus).ProcessMessages();

            m_Scene.UpdatePosition(m_Clock.Time);

            return true;
        }

        public void StartRendering()
        {
            m_Root.StartRendering();
        }

        public void Dispose()
        {
            m_InputController.Dispose();
        }
    }
}
