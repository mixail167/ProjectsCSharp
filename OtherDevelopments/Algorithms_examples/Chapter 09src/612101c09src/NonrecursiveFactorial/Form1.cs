using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NonrecursiveFactorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                long n = long.Parse(nTextBox.Text);
                long result = Factorial(n);
                resultTextBox.Text = result.ToString();
            }
            catch
            {
                resultTextBox.Text = "Infinity";
            }
        }

        // Return n!.
        private long Factorial(long n)
        {
            // Make a variable to keep track of the returned value.
            // Initialize it to 1 so we can multiply it by returned results.
            // (The result is 1 if we do not enter the loop at all.)
            long result = 1;

            // Start a loop controlled by the recursion stopping condition.
            while (n != 0)
            {
                // Save the result from this "recursive" call.
                checked
                {
                    result *= n;
                }

                // Prepare for "recursion."
                n--;
            }

            // Return the accumulated result.
            return result;
        }
    }
}
