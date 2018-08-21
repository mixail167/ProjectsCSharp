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

namespace CircularQueue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Variables to manage the queue. (In a real application,
        // it would be better to wrap this with a class.)
        private string[] Queue = new string[8];
        private int Next = 0, Last = 0;

        // Draw the queue.
        private void queuePictureBox_Paint(object sender, PaintEventArgs e)
        {
            float radius1 = 90;
            float radius2 = 60;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Map to a more convenient coordinate system.
            RectangleF rect = new RectangleF(-100, -100, 200, 200);
            PointF[] pts =
            {
                new PointF(0, 0),
                new PointF(queuePictureBox.ClientSize.Width, 0),
                new PointF(0, queuePictureBox.ClientSize.Height),
            };
            Matrix transform = new Matrix(rect, pts);
            e.Graphics.Transform = transform;

            // Draw.
            using (Pen thin_pen = new Pen(Color.Black, 0))
            {
                // Draw the circles.
                e.Graphics.DrawEllipse(thin_pen, -radius1, -radius1, 2 * radius1, 2 * radius1);
                e.Graphics.DrawEllipse(thin_pen, -radius2, -radius2, 2 * radius2, 2 * radius2);

                // Draw the dividers.
                float theta = 0;
                float dtheta = (float)(2 * Math.PI / Queue.Length);
                for (int i = 0; i < Queue.Length; i++)
                {
                    float x1 = (float)(radius1 * Math.Cos(theta));
                    float y1 = (float)(radius1 * Math.Sin(theta));
                    float x2 = (float)(radius2 * Math.Cos(theta));
                    float y2 = (float)(radius2 * Math.Sin(theta));
                    e.Graphics.DrawLine(thin_pen, x1, y1, x2, y2);
                    theta += dtheta;
                }

                // Draw the letters.
                theta = dtheta / 2f;
                float radius3 = (radius1 + radius2) / 2f;
                using (Font font = new Font("Times New Roman", 14))
                {
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        for (int i = 0; i < Queue.Length; i++)
                        {
                            DrawRotatedText(e.Graphics, transform, Queue[i], theta, radius3, font, sf);

                            // Move to the next position.
                            theta += dtheta;
                        }

                        // Draw Next and Last.
                        sf.LineAlignment = StringAlignment.Near;
                        theta = dtheta / 2f + Next * dtheta;
                        DrawRotatedText(e.Graphics, transform, "Next", theta, radius2, font, sf);
                        theta = dtheta / 2f + Last * dtheta;
                        DrawRotatedText(e.Graphics, transform, "Last", theta, radius2, font, sf);
                    }
                }
            }
        }

        // Draw rotated text at the given position.
        private void DrawRotatedText(Graphics gr, Matrix transform, string text, float theta, float radius, Font font, StringFormat sf)
        {
            // Reset the transformation.
            gr.Transform = transform;

            // Prepend a translation to where the text belongs.
            float x = (float)(radius * Math.Cos(theta));
            float y = (float)(radius * Math.Sin(theta));
            gr.TranslateTransform(x, y, MatrixOrder.Prepend);

            // Prepend a rotation to the correct angle.
            float angle = (float)(theta / Math.PI * 180) + 90;
            gr.RotateTransform(angle, MatrixOrder.Prepend);

            // Draw the text.
            gr.DrawString(text, font, Brushes.Black, 0, 0, sf);
        }

        // Enqueue an item.
        private void Enqueue(string value)
        {
            // Make sure there's room to add an item.
            int new_next = (Next + 1) % Queue.Length;
            if (new_next == Last)
                throw new InvalidOperationException("Queue is full.");

            Queue[Next] = value;
            Next = new_next;
            queuePictureBox.Refresh();
        }

        // Dequeue an item.
        private string Dequeue()
        {
            // Make sure there's an item to remove.
            if (Next == Last)
                throw new InvalidOperationException("Queue is empty.");

            string result = Queue[Last];
            Queue[Last] = "";               // Remove old data to update the picture.
            Last = (Last + 1) % Queue.Length;
            queuePictureBox.Refresh();
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Enqueue("M");
            Enqueue("O");
            Enqueue("V");
            Enqueue("I");
            Enqueue("N");
            Enqueue("G");
        }

        // Add an item to the queue.
        private void enqueueButton_Click(object sender, EventArgs e)
        {
            try
            {
                Enqueue(itemTextBox.Text);
                itemTextBox.Clear();
                itemTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Remove an item from the queue.
        private void dequeueButton_Click(object sender, EventArgs e)
        {
            try
            {
                itemTextBox.Text = Dequeue();
                itemTextBox.Select(0, 0);
                itemTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
