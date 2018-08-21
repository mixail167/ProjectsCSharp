using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gcd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void findGcdButton_Click(object sender, EventArgs e)
        {
            long number1 = long.Parse(number1TextBox.Text);
            long number2 = long.Parse(number2TextBox.Text);

            long gcd = Gcd(number1, number2);
            gcdTextBox.Text = gcd.ToString();

            long lcm = Lcm(number1, number2);
            lcmTextBox.Text = lcm.ToString();
        }

        // Find GCD(a, b) recursively.
        // GCD(a, b) = GCD(b, a mod b).
        private long RecursiveGcd(long a, long b)
        {
            if (b == 0) return a;
            return RecursiveGcd(b, a % b);
        }

        // Find GCD(a, b).
        // GCD(a, b) = GCD(b, a mod b).
        private long Gcd(long a, long b)
        {
            while (b != 0)
            {
                // Calculate the remainder.
                long remainder = a % b;

                // Calculate GCD(b, remainder).
                a = b;
                b = remainder;
            }

            // GCD(a, 0) is a.
            return a;
        }

        // Find LCM(a, b).
        // LCM(a, b) = a * b * GCD(a, b).
        private long Lcm(long a, long b)
        {
            return a * (b / Gcd(a, b));
        }
    }
}
