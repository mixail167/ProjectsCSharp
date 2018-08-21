using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedListPalindrome
{
    class LetterCell
    {
        public char Letter;
        public LetterCell Next, Prev;

        // Parameterless constructor.
        public LetterCell()
        {
            Letter = '\0';
            Next = null;
            Prev = null;
        }

        // Make a list to represent a string.
        public LetterCell(string txt)
        {
            // Create the end sentinel.
            LetterCell endSentinel = new LetterCell();

            // Make it into a list.
            LetterCell firstCell = endSentinel;
            foreach (char ch in txt)
            {
                LetterCell cell = new LetterCell();
                cell.Letter = ch;
                cell.Next = firstCell;
                firstCell.Prev = cell;

                firstCell = cell;
            }

            // Make this cell be the sentinel.
            Next = firstCell;
            firstCell.Prev = this;
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

        // Return true if the cells after this one form a palindrome.
        public bool IsListPalindrome()
        {
            // Check each cell for a palindrome.
            for (LetterCell cell = this.Next; cell != null; cell = cell.Next)
            {
                if (IsPalindromeAtCell(cell)) return true;
            }
            return false;
        }

        // Return true if the cells before and after this one form a palindrome.
        public bool IsPalindromeAtCell(LetterCell cell)
        {
            return
                (IsEvenPalindromeAtCell(cell) ||
                 IsOddPalindromeAtCell(cell));
        }

        // Return true if the cells before and after this
        // one form a palindrome of odd length.
        public bool IsEvenPalindromeAtCell(LetterCell cell)
        {
            // Go forward and backward comparing letters.
            LetterCell NextCell = cell;
            LetterCell PrevCell = cell;
            while ((NextCell != null) && (PrevCell != null))
            {
                if (NextCell.Letter != PrevCell.Letter) return false;
                NextCell = NextCell.Next;
                PrevCell = PrevCell.Prev;
            }
            if ((NextCell != null) || (PrevCell != null)) return false;
            return true;
        }

        // Return true if the cells before this one and this
        // one to the end one form a palindrome of even length.
        public bool IsOddPalindromeAtCell(LetterCell cell)
        {
            // Go forward and backward comparing letters.
            LetterCell NextCell = cell;
            LetterCell PrevCell = cell.Prev;
            while ((NextCell != null) && (PrevCell != null))
            {
                if (NextCell.Letter != PrevCell.Letter) return false;
                NextCell = NextCell.Next;
                PrevCell = PrevCell.Prev;
            }
            if ((NextCell != null) || (PrevCell != null)) return false;
            return true;
        }
    }
}