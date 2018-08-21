using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarmichaelNumbers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Generate Carmichael numbers.
        private void goButton_Click(object sender, EventArgs e)
        {
            resultListBox.Items.Clear();
            Cursor = Cursors.WaitCursor;
            Refresh();

            // Get the number of numbers to check.
            int maxNumber = int.Parse(maxNumberTextBox.Text);

            // Make a Sieve of Eratosthenes.
            bool[] isComposite = MakeSieve(maxNumber);

            // Check for Carmichael numbers.
            for (int i = 2; i < maxNumber; i++)
            {
                // Only check non-primes.
                if (isComposite[i])
                {
                    // See if i is a Carmichael number.
                    if (IsCarmichael(i))
                    {
                        string txt = i.ToString() + " = ";
                        List<int> factors = PrimeFactors(i);
                        foreach (int factor in factors)
                            txt += factor.ToString() + " x ";
                        txt = txt.Substring(0, txt.Length - 3);
                        resultListBox.Items.Add(txt);
                    }
                }
            }

            countLabel.Text =
                resultListBox.Items.Count.ToString() +
                " Carmichael numbers";
            Cursor = Cursors.Default;
        }

        // Make a Sieve of Eratosthenes.
        private bool[] MakeSieve(int maxNumber)
        {
            bool[] isComposite = new bool[maxNumber + 1];

            // "Cross out" multiples of 2.
            for (long i = 4; i <= maxNumber; i += 2)
            {
                isComposite[i] = true;
            }

            // "Cross out" multiples of primes found so far.
            long nextPrime = 3;
            long stopAt = (long)Math.Sqrt(maxNumber);
            while (nextPrime < stopAt)
            {
                // "Cross out" multiples of this prime.
                for (long i = nextPrime * 2; i <= maxNumber; i += nextPrime)
                {
                    isComposite[i] = true;
                }

                // Move to the next prime.
                do
                    nextPrime += 2;
                while ((nextPrime <= maxNumber) && (isComposite[nextPrime]));
            }

            return isComposite;
        }

        // Return true if the number is a Carmichael number.
        private bool IsCarmichael(int number)
        {
            // Check all possible witnesses.
            for (int i = 1; i < number - 1; i++)
            {
                // Only check numbers with GCD(number, 1) = 1.
                if (Gcd(number, i) == 1)
                {
                    // Calculate: i ^ (number-1) mod number.
                    int result = (int)Exponentiate(i, number - 1, number);

                    // If we found a Fermat witness,
                    // then this is not a Carmichael number.
                    if (result != 1) return false;
                }
            }

            // They're all a bunch of liars!
            // This is a Carmichael number.
            return true;
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

        // Return the number's factors.
        private List<int> PrimeFactors(int number)
        {
            List<int> factors = new List<int>();

            // Pull out factors of 2.
            while (number % 2 == 0)
            {
                factors.Add(2);
                number /= 2;
            }

            // Check odd numbers up to Sqrt(number).
            int maxFactor = (int)Math.Sqrt(number);
            int testFactor = 3;
            while (testFactor <= maxFactor)
            {
                while (number % testFactor == 0)
                {
                    factors.Add(testFactor);
                    number /= testFactor;
                }
                maxFactor = (int)Math.Sqrt(number);
                testFactor += 2;
            }

            // If there's anything left of the number, add it.
            if (number > 1) factors.Add(number);

            return factors;
        }

        // Perform the exponentiation.
        private long Exponentiate(long value, long exponent, long modulus)
        {
            // Make lists of powers and the value to that power.
            List<long> powers = new List<long>();
            List<long> valueToPowers = new List<long>();

            // Makes sure the value isn't greater than the modulus.
            value = value % modulus;

            // Start with the power 1 and value^1.
            long lastPower = 1;
            long lastValue = value;
            powers.Add(lastPower);
            valueToPowers.Add(lastValue);

            // Calculate other powers until we get to one bigger than exponent.
            while (lastPower < exponent)
            {
                lastPower *= 2;
                lastValue = (lastValue * lastValue) % modulus;
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
                    result = (result * valueToPowers[powerIndex]) % modulus;
                }
            }

            // Return the result.
            return result;
        }
    }
}
