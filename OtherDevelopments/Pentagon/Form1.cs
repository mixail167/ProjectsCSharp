using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pentagon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DrawPolygon(Graphics g, int count, Point center, float r)
        {
            Pen pen = Pens.Navy;
            double angle = -Math.PI * 0.5;
            Point[] points = new Point[count];
            for (int i = 0; i < count; i++)
            {
                points[i] = new Point(
                    center.X + (int)Math.Round(Math.Cos(angle + Math.PI * 2.0 * i / count) * r),
                    center.Y + (int)Math.Round(Math.Sin(angle + Math.PI * 2.0 * i / count) * r)
                );
            }
            g.DrawPolygon(pen, points);
            Font font = this.Font;
            string str = string.Format("N = {0}", count);
            SizeF size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, new PointF(center.X - size.Width * 0.5f, center.Y - size.Height * 0.5f));
        }

        private int polyCount = 5;

        private void picDraw_Paint(object sender, PaintEventArgs e)
        {
            DrawPolygon(e.Graphics, polyCount, new Point(100, 100), 80);
        }

        private void picDraw_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    polyCount++;
                    (sender as Control).Invalidate();
                    break;
                case MouseButtons.Right:
                    if (polyCount > 3)
                    {
                        polyCount--;
                        (sender as Control).Invalidate();
                    }
                    break;
            }
        }
    }
}
