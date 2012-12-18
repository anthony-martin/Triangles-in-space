using Mogre;

namespace TrianglesInSpace.Rendering
{
    public class Renderer
    {
        private Camera m_Camera;
        private SceneManager m_SceneManager;
        private Root m_Root;
        private RenderWindow m_RenderWindow;
        private SceneNode m_TriangleNode;
        private SceneNode m_ClickNode;
        private Light m_Light;

        public Renderer()
        {
            CreateRoot();
            DefineResources();
            CreateRenderSystem();
            CreateRenderWindow();
            InitializeResources();
            CreateScene();
        }

        private void CreateRoot()
        {
            m_Root = new Root();
            m_Root.LoadPlugin("RenderSystem_Direct3D9");
            m_Root.LoadPlugin("RenderSystem_GL");
            m_Root.LoadPlugin("Plugin_OctreeSceneManager");

            m_SceneManager = m_Root.CreateSceneManager(SceneType.ST_GENERIC);
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
            renderSystem.SetConfigOption("Video Mode", "800 x 600 @ 32-bit colour");
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
        
        private void CreateRenderWindow()
        {
            m_RenderWindow = m_Root.Initialise(true, "Main Ogre Window");
        }


        private void CreateScene()
        {
            CreateCamera();

            Viewport viewport = m_RenderWindow.AddViewport(m_Camera);
            viewport.BackgroundColour = ColourValue.Black;
            m_Camera.AspectRatio = (double)viewport.ActualWidth / (double)viewport.ActualHeight;

            m_SceneManager.AmbientLight = new ColourValue(1, 1, 1);

            CreateTriangleNode();
            CreateClickStar();
            LetThereBeLight();
        }

        private void CreateCamera()
        {
            m_Camera = m_SceneManager.CreateCamera("myCamera1");
            m_Camera.ProjectionType = ProjectionType.PT_ORTHOGRAPHIC;
            m_Camera.SetPosition(-1, 2000, 0);
            m_Camera.NearClipDistance = 5;
            m_Camera.FarClipDistance = 2501;
            m_Camera.LookAt(Vector3.ZERO);
        }

        private void CreateTriangleNode()
        {
            var triangle = m_SceneManager.CreateEntity("triangle", "triangle");

            m_TriangleNode = m_SceneManager.RootSceneNode.CreateChildSceneNode("triangle");
            m_TriangleNode.AttachObject(triangle);
        }

        private void CreateClickStar()
        {
            var clickStar = m_SceneManager.CreateEntity("Star", "star");
            m_ClickNode = m_SceneManager.RootSceneNode.CreateChildSceneNode("ClickNode");
            m_ClickNode.AttachObject(clickStar);
        }

        private void LetThereBeLight()
        {
            m_Light = m_SceneManager.CreateLight("pointLight");
            m_Light.Type = Light.LightTypes.LT_POINT;
            m_Light.Position = new Vector3(250, 150, 250);
            m_Light.DiffuseColour = ColourValue.White;
            m_Light.SpecularColour = ColourValue.White;
        }


        public void StartRendering()
        {
            m_Root.StartRendering();
        }
    }
}
