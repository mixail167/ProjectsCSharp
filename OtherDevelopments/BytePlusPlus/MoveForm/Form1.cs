using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MoveForm
{
    public partial class Form1 : Form
    {       
        public const int WM_MCLBUTTONDOWN = 0XA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_MCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
