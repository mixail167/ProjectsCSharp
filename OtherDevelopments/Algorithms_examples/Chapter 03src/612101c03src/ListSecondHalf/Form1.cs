using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListSecondHalf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Display the second halves of the text.
        private void goButton_Click(object sender, EventArgs e)
        {
            halfWoMiddleTextBox.Clear();
            halfWithMiddleTextBox.Clear();

            // Make the list.
            LetterCell sentinel = new LetterCell(stringTextBox.Text);
            if (sentinel.Next == null) return;

            // Get the second halves.
            LetterCell half;
            half = sentinel.FindSecondHalf(false);
            if (half != null) halfWoMiddleTextBox.Text = half.ToString();

            half = sentinel.FindSecondHalf(true);
            if (half != null) halfWithMiddleTextBox.Text = half.ToString();
        }
    }
}
