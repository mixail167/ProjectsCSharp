using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    public class SortedLinkedList<T> where T : IComparable
    {
        // Holds a single item in the list.
        private class ListCell
        {
            public T Value;
            public ListCell Next;
        }

        // The top sentinel.
        private ListCell Sentinel = new ListCell() { Next = null };

        // Add an item to the list.
        public void Add(T value)
        {
            // Find the cell before the one holding the item.
            ListCell before = FindCellBefore(value);

            // If you like, make sure the item isn't already in the list.
            if ((before.Next != null) && (before.Next.Value.CompareTo(value) == 0))
                throw new ArgumentException(
                    "Duplicate value. The SortedList already holds the value " +
                    value.ToString(), "value");

            // Insert the new item.
            ListCell newCell = new ListCell();
            newCell.Value = value;
            newCell.Next = before.Next;
            before.Next = newCell;
        }

        // Delete an item from the list.
        public void Delete(T value)
        {
            // Find the cell before the one holding the item.
            ListCell before = FindCellBefore(value);

            // If there's no such value, throw an exception.
            if ((before.Next == null) || (!before.Next.Value.Equals(value)))
                throw new ArgumentOutOfRangeException("value", value,
                    "Indicated value is not in the list.");

            // Delete the cell containing the item.
            ListCell cell = before.Next;
            before.Next = cell.Next;
            // cell.Next = null;
            // free(cell);
        }

        // Return the last cell before the indicated value.
        private ListCell FindCellBefore(T value)
        {
            ListCell before = Sentinel;
            while ((before.Next != null) && (before.Next.Value.CompareTo(value) < 0))
                before = before.Next;
            return before;
        }

        // Iterator.
        public IEnumerator<T> GetEnumerator()
        {
            // Find the cell before the one holding the item.
            ListCell cell = Sentinel.Next;
            while (cell != null)
            {
                yield return cell.Value;
                cell = cell.Next;
            }
        }
    }
}
