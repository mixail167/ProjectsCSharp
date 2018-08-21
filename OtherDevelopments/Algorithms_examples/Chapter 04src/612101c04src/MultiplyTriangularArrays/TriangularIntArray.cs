using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplyTriangularArrays
{
    class TriangularIntArray
    {
        // The linear array where we store items.
        private int[] Values;
        private int NumRows;

        // Initialize the array by allocating the Values array.
        public TriangularIntArray(int numRows)
        {
            NumRows = numRows;
            int numItems = NumCellsForRows(numRows);
            Values = new int[numItems];
        }

        // Convert a row and column into a linear array index.
        private int RowColumnToIndex(int row, int col)
        {
            if (row >= NumRows)
            {
                throw new ArgumentOutOfRangeException("row", row,
                    "The row value " + row.ToString() +
                    " must be less than the number of rows " +
                    NumRows.ToString());
            }
            if (col >= NumRows)
            {
                throw new ArgumentOutOfRangeException("col", col,
                    "The column value " + col.ToString() +
                    " must be less than the number of rows " +
                    NumRows.ToString());
            }

            // Convert upper-triangular to lower-triangular.
            if (col > row)
            {
                int temp = col;
                col = row;
                row = temp;
            }

            // Return the index.
            return NumCellsForRows(row) + col;
        }

        // Return the number of cells in an array with this many rows.
        private int NumCellsForRows(int rows)
        {
            return (rows * rows + rows) / 2;
        }

        // Get or set an array value.
        public int this[int row, int col]
        {
            get
            {
                int index = RowColumnToIndex(row, col);
                return Values[index];
            }
            set
            {
                int index = RowColumnToIndex(row, col);
                Values[index] = value;
            }
        }

        // Return a textual representation of the array.
        public override string ToString()
        {
            string result = "";
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    result += this[row, col].ToString() + " ";
                }
                result += Environment.NewLine;
            }
            return result;
        }

        // Multiply this array on the right by another array.
        public TriangularIntArray TimesFull(TriangularIntArray other)
        {
            TriangularIntArray result = new TriangularIntArray(this.NumRows);
            for (int i = 0; i < NumRows; i++)
            {
                for (int j = 0; j < NumRows; j++)
                {
                    // Calculate the [i, j] entry.
                    int total = 0;
                    for (int k = 0; k < NumRows; k++)
                    {
                        if ((i >= k) && (k >= j)) total += this[i, k] * other[k, j];
                    }
                    result[i, j] = total;
                }
            }
            return result;
        }

        // Multiply this array on the right by another array.
        public TriangularIntArray Times(TriangularIntArray other)
        {
            TriangularIntArray result = new TriangularIntArray(this.NumRows);
            for (int i = 0; i < NumRows; i++)
            {
                for (int j = 0; j < NumRows; j++)
                {
                    // Calculate the [i, j] entry.
                    int total = 0;
                    for (int k = j; k <= i; k++)
                    {
                        total += this[i, k] * other[k, j];
                    }
                    result[i, j] = total;
                }
            }
            return result;
        }
    }
}
