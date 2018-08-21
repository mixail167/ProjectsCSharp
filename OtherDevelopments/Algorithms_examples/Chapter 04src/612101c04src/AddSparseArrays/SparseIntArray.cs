using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddSparseArrays
{
    class SparseIntArray
    {
        // Holds data to represnet a row.
        private class IntArrayRow
        {
            public int RowNumber;
            public IntArrayRow NextRow;
            public IntArrayEntry RowSentinel;
        }

        // Holds data for an array entry.
        public class IntArrayEntry
        {
            public int ColumnNumber;
            public int Value;
            public IntArrayEntry NextEntry;
        }

        // The default value for empty entries.
        public int DefaultValue;

        // The row sentinel.
        private IntArrayRow TopSentinel;

        // Create the sentinels.
        public SparseIntArray(int defaultValue)
        {
            // Save the default value.
            DefaultValue = defaultValue;

            // Create the row sentinel.
            TopSentinel = new IntArrayRow();
            TopSentinel.NextRow = null;
        }

        // Get or set an array value.
        public int this[int row, int col]
        {
            get
            {
                // Find the entry.
                IntArrayEntry entry = FindEntry(row, col, false);

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
                    IntArrayEntry entry = FindEntry(row, col, true);

                    // Save the value.
                    entry.Value = value;
                }
            }
        }

        // Find the ArrayRow before this row.
        private IntArrayRow FindRowBefore(int row)
        {
            // Start at the sentinel.
            IntArrayRow arrayRow = TopSentinel;

            // Find the row before the required one.
            while ((arrayRow.NextRow != null) && (arrayRow.NextRow.RowNumber < row))
                arrayRow = arrayRow.NextRow;

            // Return the ArrayRow before the row or null.
            return arrayRow;
        }

        // Find the ArrayRow for this row.
        // If create is true, create the ArrayRow if it doesn't exist.
        private IntArrayRow FindRow(int row, bool create)
        {
            // Find the ArrayRow before the one we want.
            IntArrayRow before = FindRowBefore(row);

            // If we found it, return it.
            if ((before.NextRow != null) && (before.NextRow.RowNumber == row))
                return before.NextRow;

            // We didn't find it. See if we should create it.
            if (create)
            {
                // Create the new ArrayRow.
                IntArrayRow newRow = new IntArrayRow();
                newRow.RowNumber = row;

                // Insert it in the row list.
                newRow.NextRow = before.NextRow;
                before.NextRow = newRow;

                // Create the row's sentinel.
                newRow.RowSentinel = new IntArrayEntry();
                newRow.RowSentinel.NextEntry = null;

                // Return it.
                return newRow;
            }

            // We didn't find it and shouldn't create it. Return null.
            return null;
        }

        // Find the ArrayEntry for this column.
        private IntArrayEntry FindColumnBefore(IntArrayEntry entry, int col)
        {
            // Find the entry before the required one.
            while ((entry.NextEntry != null) && (entry.NextEntry.ColumnNumber < col))
                entry = entry.NextEntry;

            // Return the ArrayRow before the row or null.
            return entry;
        }

        // Find the ArrayEntry for this column.
        // If create is true, create the ArrayEntry if it doesn't exist.
        private IntArrayEntry FindColumn(IntArrayEntry entry, int col, bool create)
        {
            // Find the entry before the required one.
            IntArrayEntry before = FindColumnBefore(entry, col);

            // If we found it, return it.
            if ((before.NextEntry != null) && (before.NextEntry.ColumnNumber == col))
                return before.NextEntry;

            // We didn't find it. See if we should create it.
            if (create)
            {
                // Create the new ArrayEntry.
                IntArrayEntry newEntry = new IntArrayEntry();
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
        private IntArrayEntry FindEntry(int row, int col, bool create)
        {
            // Find the entry's row.
            IntArrayRow arrayRow = FindRow(row, create);

            // If we didn't find it (or create it), return null.
            if (arrayRow == null) return null;

            // Find the entry in this row and return it.
            return FindColumn(arrayRow.RowSentinel, col, create);
        }

        // Delete the indicated entry if it exists.
        public void DeleteEntry(int row, int col)
        {
            // Find the row before the entry's row.
            IntArrayRow rowBefore = FindRowBefore(row);

            // If we didn't find the row, we're done.
            IntArrayRow arrayRow = rowBefore.NextRow;
            if ((arrayRow == null) || (arrayRow.RowNumber != row)) return;

            // Find the entry before this entry's entry.
            IntArrayEntry entryBefore = FindColumnBefore(arrayRow.RowSentinel, col);
            IntArrayEntry arrayEntry = entryBefore.NextEntry;

            // If we didn't find the entry, we're done.
            if ((arrayEntry == null) || (arrayEntry.ColumnNumber != col)) return;

            // Remove the entry from the row's list.
            entryBefore.NextEntry = arrayEntry.NextEntry;
            // arrayEntry.NextEntry = null;
            // free(arrayEntry);

            // If there are no more entries in the row, remove it.
            IntArrayEntry arraySentinel = arrayRow.RowSentinel;
            if (arraySentinel.NextEntry == null)
            {
                rowBefore.NextRow = arrayRow.NextRow;
                // arrayRow.RowSentinel = null;
                // free(arraySentinel);
                // free(arrayRow);
            }
        }

        // Return a textual representation of the array.
        public override string ToString()
        {
            string result = "";
            SparseIntArray.IntArrayRow row = TopSentinel.NextRow;
            while (row != null)
            {
                SparseIntArray.IntArrayEntry value = row.RowSentinel.NextEntry;
                while (value != null)
                {
                    result += "(" + row.RowNumber.ToString() + ", " +
                        value.ColumnNumber.ToString() + ")" +
                        value.Value.ToString() + " ";
                    value = value.NextEntry;
                }
                result += Environment.NewLine;
                row = row.NextRow;
            }
            return result;
        }

        // Copy the entries starting at fromEntry into
        // the destination entry list after toEntry.
        private void CopyEntries(IntArrayEntry fromEntry, IntArrayEntry toEntry)
        {
            while (fromEntry != null)
            {
                toEntry.NextEntry = new IntArrayEntry();
                toEntry = toEntry.NextEntry;
                toEntry.ColumnNumber = fromEntry.ColumnNumber;
                toEntry.Value = fromEntry.Value;
                toEntry.NextEntry = null;

                // Move to the next entry.
                fromEntry = fromEntry.NextEntry;
            }
        }

        // Add the entries in the two lists fromEntry1 and fromEntry2
        // and save the sums in the destination entry list after toEntry.
        private void AddEntries(IntArrayEntry fromEntry1, IntArrayEntry fromEntry2, IntArrayEntry toEntry)
        {
            // Repeat as long as either from list has items.
            while ((fromEntry1 != null) && (fromEntry2 != null))
            {
                // Make the new result entry.
                toEntry.NextEntry = new IntArrayEntry();
                toEntry = toEntry.NextEntry;
                toEntry.NextEntry = null;

                // See which column number is smaller.
                if (fromEntry1.ColumnNumber < fromEntry2.ColumnNumber)
                {
                    // Copy the fromEntry1 entry.
                    toEntry.ColumnNumber = fromEntry1.ColumnNumber;
                    toEntry.Value = fromEntry1.Value;
                    fromEntry1 = fromEntry1.NextEntry;
                }
                else if (fromEntry2.ColumnNumber < fromEntry1.ColumnNumber)
                {
                    // Copy the fromEntry2 entry.
                    toEntry.ColumnNumber = fromEntry2.ColumnNumber;
                    toEntry.Value = fromEntry2.Value;
                    fromEntry2 = fromEntry2.NextEntry;
                }
                else
                {
                    // The column numbers are the same. Add both entries.
                    toEntry.ColumnNumber = fromEntry1.ColumnNumber;
                    toEntry.Value = fromEntry1.Value + fromEntry2.Value;
                    fromEntry1 = fromEntry1.NextEntry;
                    fromEntry2 = fromEntry2.NextEntry;
                }
            }

            // Add the rest of the entries from the list that is not empty.
            if (fromEntry1 != null) CopyEntries(fromEntry1, toEntry);
            if (fromEntry2 != null) CopyEntries(fromEntry2, toEntry);
        }

        // Add two SparseArrays representing matrices.
        public SparseIntArray Add(SparseIntArray other)
        {
            SparseIntArray result = new SparseIntArray(this.DefaultValue);

            // Variables to move through all of the arrays.
            IntArrayRow array1Row = this.TopSentinel.NextRow;
            IntArrayRow array2Row = other.TopSentinel.NextRow;
            IntArrayRow resultRow = result.TopSentinel;

            while ((array1Row != null) && (array2Row != null))
            {
                // Make a new result row.
                resultRow.NextRow = new IntArrayRow();
                resultRow = resultRow.NextRow;
                resultRow.RowSentinel = new IntArrayEntry();
                resultRow.NextRow = null;

                // See which input row has the smaller row number.
                if (array1Row.RowNumber < array2Row.RowNumber)
                {
                    // array1Row comes first. Copy its values into result.
                    resultRow.RowNumber = array1Row.RowNumber;
                    CopyEntries(array1Row.RowSentinel.NextEntry, resultRow.RowSentinel);
                    array1Row = array1Row.NextRow;
                }
                else if (array2Row.RowNumber < array1Row.RowNumber)
                {
                    // array2Row comes first. Copy its values into result.
                    resultRow.RowNumber = array2Row.RowNumber;
                    CopyEntries(array2Row.RowSentinel.NextEntry, resultRow.RowSentinel);
                    array2Row = array2Row.NextRow;
                }
                else
                {
                    // The row numbers are the same. Add their values.
                    resultRow.RowNumber = array1Row.RowNumber;
                    AddEntries(
                        array1Row.RowSentinel.NextEntry,
                        array2Row.RowSentinel.NextEntry,
                        resultRow.RowSentinel);
                    array1Row = array1Row.NextRow;
                    array2Row = array2Row.NextRow;
                }
            }

            // Add any remaining rows.
            if (array1Row != null)
            {
                // Make a new result row.
                resultRow.NextRow = new IntArrayRow();
                resultRow = resultRow.NextRow;
                resultRow.RowNumber = array1Row.RowNumber;
                resultRow.RowSentinel = new IntArrayEntry();
                resultRow.NextRow = null;
                CopyEntries(array1Row.RowSentinel.NextEntry, resultRow.RowSentinel);
            }
            if (array2Row != null)
            {
                // Make a new result row.
                resultRow.NextRow = new IntArrayRow();
                resultRow = resultRow.NextRow;
                resultRow.RowNumber = array2Row.RowNumber;
                resultRow.RowSentinel = new IntArrayEntry();
                resultRow.NextRow = null;
                CopyEntries(array2Row.RowSentinel.NextEntry, resultRow.RowSentinel);
            }

            return result;
        }
    }
}
