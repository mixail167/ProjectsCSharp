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

namespace AdaptiveTrapezoidIntegration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The function we will integrate.
        private double F(double x)
        {
            return 1 + x + Math.Sin(2.0 * x);
        }
        private double AntiDerivativeF(double x)
        {
            return x + x * x / 2.0 - Math.Cos(2.0 * x) / 2.0;
        }
        private double dFdx(double x)
        {
            return 1 + 2.0 * Math.Cos(2.0 * x);
        }
        private double d2Fdx2(double x)
        {
            return -4.0 * Math.Sin(2.0 * x);
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

            // Get the X coordinates to use for intervals.
            List<double> xValues = IntervalXValues(
                F, xmin, xmax, ymin, ymax, intervals);

            // Integrate.
            double estArea = IntegrateTrapezoidAdaptive(F, xValues);
            estAreaTextBox.Text = estArea.ToString();

            // Display the true area and percent error.
            double trueArea = AntiDerivativeF(xmax) - AntiDerivativeF(xmin);
            trueAreaTextBox.Text = trueArea.ToString();

            double pctError = 100 * (estArea - trueArea) / trueArea;
            pctErrorTextBox.Text = pctError.ToString("0.000") + "%";

            numIntervalsTextBox.Text = (xValues.Count - 1).ToString();

            // Draw the graph.
            DrawGraph(F, xValues);
        }

        // Make a list of interval X coordinate values.
        private List<double> IntervalXValues(Func<double, double> function, double xmin, double xmax, double ymin, double ymax, int numSlices)
        {
            // Calculate the size of an initial slice.
            double sliceWidth = (xmax - xmin) / numSlices;

            // Find the X coordinates.
            double x = xmin;
            List<double> xValues = new List<double>();
            while (x < xmax)
            {
                // Get the second derivative at this point.
                double d2 = d2Fdx2(x);

                // Use the derivative to decide how many intervals
                // to break the slice into.
                int intervals = 1 + (int)Math.Abs(d2);
                double dx = sliceWidth / intervals;

                // Process the intervals.
                for (int interval = 0; interval < intervals; interval++)
                {
                    // Add this X value to the result list.
                    xValues.Add(x);

                    // Find the end of the interval.
                    x += dx;
                    if (x >= xmax) break;
                }
            }

            // Add the final point.
            if (xValues[xValues.Count - 1] < xmax) xValues.Add(xmax);

            // Return the list.
            return xValues;
        }

        // Integrate by using the trapezoid rule.
        private double IntegrateTrapezoidAdaptive(Func<double, double> function, List<double> xValues)
        {
            // Process the intervals.
            double total = 0;
            for (int interval = 0; interval < xValues.Count - 1; interval++)
            {
                double x1 = xValues[interval];
                double x2 = xValues[interval + 1];
                total += (x2 - x1) * (function(x1) + function(x2)) / 2.0f;
            }
            return total;
        }

        // Draw the graph.
        // (This is for visualization and isn't part of the algorithm.)
        private void DrawGraph(Func<double, double> function, List<double> xValues)
        {
            // Get parameters.
            float xmin = float.Parse(xMinTextBox.Text);
            float xmax = float.Parse(xMaxTextBox.Text);

            double y0, y1;
            GetYBounds(xmin, xmax, out y0, out y1);
            float ymin = (float)y0;
            float ymax = (float)y1;

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
                    // Fill and draw the trapezoids.
                    FillTrapezoids(gr, function, xValues);
                    DrawTrapezoids(gr, function, xValues);

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
                    thin_pen.Color = Color.Green;
                    gr.DrawLines(thin_pen, graphPts.ToArray());
                }

                // Display the graph.
                graphPictureBox.Image = bm;
            }
        }

        // Draw the integration trapezoids.
        private void DrawTrapezoids(Graphics gr, Func<double, double> function, List<double> xValues)
        {
            using (Pen thin_pen = new Pen(Color.Blue, 0))
            {
                // Draw the trapezoids.
                for (int interval = 0; interval < xValues.Count - 1; interval++)
                {
                    // Fill the slice.
                    float x1 = (float)xValues[interval];
                    float x2 = (float)xValues[interval + 1];
                    float y1 = (float)function(x1);
                    float y2 = (float)function(x2);
                    PointF[] pts =
                {
                    new PointF(x1, 0),
                    new PointF(x1, y1),
                    new PointF(x2, y2),
                    new PointF(x2, 0),
                };
                    gr.DrawPolygon(thin_pen, pts);
                }
            }
        }

        // Draw the integration trapezoids.
        private void FillTrapezoids(Graphics gr, Func<double, double> function, List<double> xValues)
        {
            // Fill the trapezoids.
            for (int interval = 0; interval < xValues.Count - 1; interval++)
            {
                // Fill the slice.
                float x1 = (float)xValues[interval];
                float x2 = (float)xValues[interval + 1];
                float y1 = (float)function(x1);
                float y2 = (float)function(x2);
                PointF[] pts =
                {
                    new PointF(x1, 0),
                    new PointF(x1, y1),
                    new PointF(x2, y2),
                    new PointF(x2, 0),
                };
                gr.FillPolygon(Brushes.LightBlue, pts);
            }
        }
    }
}
