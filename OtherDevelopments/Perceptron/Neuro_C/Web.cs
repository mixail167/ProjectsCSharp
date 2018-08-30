using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Web
    {
        public int[,] mul;
        public int[,] weight;
        public int[,] input;
        public int limit = 9;
        public int sum;

        public Web(int sizex, int sizey, int[,] inP)
        {
            weight = new int[sizex, sizey];
            mul = new int[sizex, sizey];
            input = new int[sizex, sizey];
            input = inP;
        }

        public void Mul_w()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 5; y++)
                    mul[x, y] = input[x, y] * weight[x, y];
        }

        public void Sum()
        {
            sum = 0;
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 5; y++)
                    sum += mul[x, y];
        }

        public bool Rez()
        {
            return sum >= limit;
        }

        public void IncW(int[,] inP)
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 5; y++)
                    weight[x, y] += inP[x, y];
        }

        public void DecW(int[,] inP)
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 5; y++)
                    weight[x, y] -= inP[x, y];
        }
    }
}
