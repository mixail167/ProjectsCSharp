using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NonrecursiveFibonacci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Calculated values.
        private long[] FibonacciValues = new long[100];

        // The maximum value calculatued so far.
        private long MaxN;

        // Set Fibonacci[0] and Fibonacci[1].
        private void Form1_Load(object sender, EventArgs e)
        {
            FibonacciValues[0] = 0;
            FibonacciValues[1] = 1;
            MaxN = 1;
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            long n = int.Parse(nTextBox.Text);
            if (n >= FibonacciValues.Length)
            {
                MessageBox.Show("N must be less than " +
                    FibonacciValues.Length.ToString());
                return;
            }

            long result = Fibonacci(n);
            resultTextBox.Text = result.ToString();
        }

        // Return the n-th Fibonacci number.
        private long Fibonacci(long n)
        {
            if (n > MaxN)
            {
                // Calculate values between Fibonacci(MaxN) and Fibonacci(n).
                for (long i = MaxN + 1; i <= n; i++)
                {
                    checked
                    {
                        FibonacciValues[i] = Fibonacci(i - 1) + Fibonacci(i - 2);
                    }
                }
                // Update MaxN.
                MaxN = n;
            }

            // Return the calculated value.
            return FibonacciValues[n];
        }
    }
}
