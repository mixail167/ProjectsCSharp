using System.Collections.Generic;

namespace SimplexMethod
{
    class Simplex
    {
        double[,] table;
        double[] targetFunc;
        List<int> basis;
        double[] values;
        double valueTargetFunc;
        bool isMin;

        public Simplex(double[,] tableKoef, double[] limits, double[] targetFunc, bool isMin)
        {
            int n = tableKoef.GetLength(0);
            int m = tableKoef.GetLength(1);
            this.targetFunc = targetFunc;
            this.isMin = isMin;
            table = new double[n, m + n + 1];
            basis = new List<int>();
            double k = (isMin) ? -1 : 1;
            for (int i = 0; i < n; i++)
            {
                table[i, n + m] = limits[i] * k;
                for (int j = 0; j < m; j++)
                {
                    table[i, j] = tableKoef[i, j] * k;
                }
                if (m + i < m + n)
                {
                    table[i, m + i] = 1;
                    basis.Add(m + i);
                }
            }
        }

        public bool Calculate()
        {
            while (!IsPosNegLimits())
            {
                int row = FindRow();
                int column = FindColumn(row);
                ConvertTable(row, column);
            }
            double[] delta = CalculateDelta();
            while (!CheckDelta(delta))
            {
                int column = FindColumn(delta);
                int row = FindRow(column);
                ConvertTable(row, column);
                delta = CalculateDelta();
            }
            SetValues();
            return true;
        }

        private int FindRow(int column)
        {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            int row = 0;
            double q = 0;
            for (int i = 0; i < n; i++)
            {
                if (table[i, column] > 0)
                {
                    row = i;
                    q = table[i, m - 1] / table[i, column];
                    break;
                }
            }
            for (int i = row + 1; i < n; i++)
            {
                if (table[i, column] > 0)
                {
                    double temp = table[i, m - 1] / table[i, column];
                    if (q >= temp)
                    {
                        q = temp;
                        row = i;
                    }
                }
            }
            return row;
        }

        private int FindColumn(double[] delta)
        {
            int column = 0;
            double maxMin = delta[0];
            for (int i = 1; i < delta.Length - 1; i++)
            {
                if ((isMin && delta[i] >= maxMin) || (!isMin && delta[i] <= maxMin))
                {
                    maxMin = delta[i];
                    column = i;
                }
            }
            return column;
        }

        private bool CheckDelta(double[] delta)
        {
            for (int i = 0; i < delta.Length - 1; i++)
            {
                if ((isMin && delta[i] > 0) || (!isMin && delta[i] < 0))
                {
                    return false;
                }
            }
            return true;
        }

        private double[] CalculateDelta()
        {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            double[] delta = new double[m];
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (basis[i] < targetFunc.Length)
                    {
                        delta[j] += targetFunc[basis[i]] * table[i, j];
                    }
                }
                if (j < targetFunc.Length)
                {
                    delta[j] -= targetFunc[j];
                }
            }
            return delta;
        }


        private void SetValues()
        {
            values = new double[targetFunc.Length];
            int m = table.GetLength(1);
            for (int i = 0; i < values.Length; i++)
            {
                int j = basis.IndexOf(i);
                if (j != -1)
                {
                    values[i] = table[j, m - 1];
                    valueTargetFunc += values[i] * targetFunc[i];
                }
            }
        }

        private void ConvertTable(int row, int column)
        {
            basis[row] = column;
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            double[,] tempTable = new double[n, m];
            for (int j = 0; j < m; j++)
            {
                tempTable[row, j] = table[row, j] / table[row, column];
            }
            for (int i = 0; i < n; i++)
            {
                if (i == row)
                {
                    continue;
                }
                for (int j = 0; j < m; j++)
                {
                    tempTable[i, j] = table[i, j] - table[i, column] * tempTable[row, j];
                }
            }
            table = tempTable;
        }

        private int FindRow()
        {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            int row = 0;
            double min = table[0, m - 1];
            for (int i = 1; i < n; i++)
            {
                if (table[i, m - 1] <= min)
                {
                    min = table[i, m - 1];
                    row = i;
                }
            }
            return row;
        }

        private int FindColumn(int row)
        {
            int m = table.GetLength(1);
            int column = 0;
            double min = table[row, 0];
            for (int i = 1; i < m - 1; i++)
            {
                if (table[row, i] <= min)
                {
                    min = table[row, i];
                    column = i;
                }
            }
            return column;
        }

        private bool IsPosNegLimits()
        {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                if (table[i, m - 1] < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public double[] GetValues()
        {
            return values;
        }

        public double GetTargetFuncValue()
        {
            return valueTargetFunc;
        }
    }
}
