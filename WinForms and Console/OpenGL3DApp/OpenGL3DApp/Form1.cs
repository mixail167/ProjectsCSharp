using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenGL3DApp
{
    public partial class Form1 : Form
    {
        private bool loaded;
        private double _widthCube;
        private double widthHeart;
        private double widthCylinder;
        private double widthTetraedron;
        private double widthTriangle;
        private Point3D[] pointsCube;
        private Point3D centerCube;
        private Point3D centerSphere;
        private OpenGL openGL;
        private float fontSize;
        private Texture[] textures;
        private double radiusSphere;
        private double radiusCylinder;
        private double intRadiusTorus;
        private double extRadiusTorus;
        private Point3DUV[] pointsSphere;
        private Point3D[] pointsHeart;
        private Point3D[] pointsCylinder;
        private Point3D[] pointsTorus;
        private Point3D[] pointsSerpinski;
        private Point3D[] pointsKox;
        private Point3D centerHeart;
        private Point3D centerCylinder;
        private Point3D centerSerpinski;
        private Point3D centerKox;
        private Point3D cameraPosition;
        private Point3D cameraView;
        private Point3D centerTorus;
        private int countIterations;

        public Form1()
        {
            InitializeComponent();
            _widthCube = 50;
            widthHeart = 50;
            fontSize = 12;
            radiusSphere = 25;
            radiusCylinder = 25;
            widthCylinder = 50;
            intRadiusTorus = 5;
            extRadiusTorus = 25;
            widthTetraedron = 50;
            countIterations = 5;
            widthTriangle = 50;
            Init();
        }

        private void Init()
        {
            centerSerpinski = new Point3D(-200, -200, 0);
            centerKox = new Point3D(0, 0, 0);
            centerTorus = new Point3D(-200, 0, 0);
            centerCube = new Point3D(200, 0, 0);
            centerSphere = new Point3D(200, 200, 0);
            centerHeart = new Point3D(0, 200, 0);
            centerCylinder = new Point3D(0, -200, 0);
            cameraPosition = new Point3D(0, 0, 500);
            cameraView = new Point3D(0, 0, -500);
            pointsCube = new Point3D[8]{
                new Point3D(-_widthCube/2,-_widthCube/2, -_widthCube/2),
                new Point3D(-_widthCube/2,_widthCube/2,-_widthCube/2),
                new Point3D(_widthCube/2,_widthCube/2,-_widthCube/2),
                new Point3D(_widthCube/2,-_widthCube/2,-_widthCube/2),                
                new Point3D(_widthCube/2,-_widthCube/2,_widthCube/2),
                new Point3D(-_widthCube/2,-_widthCube/2, _widthCube/2),
                new Point3D(-_widthCube/2,_widthCube/2,_widthCube/2),
                new Point3D(_widthCube/2,_widthCube/2,_widthCube/2)
            };
            pointsSphere = Rotate3D(CreateSphere(radiusSphere), 270, 0, 0);
            pointsCylinder = Rotate3D(CreateCylinder(radiusCylinder, widthCylinder, 10), 90, 0, 0);
            pointsHeart = CreateHeart(widthHeart);
            pointsSerpinski = CreateSerpinski(widthTetraedron, countIterations);
            pointsKox = CreateKox(widthTriangle, 8);
            pointsTorus = CreateTorus(intRadiusTorus, extRadiusTorus/*, 0, 0, 360, 1800, 10, 10, 5, 1*/);
            SetCamera(cameraPosition, cameraView);
        }

        private List<Point3D> Fractal(Point3D p1, Point3D p2, Point3D p3, int countIterations, List<Point3D> points)
        {
            if (countIterations > 0)
            {
                Point3D p4 = new Point3D((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3, 0);
                Point3D p5 = new Point3D((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3, 0);
                Point3D ps = new Point3D((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2, 0);
                Point3D pn = new Point3D((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3, 0);
                points.Add(p4);
                points.Add(pn);
                points.Add(p5);
                points = Fractal(p4, pn, p5, countIterations - 1, points);
                points = Fractal(pn, p5, p4, countIterations - 1, points);
                points = Fractal(p1, p4, new Point3D((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3, 0), countIterations - 1, points);
                points = Fractal(p5, p2, new Point3D((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3, 0), countIterations - 1, points);

            }
            return points;
        }

        private Point3D[] CreateKox(double width, int countIterations)
        {
            List<Point3D> points = new List<Point3D>();
            double h1 = width / 2 * Math.Sqrt(3);
            points.Add(new Point3D(0, (h1 * 2) / 3, 0));
            points.Add(new Point3D(-width / 2, -h1 / 3, 0));
            points.Add(new Point3D(width / 2, -h1 / 3, 0));
            points = Fractal(points[0], points[1], points[2], countIterations, points);
            points = Fractal(points[1], points[2], points[0], countIterations, points);
            points = Fractal(points[2], points[0], points[1], countIterations, points);
            return points.ToArray();
        }

        private Point3D[] CreateSerpinski(double width, int countIterations)
        {
            Point3D[] startPoints = new Point3D[4];
            double h1 = width / 2 * Math.Sqrt(3);
            double h2 = width * Math.Sqrt(2.0 / 3.0);
            startPoints[0] = new Point3D(0, 0, h2 / 2);
            startPoints[1] = new Point3D(0, (h1 * 2) / 3, -h2 / 2);
            startPoints[2] = new Point3D(-width / 2, -h1 / 3, -h2 / 2);
            startPoints[3] = new Point3D(width / 2, -h1 / 3, -h2 / 2);
            List<Point3D> points = DivideTriangle(startPoints[0], startPoints[1], startPoints[2], countIterations, new List<Point3D>());
            points = DivideTriangle(startPoints[3], startPoints[2], startPoints[1], countIterations, points);
            points = DivideTriangle(startPoints[0], startPoints[3], startPoints[1], countIterations, points);
            points = DivideTriangle(startPoints[0], startPoints[2], startPoints[3], countIterations, points);
            return points.ToArray();
        }

        private List<Point3D> DivideTriangle(Point3D a, Point3D b, Point3D c, int countIterations, List<Point3D> points)
        {
            if (countIterations > 0)
            {
                Point3D v1 = new Point3D((a.X + b.X) / 2, (a.Y + b.Y) / 2, (a.Z + b.Z) / 2);
                Point3D v2 = new Point3D((a.X + c.X) / 2, (a.Y + c.Y) / 2, (a.Z + c.Z) / 2);
                Point3D v3 = new Point3D((b.X + c.X) / 2, (b.Y + c.Y) / 2, (b.Z + c.Z) / 2);
                points = DivideTriangle(a, v1, v2, countIterations - 1, points);
                points = DivideTriangle(c, v2, v3, countIterations - 1, points);
                points = DivideTriangle(b, v3, v1, countIterations - 1, points);
            }
            else
            {
                points.Add(a);
                points.Add(b);
                points.Add(c);
            }
            return points;
        }

        //private List<Point3D> DivideQuads(Point3D a, Point3D b, Point3D c, Point3D d, double widthCube, int countIterations, List<Point3D> points)
        //{
        //    if (countIterations > 0)
        //    {
        //        widthCube /= 3;
        //        Point3D v1 = new Point3D(a.X + widthCube, a.Y, a.Z);
        //        Point3D v2 = new Point3D(a.X + widthCube * 2, a.Y, a.Z);
        //        Point3D v3 = new Point3D(a.X, a.Y * widthCube, a.Z);
        //        Point3D v4 = new Point3D(a.X, a.Y * widthCube * 2, a.Z);
        //        Point3D v5 = new Point3D(a.X + widthCube, a.Y * widthCube, a.Z);
        //        points = DivideQuads(a, v3, v5, v1, widthCube, countIterations - 1, points);//v3 v4
        //        //points = DivideQuads(c, v4, v3, v2, widthCube, countIterations - 1, points);//v1 v2
        //        //points = DivideQuads(b, v3, v1, v4, widthCube, countIterations - 1, points);//v2 v4
        //        //points = DivideQuads(d, v2, v4, v1, widthCube, countIterations - 1, points);//v1 v3
        //        //points = DivideQuads(a, v1, v2, v3, widthCube, countIterations - 1, points);//v3 v4
        //        //points = DivideQuads(c, v4, v3, v2, widthCube, countIterations - 1, points);//v1 v2
        //        //points = DivideQuads(b, v3, v1, v4, widthCube, countIterations - 1, points);//v2 v4
        //        //points = DivideQuads(d, v2, v4, v1, widthCube, countIterations - 1, points);//v1 v3
        //    }
        //    else
        //    {
        //        points.Add(a);
        //        points.Add(b);
        //        points.Add(c);
        //        points.Add(d);
        //    }
        //    return points;
        //}

        private Point3D[] CreateTorus(double intR, double extR, int startAlpha = 0, int startBeta = 0, int finishAlpha = 360, int finishBeta = 360, int extStep = 5, int intStep = 5, double k = 0, double p = 0)
        {
            List<Point3D> points = new List<Point3D>();
            for (int i = startBeta; i < finishBeta; i += extStep)
            {
                for (int j = startAlpha; j < finishAlpha; j += intStep)
                {
                    points.Add(GetPointTorus(intR, extR, j, i, k, p));
                    points.Add(GetPointTorus(intR, extR, j + intStep, i, k, p));
                    points.Add(GetPointTorus(intR, extR, j + intStep, i + extStep, k, p));
                    points.Add(GetPointTorus(intR, extR, j, i + extStep, k, p));
                }
            }
            return points.ToArray();
        }

        private Point3DUV[] CreateSphere(double radius, int startAlpha = 0, int startBeta = 0, int finishAlpha = 360, int finishBeta = 180, int extStep = 5, int intStep = 5)
        {
            List<Point3DUV> points = new List<Point3DUV>();
            double dV = extStep * 1.0 / (finishBeta - startBeta);
            double dU = intStep * 1.0 / (finishAlpha - startAlpha);
            for (double j = startBeta, v = 0; j < finishBeta; j += extStep, v += dV)
            {
                for (double i = startAlpha, u = 0; i < finishAlpha; i += intStep, u += dU)
                {
                    points.Add(GetPointSphere(radius, j, i, u, v));
                    points.Add(GetPointSphere(radius, j + extStep, i, u, v + dV));
                    points.Add(GetPointSphere(radius, j + extStep, i + intStep, u + dU, v + dV));
                    points.Add(GetPointSphere(radius, j, i + intStep, u + dU, v));
                }
            }
            return points.ToArray();
        }

        private Point3DUV GetPointSphere(double radius, double alpha, double beta, double u, double v)
        {
            alpha *= Math.PI / 180;
            beta *= Math.PI / 180;
            return new Point3DUV(radius * Math.Sin(alpha) * Math.Cos(beta),
                               radius * Math.Sin(alpha) * Math.Sin(beta),
                               radius * Math.Cos(alpha), u, v);
        }

        private Point3D GetPointTorus(double intR, double extR, double alpha, double beta, double k, double p)
        {
            alpha *= Math.PI / 180;
            beta *= Math.PI / 180;
            extR += p * beta;
            return new Point3D((extR + intR * Math.Cos(alpha)) * Math.Sin(beta),
                               (extR + intR * Math.Cos(alpha)) * Math.Cos(beta),
                               intR * Math.Sin(alpha) + k * beta);
        }

        private Point3D[] CreateCylinder(double radius, double width, int step)
        {
            List<Point3D> points = new List<Point3D>();
            points.AddRange(DrawCircule(0, 0, radius, step: step, z: -width / 2));
            points.Add(new Point3D(0, 0, -width / 2));
            points.AddRange(DrawCircule(0, 0, radius, step: step, z: width / 2));
            points.Add(new Point3D(0, 0, width / 2));
            return points.ToArray();
        }

        private Point3D[] CreateHeart(double widthHeart)
        {
            List<Point3D> points = new List<Point3D>();
            points.AddRange(DrawCircule(widthHeart / 2 - widthHeart / 4, widthHeart / 4, widthHeart / 4, angleFinish: 180, z: -10));
            points.AddRange(DrawCircule(-widthHeart / 4, widthHeart / 4, widthHeart / 4, angleFinish: 180, z: -10));
            points.Add(new Point3D(0, -25, -10));
            points.AddRange(DrawCircule(widthHeart / 2 - widthHeart / 4, widthHeart / 4, widthHeart / 4, angleFinish: 180, z: 10));
            points.AddRange(DrawCircule(-widthHeart / 4, widthHeart / 4, widthHeart / 4, angleFinish: 180, z: 10));
            points.Add(new Point3D(0, -25, 10));
            return points.ToArray();
        }

        private static Point3D[] DrawCircule(double xCentre = 0.0, double yCentre = 0.0, double radius = 1.0, int angleStart = 0, int angleFinish = 360, int step = 10, double z = 0)
        {
            List<Point3D> points = new List<Point3D>();
            for (int angle = angleStart; angle <= angleFinish; angle += step)
            {
                double x = radius * Math.Cos(angle * Math.PI / 180);
                double y = radius * Math.Sin(angle * Math.PI / 180);
                points.Add(new Point3D(x + xCentre, y + yCentre, z));
            }
            return points.ToArray();
        }

        void PaintScene()
        {
            openGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //Ординаты
            openGL.Begin(OpenGL.GL_LINES);
            openGL.Color(1.0f, 1.0f, 1.0f, 1.0f);
            openGL.Vertex(0, -100000, 0);
            openGL.Vertex(0, 100000, 0);
            openGL.Vertex(-100000, 0, 0);
            openGL.Vertex(100000, 0, 0);
            openGL.Vertex(0, 0, -100000);
            openGL.Vertex(0, 0, 100000);
            openGL.End();

            Random r = new Random();

            //Узор Серпинского
            openGL.Begin(OpenGL.GL_TRIANGLES);
            for (int i = 0; i < pointsSerpinski.Length; i++)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsSerpinski[i].X + centerSerpinski.X, pointsSerpinski[i].Y + centerSerpinski.Y, pointsSerpinski[i].Z + centerSerpinski.Z);
            }
            openGL.End();

            //Снежинка Коха
            openGL.Begin(OpenGL.GL_TRIANGLES);
            for (int i = 0; i < pointsKox.Length; i++)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsKox[i].X + centerKox.X, pointsKox[i].Y + centerKox.Y, pointsKox[i].Z + centerKox.Z);
            }
            openGL.End();

            //Тор
            for (int i = 0; i < pointsTorus.Length; i += 4)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Begin(OpenGL.GL_QUADS);
                openGL.Vertex(pointsTorus[i].X + centerTorus.X, pointsTorus[i].Y + centerTorus.Y, pointsTorus[i].Z + centerTorus.Z);
                openGL.Vertex(pointsTorus[i + 1].X + centerTorus.X, pointsTorus[i + 1].Y + centerTorus.Y, pointsTorus[i + 1].Z + centerTorus.Z);
                openGL.Vertex(pointsTorus[i + 2].X + centerTorus.X, pointsTorus[i + 2].Y + centerTorus.Y, pointsTorus[i + 2].Z + centerTorus.Z);
                openGL.Vertex(pointsTorus[i + 3].X + centerTorus.X, pointsTorus[i + 3].Y + centerTorus.Y, pointsTorus[i + 3].Z + centerTorus.Z);
                openGL.End();
            }

            //Сердце
            int middle = pointsHeart.Length / 2;
            int quarter = (middle - 1) / 2;
            openGL.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < quarter; i++)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsHeart[i].X + centerHeart.X, pointsHeart[i].Y + centerHeart.Y, pointsHeart[i].Z + centerHeart.Z);
            }
            openGL.End();
            openGL.Begin(OpenGL.GL_POLYGON);
            for (int i = quarter; i < middle - 1; i++)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsHeart[i].X + centerHeart.X, pointsHeart[i].Y + centerHeart.Y, pointsHeart[i].Z + centerHeart.Z);
            }
            openGL.End();
            openGL.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < quarter; i++)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsHeart[i + middle].X + centerHeart.X, pointsHeart[i + middle].Y + centerHeart.Y, pointsHeart[i + middle].Z + centerHeart.Z);
            }
            openGL.End();
            openGL.Begin(OpenGL.GL_POLYGON);
            for (int i = quarter; i < middle - 1; i++)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsHeart[i + middle].X + centerHeart.X, pointsHeart[i + middle].Y + centerHeart.Y, pointsHeart[i + middle].Z + centerHeart.Z);
            }
            openGL.End();
            openGL.Begin(OpenGL.GL_TRIANGLES);
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[0].X + centerHeart.X, pointsHeart[0].Y + centerHeart.Y, pointsHeart[0].Z + centerHeart.Z);
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[middle - 2].X + centerHeart.X, pointsHeart[middle - 2].Y + centerHeart.Y, pointsHeart[middle - 2].Z + centerHeart.Z);
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[middle - 1].X + centerHeart.X, pointsHeart[middle - 1].Y + centerHeart.Y, pointsHeart[middle - 1].Z + centerHeart.Z);
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[middle].X + centerHeart.X, pointsHeart[middle].Y + centerHeart.Y, pointsHeart[middle].Z + centerHeart.Z);
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[pointsHeart.Length - 2].X + centerHeart.X, pointsHeart[pointsHeart.Length - 2].Y + centerHeart.Y, pointsHeart[pointsHeart.Length - 2].Z + centerHeart.Z);
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[pointsHeart.Length - 1].X + centerHeart.X, pointsHeart[pointsHeart.Length - 1].Y + centerHeart.Y, pointsHeart[pointsHeart.Length - 1].Z + centerHeart.Z);
            openGL.End();
            openGL.Begin(OpenGL.GL_QUAD_STRIP);
            for (int i = 0; i < middle; i++)
            {
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsHeart[i].X + centerHeart.X, pointsHeart[i].Y + centerHeart.Y, pointsHeart[i].Z + centerHeart.Z);
                openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
                openGL.Vertex(pointsHeart[i + middle].X + centerHeart.X, pointsHeart[i + middle].Y + centerHeart.Y, pointsHeart[i + middle].Z + centerHeart.Z);
            }
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[0].X + centerHeart.X, pointsHeart[0].Y + centerHeart.Y, pointsHeart[0].Z + centerHeart.Z);
            openGL.Color(r.NextDouble(), r.NextDouble(), r.NextDouble());
            openGL.Vertex(pointsHeart[middle].X + centerHeart.X, pointsHeart[middle].Y + centerHeart.Y, pointsHeart[middle].Z + centerHeart.Z);
            openGL.End();

            openGL.Enable(OpenGL.GL_TEXTURE_2D);

            //Куб
            openGL.Color(1.0f, 1.0f, 1.0f, 1.0f);
            textures[0].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(pointsCube[0].X + centerCube.X, pointsCube[0].Y + centerCube.Y, pointsCube[0].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(pointsCube[1].X + centerCube.X, pointsCube[1].Y + centerCube.Y, pointsCube[1].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(pointsCube[2].X + centerCube.X, pointsCube[2].Y + centerCube.Y, pointsCube[2].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(pointsCube[3].X + centerCube.X, pointsCube[3].Y + centerCube.Y, pointsCube[3].Z + centerCube.Z);
            openGL.End();

            textures[1].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(pointsCube[4].X + centerCube.X, pointsCube[4].Y + centerCube.Y, pointsCube[4].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(pointsCube[5].X + centerCube.X, pointsCube[5].Y + centerCube.Y, pointsCube[5].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(pointsCube[6].X + centerCube.X, pointsCube[6].Y + centerCube.Y, pointsCube[6].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(pointsCube[7].X + centerCube.X, pointsCube[7].Y + centerCube.Y, pointsCube[7].Z + centerCube.Z);
            openGL.End();

            textures[2].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(pointsCube[1].X + centerCube.X, pointsCube[1].Y + centerCube.Y, pointsCube[1].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(pointsCube[2].X + centerCube.X, pointsCube[2].Y + centerCube.Y, pointsCube[2].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(pointsCube[7].X + centerCube.X, pointsCube[7].Y + centerCube.Y, pointsCube[7].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(pointsCube[6].X + centerCube.X, pointsCube[6].Y + centerCube.Y, pointsCube[6].Z + centerCube.Z);
            openGL.End();

            textures[3].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(pointsCube[0].X + centerCube.X, pointsCube[0].Y + centerCube.Y, pointsCube[0].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(pointsCube[3].X + centerCube.X, pointsCube[3].Y + centerCube.Y, pointsCube[3].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(pointsCube[4].X + centerCube.X, pointsCube[4].Y + centerCube.Y, pointsCube[4].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(pointsCube[5].X + centerCube.X, pointsCube[5].Y + centerCube.Y, pointsCube[5].Z + centerCube.Z);
            openGL.End();

            textures[4].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(pointsCube[0].X + centerCube.X, pointsCube[0].Y + centerCube.Y, pointsCube[0].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(pointsCube[1].X + centerCube.X, pointsCube[1].Y + centerCube.Y, pointsCube[1].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(pointsCube[6].X + centerCube.X, pointsCube[6].Y + centerCube.Y, pointsCube[6].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(pointsCube[5].X + centerCube.X, pointsCube[5].Y + centerCube.Y, pointsCube[5].Z + centerCube.Z);
            openGL.End();

            textures[5].Bind(openGL);

            openGL.Begin(OpenGL.GL_QUADS);
            openGL.TexCoord(1.0f, 0.0f); openGL.Vertex(pointsCube[2].X + centerCube.X, pointsCube[2].Y + centerCube.Y, pointsCube[2].Z + centerCube.Z);
            openGL.TexCoord(1.0f, 1.0f); openGL.Vertex(pointsCube[3].X + centerCube.X, pointsCube[3].Y + centerCube.Y, pointsCube[3].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 1.0f); openGL.Vertex(pointsCube[4].X + centerCube.X, pointsCube[4].Y + centerCube.Y, pointsCube[4].Z + centerCube.Z);
            openGL.TexCoord(0.0f, 0.0f); openGL.Vertex(pointsCube[7].X + centerCube.X, pointsCube[7].Y + centerCube.Y, pointsCube[7].Z + centerCube.Z);
            openGL.End();

            //Цилиндр
            textures[7].Bind(openGL);
            openGL.Color(1.0f, 1.0f, 1.0f, 1.0f);
            middle = pointsCylinder.Length / 2;
            double a = GetDistance(pointsCylinder[0], pointsCylinder[middle / 2 - 1]) / 2;
            double b = GetDistance(pointsCylinder[middle + 1], pointsCylinder[middle + middle / 2 - 1]) / 2;
            double k = b / 10;
            openGL.Begin(OpenGL.GL_TRIANGLE_FAN);
            openGL.TexCoord(0.5, 0.0); openGL.Vertex(pointsCylinder[middle - 1].X + centerCylinder.X, pointsCylinder[middle - 1].Y + centerCylinder.Y, pointsCylinder[middle - 1].Z + centerCylinder.Z);
            for (int i = 0; i < middle - 1; i++)
            {
                if (i % 2 == 0)
                    openGL.TexCoord(0.0, k);
                else
                    openGL.TexCoord(1.0, k);
                openGL.Vertex(pointsCylinder[i].X + centerCylinder.X, pointsCylinder[i].Y + centerCylinder.Y, pointsCylinder[i].Z + centerCylinder.Z);
            }
            openGL.End();
            openGL.Begin(OpenGL.GL_TRIANGLE_FAN);
            openGL.TexCoord(0.5, 0.0); openGL.Vertex(pointsCylinder[pointsCylinder.Length - 1].X + centerCylinder.X, pointsCylinder[pointsCylinder.Length - 1].Y + centerCylinder.Y, pointsCylinder[pointsCylinder.Length - 1].Z + centerCylinder.Z);
            for (int i = middle; i < pointsCylinder.Length - 1; i++)
            {
                if (i % 2 == 0)
                    openGL.TexCoord(0.0, k);
                else
                    openGL.TexCoord(1.0, k);
                openGL.Vertex(pointsCylinder[i].X + centerCylinder.X, pointsCylinder[i].Y + centerCylinder.Y, pointsCylinder[i].Z + centerCylinder.Z);
            }
            openGL.End();
            k = GetDistance(pointsCylinder[0], pointsCylinder[middle]) / 10;
            double length = Math.PI * (a + b) / 200 / 2;
            for (int i = 0; i < middle - 2; i++)
            {
                openGL.Begin(OpenGL.GL_QUADS);
                openGL.TexCoord(i * length, k);
                openGL.Vertex(pointsCylinder[i].X + centerCylinder.X, pointsCylinder[i].Y + centerCylinder.Y, pointsCylinder[i].Z + centerCylinder.Z);
                openGL.TexCoord(i * length, 0.0);
                openGL.Vertex(pointsCylinder[i + middle].X + centerCylinder.X, pointsCylinder[i + middle].Y + centerCylinder.Y, pointsCylinder[i + middle].Z + centerCylinder.Z);
                openGL.TexCoord((i + 1) * length, 0.0);
                openGL.Vertex(pointsCylinder[i + middle + 1].X + centerCylinder.X, pointsCylinder[i + middle + 1].Y + centerCylinder.Y, pointsCylinder[i + middle + 1].Z + centerCylinder.Z);
                openGL.TexCoord((i + 1) * length, k);
                openGL.Vertex(pointsCylinder[i + 1].X + centerCylinder.X, pointsCylinder[i + 1].Y + centerCylinder.Y, pointsCylinder[i + 1].Z + centerCylinder.Z);
                openGL.End();
            }

            //Сфера
            openGL.Color(1.0f, 1.0f, 1.0f, 1.0f);
            textures[6].Bind(openGL);
            for (int i = 0; i < pointsSphere.Length; i += 4)
            {
                openGL.Begin(OpenGL.GL_QUADS);
                openGL.TexCoord(pointsSphere[i].U, pointsSphere[i].V); openGL.Vertex(pointsSphere[i].X + centerSphere.X, pointsSphere[i].Y + centerSphere.Y, pointsSphere[i].Z + centerSphere.Z);
                openGL.TexCoord(pointsSphere[i + 1].U, pointsSphere[i + 1].V); openGL.Vertex(pointsSphere[i + 1].X + centerSphere.X, pointsSphere[i + 1].Y + centerSphere.Y, pointsSphere[i + 1].Z + centerSphere.Z);
                openGL.TexCoord(pointsSphere[i + 2].U, pointsSphere[i + 2].V); openGL.Vertex(pointsSphere[i + 2].X + centerSphere.X, pointsSphere[i + 2].Y + centerSphere.Y, pointsSphere[i + 2].Z + centerSphere.Z);
                openGL.TexCoord(pointsSphere[i + 3].U, pointsSphere[i + 3].V); openGL.Vertex(pointsSphere[i + 3].X + centerSphere.X, pointsSphere[i + 3].Y + centerSphere.Y, pointsSphere[i + 3].Z + centerSphere.Z);
                openGL.End();
            }

            openGL.Disable(OpenGL.GL_TEXTURE_2D);

            //Текст
            openGL.DrawText(5, (int)(openGLControl1.Height - fontSize), 1, 1, 1, string.Empty, fontSize, "Info");
            for (int i = 0; i < pointsCube.Length; i++)
            {
                openGL.DrawText(5, (int)(openGLControl1.Height - (i + 2) * fontSize), 1, 1, 1, string.Empty, fontSize, GetInfo(pointsCube[i], i + 1, centerCube));
            }

            openGL.Flush();
        }

        private double GetDistance(Point3D point1, Point3D point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2) + Math.Pow(point2.Z - point1.Z, 2));
        }

        private string GetInfo(Point3D point, int index, Point3D center)
        {
            return string.Format("P{0}: x = {1:f2}, y = {2:f2}, z = {3:f2}\n", index, point.X + center.X, point.Y + center.Y, point.Z + center.Z);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            openGL = openGLControl1.OpenGL;
            textures = new Texture[8] { new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture(), new Texture() };
            textures[0].Create(openGL, Properties.Resources.BMW);
            textures[1].Create(openGL, Properties.Resources.WOT);
            textures[2].Create(openGL, Properties.Resources.tiger);
            textures[3].Create(openGL, Properties.Resources.kulak);
            textures[4].Create(openGL, Properties.Resources.barselon);
            textures[5].Create(openGL, Properties.Resources.VSRB);
            textures[6].Create(openGL, Properties.Resources.earth3);
            textures[7].Create(openGL, Properties.Resources.kirpich);
            openGL.Enable(OpenGL.GL_DEPTH_TEST);
            openGL.DepthMask((byte)OpenGL.GL_TRUE);
        }

        private void glControl1_SizeChanged(object sender, EventArgs e)
        {
            SetCamera(cameraPosition, cameraView);
        }

        private void SetCamera(Point3D cameraPosition, Point3D cameraView)
        {
            try
            {
                openGL.MatrixMode(OpenGL.GL_PROJECTION);
                openGL.LoadIdentity();
                openGL.Perspective(60.0f, (double)openGLControl1.Width / (double)openGLControl1.Height, 0.01, double.MaxValue);
                openGL.MatrixMode(OpenGL.GL_MODELVIEW);
                openGL.LoadIdentity();
                openGL.LookAt(cameraPosition.X,
                    cameraPosition.Y,
                    cameraPosition.Z,
                    cameraView.X + cameraPosition.X,
                    cameraView.Y + cameraPosition.Y,
                    cameraView.Z + cameraPosition.Z,
                    0, 1, 0);
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                centerCube = Translate3D(centerCube, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
                centerSphere = Translate3D(centerSphere, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
                centerHeart = Translate3D(centerHeart, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
                centerCylinder = Translate3D(centerCylinder, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
                centerTorus = Translate3D(centerTorus, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
                centerSerpinski = Translate3D(centerSerpinski, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
                centerKox = Translate3D(centerKox, (double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
            }
            else if (radioButton2.Checked)
            {
                pointsCube = Rotate3D(pointsCube, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
                pointsSphere = Rotate3D(pointsSphere, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
                pointsHeart = Rotate3D(pointsHeart, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
                pointsCylinder = Rotate3D(pointsCylinder, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
                pointsTorus = Rotate3D(pointsTorus, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
                pointsSerpinski = Rotate3D(pointsSerpinski, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
                pointsKox = Rotate3D(pointsKox, (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown4.Value);
            }
            else
            {
                pointsCube = Scale3D(pointsCube, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
                pointsSphere = Scale3D(pointsSphere, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
                pointsHeart = Scale3D(pointsHeart, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
                pointsCylinder = Scale3D(pointsCylinder, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
                pointsTorus = Scale3D(pointsTorus, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
                pointsSerpinski = Scale3D(pointsSerpinski, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
                pointsKox = Scale3D(pointsKox, (double)numericUpDown5.Value, (double)numericUpDown6.Value, (double)numericUpDown9.Value);
            }
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

        private Point3DUV[] Scale3D(Point3DUV[] points, double sx, double sy, double sz)
        {
            double[,] matrix = new double[4, 4] { { sx, 0, 0, 0 }, { 0, sy, 0, 0 }, { 0, 0, sz, 0 }, { 0, 0, 0, 1 } };
            for (int i = 0; i < points.Length; i++)
            {
                double[,] vector = GetVector3D(points[i]);
                vector = Multiplication(vector, matrix);
                points[i].X = vector[0, 0];
                points[i].Y = vector[0, 1];
                points[i].Z = vector[0, 2];
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

        private Point3D Rotate3D(Point3D point, double alphaX, double alphaY, double alphaZ)
        {
            alphaX *= Math.PI / 180;
            alphaY *= Math.PI / 180;
            alphaZ *= Math.PI / 180;
            double[,] matrixZ = new double[4, 4] { { Math.Cos(alphaZ), Math.Sin(alphaZ), 0, 0 }, { -Math.Sin(alphaZ), Math.Cos(alphaZ), 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            double[,] matrixX = new double[4, 4] { { 1, 0, 0, 0 }, { 0, Math.Cos(alphaX), Math.Sin(alphaX), 0 }, { 0, -Math.Sin(alphaX), Math.Cos(alphaX), 0 }, { 0, 0, 0, 1 } };
            double[,] matrixY = new double[4, 4] { { Math.Cos(alphaY), 0, -Math.Sin(alphaY), 0 }, { 0, 1, 0, 0 }, { Math.Sin(alphaY), 0, Math.Cos(alphaY), 0 }, { 0, 0, 0, 1 } };

            double[,] vector = GetVector3D(point);
            vector = Multiplication(vector, matrixZ);
            vector = Multiplication(vector, matrixX);
            vector = Multiplication(vector, matrixY);
            point = GetPoint3D(vector);
            return point;
        }

        private Point3DUV[] Rotate3D(Point3DUV[] points, double alphaX, double alphaY, double alphaZ)
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
                points[i].X = vector[0, 0];
                points[i].Y = vector[0, 1];
                points[i].Z = vector[0, 2];
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
            return new Point3D(vector[0, 0], vector[0, 1], vector[0, 2]);
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
                centerCube = Translate3D(centerCube, 0, 5, 0);
                centerSphere = Translate3D(centerSphere, 0, 5, 0);
                centerHeart = Translate3D(centerHeart, 0, 5, 0);
                centerCylinder = Translate3D(centerCylinder, 0, 5, 0);
                centerTorus = Translate3D(centerTorus, 0, 5, 0);
                centerSerpinski = Translate3D(centerSerpinski, 0, 5, 0);
                centerKox = Translate3D(centerKox, 0, 5, 0);
            }
            else if (e.KeyCode == Keys.Down)
            {
                centerCube = Translate3D(centerCube, 0, -5, 0);
                centerSphere = Translate3D(centerSphere, 0, -5, 0);
                centerHeart = Translate3D(centerHeart, 0, -5, 0);
                centerCylinder = Translate3D(centerCylinder, 0, -5, 0);
                centerTorus = Translate3D(centerTorus, 0, -5, 0);
                centerSerpinski = Translate3D(centerSerpinski, 0, -5, 0);
                centerKox = Translate3D(centerKox, 0, -5, 0);
            }
            else if (e.KeyCode == Keys.Left)
            {
                centerCube = Translate3D(centerCube, -5, 0, 0);
                centerSphere = Translate3D(centerSphere, -5, 0, 0);
                centerHeart = Translate3D(centerHeart, -5, 0, 0);
                centerCylinder = Translate3D(centerCylinder, -5, 0, 0);
                centerTorus = Translate3D(centerTorus, -5, 0, 0);
                centerSerpinski = Translate3D(centerSerpinski, -5, 0, 0);
                centerKox = Translate3D(centerKox, -5, 0, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                centerCube = Translate3D(centerCube, 5, 0, 0);
                centerSphere = Translate3D(centerSphere, 5, 0, 0);
                centerHeart = Translate3D(centerHeart, 5, 0, 0);
                centerCylinder = Translate3D(centerCylinder, 5, 0, 0);
                centerTorus = Translate3D(centerTorus, 5, 0, 0);
                centerSerpinski = Translate3D(centerSerpinski, 5, 0, 0);
                centerKox = Translate3D(centerKox, 5, 0, 0);
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                centerCube = Translate3D(centerCube, 0, 0, 5);
                centerSphere = Translate3D(centerSphere, 0, 0, 5);
                centerHeart = Translate3D(centerHeart, 0, 0, 5);
                centerCylinder = Translate3D(centerCylinder, 0, 0, 5);
                centerTorus = Translate3D(centerTorus, 0, 0, 5);
                centerSerpinski = Translate3D(centerSerpinski, 0, 0, 5);
                centerKox = Translate3D(centerKox, 0, 0, 5);
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                centerCube = Translate3D(centerCube, 0, 0, -5);
                centerSphere = Translate3D(centerSphere, 0, 0, -5);
                centerHeart = Translate3D(centerHeart, 0, 0, -5);
                centerCylinder = Translate3D(centerCylinder, 0, 0, -5);
                centerTorus = Translate3D(centerTorus, 0, 0, -5);
                centerSerpinski = Translate3D(centerSerpinski, 0, 0, -5);
                centerKox = Translate3D(centerKox, 0, 0, -5);
            }
            else if (e.KeyCode == Keys.Space)
            {
                pointsCube = Rotate3D(pointsCube, 60, 60, 60);
                pointsSphere = Rotate3D(pointsSphere, 60, 60, 60);
                pointsHeart = Rotate3D(pointsHeart, 60, 60, 60);
                pointsCylinder = Rotate3D(pointsCylinder, 60, 60, 60);
                pointsTorus = Rotate3D(pointsTorus, 60, 60, 60);
                pointsSerpinski = Rotate3D(pointsSerpinski, 60, 60, 60);
                pointsKox = Rotate3D(pointsKox, 60, 60, 60);
            }
            else if (e.KeyCode == Keys.NumPad8)
            {
                cameraPosition = Translate3D(cameraPosition, 0, 5, 0);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.NumPad2)
            {
                cameraPosition = Translate3D(cameraPosition, 0, -5, 0);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                cameraPosition = Translate3D(cameraPosition, -5, 0, 0);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                cameraPosition = Translate3D(cameraPosition, 5, 0, 0);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.NumPad9)
            {
                cameraPosition = Translate3D(cameraPosition, 0, 0, -5);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.NumPad3)
            {
                cameraPosition = Translate3D(cameraPosition, 0, 0, 5);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.W)
            {
                cameraView = Rotate3D(cameraView, 5, 0, 0);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.S)
            {
                cameraView = Rotate3D(cameraView, -5, 0, 0);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.A)
            {
                cameraView = Rotate3D(cameraView, 0, 5, 0);
                SetCamera(cameraPosition, cameraView);
            }
            else if (e.KeyCode == Keys.D)
            {
                cameraView = Rotate3D(cameraView, 0, -5, 0);
                SetCamera(cameraPosition, cameraView);
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
                pointsCube = Scale3D(pointsCube, 1.5, 1.5, 1.5);
                pointsSphere = Scale3D(pointsSphere, 1.5, 1.5, 1.5);
                pointsHeart = Scale3D(pointsHeart, 1.5, 1.5, 1.5);
                pointsCylinder = Scale3D(pointsCylinder, 1.5, 1.5, 1.5);
                pointsTorus = Scale3D(pointsTorus, 1.5, 1.5, 1.5);
                pointsSerpinski = Scale3D(pointsSerpinski, 1.5, 1.5, 1.5);
                pointsKox = Scale3D(pointsKox, 1.5, 1.5, 1.5);
            }
            else
            {
                pointsCube = Scale3D(pointsCube, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
                pointsSphere = Scale3D(pointsSphere, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
                pointsHeart = Scale3D(pointsHeart, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
                pointsCylinder = Scale3D(pointsCylinder, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
                pointsTorus = Scale3D(pointsTorus, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
                pointsSerpinski = Scale3D(pointsSerpinski, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
                pointsKox = Scale3D(pointsKox, 2.0 / 3.0, 2.0 / 3.0, 2.0 / 3.0);
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
    }
}
