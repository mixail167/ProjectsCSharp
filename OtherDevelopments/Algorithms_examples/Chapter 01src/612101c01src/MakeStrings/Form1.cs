using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeStrings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void makeStringsButton_Click(object sender, EventArgs e)
        {
            // Make strings.
            int numLetters = int.Parse(numLettersTextBox.Text);
            List<string> strings = new List<string>();
            MakeStrings(strings, "", numLetters);

            // Only at most 1000 values.
            stringsListBox.DataSource = strings.Take(1000).ToArray();
            int count = strings.Count();
            countLabel.Text = Math.Min(1000, count) + " of " + count.ToString("0,000") + " strings shown";
        }

        // Recursively make strings that use the indicated prefix.
        private void MakeStrings(List<string> strings, string prefix, int numLetters)
        {
            // Make strings starting with every letter.
            foreach (char ch in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                // Make strings with one fewer letters.
                if (numLetters == 1) strings.Add(prefix + ch);
                else MakeStrings(strings, prefix + ch, numLetters - 1);
            }
        }
    }
}
