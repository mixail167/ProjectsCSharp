using System;

namespace SimplexMethod
{
    class Program
    {
        static void Solve(double[,] tableKoef, double[] limits, double[] targetFunc, bool isMin)
        {
            Simplex simplex = new Simplex(tableKoef, limits, targetFunc, isMin);
            if (simplex.Calculate())
            {
                double[] values = simplex.GetValues();
                double value = simplex.GetTargetFuncValue();
                for (int i = 0; i < values.Length; i++)
                {
                    Console.WriteLine("x[{0}] = {1:f3}", i, values[i]);
                }
                Console.WriteLine("F = {0:f3}", value);
            }
            else
            {
                Console.WriteLine("Задача неразрешима.");
            }
        }

        static void Main(string[] args)
        {
            double[,] tableKoef = new double[,]
            {
                { 1, 1 },
                { -0.5, 1 }
            };
            double[] limits = new double[] { 3, 1 };
            double[] targetFunc = new double[] { 1, 2 };
            bool isMin = true;
            Solve(tableKoef, limits, targetFunc, isMin);
            tableKoef = new double[,]
            {
                { 180, 190, 30, 10, 260, 130, 21 },
                { 20, 3, 40, 865, 310, 3, 2 },
                { 0, 0, 50, 6, 20, 650, 200 },
                { 9, 10, 7, 12, 60, 20, 10 }
            };
            limits = new double[] { 118, 56, 500, 8 };
            targetFunc = new double[] { 1.8, 1, 0.28, 3.4, 2.9, 0.5, 0.1 };
            isMin = true;
            Solve(tableKoef, limits, targetFunc, isMin);
            tableKoef = new double[,]
            {
                { 2,12 },
                { 4,6 },
                { 3,0 },
                { 0,18 }
            };
            limits = new double[] { 20, 32, 14, 42 };
            targetFunc = new double[] { 12, 10 };
            isMin = true;
            Solve(tableKoef, limits, targetFunc, isMin);
            tableKoef = new double[,]
            {
                { 2,3,6 },
                { 4,2,4 },
                { 4,6,8 }
            };
            limits = new double[] { 240, 200, 160 };
            targetFunc = new double[] { 4, 5, 4 };
            isMin = false;
            Solve(tableKoef, limits, targetFunc, isMin);
            tableKoef = new double[,]
            {
                { 2,1 },
                { 1,3 },
                { 0,1 }
            };
            limits = new double[] { 64, 72, 20 };
            targetFunc = new double[] { 4, 6 };
            isMin = false;
            Solve(tableKoef, limits, targetFunc, isMin);
            tableKoef = new double[,]
            {
                { -3,-4 },
                { 1,3 },
                { 2,1 }
            };
            limits = new double[] { -6, 3, 4 };
            targetFunc = new double[] { 4, 16 };
            isMin = false;
            Solve(tableKoef, limits, targetFunc, isMin);           
            Console.ReadKey();
        }
    }
}
