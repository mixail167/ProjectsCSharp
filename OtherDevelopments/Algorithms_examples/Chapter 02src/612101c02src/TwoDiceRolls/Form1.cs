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
using System.Drawing.Text;

namespace TwoDiceRolls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool UseColor = true;

        // Generate the rolls.
        private void rollButton_Click(object sender, EventArgs e)
        {
            GraphPictureBox.Image = null;
            Cursor = Cursors.WaitCursor;
            Refresh();

            // Make an array to hold value counts.
            // The value counts[i] represents rolls of i.
            long[] counts = new long[13];

            // Roll.
            Random rand = new Random();
            long numTrials = long.Parse(numTrialsTextBox.Text);
            for (int i = 0; i < numTrials; i++)
            {
                int result = rand.Next(1, 7) + rand.Next(1, 7);
                counts[result]++;
            }

            // The expected percentages.
            float[] expected = 
            {
                0, 0, 1 / 36f, 2 / 36f, 3 / 36f, 4 / 36f, 5 / 36f, 
                6 / 36f, 5 / 36f, 4 / 36f, 3 / 36f, 2 / 36f, 1 / 36f
            };

            // Display the results.
            Bitmap bm = new Bitmap(
                GraphPictureBox.ClientSize.Width,
                GraphPictureBox.ClientSize.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Far;

                    float ymax = GraphPictureBox.ClientSize.Height;
                    float scaleX = GraphPictureBox.ClientSize.Width / 11;
                    float scaleY = ymax / (counts.Max() * 1.05f);

                    Brush fillBrush = Brushes.LightBlue;
                    if (!UseColor) fillBrush = Brushes.Silver;
                    Pen outlinePen = Pens.Blue;
                    if (!UseColor) outlinePen = Pens.Black;

                    for (int i = 2; i <= 12; i++)
                    {
                        gr.FillRectangle(fillBrush,
                            (i - 2) * scaleX, ymax - counts[i] * scaleY,
                            scaleX, counts[i] * scaleY);
                        gr.DrawRectangle(outlinePen,
                            (i - 2) * scaleX, ymax - counts[i] * scaleY,
                            scaleX, counts[i] * scaleY);

                        float percent = 100 * counts[i] / (float)numTrials;
                        float expectedPercent = 100 * expected[i];
                        float error = 100 * (expectedPercent - percent) / expectedPercent;
                        string txt = percent.ToString("0.00") +
                            Environment.NewLine +
                            expectedPercent.ToString("0.00") +
                            Environment.NewLine +
                            error.ToString("0.00") + "%";
                        gr.DrawString(txt, this.Font, Brushes.Black,
                            (i - 2) * scaleX, ymax - counts[i] * scaleY);
                    }

                    // Scale the expected percentages for the number of rolls.
                    for (int i = 0; i < expected.Length; i++)
                    {
                        expected[i] *= numTrials;
                    }

                    // Draw the expected results.
                    List<PointF> expectedPoints = new List<PointF>();
                    expectedPoints.Add(new PointF(0, ymax));
                    for (int i = 2; i <= 12; i++)
                    {
                        float y = ymax - expected[i] * scaleY;
                        expectedPoints.Add(new PointF((i - 2) * scaleX, y));
                        expectedPoints.Add(new PointF((i - 1) * scaleX, y));
                    }
                    expectedPoints.Add(new PointF(11 * scaleX, ymax));
                    using (Pen pen = new Pen(Color.Red, 3))
                    {
                        if (!UseColor) pen.Color = Color.Black;
                        gr.DrawLines(pen, expectedPoints.ToArray());
                    }
                }
            }

            GraphPictureBox.Image = bm;
            Cursor = Cursors.Default;
        }
    }
}
