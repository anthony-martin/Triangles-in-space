using System;
using Mogre;
using TrianglesInSpace.Primitives;
using Box = TrianglesInSpace.Primitives.Box;

namespace TrianglesInSpace.Rendering
{
    public interface IOverlayScene
    {
        
    }

    public class OverlayScene
    {
        private Vector m_ScreenSize;
        private Overlay m_Overlay;
        private OverlayContainer m_Container;

        public OverlayScene(Vector screenSize)
        {
            m_ScreenSize = screenSize;
            
            //note we need to load a font if we want to use a font
            var font = FontManager.Singleton.Create("Arial", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            font.SetParameter("type", "truetype");
            font.SetParameter("source", "Arial.ttf");
            font.SetParameter("size", "16");
            font.SetParameter("resolution", "96");
            font.Load();
            m_Container = (OverlayContainer)OverlayManager.Singleton.CreateOverlayElement("Panel", "PanelName");

            //note positions and sizes are in relative screen space
            m_Container.SetPosition(0.35, 0.3);
            m_Container.SetDimensions(0.3, 0.5);
            m_Container.MaterialName = "triangle/red";


            m_Overlay = OverlayManager.Singleton.Create("bob");
            
            m_Overlay.Add2D(m_Container);

            m_Overlay.Show();
        }

        public void AddButton( string text, Vector positon, Vector dimensions )
        {
            // unique name or bang
            TextAreaOverlayElement button = (TextAreaOverlayElement)OverlayManager.Singleton.CreateOverlayElement("TextArea", text);
            button.MetricsMode = GuiMetricsMode.GMM_PIXELS;
            button.Caption = text;
            button.SetPosition(positon.X, positon.Y);
            button.SetDimensions(dimensions.X, dimensions.Y);
            button.CharHeight = 26;
            button.FontName = "Arial";
            button.ColourBottom = new ColourValue(0.3f, 0.5f, 0.3f);
            button.ColourTop = new ColourValue(0.5f, 0.7f, 0.5f);

            m_Container.AddChild(button);
        }
    }
}
