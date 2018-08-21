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

namespace RectangleRule
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool UseColor = true;

        // The function we will integrate.
        private double F(double x)
        {
            return 1 + x + Math.Sin(2.0 * x);
        }
        private double AntiDerivativeF(double x)
        {
            return x + x * x / 2.0 - Math.Cos(2.0 * x) / 2.0;
        }

        // Get the minimum and maximum Y values.
        private void GetYBounds(double xmin, double xmax, out double ymin, out double ymax)
        {
            ymin = F(xmin);
            ymax = ymin;
            for (double x = xmin; x <= xmax; x += 0.1)
            {
                double y = F(x);
                if (y < ymin) ymin = y;
                if (y > ymax) ymax = y;
            }

            double dy = ymax - ymin;
            ymin -= dy * 0.05;
            ymax += dy * 0.05;
        }

        // Integrate the function.
        private void integrateButton_Click(object sender, EventArgs e)
        {
            // Get parameters.
            double xmin = double.Parse(xMinTextBox.Text);
            double xmax = double.Parse(xMaxTextBox.Text);

            double ymin, ymax;
            GetYBounds(xmin, xmax, out ymin, out ymax);
            
            int intervals = int.Parse(intervalsTextBox.Text);

            // Integrate.
            double estArea = IntegrateRectangle(F, xmin, xmax, ymin, ymax, intervals);
            estAreaTextBox.Text = estArea.ToString();

            // Display the true area and percent error.
            double trueArea = AntiDerivativeF(xmax) - AntiDerivativeF(xmin);
            trueAreaTextBox.Text = trueArea.ToString();

            double pctError = 100 * (estArea - trueArea) / trueArea;
            pctErrorTextBox.Text = pctError.ToString("0.000") + "%";

            // Draw the graph.
            DrawGraph(F);
        }

        // Integrate by using the rectangle rule.
        private double IntegrateRectangle(Func<double, double> function, double xmin, double xmax, double ymin, double ymax, int intervals)
        {
            double dx = (xmax - xmin) / intervals;
            double total = 0;

            // Perform the integration.
            double x = xmin;
            for (int interval = 0; interval < intervals; interval++)
            {
                // Add the area in the rectangle for this slice.
                total += dx * function(x);

                // Move to the next slice.
                x += dx;
            }

            return total;
        }

        // Draw the graph.
        // (This is for visualization and isn't part of the algorithm.)
        private void DrawGraph(Func<double, double> function)
        {
            // Get parameters.
            float xmin = float.Parse(xMinTextBox.Text);
            float xmax = float.Parse(xMaxTextBox.Text);

            double y0, y1;
            GetYBounds(xmin, xmax, out y0, out y1);
            float ymin = (float)y0;
            float ymax = (float)y1;

            int intervals = int.Parse(intervalsTextBox.Text);

            // The area we will display.
            float xMargin = (xmax - xmin) / 10f;
            float yMargin = (ymax - ymin) / 10f;
            float wxmin = xmin - xMargin;
            float wxmax = xmax + xMargin;
            float wymin = ymin - yMargin;
            float wymax = ymax + yMargin;

            // Make a Bitmap.
            Bitmap bm = new Bitmap(
                graphPictureBox.ClientSize.Width,
                graphPictureBox.ClientSize.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Scale to make the graph fit nicely.
                RectangleF worldRect = new RectangleF(
                    wxmin, wymax, wxmax - wxmin, wymin - wymax);
                PointF[] devicePoints =
                {
                    new PointF(0, 0),
                    new PointF(graphPictureBox.ClientSize.Width, 0),
                    new PointF(0, graphPictureBox.ClientSize.Height),
                };
                gr.Transform = new Matrix(worldRect, devicePoints);

                // Draw.
                gr.Clear(Color.White);
                using (Pen thin_pen = new Pen(Color.Red, 0))
                {
                    if (!UseColor) thin_pen.Color = Color.Black;
                    // Fill and draw the rectangles.
                    FillRectangles(gr, function, xmin, xmax, ymin, ymax, intervals);
                    DrawRectangles(gr, function, xmin, xmax, ymin, ymax, intervals);

                    // Draw the X axis.
                    gr.DrawLine(thin_pen, wxmin, 0, wxmax, 0);
                    for (int x = (int)wxmin; x <= wxmax; x++)
                    {
                        gr.DrawLine(thin_pen,
                            x, -yMargin / 4,
                            x, yMargin / 4);
                    }

                    // Draw the Y axis.
                    gr.DrawLine(thin_pen, 0, wymin, 0, wymax);
                    for (int y = (int)wymin; y <= wymax; y++)
                    {
                        gr.DrawLine(thin_pen,
                            -xMargin / 4, y,
                            xMargin / 4, y);
                    }

                    // See how far apart 1 pixel on the screen
                    // is in world coordinates.
                    PointF[] pts = { new PointF(0, 0), new PointF(1, 1) };
                    Matrix inverse = gr.Transform;
                    inverse.Invert();
                    inverse.TransformPoints(pts);
                    float graphDx = pts[1].X - pts[0].X;

                    // Draw the function.
                    List<PointF> graphPts = new List<PointF>();
                    for (float x = wxmin; x <= wxmax; x += graphDx)
                    {
                        graphPts.Add(new PointF(x, (float)function(x)));
                    }
                    if (UseColor) thin_pen.Color = Color.Green;
                    gr.DrawLines(thin_pen, graphPts.ToArray());
                }

                // Display the graph.
                graphPictureBox.Image = bm;
            }
        }

        // Draw the integration rectangles.
        private void DrawRectangles(Graphics gr, Func<double, double> function, double xmin, double xmax, double ymin, double ymax, int intervals)
        {
            using (Pen thin_pen = new Pen(Color.Blue, 0))
            {
                if (!UseColor) thin_pen.Color = Color.Black;
                float dx = (float)((xmax - xmin) / intervals);

                // Perform the integration.
                float x = (float)xmin;
                for (int interval = 0; interval < intervals; interval++)
                {
                    // Draw the slice.
                    float y = (float)function(x);
                    if (y >= 0)
                    {
                        gr.DrawRectangle(thin_pen, x, 0, dx, y);
                    }
                    else
                    {
                        gr.DrawRectangle(thin_pen, x, y, dx, -y);
                    }

                    // Move to the next slice.
                    x += dx;
                }
            }
        }

        // Fill the integration rectangles.
        private void FillRectangles(Graphics gr, Func<double, double> function, double xmin, double xmax, double ymin, double ymax, int intervals)
        {
            float dx = (float)((xmax - xmin) / intervals);

            // Perform the integration.
            float x = (float)xmin;
            for (int interval = 0; interval < intervals; interval++)
            {
                // Fill the slice.
                float y = (float)function(x);

                if (y >= 0)
                {
                    if (UseColor) gr.FillRectangle(Brushes.LightBlue, x, 0, dx, y);
                    else gr.FillRectangle(Brushes.Silver, x, 0, dx, y);
                }
                else
                {
                    if (UseColor) gr.FillRectangle(Brushes.LightBlue, x, y, dx, -y);
                    else gr.FillRectangle(Brushes.Silver, x, y, dx, -y);
                }

                // Move to the next slice.
                x += dx;
            }
        }
    }
}
