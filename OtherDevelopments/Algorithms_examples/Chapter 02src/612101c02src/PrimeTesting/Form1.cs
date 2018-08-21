using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimeTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Random number generator.
        private Random Rand = new Random();

        // Pick a random number.
        private void randomButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text = Rand.Next(int.MaxValue).ToString();
        }

        // Find a number that is probably prime.
        private void findPrimeButton_Click(object sender, EventArgs e)
        {
            // Find a probable prime.
            int numTests;
            numberTextBox.Text = FindProbablePrime(
                100000000, 2000000000, 100, out numTests).ToString();
            isPrimeTextBox.Text = "Found probable prime in " + numTests.ToString() + " attempts.";

            // Factor it.
            DisplayFactors();
        }

        // See if this number is prime.
        private void isPrimeButton_Click(object sender, EventArgs e)
        {
            int number = int.Parse(numberTextBox.Text);
            if (IsPrimeFermat(number, 100))
                isPrimeTextBox.Text = "Probably prime.";
            else
                isPrimeTextBox.Text = "Not prime.";
        }

        // Use Fermat's little theorem to see if the number is probably prime.
        private bool IsPrimeFermat(int number, int numTrials)
        {
            for (int trial = 0; trial < numTrials; trial++)
            {
                // Pick a random test number.
                int test = Rand.Next(2, number);
                do
                {
                    test = Rand.Next(2, number);
                } while (Gcd(test, number) != 1);

                // Make sure it is relatively prime with number.

                // Calculate: test ^ (number-1) mod number.
                int result = (int)Exponentiate(test, number - 1, number);

                // If this is not -1, then the number is not prime.
                if (result != 1) return false;
            }

            // If we made it this far, the number is probably prime.
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

        // Pick a number that is probably prime between the given bounds.
        private int FindProbablePrime(int lowerBound, int upperBound, int numTrials)
        {
            int numTests;
            return FindProbablePrime(lowerBound, upperBound, numTrials, out numTests);
        }

        // Pick a number that is probably prime between the given bounds.
        private int FindProbablePrime(int lowerBound, int upperBound, int numTrials, out int numTests)
        {
            numTests = 0;
            do
            {
                numTests++;
                int number = Rand.Next(lowerBound, upperBound);
                if (IsPrimeFermat(number, numTrials)) return number;
            } while (true);
        }

        // Factor the number.
        private void factorButton_Click(object sender, EventArgs e)
        {
            DisplayFactors();
        }
        private void DisplayFactors()
        {
            // Get the factors.
            int number = int.Parse(numberTextBox.Text);
            List<int> factors = PrimeFactors(number);

            // Display the factors.
            string result = "1";
            foreach (int factor in factors)
                result += " x " + factor.ToString();
            factorsTextBox.Text = result;
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

        private void numberTextBox_TextChanged(object sender, EventArgs e)
        {
            isPrimeTextBox.Clear();
            factorsTextBox.Clear();
        }
    }
}
