using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrianglesInSpace.Rendering;
using System.Windows.Interop;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Time;
using TrianglesInSpace.Input;

namespace TrianglesInSpace.Wpf
{
    /// <summary>
    /// Interaction logic for GameFormWpf.xaml
    /// </summary>
    public partial class GameFormWpf : Window
    {
        private readonly IRenderer m_Renderer;
        public GameFormWpf()
        {
            InitializeComponent();
        }

        public GameFormWpf(IRenderer renderer, IMainFormModel model)
        {
            m_Renderer = renderer;
            DataContext = model;

            InitializeComponent();

            var host = m_Renderer as HwndHost;
            m_GameWindow.Child = host;
            host.MessageHook += onMessage;

            

            //host.KeyDown += m_GameWindow_KeyDown;
           // m_Main
        }

        public void GO()
        {
            Show();
            m_Renderer.StartRendering();

            if(true)
            {
            }
        }

        private IntPtr onMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            
            

            return IntPtr.Zero;
        }

        private void m_GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NullReferenceException();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
