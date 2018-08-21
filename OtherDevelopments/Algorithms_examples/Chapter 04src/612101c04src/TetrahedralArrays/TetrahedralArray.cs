using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrahedralArrays
{
    class TetrahedralArray<T>
    {
        // The linear array where we store items.
        private T[] Values;
        private int NumRows;

        // Initialize the array by allocating the Values array.
        public TetrahedralArray(int numRows)
        {
            NumRows = numRows;
            int numItems = NumCellsForTetrahedralRows(numRows);
            Values = new T[numItems];
        }

        // Convert a row and column into a linear array index.
        private int RowColumnHeightToIndex(int row, int col, int hgt)
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
            if (hgt >= NumRows)
            {
                throw new ArgumentOutOfRangeException("hgt", hgt,
                    "The hgt value " + hgt.ToString() +
                    " must be less than the number of rows " +
                    NumRows.ToString());
            }

            // Convert upper-triangular to lower-triangular.
            if (hgt > col)
            {
                int temp = hgt;
                hgt = col;
                col = temp;
            }
            if (col > row)
            {
                int temp = col;
                col = row;
                row = temp;
            }

            // Return the index.
            return NumCellsForTetrahedralRows(row) +
                NumCellsForTriangleRows(col) + hgt;
        }

        // Return the number of cells in a triangular array with this many rows.
        private int NumCellsForTriangleRows(int rows)
        {
            return (rows * rows + rows) / 2;
        }

        // Return the number of cells in a tetrahedral array with this many rows.
        private int NumCellsForTetrahedralRows(int rows)
        {
            return (rows * rows * rows + 3 * rows * rows + 2 * rows) / 6;
        }

        // Get or set an array value.
        public T this[int row, int col, int hgt]
        {
            get
            {
                int index = RowColumnHeightToIndex(row, col, hgt);
                return Values[index];
            }
            set
            {
                int index = RowColumnHeightToIndex(row, col, hgt);
                Values[index] = value;
            }
        }
    }
}
