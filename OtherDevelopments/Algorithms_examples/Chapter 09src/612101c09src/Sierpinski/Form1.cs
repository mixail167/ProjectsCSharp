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

namespace Sierpinski
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
                sierpinskiPictureBox.ClientSize.Width,
                sierpinskiPictureBox.ClientSize.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.Clear(Color.White);

                const int margin = 10;
                int depth = (int)depthNumericUpDown.Value;
                float dx = (float)((bm.Width - 2 * margin) / (Math.Pow(2, depth + 2) - 2));
                float dy = (float)((bm.Height - 2 * margin) / (Math.Pow(2, depth + 2) - 2));
                CurrentX = margin + dx;
                CurrentY = margin;
                Sierpinski(depth, gr, dx, dy);

                // Draw a box around it. (For debugging.)
                //gr.DrawRectangle(Pens.Red,
                //    margin, margin,
                //    bm.Width - 2 * margin, bm.Height - 2 * margin);
            }

            sierpinskiPictureBox.Image = bm;
        }

        // The current position.
        private float CurrentX, CurrentY;

        // Draw a Sierpinski curve.
        private void Sierpinski(int depth, Graphics gr, float dx, float dy)
        {
            SierpRight(depth, gr, dx, dy);
            DrawRelative(gr, dx, dy);
            SierpDown(depth, gr, dx, dy);
            DrawRelative(gr, -dx, dy);
            SierpLeft(depth, gr, dx, dy);
            DrawRelative(gr, -dx, -dy);
            SierpUp(depth, gr, dx, dy);
            DrawRelative(gr, dx, -dy);
        }

        // Draw right across the top.
        private void SierpRight(int depth, Graphics gr, float dx, float dy)
        {
            if (depth > 0)
            {
                depth--;

                SierpRight(depth, gr, dx, dy);
                DrawRelative(gr, dx, dy);
                SierpDown(depth, gr, dx, dy);
                DrawRelative(gr, 2 * dx, 0);
                SierpUp(depth, gr, dx, dy);
                DrawRelative(gr, dx, -dy);
                SierpRight(depth, gr, dx, dy);
            }
        }

        // Draw down on the right.
        private void SierpDown(int depth, Graphics gr, float dx, float dy)
        {
            if (depth > 0)
            {
                depth--;
                SierpDown(depth, gr, dx, dy);
                DrawRelative(gr, -dx, dy);
                SierpLeft(depth, gr, dx, dy);
                DrawRelative(gr, 0, 2 * dy);
                SierpRight(depth, gr, dx, dy);
                DrawRelative(gr, dx, dy);
                SierpDown(depth, gr, dx, dy);
            }
        }

        // Draw left across the bottom.
        private void SierpLeft(int depth, Graphics gr, float dx, float dy)
        {
            if (depth > 0)
            {
                depth--;
                SierpLeft(depth, gr, dx, dy);
                DrawRelative(gr, -dx, -dy);
                SierpUp(depth, gr, dx, dy);
                DrawRelative(gr, -2 * dx, 0);
                SierpDown(depth, gr, dx, dy);
                DrawRelative(gr, -dx, dy);
                SierpLeft(depth, gr, dx, dy);
            }
        }

        // Draw up along the left.
        private void SierpUp(int depth, Graphics gr, float dx, float dy)
        {
            if (depth > 0)
            {
                depth--;
                SierpUp(depth, gr, dx, dy);
                DrawRelative(gr, dx, -dy);
                SierpRight(depth, gr, dx, dy);
                DrawRelative(gr, 0, -2 * dy);
                SierpLeft(depth, gr, dx, dy);
                DrawRelative(gr, -dx, -dy);
                SierpUp(depth, gr, dx, dy);
            }
        }

        // Draw a line between (CurrentX, CurrentY) and (CurrentX + dx, CurrentY + dy).
        // Then update CurrentX and CurrentY.
        private void DrawRelative(Graphics gr, float dx, float dy)
        {
            gr.DrawLine(Pens.Black,
                CurrentX, CurrentY,
                CurrentX + dx, CurrentY + dy);
            CurrentX += dx;
            CurrentY += dy;
        }
    }
}
