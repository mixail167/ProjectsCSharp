using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSecondHalf
{
    class LetterCell
    {
        public char Letter;
        public LetterCell Next;

        // Parameterless constructor.
        public LetterCell()
        {
        }

        // Make a list from a string.
        public LetterCell(string txt)
        {
            // Add each letter to the end.
            this.Next = null;
            LetterCell lastCell = this;
            foreach (char ch in txt)
            {
                LetterCell newCell = new LetterCell();
                newCell.Letter = ch;
                newCell.Next = null;

                lastCell.Next = newCell;
                lastCell = newCell;
            }
        }

        // Return the text in this cell and those that follow.
        public override string ToString()
        {
            string result = "";
            for (LetterCell cell = this; cell != null; cell = cell.Next)
            {
                result += cell.Letter;
            }
            return result;
        }

        // Return the first cell in the second half of the list.
        // If the list has an odd number of cells and
        // countMiddleCell is true, return the middle cell.
        // If the list has an odd number of cells and
        // countMiddleCell is false, return the cell after the middle.
        public LetterCell FindSecondHalf(bool countMiddleCell)
        {
            // Find the list's halfway point.
            LetterCell slowCell = this;
            LetterCell fastCell = slowCell;
            for (; ; )
            {
                // Move slowCell.
                slowCell = slowCell.Next;

                // Move fastCell.
                fastCell = fastCell.Next;
                if (fastCell == null)
                {
                    // The list has an even number of items and
                    // slowCell is the first cell in the second half.
                    return slowCell;
                }

                fastCell = fastCell.Next;
                if (fastCell == null)
                {
                    // The list has an odd number of items
                    // and slowCell is the middle item.
                    if (!countMiddleCell) slowCell = slowCell.Next;
                    return slowCell;
                }
            }
        }
    }
}
