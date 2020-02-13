using System;

namespace SimplexMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] tableKoef = new double[,]
            {
                { 1, 1 },
                { -0.5, 1 }
            };
            double[] limits = new double[] { 3, 1 };
            double[] centralFunc = new double[] { 1, 2 };
            Simplex simplex = new Simplex(tableKoef, limits, centralFunc);
            if (simplex.Calculate())
            {
                double[] values = simplex.GetValues();
                double value = simplex.GetCentralFuncValue();
                for (int i = 0; i < values.Length; i++)
                {
                    Console.WriteLine("x[{0}] = {1:f2}", i, values[i]);
                }
                Console.WriteLine("F = {0:f2}", value);               
            }
            else
            {
                Console.WriteLine("Задача неразрешима.");
            }
            Console.ReadKey();
        }
    }
}
