using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedSorts
{
    class ValueCell
    {
        public int Value;
        public ValueCell Next;

        // Make a copy of the list (non-recursively).
        public ValueCell CopyList()
        {
            // Make a new top for the list.
            ValueCell newTop = new ValueCell();
            newTop.Value = this.Value;

            // Make a cell to hold the last cell in the new list.
            ValueCell lastCell = newTop;

            // Make the other cells.
            for (ValueCell cell = this.Next; cell != null; cell = cell.Next)
            {
                // Make the new cell.
                ValueCell newCell = new ValueCell();
                newCell.Value = cell.Value;

                // Add it to the list.
                lastCell.Next = newCell;
                lastCell = newCell;
            }

            // End the list.
            lastCell.Next = null;

            // Return the copy.
            return newTop;
        }

        // Use selectionsort to sort the list after this cell.
        // (This assumes we are the sentinel.)
        public void Selectionsort()
        {
            // Make a new top cell reference.
            // Note that the new list doesn't have a sentinel.
            ValueCell newTop = null;

            // Repeat until the list is empty.
            while (this.Next != null)
            {
                // Find the next biggest value in the list.
                ValueCell bestBefore = this;
                int bestValue = bestBefore.Next.Value;

                // Search the other cells to find a bigger value.
                for (ValueCell cell = bestBefore.Next; cell.Next != null; cell = cell.Next)
                {
                    // See if the next cell's value is bigger than
                    // the biggest value we've found so far.
                    if (cell.Next.Value > bestValue)
                    {
                        // Replace the biggest value found.
                        bestValue = cell.Next.Value;
                        bestBefore = cell;
                    }
                }

                // Remove the best cell from the old list.
                ValueCell bestCell = bestBefore.Next;
                bestBefore.Next = bestCell.Next;

                // Add the cell to the top of the new list.
                bestCell.Next = newTop;
                newTop = bestCell;
            }

            // Make this cell be the sentinel for the new list.
            this.Next = newTop;
        }

        // Use insertionsort to sort the list after this cell.
        // (This assumes we are the sentinel.)
        public void Insertionsort()
        {
            // Make a sentinel for the new list.
            ValueCell newTop = new ValueCell() { Next = null };

            // Repeat until the list is empty.
            while (this.Next != null)
            {
                // Remove the next cell from the original list.
                ValueCell cell = this.Next;
                int cellValue = cell.Value;
                this.Next = cell.Next;

                // Insert it in the new list.
                for (ValueCell before = newTop; ; before = before.Next)
                {
                    // If before.Next is null or the next
                    // cell's value >= this one, insert the item here.
                    if ((before.Next == null) || (before.Next.Value >= cellValue))
                    {
                        // Insert the new value after before.
                        cell.Next = before.Next;
                        before.Next = cell;
                        break;
                    }
                }
            }

            // Make this cell be the sentinel for the new list.
            this.Next = newTop.Next;
        }

        // Verify that the list below this item is sorted (non-recursively).
        public bool IsSorted()
        {
            // An empty list is sorted.
            if (this.Next == null) return true;

            for (ValueCell cell = this.Next; cell.Next != null; cell = cell.Next)
            {
                if (cell.Value > cell.Next.Value) return false;
            }

            // If we get this far, the list is sorted.
            return true;
        }
    }
}
