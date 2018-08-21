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

namespace NonrecursiveHilbert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(
                hilbertPictureBox.ClientSize.Width,
                hilbertPictureBox.ClientSize.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.Clear(Color.White);

                const int margin = 10;
                int depth = (int)depthNumericUpDown.Value;
                float dx = (float)((bm.Width - 2 * margin) / (Math.Pow(2, depth + 1) - 1));
                CurrentX = margin;
                CurrentY = margin;
                Hilbert(depth, gr, dx, 0);
            }

            hilbertPictureBox.Image = bm;
        }

        // The current position.
        private float CurrentX, CurrentY;

        // Draw the Hilbert curve.
        private void Hilbert(int depth, Graphics gr, float dx, float dy)
        {
            // Make stacks to store information before recursion.
            Stack<int> sections = new Stack<int>();
            Stack<int> depths = new Stack<int>();
            Stack<float> dxs = new Stack<float>();
            Stack<float> dys = new Stack<float>();

            // Determines which section of code to execute next.
            int section = 1;

            while (section > 0)
            {
                if (section == 1)
                {
                    section++;
                    if (depth > 0)
                    {
                        sections.Push(section);
                        depths.Push(depth);
                        dxs.Push(dx);
                        dys.Push(dy);
                        // Hilbert(depth - 1, gr, dy, dx);
                        depth--;
                        float temp = dx;
                        dx = dy;
                        dy = temp;
                        section = 1;
                    }
                }
                else if (section == 2)
                {
                    DrawRelative(gr, dx, dy);
                    section++;
                    if (depth > 0)
                    {
                        sections.Push(section);
                        depths.Push(depth);
                        dxs.Push(dx);
                        dys.Push(dy);
                        // Hilbert(depth - 1, gr, dx, dy);
                        depth--;
                        section = 1;
                    }
                }
                else if (section == 3)
                {
                    DrawRelative(gr, dy, dx);
                    section++;
                    if (depth > 0)
                    {
                        sections.Push(section);
                        depths.Push(depth);
                        dxs.Push(dx);
                        dys.Push(dy);
                        // Hilbert(depth - 1, gr, dx, dy);
                        depth--;
                        section = 1;
                    }
                }
                else if (section == 4)
                {
                    DrawRelative(gr, -dx, -dy);
                    section++;
                    if (depth > 0)
                    {
                        sections.Push(section);
                        depths.Push(depth);
                        dxs.Push(dx);
                        dys.Push(dy);
                        // Hilbert(depth - 1, gr, -dy, -dx);
                        depth--;
                        float temp = dx;
                        dx = -dy;
                        dy = -temp;
                        section = 1;
                    }
                }
                else if (section == 5)
                {
                    // Return from a recursion.
                    // If there's nothing to pop, we're at the top.
                    if (sections.Count == 0) section = -1;
                    else
                    {
                        // Pop the previous parameters.
                        section = sections.Pop();
                        depth = depths.Pop();
                        dx = dxs.Pop();
                        dy = dys.Pop();
                    }
                }
            }
        }

        // Draw starting at the indicated point and update x and y.
        private void DrawRelative(Graphics gr, float dx, float dy)
        {
            gr.DrawLine(Pens.Blue,
                CurrentX, CurrentY,
                CurrentX + dx, CurrentY + dy);
            CurrentX += dx;
            CurrentY += dy;
        }
    }
}
