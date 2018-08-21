using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace Select3ofN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            int n = (int)nNumericUpDown.Value;
            int k = 3;

            // Make the list of items.
            int ascA = (int)'A';
            List<string> items = new List<string>();
            for (int i = 0; i < n; i++)
                items.Add(((char)(ascA + i)).ToString());

            withDuplicatesListBox.DataSource = Select3WithDuplicates(items);
            withDuplicatesLabel.Text = withDuplicatesListBox.Items.Count.ToString() + " combinations";

            withoutDuplicatesListBox.DataSource = Select3WithoutDuplicates(items);
            withoutDuplicatesLabel.Text = withoutDuplicatesListBox.Items.Count.ToString() + " combinations";

            // Verify the counts with calculations.
            Debug.Assert(withDuplicatesListBox.Items.Count == Choose(n + k - 1, k));
            Debug.Assert(withoutDuplicatesListBox.Items.Count == Choose(n, k));

        }

        // Generate selections of 3 items allowing duplicates.
        private List<string> Select3WithDuplicates(List<string> items)
        {
            List<string> results = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = i; j < items.Count; j++)
                {
                    for (int k = j; k < items.Count; k++)
                    {
                        results.Add(items[i] + items[j] + items[k]);
                    }
                }
            }
            return results;
        }

        // Generate selections of 3 items without allowing duplicates.
        private List<string> Select3WithoutDuplicates(List<string> items)
        {
            List<string> results = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = i + 1; j < items.Count; j++)
                {
                    for (int k = j + 1; k < items.Count; k++)
                    {
                        results.Add(items[i] + items[j] + items[k]);
                    }
                }
            }
            return results;
        }

        private long Choose(long m, long n)
        {
            return Factorial(m) / Factorial(n) / Factorial(m - n);
        }
        private long Factorial(long n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++) result *= i;
            return result;
        }
    }
}
