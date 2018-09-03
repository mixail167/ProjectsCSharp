using System;
using System.Collections.Generic;
using System.Drawing;

namespace Algorithms
{
    class Romb
    {
        private PointF centerPoint;
        private double radius;
        private PointF[] points;

        public Romb(PointF centerPoint, double radius, PointF[] points)
        {
            this.radius = radius;
            this.centerPoint = centerPoint;
            this.points = points;
        }

        public PointF CenterPoint
        {
            get { return centerPoint; }
            set { this.centerPoint = value; }
        }

        public double Radius
        {
            get { return radius; }
            set { this.radius = value; }
        }

        public PointF[] Points
        {
            get { return points; }
            set { this.points = value; }
        }

        public static Romb[,] CreateRombMesh(double x0, double y0, double a, int xCount, int yCount, double radius)
        {
            double cx0 = x0 + 2 * a;
            double cy0 = y0;
            Romb[,] rombs = new Romb[xCount, yCount];
            for (int y = 0; y < yCount; y++)
            {
                for (int x = 0; x < xCount; x++)
                {
                    double cx = cx0 + 2 * a * (x + y);
                    double cy = cy0 + a * (x - y);
                    PointF[] points = new PointF[4];
                    points[0] = new PointF((float)(cx - 2 * a), (float)(cy));
                    points[1] = new PointF((float)(cx), (float)(cy - a));
                    points[2] = new PointF((float)(cx + 2 * a), (float)(cy));
                    points[3] = new PointF((float)(cx), (float)(cy + a));
                    rombs[x, y] = new Romb(new PointF((float)cx, (float)cy), radius, points);
                }
            }
            return rombs;
        }

        public static Point Check(Romb[,] rombMesh, Point point)
        {
            for (int i = 0; i < rombMesh.GetLength(0); i++)
            {
                for (int j = 0; j < rombMesh.GetLength(1); j++)
                {
                    if (GetFunction(rombMesh[i, j].Points[0], rombMesh[i, j].Points[1], point) >= 0 &&
                        GetFunction(rombMesh[i, j].Points[1], rombMesh[i, j].Points[2], point) >= 0 &&
                        GetFunction(rombMesh[i, j].Points[2], rombMesh[i, j].Points[3], point) <= 0 &&
                        GetFunction(rombMesh[i, j].Points[3], rombMesh[i, j].Points[0], point) <= 0)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return new Point(-1, -1);
        }

        private static double GetFunction(PointF point1, PointF point2, Point point)
        {
            return (point1.Y - point2.Y) * point.X / (point2.X - point1.X) + point.Y + (point1.X * point2.Y - point2.X * point1.Y) / (point2.X - point1.X);
        }
    }
}
