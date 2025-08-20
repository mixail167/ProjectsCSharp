using System;

namespace Graph.Algorithms
{
    class HamiltonianCycle
    {
        int[] c;    // номер хода, на котором посещается вершина
        int[] path; // номера посещаемых вершин
        readonly int[,] adjacencyMatrix;
        readonly int v0_index;
        readonly int length;

        internal HamiltonianCycle(int v0, int[,] adjacencyMatrix)
        {
            if (adjacencyMatrix == null)
            {
                throw new ArgumentException("Матрица смежности не инициализирована");
            }
            else
            {
                this.adjacencyMatrix = adjacencyMatrix;
            }
            length = adjacencyMatrix.GetLength(0);
            if (v0 <= 0 || v0 > length)
            {
                throw new ArgumentException("Неверный номер вершины");
            }
            else
            {
                v0_index = v0 - 1;
            }
            
        }

        public bool Solve()
        {
            c = new int[length];
            path = new int[length];
            path[0] = v0_index;
            for (int i = 0; i < length; i++)
            {
                c[i] = -1;
            }
            c[v0_index] = v0_index;
            return SolveRecurse(1);
        }

        public int[] GetPath()
        {
            int[] output = new int[length + 1];
            for (int i = 0; i < length; i++)
            {
                output[i] = path[i]+1;
            }
            output[length] = path[0] + 1;
            return output;
        }

        private bool SolveRecurse(int k)
        {
            bool solution = false;
            int length = adjacencyMatrix.GetLength(0);
            for (int i = 0; i < length && !solution; i++)
            {
                if (adjacencyMatrix[i, path[k - 1]] == 1 || adjacencyMatrix[path[k - 1], i] == 1)
                {
                    if (k == length && i == v0_index)
                    {
                        solution = true;
                    }
                    else if (c[i] == -1)
                    {
                        c[i] = k;
                        path[k] = i;
                        solution = SolveRecurse(k + 1);
                        if (!solution)
                        {
                            c[i] = -1;
                        }
                    }
                    else continue;
                }
            }
            return solution;
        }
    }
}
