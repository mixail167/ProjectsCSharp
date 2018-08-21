using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIsPalindrome
{
    class LetterCell
    {
        public char Letter;
        public LetterCell Next;

        // Parameterless constructor.
        public LetterCell()
        {
            Next = null;
        }

        // Make a list to represent a string.
        public LetterCell(string txt)
        {
            // Make it into a list.
            LetterCell firstCell = null;
            foreach (char ch in txt)
            {
                LetterCell cell = new LetterCell();
                cell.Letter = ch;
                cell.Next = firstCell;
                firstCell = cell;
            }

            // Make this cell be the sentinel.
            Next = firstCell;
        }

        // Return a string representing this
        // cell and those that follow.
        public override string ToString()
        {
            string result = "";
            for (LetterCell cell = this; cell != null; cell = cell.Next)
            {
                result += cell.Letter;
            }
            return result;
        }

        // Use a reversed list to return true if the
        // cells after this one form a palindrome.
        public bool IsPalindromeReverse()
        {
            // Make a reversed list.
            LetterCell newTop = new LetterCell();
            newTop.Next = null;
            for (LetterCell oldCell = this.Next; oldCell != null; oldCell = oldCell.Next)
            {
                LetterCell newCell = new LetterCell();
                newCell.Letter = oldCell.Letter;
                newCell.Next = newTop.Next;
                newTop.Next = newCell;
            }

            // Compare the lists.
            LetterCell cell1 = this.Next;
            LetterCell cell2 = newTop.Next;
            while (cell1 != null)
            {
                if (cell1.Letter != cell2.Letter) return false;
                cell1 = cell1.Next;
                cell2 = cell2.Next;
            }
            return true;
        }

        // Use half a reversed list to return true if the
        // cells after this one form a palindrome.
        public bool IsPalindromeReverseHalf()
        {
            // Lists with 0 or 1 letters are palindromes.
            if ((this.Next == null) || (this.Next.Next == null)) return true;

            // Find the list's halfway point.
            LetterCell secondHalf = FindSecondHalf(false);

            // Make a reversed list.
            LetterCell lastCell = null;
            for (LetterCell oldCell = secondHalf; oldCell != null; oldCell = oldCell.Next)
            {
                LetterCell newCell = new LetterCell();
                newCell.Letter = oldCell.Letter;
                newCell.Next = lastCell;
                lastCell = newCell;
            }

            // Compare the lists.
            LetterCell cell1 = lastCell;
            LetterCell cell2 = this.Next;
            while (cell1 != null)
            {
                if (cell1.Letter != cell2.Letter) return false;
                cell1 = cell1.Next;
                cell2 = cell2.Next;
            }
            return true;
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

        // Recursively see if the
        // cells after this one form a palindrome.
        public bool IsPalindromeRecursive()
        {
            // Find the start of the second half.
            LetterCell secondHalf = FindSecondHalf(false);

            // Recursively compare the second and first halves.
            LetterCell firstHalf = this.Next;
            return CompareHalves(ref firstHalf, secondHalf);
        }

        // Recursively compare the second and first halves.
        private bool CompareHalves(ref LetterCell firstHalf, LetterCell secondHalf)
        {
            // See if we're at the end of the second half.
            if (secondHalf.Next == null)
            {
                // Compare the last cell in the second half
                // to the first cell in the first half.
                bool ourResult = (secondHalf.Letter == firstHalf.Letter);

                // Move firstHalf to the next item.
                firstHalf = firstHalf.Next;

                // Return so the next level up can continue checking.
                return ourResult;
            }

            // We are not at the end of the second half.
            // Recursively check one cell farther the second half.
            // If the recursive call returns false,
            // this is not a palindrome.
            if (!CompareHalves(ref firstHalf, secondHalf.Next))
                return false;

            // Compare the next cells in the two halves.
            if (firstHalf.Letter != secondHalf.Letter) return false;

            // If we get this far, it is a palindrome so far.
            // Move firstHalf to the next item.
            firstHalf = firstHalf.Next;

            // So far so good.
            return true;
        }
    }
}