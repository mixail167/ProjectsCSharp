using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangularArrays
{
    class TriangularArray<T>
    {
        // The linear array where we store items.
        private T[] Values;
        private int NumRows;

        // Initialize the array by allocating the Values array.
        public TriangularArray(int numRows)
        {
            NumRows = numRows;
            int numItems = NumCellsForRows(numRows);
            Values = new T[numItems];
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
        public T this[int row, int col]
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
    }
}
