using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factorial
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
            if (n <= 1) return 1;
            checked
            {
                return n * Factorial(n - 1);
            }
        }
    }
}
