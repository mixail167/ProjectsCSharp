using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SieveOfEratosthenes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Find primes.
        private void findPrimesButton_Click(object sender, EventArgs e)
        {
            primesListBox.Items.Clear();
            countLabel.Text = "";
            Cursor = Cursors.WaitCursor;
            Refresh();

            // Get the primes.
            long maxNumber = long.Parse(maxTextBox.Text);
            List<long> primes = FindPrimes(maxNumber);

            // Display the results.
            foreach (long prime in primes)
                primesListBox.Items.Add(prime);
            countLabel.Text = primes.Count.ToString() + " primes";

            Cursor = Cursors.Default;
        }

        // Find the primes between 2 and maxNumber (inclusive).
        private List<long> FindPrimes(long maxNumber)
        {
            // Allocate an array for the numbers.
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
                //for (long i = nextPrime * nextPrime; i <= maxNumber; i += nextPrime)
                {
                    isComposite[i] = true;
                }

                // Move to the next prime.
                do
                    nextPrime += 2;
                while ((nextPrime <= maxNumber) && (isComposite[nextPrime]));
            }

            // Copy the primes into a List<long>.
            List<long> primes = new List<long>();
            for (long i = 2; i <= maxNumber; i++)
                if (!isComposite[i]) primes.Add(i);

            // Return the primes.
            return primes;
        }
    }
}
