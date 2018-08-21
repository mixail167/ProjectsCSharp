using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomTrees
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const bool UseColor = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
            DoubleBuffered = true;
            Seed = int.Parse(seedTextBox.Text);
            Refresh();
        }

        // The random number generator seed.
        private int Seed = 0;

        private void goButton_Click(object sender, EventArgs e)
        {
            Seed = int.Parse(seedTextBox.Text);
            Refresh();
        }

        // Draw a random tree.
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Initialize the random number generator.
            Random rand = new Random(Seed);

            // Start drawing.
            float x = ClientSize.Width / 2;
            float y = goButton.Top - 5;
            float thickness = 5;
            float length = (int)(y * rand.Next(20, 30) / 100.0);
            double angle = -Math.PI / 2;
            DrawTree(rand, e.Graphics, thickness, length, x, y, angle);
        }

        // Draw a branch.
        private void DrawTree(Random rand, Graphics gr, float thickness, float length, float x, float y, double angle)
        {
            // See where this branch ends.
            float x1 = (float)(x + length * Math.Cos(angle));
            float y1 = (float)(y + length * Math.Sin(angle));

            int greenness = (int)(10 * length);
            if (greenness > 255) greenness = 255;
            if (!UseColor) greenness = 0;
            Color color = Color.FromArgb(0, greenness, 0);
            using (Pen pen = new Pen(color, thickness))
            {
                // Draw the branch.
                gr.DrawLine(pen, x, y, x1, y1);
            }

            // Draw branches off of this one.
            int numBranches = rand.Next(2, 6);
            for (int i = 0; i < numBranches; i++)
            {
                float scale = rand.Next(50, 75) / 100f;
                float newThickness = thickness * scale;
                float newLength = length * scale;

                // Don't draw if it's too short.
                if (newLength > 3)
                {
                    double newAngle = angle + rand.Next(-60, 61) * Math.PI / 180.0;
                    DrawTree(rand, gr, newThickness, newLength, x1, y1, newAngle);
                }
            }
        }

        // Randomize the seed and redraw.
        private void randomButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            Seed = rand.Next();
            seedTextBox.Text = Seed.ToString();
            Refresh();
        }
    }
}
