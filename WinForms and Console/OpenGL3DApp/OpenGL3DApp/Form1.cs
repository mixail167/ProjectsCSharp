using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenGL3DApp
{
    public partial class Form1 : Form
    {
        private bool loaded;
        private int width;
        private Point3D[] points;
        private Point3D center;
        private OpenGL openGL;
        private float fontSize;
        private Texture[] texture;

        public Form1()
        {
            InitializeComponent();
            width = 50;
            fontSize = 12;
            Init();
        }

        private void Init()
        {
            points = new Point3D[8]{
                new Point3D(-width/2,-width/2, -width/2),
                new Point3D(-width/2,width/2,-width/2),
                new Point3D(width/2,width/2,-width/2),
                new Point3D(width/2,-width/2,-width/2),                
                new Point3D(width/2,-width/2,width/2),
                new Point3D(-width/2,-width/2, width/2),
                new Point3D(-width/2,width/2,width/2),
                new Point3D(width/2,width/2,width/2)
            };
            center = new Point3D(0, 0, 0);
        }

        void PaintScene()
        {
            openGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            openGL.Begin(OpenGL.GL_LINES);
            openGL.Color(1f, 1f, 1f);
            openGL.Vertex(0, -100000, 0);
            openGL.Vertex(0, 100000, 0);
            openGL.Vertex(-100000, 0, 0);
            openGL.Vertex(100000, 0, 0);
            openGL.Vertex(0, 0, -100000);
            openGL.Vertex(0, 0, 100000);
            openGL.End();

            texture[0].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(points[0].X + center.X, points[0].Y + center.Y, points[0].Z + center.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(points[1].X + center.X, points[1].Y + center.Y, points[1].Z + center.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(points[2].X + center.X, points[2].Y + center.Y, points[2].Z + center.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(points[3].X + center.X, points[3].Y + center.Y, points[3].Z + center.Z);
            openGL.End();

            texture[1].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(points[4].X + center.X, points[4].Y + center.Y, points[4].Z + center.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(points[5].X + center.X, points[5].Y + center.Y, points[5].Z + center.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(points[6].X + center.X, points[6].Y + center.Y, points[6].Z + center.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(points[7].X + center.X, points[7].Y + center.Y, points[7].Z + center.Z);
            openGL.End();

            texture[2].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(points[1].X + center.X, points[1].Y + center.Y, points[1].Z + center.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(points[2].X + center.X, points[2].Y + center.Y, points[2].Z + center.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(points[7].X + center.X, points[7].Y + center.Y, points[7].Z + center.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(points[6].X + center.X, points[6].Y + center.Y, points[6].Z + center.Z);
            openGL.End();

            texture[3].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(points[0].X + center.X, points[0].Y + center.Y, points[0].Z + center.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(points[3].X + center.X, points[3].Y + center.Y, points[3].Z + center.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(points[4].X + center.X, points[4].Y + center.Y, points[4].Z + center.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(points[5].X + center.X, points[5].Y + center.Y, points[5].Z + center.Z);
            openGL.End();

            texture[4].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(points[0].X + center.X, points[0].Y + center.Y, points[0].Z + center.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(points[1].X + center.X, points[1].Y + center.Y, points[1].Z + center.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(points[6].X + center.X, points[6].Y + center.Y, points[6].Z + center.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(points[5].X + center.X, points[5].Y + center.Y, points[5].Z + center.Z);
            openGL.End();

            texture[5].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(points[2].X + center.X, points[2].Y + center.Y, points[2].Z + center.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(points[3].X + center.X, points[3].Y + center.Y, points[3].Z + center.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(points[4].X + center.X, points[4].Y + center.Y, points[4].Z + center.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(points[7].X + center.X, points[7].Y + center.Y, points[7].Z + center.Z);
            openGL.End();

            openGL.DrawText(5, (int)(openGLControl1.Height - fontSize), 1, 1, 1, string.Empty, fontSize, "Info");
            for (int i = 0; i < points.Length; i++)
            {
                openGL.DrawText(5, (int)(openGLControl1.Height - (i + 2) * fontSize), 1, 1, 1, string.Empty, fontSize, GetInfo(points[i], i + 1, center));
            }
        }

        private string GetInfo(Point3D point, int index, Point3D center)
        {
            return string.Format("P{0}: x = {1:f2}, y = {2:f2}, z = {3:f2}\n", index, point.X + center.X, point.Y + center.Y, point.Z + center.Z);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            openGL = openGLControl1.OpenGL;
            openGL.Enable(OpenGL.GL_TEXTURE_2D);
            texture = new Texture[6] { new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), };
            texture[0].Create(openGL, Properties.Resources.BMW);
            texture[1].Create(openGL, Properties.Resources.WOT);
            texture[2].Create(openGL, Properties.Resources.tiger);
            texture[3].Create(openGL, Properties.Resources.kulak);
            texture[4].Create(openGL, Properties.Resources.barselon);
            texture[5].Create(openGL, Properties.Resources.VSRB);
            openGL.Enable(OpenGL.GL_DEPTH_TEST);
            openGL.DepthMask((byte)OpenGL.GL_TRUE);
        }

        private void glControl1_SizeChanged(object sender, EventArgs e)
        {
            openGL.LoadIdentity();
            openGL.Perspective(60.0f, (double)openGLControl1.Width / (double)openGLControl1.Height, 0.01, double.MaxValue);
            openGL.LookAt(300, 300, 300,
                        0, 0, 0,
                        0, 1, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                center = Translate3D(center, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
            }
            else if (radioButton2.Checked)
            {
                points = Rotate3D(points, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
            }
            else
            {
                points = Scale3D(points, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
            }
            PaintScene();
        }

        private Point3D[] Scale3D(Point3D[] points, double sx, double sy, double sz)
        {
            double[,] matrix = new double[4, 4] { { sx, 0, 0, 0 }, { 0, sy, 0, 0 }, { 0, 0, sz, 0 }, { 0, 0, 0, 1 } };
            for (int i = 0; i < points.Length; i++)
            {
                double[,] vector = GetVector3D(points[i]);
                vector = Multiplication(vector, matrix);
                points[i] = GetPoint3D(vector);
            }
            return points;
        }

        private Point3D[] Rotate3D(Point3D[] points, double alphaX, double alphaY, double alphaZ)
        {
            alphaX *= Math.PI / 180;
            alphaY *= Math.PI / 180;
            alphaZ *= Math.PI / 180;
            double[,] matrixZ = new double[4, 4] { { Math.Cos(alphaZ), Math.Sin(alphaZ), 0, 0 }, { -Math.Sin(alphaZ), Math.Cos(alphaZ), 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            double[,] matrixX = new double[4, 4] { { 1, 0, 0, 0 }, { 0, Math.Cos(alphaX), Math.Sin(alphaX), 0 }, { 0, -Math.Sin(alphaX), Math.Cos(alphaX), 0 }, { 0, 0, 0, 1 } };
            double[,] matrixY = new double[4, 4] { { Math.Cos(alphaY), 0, -Math.Sin(alphaY), 0 }, { 0, 1, 0, 0 }, { Math.Sin(alphaY), 0, Math.Cos(alphaY), 0 }, { 0, 0, 0, 1 } };
            for (int i = 0; i < points.Length; i++)
            {
                double[,] vector = GetVector3D(points[i]);
                vector = Multiplication(vector, matrixZ);
                vector = Multiplication(vector, matrixX);
                vector = Multiplication(vector, matrixY);
                points[i] = GetPoint3D(vector);
            }
            return points;
        }

        private Point3D Translate3D(Point3D point, double dx, double dy, double dz)
        {
            double[,] matrix = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { dx, dy, dz, 1 } };
            double[,] vector = GetVector3D(point);
            vector = Multiplication(vector, matrix);
            point = GetPoint3D(vector);
            return point;
        }

        private double[,] GetVector3D(Point3D point)
        {
            return new double[1, 4] { { point.X, point.Y, point.Z, 1 } };
        }

        private Point3D GetPoint3D(double[,] vector)
        {
            return new Point3D((float)vector[0, 0], (float)vector[0, 1], (float)vector[0, 2]);
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
            PaintScene();
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            PaintScene();
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                center = Translate3D(center, 0, 5, 0);
                PaintScene();
            }
            else if (e.KeyCode == Keys.Down)
            {
                center = Translate3D(center, 0, -5, 0);
                PaintScene();
            }
            else if (e.KeyCode == Keys.Left)
            {
                center = Translate3D(center, -5, 0, 0);
                PaintScene();
            }
            else if (e.KeyCode == Keys.Right)
            {
                center = Translate3D(center, 5, 0, 0);
                PaintScene();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                center = Translate3D(center, 0, 0, 5);
                PaintScene();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                center = Translate3D(center, 0, 0, -5);
                PaintScene();
            }
            else if (e.KeyCode == Keys.Space)
            {
                points = Rotate3D(points, 60, 60, 60);
                PaintScene();
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
                points = Scale3D(points, 1.5, 1.5, 1.5);
            }
            else
            {
                points = Scale3D(points, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
            }
            PaintScene();
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
    }
}
