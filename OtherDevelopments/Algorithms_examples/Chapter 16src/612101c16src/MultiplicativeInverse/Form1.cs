using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiplicativeInverse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Exhaustively find the inverse.
        private void calculateButton_Click(object sender, EventArgs e)
        {
            int number = int.Parse(numberTextBox.Text);
            int modulus = int.Parse(modulusTextBox.Text);
            int inverse = Inverse(number, modulus);
            inverseTextBox.Text = inverse.ToString();
            int product = (number * inverse) % modulus;
            verifyTextBox.Text = string.Format("{0} * {1} = {2} mod {3}",
                number, inverse, product, modulus);
        }

        private int Inverse(int number, int modulus)
        {
            for (int i = 1; i < modulus; i++)
                if ((i * number) % modulus == 1) return i;
            return -1;
        }
    }
}
