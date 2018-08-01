using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenGLApp
{
    public partial class Form1 : Form
    {
        private bool loaded;
        private Color currentColor;
        private int width;
        private PointF[] points;
        private PointF center;

        public Form1()
        {
            InitializeComponent();
            width = 50;
            Init();
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
            panel1.BackColor = currentColor;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = currentColor;
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog1.Color;
                panel1.BackColor = currentColor;
                PaintScene();
            }
        }

        void PaintScene()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Color3(Color.White);
            GL.Begin(BeginMode.Lines);
            GL.Vertex2(0, -glControl1.Height / 2);
            GL.Vertex2(0, glControl1.Height / 2);
            GL.Vertex2(-glControl1.Width / 2, 0);
            GL.Vertex2(glControl1.Width / 2, 0);
            GL.End();
            GL.Color3(currentColor);
            GL.Begin(BeginMode.Polygon);
            GL.Vertex2(points[0].X + center.X, points[0].Y + center.Y);
            GL.Vertex2(points[1].X + center.X, points[1].Y + center.Y);
            GL.Vertex2(points[2].X + center.X, points[2].Y + center.Y);
            GL.Vertex2(points[3].X + center.X, points[3].Y + center.Y);
            GL.End(); 
            glControl1.SwapBuffers();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-glControl1.Width / 2, glControl1.Width / 2, -glControl1.Height / 2, glControl1.Height / 2, double.MinValue, double.MaxValue);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                PaintScene();
            }
        }

        private void glControl1_SizeChanged(object sender, EventArgs e)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-glControl1.Width / 2, glControl1.Width / 2, -glControl1.Height / 2, glControl1.Height / 2, -100, 100);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
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
            PaintScene();
        }

        private PointF[] Scale2D(PointF[] points, double sx, double sy)
        {
            double[,] matrix = new double[3, 3] { { sx, 0, 0 }, { 0, sy, 0 }, { 0, 0, 1 } };
            for (int i = 0; i < points.Length; i++)
            {
                double[,] vector = GetVector2D(points[i]);
                double[,] result = Multiplication(vector, matrix);
                points[i].X = (float)result[0, 0];
                points[i].Y = (float)result[0, 1];
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
                double[,] result = Multiplication(vector, matrix);
                points[i].X = (float)result[0, 0];
                points[i].Y = (float)result[0, 1];
            }
            return points;
        }

        private PointF Translate2D(PointF point, double dx, double dy)
        {
            double[,] matrix = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { dx, dy, 1 } };
            double[,] vector = GetVector2D(point);
            double[,] result = Multiplication(vector, matrix);
            point.X = (float)result[0, 0];
            point.Y = (float)result[0, 1];
            return point;
        }

        private double[,] GetVector2D(PointF point)
        {
            return new double[1, 3] { { point.X, point.Y, 1 } };
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
    }
}
