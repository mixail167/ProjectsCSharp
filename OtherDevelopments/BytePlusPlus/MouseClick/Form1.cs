using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MouseClick
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dsFlags, int dx, int dy, int cButtons, int dsExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;


        private void DoMouseLeftClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private void DoMouseRightClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        }

        private void DoMouseDoubleLeftClick(int x, int y)
        {
            DoMouseLeftClick(x, y);
            DoMouseLeftClick(x, y);
        }

        public Form1()
        {
            InitializeComponent();
            ClickMouse();
        }

        private void ClickMouse()
        {
            POINT p = new POINT();
            for (int i = 0; i < 10; i++)
            {
                GetCursorPos(ref p);
                ClientToScreen(Handle, ref p);
                DoMouseLeftClick(p.x, p.y);
                Thread.Sleep(100);
            }
        }
    }
}
