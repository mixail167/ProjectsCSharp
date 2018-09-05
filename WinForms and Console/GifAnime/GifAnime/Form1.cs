using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GifAnime
{
    public partial class Form1 : Form
    {
        bool currentlyAnimating = false;
        Bitmap animatedImage;
        private double koef;
        List<Image> frames;

        public Form1()
        {
            InitializeComponent();
            animatedImage = Properties.Resources.gif4;
            double koef2;
            if (animatedImage.Width >= animatedImage.Height)
            {
                koef = pictureBox1.Width * 1.0 / animatedImage.Width;
            }
            else
            {
                koef = pictureBox1.Height * 1.0 / animatedImage.Height;
            }
            koef2 = animatedImage.Height * 1.0 / animatedImage.Width;
            frames = Frames(animatedImage);
            TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel()
            {
                AutoScroll = true,
                ColumnCount = 1,
                RowCount = frames.Count,
                Dock = DockStyle.Fill
            };
            for (int i = 0; i < frames.Count; i++)
            {
                PictureBox pictureBox = new PictureBox()
                {
                    Parent = tableLayoutPanel2,
                    Image = frames[i],
                    Size = new Size(150, (int)(150 * koef2)),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                tableLayoutPanel2.Controls.Add(pictureBox, 0, i);
            }
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
        }

        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
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
            e.Graphics.DrawImage(animatedImage, new RectangleF(0, 0, (float)(animatedImage.Width * koef), (float)(animatedImage.Height * koef)));
        }

        private List<Image> Frames(Image image)
        {
            List<Image> frames = new List<Image>();
            FrameDimension d = new FrameDimension(image.FrameDimensionsList[0]);
            for (int i = 0; i < image.GetFrameCount(d); i++)
            {
                image.SelectActiveFrame(d, i);
                frames.Add(new Bitmap(image));
            }
            return frames;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(animatedImage.Width * frames.Count, animatedImage.Height);
                Graphics g = Graphics.FromImage(image);
                for (int i = 0; i < frames.Count; i++)
                {
                    g.DrawImage(frames[i], i * animatedImage.Width, 0);
                }
                image.Save(saveFileDialog1.FileName,ImageFormat.Bmp);
            }
        }
    }
}
