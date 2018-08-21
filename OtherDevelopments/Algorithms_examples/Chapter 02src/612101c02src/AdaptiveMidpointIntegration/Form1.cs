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

namespace AdaptiveMidpointIntegration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool UseColor = true;

        // The number of slices we drew.
        private int NumIntervals;

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
            double maxSliceError = double.Parse(maxSliceErrorTextBox.Text.Replace("%", ""));
            if (maxSliceErrorTextBox.Text.Contains("%")) maxSliceError /= 100.0;

            // Integrate.
            double estArea = IntegrateAdaptiveMidpoint(F, xmin, xmax, intervals, maxSliceError);
            estAreaTextBox.Text = estArea.ToString();

            // Display the true area and percent error.
            double trueArea = AntiDerivativeF(xmax) - AntiDerivativeF(xmin);
            trueAreaTextBox.Text = trueArea.ToString();

            double pctError = 100 * (estArea - trueArea) / trueArea;
            pctErrorTextBox.Text = pctError.ToString("0.000") + "%";

            // Draw the graph.
            DrawGraph(F);

            // Display the number of intervals used.
            numIntervalsTextBox.Text = NumIntervals.ToString();
        }

        // Integrate by using an adaptive midpoint trapezoid rule.
        private double IntegrateAdaptiveMidpoint(Func<double, double> function, double xmin, double xmax, int intervals, double maxSliceError)
        {
            double dx = (xmax - xmin) / intervals;
            double total = 0;

            // Perform the integration.
            double x = xmin;
            for (int interval = 0; interval < intervals; interval++)
            {
                // Add the area in the rectangle for this slice.
                total += SliceArea(function, x, x + dx, maxSliceError);

                // Move to the next slice.
                x += dx;
            }

            return total;
        }

        // Return the area for this slice.
        private double SliceArea(Func<double, double> function, double x1, double x2, double maxSliceError)
        {
            // Calculate the function at the end points and the midpoint.
            double y1 = function(x1);
            double y2 = function(x2);
            double xm = (x1 + x2) / 2.0;
            double ym = function(xm);

            // Calculate the area for the large slice and two sub-slices.
            double area12 = (x2 - x1) * (y1 + y2) / 2.0;
            double area1m = (xm - x1) * (y1 + ym) / 2.0;
            double aream2 = (x2 - xm) * (ym + y2) / 2.0;
            double area1m2 = area1m + aream2;

            // See how close we are.
            double error = (area1m2 - area12) / area12;

            // See if this is small enough.
            if (Math.Abs(error) < maxSliceError) return area1m2;

            // The error is too big. Divide the slice and try again.
            return
                SliceArea(function, x1, xm, maxSliceError) +
                SliceArea(function, xm, x2, maxSliceError);
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
            double maxSliceError = double.Parse(maxSliceErrorTextBox.Text.Replace("%", ""));
            if (maxSliceErrorTextBox.Text.Contains("%")) maxSliceError /= 100.0;

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
                    // Fill and draw the trapezoids.
                    NumIntervals = 0;
                    FillTrapezoids(gr, function, xmin, xmax, ymin, ymax, intervals, maxSliceError);
                    DrawTrapezoids(gr, function, xmin, xmax, ymin, ymax, intervals, maxSliceError);

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

        // Draw the integration trapezoids.
        private void DrawTrapezoids(Graphics gr, Func<double, double> function, double xmin, double xmax, double ymin, double ymax, int intervals, double maxSliceError)
        {
            double dx = (xmax - xmin) / intervals;

            // Perform the integration.
            double x = xmin;
            for (int interval = 0; interval < intervals; interval++)
            {
                // Draw this slice.
                DrawOneTrapezoid(gr, function, x, x + dx, maxSliceError);

                // Move to the next slice.
                x += dx;
            }
        }

        // Draw one trapezoid.
        private void DrawOneTrapezoid(Graphics gr, Func<double, double> function, double x1, double x2, double maxSliceError)
        {
            // Calculate the function at the end points and the midpoint.
            double y1 = function(x1);
            double y2 = function(x2);
            double xm = (x1 + x2) / 2.0;
            double ym = function(xm);

            // Calculate the area for the large slice and two sub-slices.
            double area12 = (x2 - x1) * (y1 + y2) / 2.0;
            double area1m = (xm - x1) * (y1 + ym) / 2.0;
            double aream2 = (x2 - xm) * (ym + y2) / 2.0;
            double area1m2 = area1m + aream2;

            // See how close we are.
            double error = (area1m2 - area12) / area12;

            // See if this is small enough.
            if (Math.Abs(error) < maxSliceError)
            {
                using (Pen thin_pen = new Pen(Color.Blue, 0))
                {
                    if (!UseColor) thin_pen.Color = Color.Black;
                    // Draw the sub-slices.
                    PointF[] pts =
                    {
                        new PointF((float)x1, 0),
                        new PointF((float)x1, (float)y1),
                        new PointF((float)xm, (float)ym),
                        new PointF((float)xm, 0),
                    };
                    gr.DrawPolygon(thin_pen, pts);
                    pts = new PointF[]
                    {
                        new PointF((float)xm, 0),
                        new PointF((float)xm, (float)ym),
                        new PointF((float)x2, (float)y2),
                        new PointF((float)x2, 0),
                    };
                    gr.DrawPolygon(thin_pen, pts);
                    return;
                }
            }

            // The error is too big. Divide the slice and try again.
            DrawOneTrapezoid(gr, function, x1, xm, maxSliceError);
            DrawOneTrapezoid(gr, function, xm, x2, maxSliceError);
        }

        // Fill the integration trapezoids.
        private void FillTrapezoids(Graphics gr, Func<double, double> function, double xmin, double xmax, double ymin, double ymax, int intervals, double maxSliceError)
        {
            double dx = (xmax - xmin) / intervals;

            // Perform the integration.
            double x = xmin;
            for (int interval = 0; interval < intervals; interval++)
            {
                // Fill this slice.
                FillOneTrapezoid(gr, function, x, x + dx, maxSliceError);

                // Move to the next slice.
                x += dx;
            }
        }

        // Fill one trapezoid.
        private void FillOneTrapezoid(Graphics gr, Func<double, double> function, double x1, double x2, double maxSliceError)
        {
            // Calculate the function at the end points and the midpoint.
            double y1 = function(x1);
            double y2 = function(x2);
            double xm = (x1 + x2) / 2.0;
            double ym = function(xm);

            // Calculate the area for the large slice and two sub-slices.
            double area12 = (x2 - x1) * (y1 + y2) / 2.0;
            double area1m = (xm - x1) * (y1 + ym) / 2.0;
            double aream2 = (x2 - xm) * (ym + y2) / 2.0;
            double area1m2 = area1m + aream2;

            // See how close we are.
            double error = (area1m2 - area12) / area12;

            // See if this is small enough.
            if (Math.Abs(error) < maxSliceError)
            {
                NumIntervals += 2;

                // Fill the sub-slices.
                PointF[] pts =
                {
                    new PointF((float)x1, 0),
                    new PointF((float)x1, (float)y1),
                    new PointF((float)xm, (float)ym),
                    new PointF((float)xm, 0),
                };
                if (UseColor) gr.FillPolygon(Brushes.LightBlue, pts);
                else gr.FillPolygon(Brushes.Silver, pts);
                pts = new PointF[]
                {
                    new PointF((float)xm, 0),
                    new PointF((float)xm, (float)ym),
                    new PointF((float)x2, (float)y2),
                    new PointF((float)x2, 0),
                };
                if (UseColor) gr.FillPolygon(Brushes.LightBlue, pts);
                else gr.FillPolygon(Brushes.Silver, pts);
                return;
            }

            // The error is too big. Divide the slice and try again.
            FillOneTrapezoid(gr, function, x1, xm, maxSliceError);
            FillOneTrapezoid(gr, function, xm, x2, maxSliceError);
        }
    }
}
