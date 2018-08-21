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

namespace NewtonsMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void graphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            const bool useColor = false;
            DrawGraph(e.Graphics, -0.5f, 4.5f, -1.5f, 1.5f, 1, 1, useColor);
        }

        // Draw the graph.
        private void DrawGraph(Graphics gr, float xmin, float xmax, float ymin, float ymax, int ticDx, int ticDy, bool UseColor)
        {
            gr.SmoothingMode = SmoothingMode.AntiAlias;

            // Scale to fit.
            RectangleF rect = new RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
            PointF[] pts = 
            {
                new PointF(0, graphPictureBox.ClientSize.Height),
                new PointF(graphPictureBox.ClientSize.Width, graphPictureBox.ClientSize.Height),
                new PointF(0, 0),
            };
            Matrix transform = new Matrix(rect, pts);
            gr.Transform = transform;

            // Get a unit in X and Y directions.
            pts = new PointF[] { new PointF(0, 0), new PointF(1, 1) };
            Matrix inverse = transform.Clone();
            inverse.Invert();
            inverse.TransformPoints(pts);
            float dx = pts[1].X - pts[0].X;
            float dy = pts[1].Y - pts[0].Y;

            using (Pen thinPen = new Pen(Color.Black, 0))
            {
                // Draw axes.
                gr.DrawLine(thinPen, xmin, 0, xmax, 0);
                for (int x = 0; x <= xmax; x += ticDx)
                    gr.DrawLine(thinPen, x, -4 * dy, x, 4 * dy);
                for (int x = 0; x >= xmin; x -= ticDx)
                    gr.DrawLine(thinPen, x, -4 * dy, x, 4 * dy);

                gr.DrawLine(thinPen, 0, ymin, 0, ymax);
                for (int y = 0; y <= ymax; y += ticDx)
                    gr.DrawLine(thinPen, -4 * dx, y, 4 * dx, y);
                for (int y = 0; y >= ymin; y -= ticDx)
                    gr.DrawLine(thinPen, -4 * dx, y, 4 * dx, y);

                // Draw the curve. y = x^3 / 5f - x * x + x.
                List<PointF> points = new List<PointF>();
                points = new List<PointF>();
                for (float x = xmin; x <= xmax; x += dx)
                {
                    points.Add(new PointF(x, F(x)));
                }
                if (UseColor) thinPen.Color = Color.Black;
                gr.DrawLines(thinPen, points.ToArray());

                // Find the zeros.
                if (UseColor) thinPen.Color = Color.Red;
                float x1 = FindZero(gr, thinPen, 0.3f);
                zero1TextBox.Text = "(" + x1.ToString("0.00") +
                    ", " + F(x1).ToString("0.00") + ")";

                if (UseColor) thinPen.Color = Color.Green;
                float x2 = FindZero(gr, thinPen, 1f);
                zero2TextBox.Text = "(" + x2.ToString("0.00") +
                    ", " + F(x2).ToString("0.00") + ")";

                if (UseColor) thinPen.Color = Color.Blue;
                float x3 = FindZero(gr, thinPen, 3);
                zero3TextBox.Text = "(" + x3.ToString("0.00") +
                    ", " + F(x3).ToString("0.00") + ")";
            }

            // Label the axes.
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            gr.ResetTransform();
            using (Font font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular))
            {
                using (StringFormat sf = new StringFormat())
                {
                    const int skip = 1;

                    // X axis.
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    for (int x = skip; x <= xmax; x += skip)
                    {
                        pts = new PointF[] { new PointF(x, 0) };
                        transform.TransformPoints(pts);
                        gr.DrawString(x.ToString(), font, Brushes.Black, pts[0], sf);
                    }
                    for (int x = -skip; x >= xmin; x -= skip)
                    {
                        pts = new PointF[] { new PointF(x, 0) };
                        transform.TransformPoints(pts);
                        gr.DrawString(x.ToString(), font, Brushes.Black, pts[0], sf);
                    }

                    // Y axis.
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Center;
                    for (int y = skip; y <= ymax; y += skip)
                    {
                        pts = new PointF[] { new PointF(0, y) };
                        transform.TransformPoints(pts);
                        gr.DrawString(y.ToString(), font, Brushes.Black, pts[0], sf);
                    }
                    for (int y = -skip; y >= ymin; y -= skip)
                    {
                        pts = new PointF[] { new PointF(0, y) };
                        transform.TransformPoints(pts);
                        gr.DrawString(y.ToString(), font, Brushes.Black, pts[0], sf);
                    }
                }
            }
        }

        // F(x).
        private float F(float x)
        {
            return x * x * x / 5f - x * x + x;
        }

        // dF/dx.
        private float dFdx(float x)
        {
            return (3 * x * x - 10 * x + 5) / 5f;
        }

        // Use Newton's Method to find a zero from this starting point.
        private float FindZero(Graphics gr, Pen pen, float startX)
        {
            float dx = 0.035f;
            float dy = 0.035f;
            const float maxError = 1e-6f;
            float x = startX;
            for (int i = 0; i < 100; i++)
            {
                // Calculate and plot this point.
                float y = F(x);
                gr.DrawEllipse(pen, x - dx, y - dy, 2 * dx, 2 * dy);
                Console.WriteLine("(" + x.ToString() + ", " + y.ToString() + ")");

                // If we have a small enough error, stop.
                if (Math.Abs(y) < maxError) break;

                // Update x.
                x -= y / dFdx(x);
            }
            Console.WriteLine("");
            return x;
        }
    }
}
