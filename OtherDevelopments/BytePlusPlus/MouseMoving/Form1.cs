using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MouseMoving
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        public Form1()
        {
            InitializeComponent();
            MoveMouse(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
        }

        private void MoveMouse(int screenWidth, int screenHeight)
        {
            POINT p = new POINT();
            Random r = new Random();
            for (int i = 0; i < 50; i++)
            {
                p.x = Convert.ToInt16(r.Next(screenWidth));
                p.y = Convert.ToInt16(r.Next(screenHeight));
                ClientToScreen(Handle, ref p);
                SetCursorPos(p.x, p.y);
                Thread.Sleep(100);
            }
        }
    }
}
