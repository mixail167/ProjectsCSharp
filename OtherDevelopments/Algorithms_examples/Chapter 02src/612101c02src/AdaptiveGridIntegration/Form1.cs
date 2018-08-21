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

namespace AdaptiveGridIntegration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool UseColor = true;

        // Variables to calculate the area.
        private Random Rand = new Random();

        // The number of boxes we use.
        private int NumBoxes;

        // The area of interest.
        private const float Wxmin = -5;
        private const float Wxmax = 5;
        private const float Wymin = -5;
        private const float Wymax = 5;
        private const float Wwid = Wxmax - Wxmin;
        private const float Whgt = Wymin - Wymax;

        // A Bitmap to show the problem and test points.
        private Bitmap Bm;
        private Graphics Gr;

        // Prepare the Bitmap.
        private void Form1_Load(object sender, EventArgs e)
        {
            MakeBitmap();
        }

        // Make the Bitmap and reset the area counts.
        private void MakeBitmap()
        {
            // Reset the area data.
            areaTextBox.Clear();
            numBoxesTextBox.Clear();

            // Make the Bitmap and Graphics object.
            Bm = new Bitmap(
                graphPictureBox.ClientSize.Width,
                graphPictureBox.ClientSize.Height);
            Gr = Graphics.FromImage(Bm);
            Gr.SmoothingMode = SmoothingMode.AntiAlias;

            // Scale to make the graph fit nicely.
            RectangleF worldRect = new RectangleF(Wxmin, Wymax, Wwid, Whgt);
            PointF[] devicePoints =
                {
                    new PointF(0, 0),
                    new PointF(graphPictureBox.ClientSize.Width, 0),
                    new PointF(0, graphPictureBox.ClientSize.Height),
                };
            Gr.Transform = new Matrix(worldRect, devicePoints);

            Brush insideBrush = Brushes.LightBlue;
            Brush outsideBrush = Brushes.White;
            if (!UseColor) insideBrush = Brushes.Gray;
            using (Pen thin_pen = new Pen(Color.Blue, 0))
            {
                if (!UseColor) thin_pen.Color = Color.Black;
                // Define the shapes.
                RectangleF ellipseRect1 = new RectangleF(-2, -4, 4, 8);
                RectangleF ellipseRect2 = new RectangleF(-4, -2, 8, 4);
                RectangleF circleRect1 = new RectangleF(-1.7f, -1.7f, 3.4f, 3.4f);
                RectangleF circleRect2 = new RectangleF(-3, -1, 2, 2);
                RectangleF circleRect3 = new RectangleF(1, -1, 2, 2);

                // Fill the shapes.
                Gr.FillEllipse(insideBrush, ellipseRect1);
                Gr.FillEllipse(insideBrush, ellipseRect2);
                Gr.FillEllipse(outsideBrush, circleRect1);
                Gr.FillEllipse(outsideBrush, circleRect2);
                Gr.FillEllipse(outsideBrush, circleRect3);

                // Draw the shapes.
                Gr.DrawEllipse(thin_pen, ellipseRect1);
                Gr.DrawEllipse(thin_pen, ellipseRect2);
                Gr.DrawEllipse(thin_pen, circleRect1);
                Gr.DrawEllipse(thin_pen, circleRect2);
                Gr.DrawEllipse(thin_pen, circleRect3);

                // Draw the X axis.
                if (UseColor) thin_pen.Color = Color.Red;
                const float tick = 0.2f;
                Gr.DrawLine(thin_pen, Wxmin, 0, Wxmax, 0);
                for (int x = (int)Wxmin; x <= Wxmax; x++)
                    Gr.DrawLine(thin_pen, x, -tick, x, tick);

                // Draw the Y axis.
                Gr.DrawLine(thin_pen, 0, Wymin, 0, Wymax);
                for (int y = (int)Wymin; y <= Wymax; y++)
                    Gr.DrawLine(thin_pen, -tick, y, tick, y);
            }

            // Display the image.
            graphPictureBox.Image = Bm;
        }

        // Perform the indicated number of trials.
        private void integrateButton_Click(object sender, EventArgs e)
        {
            MakeBitmap();
            Cursor = Cursors.WaitCursor;

            // Get the parameters.
            int numRows = int.Parse(numRowsColsTextBox.Text);
            double minBoxArea = double.Parse(minBoxAreaTextBox.Text);
            NumBoxes = 0;

            // Start with the full box.
            using (Pen thin_pen = new Pen(Color.Blue, 0))
            {
                if (!UseColor) thin_pen.Color = Color.Black;
                double area = IntegrateAdaptiveGrid(thin_pen,
                    Wxmin, Wxmax, Wymin, Wymax, numRows, minBoxArea);
                areaTextBox.Text = area.ToString();
                numBoxesTextBox.Text = NumBoxes.ToString();
            }

            // Display the result.
            graphPictureBox.Refresh();

            Cursor = Cursors.Default;
        }

        // Use an adaptive grid to estimate the area inside this box.
        private double IntegrateAdaptiveGrid(Pen thin_pen, float boxXmin, float boxXmax, float boxYmin, float boxYmax, int numRows, double minBoxArea)
        {
            NumBoxes++;

            // Prepare to divide the box into sub-boxes.
            float wid = boxXmax - boxXmin;
            float hgt = boxYmax - boxYmin;
            float dx = wid / numRows;
            float dy = hgt / numRows;
            float xmin = boxXmin;

            // See if there are both hits and misses in the box.
            bool hasHits = false;
            bool hasMisses = false;
            float y = boxYmin;
            for (int row = 0; row < numRows; row++)
            {
                float x = boxXmin;
                for (int col = 0; col < numRows; col++)
                {
                    if (PointIsInShape(x, y))
                    {
                        hasHits = true;
                        if (hasMisses) break;
                    }
                    else
                    {
                        hasMisses = true;
                        if (hasHits) break;
                    }
                    if (hasHits && hasMisses) break;
                    x += dx;
                }
                y += dy;
            }

            // If there were no hits, return 0.
            if (!hasHits) return 0;

            // If there were no misses, return the box's area.
            float boxArea = Math.Abs(wid * hgt);
            if (!hasMisses) return boxArea;

            // See if the box is too small to divide.
            if (boxArea < minBoxArea)
            {
                // Too small. See how many points are in the shape.
                int numHits = 0;
                int numMisses = 0;
                y = boxYmin;
                for (int row = 0; row < numRows; row++)
                {
                    float x = boxXmin;
                    for (int col = 0; col < numRows; col++)
                    {
                        if (PointIsInShape(x, y)) numHits++;
                        else numMisses++;
                        x += dx;
                    }
                    y += dy;
                }

                return boxArea * numHits / (float)(numRows * numRows);
            }

            // Draw the box.
            Gr.DrawRectangle(thin_pen, boxXmin, boxYmin, wid, hgt);

            // Divide the box.
            double area = 0;
            xmin = boxXmin;
            y = boxYmin;
            for (int row = 0; row < numRows; row++)
            {
                float x = boxXmin;
                for (int col = 0; col < numRows; col++)
                {
                    area += IntegrateAdaptiveGrid(thin_pen,
                        x, x + dx, y, y + dy, numRows, minBoxArea);
                    x += dx;
                }
                y += dy;
            }

            // Return the result.
            return area;
        }

        // Return true if the point is inside the shape.
        private bool PointIsInShape(float x, float y)
        {
            // See if it is inside all of the ellipses.
            if ((x * x / 4.0 + y * y / 16.0 > 1.0) &&
                (x * x / 16.0 + y * y / 4.0 > 1.0)) return false;

            // See if it is inside any circle.
            if (x * x + y * y < 1.7 * 1.7) return false;
            if ((x + 2) * (x + 2) + y * y < 1) return false;
            if ((x - 2) * (x - 2) + y * y < 1) return false;

            return true;
        }

        // Reset.
        private void resetButton_Click(object sender, EventArgs e)
        {
            MakeBitmap();
        }
    }
}
