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

namespace GcdTimes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            const bool UseColor = true;

            graphPictureBox.Image = null;
            xminLabel.Text = "";
            xmaxLabel.Text = "";
            yminLabel.Text = "";
            ymaxLabel.Text = "";
            Cursor = Cursors.WaitCursor;
            Refresh();

            // Perform the trials.
            Random rand = new Random();
            int numTrials = int.Parse(numTrialsTextBox.Text);
            float[] x = new float[numTrials];
            float[] y = new float[numTrials];
            int max = int.Parse(maxTextBox.Text);
            for (int i = 0; i < numTrials; i++)
            {
                int a = rand.Next(1, max);
                int b = rand.Next(1, max);
                int steps = GcdSteps(a, b);
                x[i] = (a + b) / 2f;
                y[i] = steps;
            }

            // Sort the results by X coordinate.
            Array.Sort(x, y);

            // Graph the result.
            int wid = graphPictureBox.ClientSize.Width;
            int hgt = graphPictureBox.ClientSize.Height;
            Bitmap bm = new Bitmap(wid, hgt);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Scale to fit.
                float xmin = x.Min();
                float xmax = x.Max();
                float ymin = y.Min();
                float ymax = y.Max();
                xminLabel.Text = xmin.ToString("0");
                xmaxLabel.Text = xmax.ToString("0");
                yminLabel.Text = ymin.ToString("0");
                ymaxLabel.Text = ymax.ToString("0");
                RectangleF rect = new RectangleF(
                    xmin, ymin, xmax - xmin, ymax - ymin);
                PointF[] pts =
                {
                    new PointF(0, hgt),
                    new PointF(wid, hgt),
                    new PointF(0, 0)
                };
                gr.Transform = new Matrix(rect, pts);

                // Convert the data into PointFs.
                PointF[] points = new PointF[numTrials];
                for (int i = 0; i < numTrials; i++)
                    points[i] = new PointF(x[i], y[i]);

                // Draw the results.
                if (UseColor) gr.DrawLines(Pens.Blue, points);
                else gr.DrawLines(Pens.Gray, points);

                // Draw y = log(x).
                for (int i = 0; i < numTrials; i++)
                    points[i] = new PointF(x[i], (float)Math.Log(x[i], 2));
                gr.DrawLines(Pens.Black, points);
            }
            graphPictureBox.Image = bm;

            // Arrange controls.
            graphPictureBox.Left = Math.Max(yminLabel.Right, ymaxLabel.Right);
            graphPictureBox.Width = ClientSize.Width - graphPictureBox.Left - 10;
            xminLabel.Left = graphPictureBox.Left;
            xmaxLabel.Left = graphPictureBox.Right - xmaxLabel.Width;
            Cursor = Cursors.Default;
        }

        // Return the number of steps needed to calculate GCD(a, b).
        // GCD(a, b) = GCD(b, a mod b).
        private int GcdSteps(int a, int b)
        {
            int steps = 0;
            while (b != 0)
            {
                steps++;

                // Calculate the remainder.
                int remainder = a % b;

                // Calculate GCD(b, remainder).
                a = b;
                b = remainder;
            }

            // GCD(a, 0) is a.
            //return a;
            return steps;
        }
    }
}
