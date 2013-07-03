using System;
using Mogre;

namespace TrianglesInSpace.Rendering
{
    public interface IOverlayScene
    {
        
    }

    public class OverlayScene
    {
        public OverlayScene()
        {
            OverlayContainer container = (OverlayContainer)OverlayManager.Singleton.CreateOverlayElement("Panel", "PanelName");

            //note we need to load a font if we want to use a font
            var font = FontManager.Singleton.Create("Arial", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            font.SetParameter("type", "truetype");
            font.SetParameter("source", "Arial.ttf");
            font.SetParameter("size", "16");
            font.SetParameter("resolution", "96");
            font.Load();

            //note positions and sizes are in relative screen space
            container.SetPosition(0.5, 0.5);
            container.SetDimensions(0.1, 0.1);
            container.MaterialName = "triangle/red";


            TextAreaOverlayElement text = (TextAreaOverlayElement)OverlayManager.Singleton.CreateOverlayElement("TextArea", "host");
            text.MetricsMode = GuiMetricsMode.GMM_PIXELS;
            text.Caption = "hello world";
            text.SetPosition(0,0);
            text.SetDimensions(50,20);
            text.CharHeight = 26;
            text.FontName = "Arial";
            text.ColourBottom = new ColourValue(0.3f, 0.5f, 0.3f);
            text.ColourTop = new ColourValue(0.5f, 0.7f, 0.5f);
            var overlay = OverlayManager.Singleton.Create("bob");
            container.AddChild(text);
            overlay.Add2D(container);

            overlay.Show();
        }
    }
}
