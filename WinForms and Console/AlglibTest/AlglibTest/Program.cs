using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlglibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] a = new double[,]
            {
                {0,3,-1,2,6},
                {2,1,0,0,3},
                {-2,-1, 0, 2, 5},
                {-5, 7, 1,1,1},
                {2,0,2,-2,1}
            };
            double det = alglib.rmatrixdet(a);
            Console.WriteLine(det.ToString());
            double[] x = new double[] { -1.0, -0.5, 0.0, +0.5, +1.0 };
            double[] y = new double[] { +1.0, 0.25, 0.0, 0.25, +1.0 };
            double t = 0.5;
            alglib.spline1dinterpolant s;
            alglib.spline1dbuildakima(x, y, out s);
            double v = alglib.spline1dcalc(s, t);
            Console.WriteLine("{0:f4}", v);
            double[,] A = new double[,]
            {
                {3, -1, 2},
                {4, -3, 3},
                {1, 3, 0},
                {5, 0, 3}
            };
            double[] b = new double[] { 2, 3, 0, 3 };
            int info;
            alglib.densesolverlsreport report;
            double[] x1;
            alglib.rmatrixsolvels(A, A.GetLength(0), A.GetLength(1), b, 0.0, out info, out report, out x1);
            if (info == 1)
            {
                for (int i = 0; i < x1.GetLength(0); i++)
                {
                    Console.WriteLine("x[{0}] = {1}", i, x1[i]);
                }
            }
            Console.ReadKey();
        }
    }
}
