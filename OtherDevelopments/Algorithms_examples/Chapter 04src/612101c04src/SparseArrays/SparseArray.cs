using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparseArrays
{
    class ArrayValue<T>
    {
        public int Row;
        public int Column;
        public T Value;
    }

    class SparseArray<T>
    {
        // Holds data to represnet a row.
        private class ArrayRow
        {
            public int RowNumber;
            public ArrayRow NextRow;
            public ArrayEntry RowSentinel;
        }

        // Holds data for an array entry.
        private class ArrayEntry
        {
            public int ColumnNumber;
            public T Value;
            public ArrayEntry NextEntry;
        }

        // The default value for empty entries.
        private T DefaultValue;

        // The row sentinel.
        private ArrayRow TopSentinel;

        // Create the sentinels.
        public SparseArray(T defaultValue)
        {
            // Save the default value.
            DefaultValue = defaultValue;

            // Create the row sentinel.
            TopSentinel = new ArrayRow();
            TopSentinel.NextRow = null;
        }

        // Get or set an array value.
        public T this[int row, int col]
        {
            get
            {
                // Find the entry.
                ArrayEntry entry = FindEntry(row, col, false);

                // If we didn't find it, return the default value.
                if (entry == null) return DefaultValue;

                // Return the entry's value.
                return entry.Value;
            }
            set
            {
                // See if this is the default value.
                if (value.Equals(DefaultValue))
                {
                    // Remove the entry from the array.
                    DeleteEntry(row, col);
                }
                else
                {
                    // Save the new value.
                    // Find the entry, creating it if necessary.
                    ArrayEntry entry = FindEntry(row, col, true);

                    // Save the value.
                    entry.Value = value;
                }
            }
        }

        // Find the ArrayRow before this row.
        private ArrayRow FindRowBefore(int row)
        {
            // Start at the sentinel.
            ArrayRow arrayRow = TopSentinel;

            // Find the row before the required one.
            while ((arrayRow.NextRow != null) && (arrayRow.NextRow.RowNumber < row))
                arrayRow = arrayRow.NextRow;

            // Return the ArrayRow before the row or null.
            return arrayRow;
        }

        // Find the ArrayRow for this row.
        // If create is true, create the ArrayRow if it doesn't exist.
        private ArrayRow FindRow(int row, bool create)
        {
            // Find the ArrayRow before the one we want.
            ArrayRow before = FindRowBefore(row);

            // If we found it, return it.
            if ((before.NextRow != null) && (before.NextRow.RowNumber == row))
                return before.NextRow;

            // We didn't find it. See if we should create it.
            if (create)
            {
                // Create the new ArrayRow.
                ArrayRow newRow = new ArrayRow();
                newRow.RowNumber = row;

                // Insert it in the row list.
                newRow.NextRow = before.NextRow;
                before.NextRow = newRow;

                // Create the row's sentinel.
                newRow.RowSentinel = new ArrayEntry();
                newRow.RowSentinel.NextEntry = null;

                // Return it.
                return newRow;
            }

            // We didn't find it and shouldn't create it. Return null.
            return null;
        }

        // Find the ArrayEntry for this column.
        private ArrayEntry FindColumnBefore(ArrayEntry entry, int col)
        {
            // Find the entry before the required one.
            while ((entry.NextEntry != null) && (entry.NextEntry.ColumnNumber < col))
                entry = entry.NextEntry;

            // Return the ArrayRow before the row or null.
            return entry;
        }

        // Find the ArrayEntry for this column.
        // If create is true, create the ArrayEntry if it doesn't exist.
        private ArrayEntry FindColumn(ArrayEntry entry, int col, bool create)
        {
            // Find the entry before the required one.
            ArrayEntry before = FindColumnBefore(entry, col);

            // If we found it, return it.
            if ((before.NextEntry != null) && (before.NextEntry.ColumnNumber == col))
                return before.NextEntry;

            // We didn't find it. See if we should create it.
            if (create)
            {
                // Create the new ArrayEntry.
                ArrayEntry newEntry = new ArrayEntry();
                newEntry.ColumnNumber = col;

                // Insert it in the row's column list.
                newEntry.NextEntry = before.NextEntry;
                before.NextEntry = newEntry;

                // Return it.
                return newEntry;
            }

            // We didn't find it and shouldn't create it. Return null.
            return null;
        }

        // Find the ArrayEntry for this row and column.
        // If create is true, create the ArrayEntry if it doesn't exist.
        private ArrayEntry FindEntry(int row, int col, bool create)
        {
            // Find the entry's row.
            ArrayRow arrayRow = FindRow(row, create);

            // If we didn't find it (or create it), return null.
            if (arrayRow == null) return null;

            // Find the entry in this row and return it.
            return FindColumn(arrayRow.RowSentinel, col, create);
        }

        // Delete the indicated entry if it exists.
        public void DeleteEntry(int row, int col)
        {
            // Find the row before the entry's row.
            ArrayRow rowBefore = FindRowBefore(row);

            // If we didn't find the row, we're done.
            ArrayRow arrayRow = rowBefore.NextRow;
            if ((arrayRow == null) || (arrayRow.RowNumber != row)) return;

            // Find the entry before this entry's entry.
            ArrayEntry entryBefore = FindColumnBefore(arrayRow.RowSentinel, col);
            ArrayEntry arrayEntry = entryBefore.NextEntry;

            // If we didn't find the entry, we're done.
            if ((arrayEntry == null) || (arrayEntry.ColumnNumber != col)) return;

            // Remove the entry from the row's list.
            entryBefore.NextEntry = arrayEntry.NextEntry;
            // arrayEntry.NextEntry = null;
            // free(arrayEntry);

            // If there are no more entries in the row, remove it.
            ArrayEntry arraySentinel = arrayRow.RowSentinel;
            if (arraySentinel.NextEntry == null)
            {
                rowBefore.NextRow = arrayRow.NextRow;
                // arrayRow.RowSentinel = null;
                // free(arraySentinel);
                // free(arrayRow);
            }
        }

        // Iterator.
        public IEnumerator<ArrayValue<T>> GetEnumerator()
        {
            ArrayRow arrayRow = TopSentinel.NextRow;
            while (arrayRow != null)
            {
                ArrayEntry arrayEntry = arrayRow.RowSentinel.NextEntry;
                while (arrayEntry != null)
                {
                    yield return new ArrayValue<T>
                    {
                        Row = arrayRow.RowNumber,
                        Column = arrayEntry.ColumnNumber,
                        Value = arrayEntry.Value
                    };
                    arrayEntry = arrayEntry.NextEntry;
                }
                arrayRow = arrayRow.NextRow;
            }
        }
    }
}
