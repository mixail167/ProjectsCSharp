using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenGl2DApp
{
    public partial class Form1 : Form
    {
        private bool loaded;
        private Color currentColor;
        private int width;
        private PointF[] points;
        private PointF center;
        private OpenGL openGL;
        private float fontSize;
        private Texture texture;
        private int count;
        private int k;

        public Form1()
        {
            InitializeComponent();
            width = 50;
            fontSize = 12;
            texture = CreateTextureFromGif(openGL, Properties.Resources.gif4, out count);
            texture.Bind(openGL);
            k = 0;
            Init();
        }

        private Texture CreateTextureFromGif(OpenGL openGL, Image image, out int count)
        {
            Texture texture = new Texture();
            FrameDimension d = new FrameDimension(image.FrameDimensionsList[0]);
            count = image.GetFrameCount(d);
            Bitmap imageTexture = new Bitmap(image.Width * count, image.Height);
            Graphics g = Graphics.FromImage(imageTexture);
            for (int i = 0; i < count; i++)
            {
                image.SelectActiveFrame(d, i);
                g.DrawImage(new Bitmap(image), i * image.Width, 0);
            }
            texture.Create(openGL, imageTexture);
            return texture;
        }

        private void Init()
        {
            points = new PointF[4]{
                new PointF(-width/2,-width/2),
                new PointF(-width/2,width/2),
                new PointF(width/2,width/2),
                new PointF(width/2,-width/2)
            };
            center = new PointF(0, 0);
            currentColor = Color.Red;
        }

        void PaintScene()
        {
            openGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            openGL.Begin(OpenGL.GL_LINES);
            openGL.Color(1f, 1f, 1f);
            openGL.Vertex(0, -openGLControl1.Width / 2);
            openGL.Vertex(0, openGLControl1.Width / 2);
            openGL.Vertex(-openGLControl1.Width / 2, 0);
            openGL.Vertex(openGLControl1.Width / 2, 0);
            openGL.End();
            openGL.Enable(OpenGL.GL_TEXTURE_2D);
            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(k * 1.0 / count, 1.0); openGL.Vertex(points[0].X + center.X, points[0].Y + center.Y);
            openGL.TexCoord(k * 1.0 / count, 0.0); openGL.Vertex(points[1].X + center.X, points[1].Y + center.Y);
            openGL.TexCoord((k + 1) * 1.0 / count, 0.0); openGL.Vertex(points[2].X + center.X, points[2].Y + center.Y);
            openGL.TexCoord((k + 1) * 1.0 / count, 1.0); openGL.Vertex(points[3].X + center.X, points[3].Y + center.Y);
            openGL.End();
            openGL.Disable(OpenGL.GL_TEXTURE_2D);
            openGL.DrawText(5, (int)(openGLControl1.Height - fontSize), 1, 1, 1, string.Empty, fontSize, "Info");
            for (int i = 0; i < points.Length; i++)
            {
                openGL.DrawText(5, (int)(openGLControl1.Height - (i + 2) * fontSize), 1, 1, 1, string.Empty, fontSize, GetInfo(points[i], i + 1, center));
            }
        }

        private string GetInfo(PointF point, int index, PointF center)
        {
            return string.Format("P{0}: x = {1:f2}, y = {2:f2}\n", index, point.X + center.X, point.Y + center.Y);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            openGL = openGLControl1.OpenGL;
            openGL.Enable(OpenGL.GL_DEPTH_TEST);
            openGL.DepthMask(1);
            openGL.DepthFunc(OpenGL.GL_LEQUAL);
            timer1.Enabled = loaded;
        }

        private void glControl1_SizeChanged(object sender, EventArgs e)
        {
            openGL.MatrixMode(OpenGL.GL_PROJECTION);
            openGL.LoadIdentity();
            openGL.Ortho2D(-openGLControl1.Width / 2, openGLControl1.Width / 2, -openGLControl1.Height / 2, openGLControl1.Height / 2);
            openGL.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                center = Translate2D(center, (double)numericUpDown1.Value, (double)numericUpDown2.Value);
            }
            else if (radioButton2.Checked)
            {
                points = Rotate2D(points, (double)numericUpDown4.Value);
            }
            else
            {
                points = Scale2D(points, (double)numericUpDown5.Value, (double)numericUpDown6.Value);
            }
        }

        private PointF[] Scale2D(PointF[] points, double sx, double sy)
        {
            double[,] matrix = new double[3, 3] { { sx, 0, 0 }, { 0, sy, 0 }, { 0, 0, 1 } };
            for (int i = 0; i < points.Length; i++)
            {
                double[,] vector = GetVector2D(points[i]);
                vector = Multiplication(vector, matrix);
                points[i] = GetPointF(vector);
            }
            return points;
        }

        private PointF[] Rotate2D(PointF[] points, double alpha)
        {
            alpha *= Math.PI / 180;
            double[,] matrix = new double[3, 3] { { Math.Cos(alpha), Math.Sin(alpha), 0 }, { -Math.Sin(alpha), Math.Cos(alpha), 0 }, { 0, 0, 1 } };
            for (int i = 0; i < points.Length; i++)
            {
                double[,] vector = GetVector2D(points[i]);
                vector = Multiplication(vector, matrix);
                points[i] = GetPointF(vector);
            }
            return points;
        }

        private PointF Translate2D(PointF point, double dx, double dy)
        {
            double[,] matrix = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { dx, dy, 1 } };
            double[,] vector = GetVector2D(point);
            vector = Multiplication(vector, matrix);
            point = GetPointF(vector);
            return point;
        }

        private double[,] GetVector2D(PointF point)
        {
            return new double[1, 3] { { point.X, point.Y, 1 } };
        }

        private PointF GetPointF(double[,] vector)
        {
            return new PointF((float)vector[0, 0], (float)vector[0, 1]);
        }

        private double[,] Multiplication(double[,] vector, double[,] matrix)
        {
            if (vector.GetLength(1) != matrix.GetLength(0))
                throw new Exception();
            double[,] result = new double[vector.GetLength(0), vector.GetLength(1)];
            alglib.rmatrixgemm(vector.GetLength(0), matrix.GetLength(1), vector.GetLength(1), 1, vector, 0, 0, 0, matrix, 0, 0, 0, 0, ref result, 0, 0);
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            PaintScene();
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                center = Translate2D(center, 0, 5);
            }
            else if (e.KeyCode == Keys.Down)
            {
                center = Translate2D(center, 0, -5);
            }
            else if (e.KeyCode == Keys.Left)
            {
                center = Translate2D(center, -5, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                center = Translate2D(center, 5, 0);
            }
            else if (e.KeyCode == Keys.Space)
            {
                points = Rotate2D(points, 60);
            }
        }

        private void openGLControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void openGLControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                points = Scale2D(points, 1.5, 1.5);
            }
            else
            {
                points = Scale2D(points, 2.0 / 3.0, 2.0 / 3.0);
            }
        }

        private void openGLControl1_EnterLeave(object sender, EventArgs e)
        {
            tableLayoutPanel1.Invalidate();
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (loaded && e.Column == 1)
            {
                if (tableLayoutPanel1.GetControlFromPosition(1, 0).Focused)
                {
                    e.Graphics.FillRectangle(Brushes.Red, e.CellBounds);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (k == count - 1)
                k = 0;
            else
                k++;
        }
    }
}
