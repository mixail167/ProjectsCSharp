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

namespace RuntimeFunctions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool drawFibonacci = true;

        private void graphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Compare the two methods for calculating the Fibonacci function.
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(Fibonacci(i) + " = " + Fibonacci2(i));
            }

            const bool useColor = true;
            DrawGraph(e.Graphics, -0.75f, 20.5f, -0.75f, 20.5f, 1, 1, useColor);
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

                // Log(X).
                points = new List<PointF>();
                for (float x = dx; x <= xmax; x += dx)
                {
                    float y = (float)Math.Log(x, 2);
                    if (float.IsInfinity(y) || float.IsNaN(y)) break;
                    points.Add(new PointF(x, y));
                }
                if (useColor) thinPen.Color = Color.Blue;
                gr.DrawLines(thinPen, points.ToArray());

                // 1.5 * Sqrt(X).
                points = new List<PointF>();
                for (float x = 0; x <= xmax; x += dx)
                {
                    float y = 1.5f * (float)Math.Sqrt(x);
                    if (float.IsInfinity(y) || float.IsNaN(y)) break;
                    points.Add(new PointF(x, y));
                }
                if (useColor) thinPen.Color = Color.Green;
                gr.DrawLines(thinPen, points.ToArray());

                // X.
                points = new List<PointF>();
                points.Add(new PointF(xmin, xmin));
                points.Add(new PointF(xmax, xmax));
                if (useColor) thinPen.Color = Color.Black;
                gr.DrawLines(thinPen, points.ToArray());

                // X * X / 5.
                points = new List<PointF>();
                for (float x = 0; x <= xmax; x += dx)
                {
                    float y = x * x / 5;
                    if (float.IsInfinity(y) || float.IsNaN(y)) break;
                    points.Add(new PointF(x, y));
                }
                if (useColor) thinPen.Color = Color.Orange;
                gr.DrawLines(thinPen, points.ToArray());

                // 2^X / 10.
                points = new List<PointF>();
                for (float x = 0; x <= xmax; x += dx)
                {
                    float y = (float)Math.Pow(2, x) / 10;
                    if (float.IsInfinity(y) || float.IsNaN(y)) break;
                    points.Add(new PointF(x, y));
                    if (y > ymax) break;
                }
                if (useColor) thinPen.Color = Color.Fuchsia;
                gr.DrawLines(thinPen, points.ToArray());

                // X! / 100.
                points = new List<PointF>();
                for (int x = 0; x <= xmax; x++)
                {
                    float y = (float)Factorial(x) / 100;
                    if (float.IsInfinity(y) || float.IsNaN(y)) break;
                    points.Add(new PointF(x, y));
                    if (y > ymax) break;
                }
                if (useColor) thinPen.Color = Color.Red;
                gr.DrawLines(thinPen, points.ToArray());

                // Fibonacci(X) / 10.
                if (drawFibonacci)
                {
                    points = new List<PointF>();
                    for (int x = 0; x <= xmax; x++)
                    {
                        float y = (float)Fibonacci(x) / 10;
                        if (float.IsInfinity(y) || float.IsNaN(y)) break;
                        points.Add(new PointF(x, y));
                        if (y > ymax) break;
                    }
                    if (useColor) thinPen.Color = Color.Blue;
                    gr.DrawLines(thinPen, points.ToArray());
                }
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
                    for (int y = skip; y <= xmax; y += skip)
                    {
                        pts = new PointF[] { new PointF(0, y) };
                        transform.TransformPoints(pts);
                        gr.DrawString(y.ToString(), font, Brushes.Black, pts[0], sf);
                    }
                }
            }

            // Draw labels.
            using (Font font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular))
            {
                DrawRotatedText(gr, font, Brushes.Blue, "y = Log(x)", 414, 440, -8, useColor);
                DrawRotatedText(gr, font, Brushes.Green, "y = 1.5 * Sqrt(x)", 410, 390, -11, useColor);
                DrawRotatedText(gr, font, Brushes.Black, "y = x", 360, 200, -45, useColor);
                DrawRotatedText(gr, font, Brushes.Orange, "y = x² / 5", 242, 140, -75, useColor);
                DrawRotatedText(gr, font, Brushes.Fuchsia, "y = 2ˣ / 10", 200, 135, -85, useColor);
                DrawRotatedText(gr, font, Brushes.Red, "y = x! / 100", 140, 125, -90, useColor);

                if (drawFibonacci)
                    DrawRotatedText(gr, font, Brushes.Blue, "y = Fibonacci(x) / 10", 321, 230, -83, useColor);
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

        // Return the nth Fibonacci number.
        private double Fibonacci(int n)
        {
            if (n == 0) return 0;
            double fibMinus2 = 0;   // Fibonacci(0)
            double fibMinus1 = 1;   // Fibonacci(1)
            double fib = 1;
            for (int i = 2; i <= n; i++)
            {
                fib = fibMinus1 + fibMinus2;
                fibMinus2 = fibMinus1;
                fibMinus1 = fib;
            }
            return fib;
        }

        // The French mathematician Abraham de Moivre discovered
        // in 1718 that you can calculate the Nth like this:
        //     Round(phi^N / Sqrt(5)) where phi = (1 + Sqrt(5)) / 2.
        private double Fibonacci2(int n)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            return Math.Round(Math.Pow(phi, n) / Math.Sqrt(5.0));
        }
    }
}
