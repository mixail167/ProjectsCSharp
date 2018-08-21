using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListIsPalindrome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // See if the string is a palindrome.
        private void checkButton_Click(object sender, EventArgs e)
        {
            // Make the string into a list.
            string txt = stringTextBox.Text.ToUpper();
            txt = RemoveNonLetters(txt);

            // Make it into a list.
            LetterCell firstCell = null;
            foreach (char ch in txt)
            {
                LetterCell cell = new LetterCell();
                cell.Letter = ch;
                cell.Next = firstCell;
                firstCell = cell;
            }

            // Add a sentinel.
            LetterCell sentinel = new LetterCell();
            sentinel.Next = firstCell;

            // Check for a palindrome in various ways.
            if (sentinel.IsPalindromeReverse())
                reverseLabel.Text = "Is a palindrome";
            else
                reverseLabel.Text = "Not a palindrome";

            if (sentinel.IsPalindromeReverseHalf())
                reverseHalfLabel.Text = "Is a palindrome";
            else
                reverseHalfLabel.Text = "Not a palindrome";

            if (sentinel.IsPalindromeRecursive())
                recursiveLabel.Text = "Is a palindrome";
            else
                recursiveLabel.Text = "Not a palindrome";
        }

        // Remove all non-letters from the string.
        private string RemoveNonLetters(string txt)
        {
            string result = "";
            foreach (char ch in txt)
            {
                if (char.IsLetter(ch)) result += ch;
            }
            return result;
        }
    }
}
