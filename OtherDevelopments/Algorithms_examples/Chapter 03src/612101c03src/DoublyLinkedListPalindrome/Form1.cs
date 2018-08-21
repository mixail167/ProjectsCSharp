using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoublyLinkedListPalindrome
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
            LetterCell sentinel = new LetterCell(txt);

            // Check for a palindrome in various ways.
            if (sentinel.IsListPalindrome())
                atCellLabel.Text = "Is a palindrome";
            else
                atCellLabel.Text = "Not a palindrome";
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
