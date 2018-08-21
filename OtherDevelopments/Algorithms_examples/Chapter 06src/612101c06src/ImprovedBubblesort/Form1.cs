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

namespace ImprovedBubblesort
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

            itemsListBox.DataSource = Items;
            sortButton.Enabled = true;
        }

        // Sort the items.
        private void sortButton_Click(object sender, EventArgs e)
        {
            itemsListBox.DataSource = null;

            // Sort.
            DateTime startTime = DateTime.Now;
            Bubblesort(Items);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Verify the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use improved bubblesort to sort the array.
        private void Bubblesort(int[] values)
        {
            // Bounds for the items that are not yet sorted.
            int imin = 0;
            int imax = values.Length - 1;

            // Repeat until the array is sorted.
            while (imin < imax)
            {
                // Record the index of the last swapped item.
                int lastSwap = imin;

                // Downward pass.
                for (int i = imin; i < imax; i++)
                {
                    // See if items i and i + 1 are out of order.
                    if (values[i] > values[i + 1])
                    {
                        // Swap them.
                        int temp = values[i];
                        values[i] = values[i + 1];
                        values[i + 1] = temp;

                        // Update the index of the last swapped item.
                        lastSwap = i;
                    }
                }

                // Update imax.
                imax = lastSwap;

                // See if we're done.
                if (imin >= imax) break;

                // Upward pass.
                lastSwap = imax;
                for (int i = imax; i > imin; i--)
                {
                    // See if items i and i - 1 are out of order.
                    if (values[i] < values[i - 1])
                    {
                        // Swap them.
                        int temp = values[i];
                        values[i] = values[i - 1];
                        values[i - 1] = temp;

                        // Update the index of the last swapped item.
                        lastSwap = i;
                    }
                }

                // Update imin.
                imin = lastSwap;
            }
        }
    }
}
