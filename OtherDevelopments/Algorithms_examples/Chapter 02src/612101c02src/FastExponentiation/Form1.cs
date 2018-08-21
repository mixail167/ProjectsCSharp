using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Note: This project checks for overflow and underflow.
// Project > Properties > Build > Advanced > Check for arithmetic overflow/underflow.

namespace FastExponentiation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Demonstrate fast exponentiation.
        private void evaluateButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Clear();

            try
            {
                long value = long.Parse(valueTextBox.Text);
                long exponent = long.Parse(exponentTextBox.Text);
                long result = Exponentiate(value, exponent);
                resultTextBox.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Perform the exponentiation.
        private long Exponentiate(long value, long exponent)
        {
            // Make lists of powers and the value to that power.
            List<long> powers = new List<long>();
            List<long> valueToPowers = new List<long>();

            // Start with the power 1 and value^1.
            long lastPower = 1;
            long lastValue = value;
            powers.Add(lastPower);
            valueToPowers.Add(lastValue);

            // Calculate other powers until we get to one bigger than exponent.
            while (lastPower < exponent)
            {
                lastPower *= 2;
                lastValue *= lastValue;
                powers.Add(lastPower);
                valueToPowers.Add(lastValue);
            }

            // Combine values to make the required power.
            long result = 1;

            // Get the index of the largest power that is smaller than exponent.
            for (int powerIndex = powers.Count - 1; powerIndex >= 0; powerIndex--)
            {
                // See if this power fits within exponent.
                if (powers[powerIndex] <= exponent)
                {
                    // It fits. Use this power.
                    exponent -= powers[powerIndex];
                    result *= valueToPowers[powerIndex];
                }
            }

            // Return the result.
            return result;
        }
    }
}
