using Mogre;

namespace TrianglesInSpace.Rendering
{
    public class ShapeCreator
    {
        private readonly SceneManager m_SceneManager;

        public ShapeCreator(SceneManager sceneManager)
        {
            m_SceneManager = sceneManager; 
        }

        public void CreateUnitTrianlge()
        {
            using (var manualObject = m_SceneManager.CreateManualObject("Triangle"))
            {
                manualObject.Begin("blue");

                manualObject.Position(10, 0, 0);
                manualObject.Position(-10, 0, 10);
                manualObject.Position(-10, 0, -10);

                manualObject.Index(0);
                manualObject.Index(2);
                manualObject.Index(1);

                manualObject.End();

                using(var mesh = manualObject.ConvertToMesh("triangle"))
                {
                    mesh._setBounds(new AxisAlignedBox(1,1,1,-1,-1,-1));
                    mesh.Load();
                }
            }


        }
    }
}
