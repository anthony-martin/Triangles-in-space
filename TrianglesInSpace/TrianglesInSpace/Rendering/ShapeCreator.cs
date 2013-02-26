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
                    mesh._setBounds(new AxisAlignedBox(10,10,10,-10,-10,-10));
                    mesh.Load();
                }
            }
        }

        public void CreateStar()
        {
            using (var manualObject = m_SceneManager.CreateManualObject("Star"))
            {
                manualObject.Begin("blue");

                manualObject.Position(10, 0, 0);        //0
                manualObject.Position(7, 0, -3);        //1
                manualObject.Position(0, 0, 0);         //2
                manualObject.Position(7, 0, 3);         //3

                manualObject.Position(-7, 0, 3);        //4
                manualObject.Position(-10, 0, 0);       //5
                manualObject.Position(-7, 0, -3);       //6

                manualObject.Position(0, 0, 10);       //7
                manualObject.Position(-3, 0, 7);       //8
                manualObject.Position(3, 0, 7);       //9

                manualObject.Position(0, 0, -10);       //10
                manualObject.Position(-3, 0, -7);       //11
                manualObject.Position(3, 0, -7);       //12

                manualObject.Index(0);
                manualObject.Index(1);
                manualObject.Index(2);

                manualObject.Index(0);
                manualObject.Index(2);
                manualObject.Index(3);

                manualObject.Index(2);
                manualObject.Index(5);
                manualObject.Index(4);

                manualObject.Index(2);
                manualObject.Index(6);
                manualObject.Index(5);

                manualObject.Index(2);
                manualObject.Index(8);
                manualObject.Index(7);

                manualObject.Index(2);
                manualObject.Index(7);
                manualObject.Index(9);

                manualObject.Index(2);
                manualObject.Index(10);
                manualObject.Index(11);

                manualObject.Index(2);
                manualObject.Index(12);
                manualObject.Index(10);

                manualObject.End();

                using (var mesh = manualObject.ConvertToMesh("star"))
                {
                    mesh._setBounds(new AxisAlignedBox(1, 1, 1, -1, -1, -1));
                    mesh.Load();
                }
            }
        }
    }
}
