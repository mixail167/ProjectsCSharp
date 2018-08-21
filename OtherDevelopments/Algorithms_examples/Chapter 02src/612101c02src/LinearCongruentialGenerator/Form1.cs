using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearCongruentialGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Generate pseudo-random numbers.
        private void generateButton_Click(object sender, EventArgs e)
        {
            numbersTextBox.Clear();
            countLabel.Text = "";
            Refresh();

            // Get parameters.
            long A = long.Parse(aTextBox.Text);
            long B = long.Parse(bTextBox.Text);
            long M = long.Parse(mTextBox.Text);

            // Generate numbers.
            Dictionary<long, long> numbers = new Dictionary<long, long>();
            string txt = "";
            long X = 0;
            for (; ; )
            {
                // Only display the first 200 numbers in the TextBox.
                if (numbers.Count <= 200)
                    txt += X.ToString() + " ";

                // See if we have generated this number before.
                if (numbers.ContainsKey(X)) break;

                // Add the number.
                numbers.Add(X, X);

                // Generate the next number.
                X = (A * X + B) % M;
            }
            numbersTextBox.Text = txt;
            countLabel.Text = numbers.Count.ToString() + " values";
        }
    }
}
