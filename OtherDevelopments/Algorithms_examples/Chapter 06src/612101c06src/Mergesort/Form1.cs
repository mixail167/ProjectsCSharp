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

namespace Mergesort
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
            DoMergesort(Items);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Validate the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use mergesort to sort the array.
        private void DoMergesort(int[] values)
        {
            // Make a scratch array.
            int[] scratch = new int[values.Length];

            // Sort.
            Mergesort(values, scratch, 0, values.Length - 1);
        }

        // Use mergesort to sort the array.
        private void Mergesort(int[] values, int[] scratch, int start, int end)
        {
            // If the array contains only 1 item, it is already sorted.
            if (start == end) return;

            // Break the array into left and right halves.
            int midpoint = (start + end) / 2;

            // Call Mergesort to sort the two halves.
            Mergesort(values, scratch, start, midpoint);
            Mergesort(values, scratch, midpoint + 1, end);

            // Merge the two sorted halves together.
            int leftIndex = start;
            int rightIndex = midpoint + 1;
            int scratchIndex = leftIndex;
            while ((leftIndex <= midpoint) && (rightIndex <= end))
            {
                if (values[leftIndex] <= values[rightIndex])
                {
                    scratch[scratchIndex] = values[leftIndex];
                    leftIndex++;
                }
                else
                {
                    scratch[scratchIndex] = values[rightIndex];
                    rightIndex++;
                }
                scratchIndex++;
            }

            // Finish copying whichever half is not empty.
            for (int i = leftIndex; i <= midpoint; i++)
            {
                scratch[scratchIndex] = values[i];
                scratchIndex++;
            }
            for (int i = rightIndex; i <= end; i++)
            {
                scratch[scratchIndex] = values[i];
                scratchIndex++;
            }

            // Copy the values back into the values array.
            for (int i = start; i <= end; i++)
            {
                values[i] = scratch[i];
            }
        }
    }
}
