using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportTask
{
    enum Methods
    {
        MMT,
        NWA,
        MAF
    }

    class TransportTaskExecutor
    {
        uint[,] matrix;
        uint[] a;
        uint[] b;
        uint[,] result;
        uint f;

        public uint[,] Matrix
        {
            set { matrix = value; }
        }

        public uint[] A
        {
            set { a = value; }
        }

        public uint[] B
        {
            set { b = value; }
        }

        public bool CheckOpen()
        {
            if (a != null && b != null)
            {
                uint sumA = GetSum(a);
                uint sumB = GetSum(b);
                return !(sumA == sumB);
            }
            return true;
        }

        uint GetSum(uint[] a)
        {
            return (uint)a.Sum(n => (int)n);
        }

        public bool Solve(Methods methods)
        {
            if (!CheckOpen() && matrix != null)
            {
                switch (methods)
                {
                    case Methods.MMT:
                        result = SolveMinTarif();
                        f = SolveF(result);
                        return true;
                    case Methods.NWA:
                        result = SolveNorthWestAngle();
                        f = SolveF(result);
                        return true;
                    case Methods.MAF:
                        result = SolveMAF();
                        f = SolveF(result);
                        return true;
                    default:
                        break;
                }

            }
            return false;
        }

        uint[,] SolveNorthWestAngle()
        {
            int m = this.matrix.GetLength(0);
            int n = this.matrix.GetLength(1);
            uint[,] matrix = new uint[m, n];
            bool[,] visited = new bool[m, n];
            uint[] a = new uint[this.a.Length];
            uint[] b = new uint[this.b.Length];
            Array.Copy(this.a, a, a.Length);
            Array.Copy(this.b, b, b.Length);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!visited[i, j])
                    {
                        SetValue(new Tuple<int, int>(i, j), ref visited, ref matrix, ref a, ref b);
                    }
                }
            }
            return matrix;
        }

        public uint[,] Result
        {
            get { return result; }
        }

        public uint F
        {
            get { return f; }
        }

        uint[,] SolveMinTarif()
        {
            int m = this.matrix.GetLength(0);
            int n = this.matrix.GetLength(1);
            uint[,] matrix = new uint[m, n];
            bool[,] visited = new bool[m, n];
            uint[] a = new uint[this.a.Length];
            uint[] b = new uint[this.b.Length];
            Array.Copy(this.a, a, a.Length);
            Array.Copy(this.b, b, b.Length);
            Tuple<int, int> coord;
            while (true)
            {
                if ((coord = GetMinIndex(visited)) != null)
                {
                    SetValue(coord, ref visited, ref matrix, ref a, ref b);
                }
                else break;
            }
            return matrix;
        }

        uint[,] SolveMAF()
        {
            int m = this.matrix.GetLength(0);
            int n = this.matrix.GetLength(1);
            uint[,] matrix = new uint[m, n];
            bool[,] visited = new bool[m, n];
            uint[] a = new uint[this.a.Length];
            uint[] b = new uint[this.b.Length];
            Array.Copy(this.a, a, a.Length);
            Array.Copy(this.b, b, b.Length);
            int[] a1 = new int[this.a.Length];
            int[] b1 = new int[this.b.Length];
            while (true)
            {
                for (int i = 0; i < m; i++)
                {
                    if (a[i] > 0)
                    {
                        int index1 = GetMinIndexRow(visited, i, -1);
                        int index2 = GetMinIndexRow(visited, i, index1);
                        if (index2 == -1)
                        {
                            a1[i] = 0;
                        }
                        else
                        {
                            a1[i] = Convert.ToInt32(this.matrix[i, index2] - this.matrix[i, index1]);
                        }
                    }
                    else
                    {
                        a1[i] = -1;
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (b[i] > 0)
                    {
                        int index1 = GetMinIndexCol(visited, i, -1);
                        int index2 = GetMinIndexCol(visited, i, index1);
                        if (index2 == -1)
                        {
                            b1[i] = 0;
                        }
                        else
                        {
                            b1[i] = Convert.ToInt32(this.matrix[index2, i] - this.matrix[index1, i]);
                        }
                    }
                    else
                    {
                        b1[i] = -1;
                    }
                }
                Tuple<int, int> coord;
                if ((coord = GetMinIndex(visited, a1, b1)) != null)
                {
                    SetValue(coord, ref visited, ref matrix, ref a, ref b);
                }
                else
                    break;
            }
            return matrix;
        }


        void SetValue(Tuple<int, int> coord, ref bool[,] visited, ref uint[,] matrix, ref uint[] a, ref uint[] b)
        {
            int row = coord.Item1;
            int col = coord.Item2;
            uint min = Min(a[row], b[col]);
            matrix[row, col] = min;
            a[row] -= min;
            b[col] -= min;
            if (a[row] != 0)
            {
                visited = ChangeVisit(visited, row, col);
            }
            else
            {
                visited = ChangeVisitRow(visited, row);
            }
            if (b[col] != 0)
            {
                visited[row, col] = true;
            }
            else
            {
                visited = ChangeVisitCol(visited, col);
            }
        }

        Tuple<int, int> GetMinIndex(bool[,] visited, int[] a, int[] b)
        {
            int row = -1;
            int col = -1;
            int max = int.MinValue;
            bool isA = false;
            for (int i = 0; i < b.Length; i++)
            {
                if (max < b[i] && b[i] != -1)
                {
                    max = b[i];
                    col = i;
                }
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (max < a[i] && a[i] != -1)
                {
                    max = a[i];
                    isA = true;
                    row = i;
                }
            }
            if (isA && row != -1)
            {
                col = GetMinIndexRow(visited, row, -1);
            }
            else if (!isA && col != -1)
            {
                row = GetMinIndexCol(visited, col, -1);
            }
            if (row == -1 || col == -1)
            {
                return null;
            }
            else
            {
                return new Tuple<int, int>(row, col);
            }
        }

        private int GetMinIndexRow(bool[,] visited, int row, int v)
        {
            int n = matrix.GetLength(1);
            int col = -1;
            uint min = uint.MaxValue;
            for (int i = 0; i < n; i++)
            {
                if (i != v)
                {
                    if (min > matrix[row, i] && !visited[row, i])
                    {
                        min = matrix[row, i];
                        col = i;
                    }
                }
            }
            return col;
        }
        private int GetMinIndexCol(bool[,] visited, int col, int v)
        {
            int n = matrix.GetLength(0);
            int row = -1;
            uint min = uint.MaxValue;
            for (int i = 0; i < n; i++)
            {
                if (i != v)
                {
                    if (min > matrix[i, col] && !visited[i, col])
                    {
                        min = matrix[i, col];
                        row = i;
                    }
                }
            }
            return row;
        }

        bool[,] ChangeVisit(bool[,] visited, int row, int col)
        {
            visited[row, col] = true;
            return visited;
        }

        bool[,] ChangeVisitRow(bool[,] visited, int row)
        {
            int n = visited.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                if (!visited[row, i])
                {
                    visited[row, i] = true;
                }
            }
            return visited;
        }

        bool[,] ChangeVisitCol(bool[,] visited, int col)
        {
            int n = visited.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                if (!visited[i, col])
                {
                    visited[i, col] = true;
                }
            }
            return visited;
        }

        uint Min(uint a, uint b)
        {
            if (a > b)
            {
                return b;
            }
            return a;
        }

        Tuple<int, int> GetMinIndex(bool[,] visited)
        {
            int row = -1;
            int col = -1;
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            uint min = uint.MaxValue;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (min > matrix[i, j] && !visited[i, j])
                    {
                        min = matrix[i, j];
                        row = i;
                        col = j;
                    }
                }
            }
            if (row == -1)
            {
                return null;
            }
            else
            {
                return new Tuple<int, int>(row, col);
            }
        }

        uint SolveF(uint[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            uint f = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    f += this.matrix[i, j] * matrix[i, j];
                }
            }
            return f;
        }
    }
}
