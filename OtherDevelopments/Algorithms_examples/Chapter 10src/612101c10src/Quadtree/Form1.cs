#define USE_COLOR

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

namespace Quadtree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The quadtree's root.
        QuadtreeNode Root;

        // The selected point.
        private bool PointIsSelected = false;
        private PointF SelectedPoint;

        // The radius of a drawn point.
        private const float Radius = 5;

        // Initialize the quadtree.
        private void Form1_Load(object sender, EventArgs e)
        {
            Root = new QuadtreeNode(new RectangleF(0, 0,
                pointsPictureBox.ClientSize.Width - 1,
                pointsPictureBox.ClientSize.Height - 1));
        }

        // Redraw with or without the quadtree areas.
        private void drawBoxesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pointsPictureBox.Refresh();
        }

        // Display the points.
        private void pointsPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the points.
#if USE_COLOR
            Root.DrawPoints(e.Graphics, Brushes.White, Pens.Blue, Radius);
#else
            Root.DrawPoints(e.Graphics, Brushes.White, Pens.Black, Radius);
#endif

            // Draw the selected point.
            if (PointIsSelected)
            {
#if USE_COLOR
                e.Graphics.FillEllipse(Brushes.LightGreen,
                    SelectedPoint.X - Radius, SelectedPoint.Y - Radius,
                    2 * Radius, 2 * Radius);
                e.Graphics.DrawEllipse(Pens.Green,
                    SelectedPoint.X - Radius, SelectedPoint.Y - Radius,
                    2 * Radius, 2 * Radius);
#else
                e.Graphics.FillEllipse(Brushes.Gray,
                    SelectedPoint.X - Radius, SelectedPoint.Y - Radius,
                    2 * Radius, 2 * Radius);
                e.Graphics.DrawEllipse(Pens.Black,
                    SelectedPoint.X - Radius, SelectedPoint.Y - Radius,
                    2 * Radius, 2 * Radius);
#endif
            }

            // Draw the quadtree if desired.
            if (drawBoxesCheckBox.Checked)
            {
                e.Graphics.SmoothingMode = SmoothingMode.None;
#if USE_COLOR
                Root.DrawAreas(e.Graphics, Pens.Red);
#else
                Root.DrawAreas(e.Graphics, Pens.Black);
#endif
            }
        }

        // Add random points to the quadtree.
        private Random rand = new Random(0);
        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                int numPoints = int.Parse(numPointsTextBox.Text);
                float xmin = Radius;
                float ymin = Radius;
                float xmax = (pointsPictureBox.ClientSize.Width - Radius) / 3;
                float ymax = (pointsPictureBox.ClientSize.Height - Radius) / 3;
                for (int i = 0; i < numPoints; i++)
                {
                    float x = xmin + (float)(
                        (rand.NextDouble() * xmax - xmin) +
                        (rand.NextDouble() * xmax - xmin) +
                        (rand.NextDouble() * xmax - xmin));
                    float y = ymin + (float)(
                        (rand.NextDouble() * ymax - ymin) +
                        (rand.NextDouble() * ymax - ymin) +
                        (rand.NextDouble() * ymax - ymin));
                    Root.AddPoint(new PointF(x, y));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Redraw.
            pointsPictureBox.Refresh();
        }

        // Select the clicked point.
        private void pointsPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Find the point closest to the selected point.
            PointIsSelected = Root.FindPoint(e.Location, Radius, out SelectedPoint);

            // Redraw.
            pointsPictureBox.Refresh();
        }
    }
}
