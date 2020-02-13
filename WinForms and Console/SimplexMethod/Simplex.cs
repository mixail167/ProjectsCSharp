namespace SimplexMethod
{
    class Simplex
    {
        double[,] table;

        public Simplex(double[,] tableKoef, double[] limits, double[] centralFunc)
        {
            int n = tableKoef.GetLength(0);
            int m = tableKoef.GetLength(1);
            table = new double[n + 1, m + 1];
            for (int i = 0; i < n; i++)
            {
                table[i + 1, 0] = limits[i];
                for (int j = 0; j < m; j++)
                {
                    table[0, j + 1] = centralFunc[j];
                    table[i + 1, j + 1] = tableKoef[i, j];
                }
            }
        }

        public bool Calculate()
        {
            while (true)
            {
                if (IsOptimalPlan())
                {
                    return true;
                }
                else if (IsUnsolvability())
                {
                    return false;
                }
                else
                {
                    int column = FindColumn();
                    int row = FindRow(column);
                    ConvertTable(row, column);
                }
            }
        }

        private void ConvertTable(int row, int column)
        {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            double[,] tempTable = new double[n, m];
            for (int i = 0; i < m; i++)
            {
                tempTable[row, i] = table[row, i] / table[row, column];
            }
            for (int i = 0; i < n; i++)
            {
                if (i != row)
                {
                    for (int j = 0; j < m; j++)
                    {
                        tempTable[i, j] = table[i, j] - table[i, column] * tempTable[row, j];
                    }
                }
            }
            table = tempTable;
        }

        private int FindRow(int column)
        {
            int n = table.GetLength(0);
            int row = 0;
            for (int i = 1; i < n && row == 0; i++)
            {
                if (table[i, column] > 0)
                {
                    row = i;
                }
            }
            for (int i = row + 1; i < n; i++)
            {
                if (table[i, column] > 0)
                {
                    double delta1 = table[i, 0] / table[i, column];
                    double delta2 = table[row, 0] / table[row, column];
                    if (delta1 < delta2)
                    {
                        row = i;
                    }
                }
            }
            return row;
        }

        private int FindColumn()
        {
            int n = table.GetLength(1);
            int column = 0;
            for (int i = 1; i < n && column == 0; i++)
            {
                if (table[0, i] > 0)
                {
                    column = i;
                }
            }
            for (int i = column + 1; i < n; i++)
            {
                if (table[0, i] > 0 && table[0, i] >= table[0, column])
                {
                    column = i;
                }
            }
            return column;
        }

        private bool IsUnsolvability()
        {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            for (int i = 1; i < m; i++)
            {
                if (table[0, i] > 0)
                {
                    for (int j = 1; j < n; j++)
                    {
                        if (table[j, i] > 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool IsOptimalPlan()
        {
            int n = table.GetLength(1);
            for (int i = 1; i < n; i++)
            {
                if (table[0, i] > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public double[] GetValues()
        {
            int n = table.GetLength(0);
            double[] values = new double[n - 1];
            for (int i = 1; i < n; i++)
            {
                values[i - 1] = table[i, 0];
            }
            return values;
        }

        public double GetCentralFuncValue()
        {
            return -table[0, 0];
        }
    }
}
