using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace ServiceAccount
{
    public partial class Form2 : Form
    {
        bool currentlyAnimating;
        Bitmap animatedImage;

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        private async void WaitForConnection()
        {
            await Task.Run(() =>
            {
                int description;
                while (!InternetGetConnectedState(out description, 0))
                {
                    Thread.Sleep(3000);
                }
            });
            Hide();
            new Form1().ShowDialog();
            Close();
        }

        public Form2()
        {
            InitializeComponent();
            animatedImage = Properties.Resources.download;
            WaitForConnection();
        }

        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                ImageAnimator.Animate(animatedImage, new EventHandler(OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            AnimateImage();
            ImageAnimator.UpdateFrames();
            e.Graphics.DrawImage(animatedImage, 0, 0, Properties.Resources.download.Width / 8, Properties.Resources.download.Height / 8);
        }
    }
}
