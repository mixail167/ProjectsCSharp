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

namespace Ch01Ex04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void graphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph(e.Graphics, -0.75f, 20.5f, -0.75f, 20.5f, 1, 1, true);
        }

        // Draw the graph.
        private void DrawGraph(Graphics gr, float xmin, float xmax, float ymin, float ymax, int ticDx, int ticDy, bool useColor)
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
            float zx1, zx2, zy1, zy2;

            using (Pen thinPen = new Pen(Color.Black, 0))
            {
                // Draw axes.
                gr.DrawLine(thinPen, xmin, 0, xmax, 0);
                for (int x = 0; x <= xmax; x += ticDx)
                {
                    gr.DrawLine(thinPen, x, -4 * dy, x, 4 * dy);
                }

                gr.DrawLine(thinPen, 0, ymin, 0, ymax);
                for (int y = 0; y <= ymax; y += ticDx)
                {
                    gr.DrawLine(thinPen, -4 * dx, y, 4 * dx, y);
                }

                // Draw curves.                
                List<PointF> points = new List<PointF>();

                // y = x * x * x / 75f - x * x / 4f + x + 10.
                points = new List<PointF>();
                for (float x = xmin; x <= xmax; x += dx)
                {
                    float y = x * x * x / 75f - x * x / 4f + x + 10;
                    points.Add(new PointF(x, y));
                    if (y > ymax) break;
                }
                if (useColor) thinPen.Color = Color.Red;
                gr.DrawLines(thinPen, points.ToArray());

                // y = x / 2 + 8
                points = new List<PointF>();
                foreach (float x in new float[] { xmin, xmax })
                {
                    float y = x / 2 + 8;
                    points.Add(new PointF(x, y));
                    if (y > ymax) break;
                }
                if (useColor) thinPen.Color = Color.Blue;
                gr.DrawLines(thinPen, points.ToArray());

                // Draw the places where the curves intersect.
                if (useColor) thinPen.Color = Color.Green;
                zx1 = (float)FindZero(5, 1e-10);
                zy1 = zx1 / 2 + 8;
                gr.DrawEllipse(thinPen, zx1 - 0.1f, zy1 - 0.1f, 0.2f, 0.2f);

                zx2 = (float)FindZero(16, 1e-10);
                zy2 = zx2 / 2 + 8;
                gr.DrawEllipse(thinPen, zx2 - 0.1f, zy2 - 0.1f, 0.2f, 0.2f);
            }

            // Label the axes.
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            gr.ResetTransform();
            using (Font font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular))
            {
                using (StringFormat sf = new StringFormat())
                {
                    const int skip = 5;

                    // X axis.
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    for (int x = skip; x <= xmax; x += skip)
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

                    // Label the points of intersection.
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    pts = new PointF[]
                    {
                        new PointF(zx1, zy1),
                        new PointF(zx2, zy2)
                    };
                    transform.TransformPoints(pts);
                    gr.DrawString(
                        "(" + zx1.ToString("0.00") +
                        ", " + zy1.ToString("0.00") + ")",
                        font, Brushes.Green, pts[0].X + 20, pts[0].Y, sf);
                    gr.DrawString(
                        "(" + zx2.ToString("0.00") +
                        ", " + zy2.ToString("0.00") + ")",
                        font, Brushes.Green, pts[1].X + 15, pts[1].Y, sf);
                }
            }

            // Draw labels.
            using (Font font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular))
            {
                DrawRotatedText(gr, font, Brushes.Red,
                    "y = x^3 / 75f - x^2 / 4f + x + 10",
                    150, 350, 0, useColor);
                DrawRotatedText(gr, font, Brushes.Blue,
                    "y = x / 2 + 8", 220, 220, -26, useColor);
            }
        }

        // Return n!
        private double Factorial(int n)
        {
            double total = 1;
            for (int i = 2; i <= n; i++) total *= n;
            return total;
        }

        // Draw rotated text at the indicated position.
        // Note: This method resets the Graphics object's transformation.
        private void DrawRotatedText(Graphics gr, Font font, Brush brush, string text, int x, int y, float angle, bool useColor)
        {
            gr.ResetTransform();
            gr.RotateTransform(angle, MatrixOrder.Append);
            gr.TranslateTransform(x, y, MatrixOrder.Append);
            if (useColor) gr.DrawString(text, font, brush, 0, 0);
            else gr.DrawString(text, font, Brushes.Black, 0, 0);
        }

        // Find a zero for the equation.
        private double FindZero(double start, double maxError)
        {
            double x = start;
            double y = F(x);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("(" + x.ToString() + ", " + y.ToString() + ")");
                if (Math.Abs(y) < maxError) break;
                x -= y / dFdx(x);
                y = F(x);
            }

            Console.WriteLine("");
            return x;
        }

        private double F(double x)
        {
            return (x * x * x / 75f - x * x / 4f + x + 10) - (x / 2 + 8);
        }

        private double dFdx(double x)
        {
            return (x * x / 25.0 - x / 2.0 + 1.0) - (0.5);
        }
    }
}
