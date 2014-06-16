using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrianglesInSpace.Rendering;

namespace TrianglesInSpace
{
	public partial class Form1 : Form
	{
        private readonly IRenderer m_Renderer;
		public Form1()
		{
			InitializeComponent();
		}

        public Form1(IRenderer renderer)
        {
            m_Renderer = renderer;
            InitializeComponent();
            string handle = panel1.Handle.ToString();
            m_Renderer.CreateRenderWindow(handle);
        }

        public void GO()
        {
            Show();
            m_Renderer.StartRendering();
        }
	}
}
