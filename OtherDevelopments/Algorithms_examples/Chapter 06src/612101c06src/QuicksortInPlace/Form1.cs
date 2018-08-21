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

namespace QuicksortInPlace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The items.
        private int[] Items;

        // Make random items.
        private void generateButton_Click(object sender, EventArgs e)
        {
            itemsListBox.DataSource = null;
            int numItems = int.Parse(numItemsTextBox.Text);
            Items = new int[numItems];

            Random rand = new Random();
            for (int i = 0; i < numItems; i++) Items[i] = rand.Next(10000, 99999);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
            sortButton.Enabled = true;
        }

        // Sort the items.
        private void sortButton_Click(object sender, EventArgs e)
        {
            itemsListBox.DataSource = null;

            // Sort.
            DateTime startTime = DateTime.Now;
            Quicksort(Items);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Verify the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use quicksort to sort the array.
        private void Quicksort(int[] values)
        {
            // Sort the whole array.
            Quicksort(values, 0, values.Length - 1);
        }

        // Sort the indicated part of the array.
        private void Quicksort(int[] values, int start, int end)
        {
            // If the list has no more than 1 element, it's sorted.
            if (start >= end) return;

            // Use the first item as the dividing item.
            int divider = values[start];

            // Move items < divider to the front of the array and
            // items >= divider to the end of the array.
            int lo = start;
            int hi = end;
            for (; ; )
            {
                // Look down from hi for a value < divider.
                while (values[hi] >= divider)
                {
                    hi--;
                    if (hi <= lo) break;
                }
                if (hi <= lo)
                {
                    // Put the divider here and break out of the outer While loop.
                    values[lo] = divider;
                    break;
                }

                // Move the value we found to the lower half.
                values[lo] = values[hi];

                // Look up from lo for a value >= divider.
                lo++;
                while (values[lo] < divider)
                {
                    lo++;
                    if (lo >= hi) break;
                }
                if (lo >= hi)
                {
                    // Put the divider here and break out of the outer While loop.
                    lo = hi;
                    values[hi] = divider;
                    break;
                }

                // Move the value we found to the upper half.
                values[hi] = values[lo];
            }

            // Recursively sort the two halves.
            Quicksort(values, start, lo - 1);
            Quicksort(values, lo + 1, end);
        }
    }
}