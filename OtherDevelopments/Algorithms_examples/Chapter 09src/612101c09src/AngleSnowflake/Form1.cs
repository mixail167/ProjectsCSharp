using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AngleSnowflake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            int depth = (int)depthNumericUpDown.Value;
            double theta = double.Parse(angleTextBox.Text);
            theta *= Math.PI / 180.0;

            Bitmap bm = new Bitmap(
                snowflakePictureBox.ClientSize.Width,
                snowflakePictureBox.ClientSize.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.Blue, 0))
                {
                    // Figure out where to put the corners.
                    const int margin = 5;
                    int h = bm.Height - 2 * margin;

                    double l = h / ((1 + Math.Cos(theta)) * Math.Sqrt(3.0) + Math.Sin(theta));
                    float y = (float)(margin + l);
                    float length = 2 * (float)(l + l * Math.Cos(theta));
                    float x = (float)((bm.Width - length) / 2);
                    PointF pt1 = new PointF(x, y);
                    PointF pt2 = new PointF(x + length, y);
                    PointF pt3 = new PointF(
                        (float)(x + length * Math.Cos(Math.PI / 3)),
                        (float)(y + length * Math.Sin(Math.PI / 3)));

                    // Draw the sides.
                    DrawKoch(gr, pen, depth, theta, pt1, 0, length);
                    DrawKoch(gr, pen, depth, theta, pt2, Math.PI * 2 / 3, length);
                    DrawKoch(gr, pen, depth, theta, pt3, -Math.PI * 2 / 3, length);
                }
            }
            snowflakePictureBox.Image = bm;
            bm.Save("KochSnowflake" + depth.ToString() + ".jpg", ImageFormat.Png);
        }

        private void DrawKoch(Graphics gr, Pen pen, int depth, double theta, PointF pt1, double angle, float length)
        {
            if (depth == 0)
            {
                PointF pt2 = new PointF(
                    (float)(pt1.X + length * Math.Cos(angle)),
                    (float)(pt1.Y + length * Math.Sin(angle)));
                gr.DrawLine(pen, pt1, pt2);
            }
            else
            {
                float newLength = (float)(length / 2.0 / (1.0 + Math.Cos(theta)));

                PointF pt2 = new PointF(
                    (float)(pt1.X + newLength * Math.Cos(angle)),
                    (float)(pt1.Y + newLength * Math.Sin(angle)));

                double theta1 = angle - theta;
                double theta2 = angle + theta;
                PointF pt3 = new PointF(
                    (float)(pt2.X + newLength * Math.Cos(theta1)),
                    (float)(pt2.Y + newLength * Math.Sin(theta1)));

                PointF pt4 = new PointF(
                    (float)(pt3.X + newLength * Math.Cos(theta2)),
                    (float)(pt3.Y + newLength * Math.Sin(theta2)));

                DrawKoch(gr, pen, depth - 1, theta, pt1, angle, newLength);
                DrawKoch(gr, pen, depth - 1, theta, pt2, theta1, newLength);
                DrawKoch(gr, pen, depth - 1, theta, pt3, theta2, newLength);
                DrawKoch(gr, pen, depth - 1, theta, pt4, angle, newLength);
            }
        }
    }
}
