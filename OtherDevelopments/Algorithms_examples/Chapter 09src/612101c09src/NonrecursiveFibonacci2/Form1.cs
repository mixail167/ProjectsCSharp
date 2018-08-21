using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NonrecursiveFibonacci2
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
                long n = int.Parse(nTextBox.Text);
                long result = Fibonacci(n);
                resultTextBox.Text = result.ToString();
            }
            catch
            {
                resultTextBox.Text = "Infinity";
            }
        }

        // Return the n-th Fibonacci number.
        private long Fibonacci(long n)
        {
            checked
            {
                if (n <= 1) return n;

                long fib_n_minus_2 = 0;                         // Fibonacci(0).
                long fib_n_minus_1 = 1;                         // Fibonacci(1).
                long fib_n = fib_n_minus_1 + fib_n_minus_2;     // Fibonacci(2).
                for (int i = 2; i < n; i++)
                {
                    fib_n_minus_2 = fib_n_minus_1;
                    fib_n_minus_1 = fib_n;
                    fib_n = fib_n_minus_1 + fib_n_minus_2;
                }
                return fib_n;
            }
        }
    }
}
