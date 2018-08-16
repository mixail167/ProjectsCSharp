using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClockWidget
{
    public partial class Form1 : Form
    {
        private OpenGL openGL;
        private bool isMouseDown;
        private Point mouseCoordinates;
        private PointF[] pointsExternalCircle;
        private PointF[] pointsInternalCircle;
        private PointF[] pointsBigInterval;
        private PointF[] pointsSmallInterval;
        private PointF[] pointsInterval;
        private PointF[] pointsHour;
        private PointF[] pointsMinute;
        private PointF[] pointsSecond;
        private PointF[] pointsCenterCircle;
        private Texture texture;

        public Form1()
        {
            InitializeComponent();
            Point location = Properties.Settings.Default.Location;
            if (Screen.PrimaryScreen.Bounds.Size.Width - this.Width < Properties.Settings.Default.Location.X)
            {
                location = new Point(Screen.PrimaryScreen.Bounds.Size.Width - this.Width, location.Y);
            }
            if (Screen.PrimaryScreen.Bounds.Size.Height - this.Height < Properties.Settings.Default.Location.Y)
            {
                location = new Point(location.X, Screen.PrimaryScreen.Bounds.Size.Height - this.Height);
            }
            Properties.Settings.Default.Location = location;
            Properties.Settings.Default.Save();
            this.Location = location;
            pointsInternalCircle = DrawCircule(0, 0, 80, 0, 360, 5);
            pointsExternalCircle = DrawCircule(0, 0, 100, 0, 360, 5);
            pointsCenterCircle = DrawCircule(0, 0, 5, 0, 360, 5);
            pointsBigInterval = new PointF[4]{
                new PointF(-2,65),
                new PointF(-2,75),
                new PointF(2,75),
                new PointF(2,65)
            };
            pointsSmallInterval = new PointF[4]{
                new PointF(-1,65),
                new PointF(-1,75),
                new PointF(1,75),
                new PointF(1,65)
            };
            pointsHour = new PointF[4]{
                new PointF(0,-15),
                new PointF(-7,0),
                new PointF(0,40),
                new PointF(7,0)
            };
            pointsMinute = new PointF[4]{
                new PointF(0,-20),
                new PointF(-5,0),
                new PointF(0,60),
                new PointF(5,0)
            };
            pointsSecond = new PointF[4]{
                new PointF(-1,-30),
                new PointF(-1,63),
                new PointF(1,63),
                new PointF(1,-30)
            };
            pointsInterval = new PointF[60 * 4];
            double alpha = 0;
            for (int i = 0; i < pointsInterval.Length; i += 4, alpha += 6)
            {
                if (alpha % 30 == 0)
                {
                    pointsInterval[i] = Rotate2D(pointsBigInterval[0], alpha);
                    pointsInterval[i + 1] = Rotate2D(pointsBigInterval[1], alpha);
                    pointsInterval[i + 2] = Rotate2D(pointsBigInterval[2], alpha);
                    pointsInterval[i + 3] = Rotate2D(pointsBigInterval[3], alpha);
                }
                else
                {
                    pointsInterval[i] = Rotate2D(pointsSmallInterval[0], alpha);
                    pointsInterval[i + 1] = Rotate2D(pointsSmallInterval[1], alpha);
                    pointsInterval[i + 2] = Rotate2D(pointsSmallInterval[2], alpha);
                    pointsInterval[i + 3] = Rotate2D(pointsSmallInterval[3], alpha);
                }
            }
            texture = new Texture();
            texture.Create(openGL, Properties.Resources.clock4);
            texture.Bind(openGL);
        }

        private static PointF[] DrawCircule(float xCentre, float yCentre, float radius, int angleStart, int angleFinish, int step)
        {
            List<PointF> points = new List<PointF>();
            for (int angle = angleStart; angle <= angleFinish; angle += step)
            {
                float x = (float)(radius * Math.Cos(angle * Math.PI / 180));
                float y = (float)(radius * Math.Sin(angle * Math.PI / 180));
                points.Add(new PointF(x + xCentre, y + yCentre));
            }
            return points.ToArray();
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            openGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            openGL.ClearColor(1f, 1f, 1f, 1f);

            openGL.Color(255 / 255f, 215 / 255f, 0 / 255f);
            openGL.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < pointsInternalCircle.Length; i++)
            {
                openGL.Vertex(pointsInternalCircle[i].X, pointsInternalCircle[i].Y);
            }
            openGL.End();

            openGL.Enable(OpenGL.GL_TEXTURE_2D);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(-65, 65);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(65, 65);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(65, -65);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(-65, -65);
            openGL.End();

            openGL.Disable(OpenGL.GL_TEXTURE_2D);

            openGL.Color(0f, 0f, 0f);
            openGL.Begin(OpenGL.GL_QUADS);
            for (int i = 0; i < pointsInterval.Length; i++)
            {
                openGL.Vertex(pointsInterval[i].X, pointsInterval[i].Y);
            }
            openGL.End();

            DateTime time = DateTime.Now;
            int hour = time.Hour;
            if (hour > 12)
            {
                hour -= 12;
            }

            openGL.Color(0f, 0f, 1f);
            openGL.Begin(OpenGL.GL_QUADS);
            for (int i = 0; i < pointsHour.Length; i++)
            {
                PointF temp = Rotate2D(pointsHour[i], -hour * 30 - time.Minute * 1.0f / 2);
                openGL.Vertex(temp.X, temp.Y);
            }
            openGL.End();

            openGL.Color(0f, 1f, 0f);
            openGL.Begin(OpenGL.GL_QUADS);
            for (int i = 0; i < pointsMinute.Length; i++)
            {
                PointF temp = Rotate2D(pointsMinute[i], -time.Minute * 6 - time.Second * 1.0f / 12);
                openGL.Vertex(temp.X, temp.Y);
            }
            openGL.End();

            openGL.Color(1f, 0f, 0f);
            openGL.Begin(OpenGL.GL_QUADS);
            for (int i = 0; i < pointsSecond.Length; i++)
            {
                PointF temp = Rotate2D(pointsSecond[i], -time.Second * 6);
                openGL.Vertex(temp.X, temp.Y);
            }
            openGL.End();

            openGL.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < pointsCenterCircle.Length; i++)
            {
                openGL.Vertex(pointsCenterCircle[i].X, pointsCenterCircle[i].Y);
            }
            openGL.End();

            openGL.Color(192 / 255f, 192 / 255f, 192 / 255f);
            openGL.Begin(OpenGL.GL_QUADS);
            for (int i = 0; i < pointsInternalCircle.Length - 1; i++)
            {
                openGL.Vertex(pointsInternalCircle[i].X, pointsInternalCircle[i].Y);
                openGL.Vertex(pointsExternalCircle[i].X, pointsExternalCircle[i].Y);
                openGL.Vertex(pointsExternalCircle[i + 1].X, pointsExternalCircle[i + 1].Y);
                openGL.Vertex(pointsInternalCircle[i + 1].X, pointsInternalCircle[i + 1].Y);
            }
            openGL.End();

            openGL.DrawText(-25 + 100, -30 + 100, 1.0f, 20 / 255f, 147 / 255f, string.Empty, 12, time.ToString("HH:mm:ss"));

            openGL.Flush();
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            openGL = openGLControl1.OpenGL;
            openGL.Enable(OpenGL.GL_DEPTH_TEST);
            openGL.DepthMask(1);
            openGL.DepthFunc(OpenGL.GL_LEQUAL);
        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            openGL.MatrixMode(OpenGL.GL_PROJECTION);
            openGL.LoadIdentity();
            openGL.Ortho2D(-openGLControl1.Width / 2, openGLControl1.Width / 2, -openGLControl1.Height / 2, openGLControl1.Height / 2);
            openGL.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);
        }

        private PointF Rotate2D(PointF points, double alpha)
        {
            alpha *= Math.PI / 180;
            double[,] matrix = new double[3, 3] { { Math.Cos(alpha), Math.Sin(alpha), 0 }, { -Math.Sin(alpha), Math.Cos(alpha), 0 }, { 0, 0, 1 } };
            double[,] vector = GetVector2D(points);
            vector = Multiplication(vector, matrix);
            return GetPointF(vector); ;
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

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mouseCoordinates = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (e.X > mouseCoordinates.X)
                {
                    this.Location = new Point(this.Location.X + 1, this.Location.Y);
                }
                else if (e.X < mouseCoordinates.X)
                {
                    this.Location = new Point(this.Location.X - 1, this.Location.Y);
                }
                if (e.Y > mouseCoordinates.Y)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y + 1);
                }
                else if (e.Y < mouseCoordinates.Y)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y - 1);
                }
            }
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
