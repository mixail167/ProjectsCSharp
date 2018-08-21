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

namespace QuicksortQueue
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

            // Gather the items before and after divider.
            Queue<int> before = new Queue<int>();
            Queue<int> after = new Queue<int>();
            for (int i = start + 1; i <= end; i++)
            {
                if (values[i] < divider) before.Enqueue(values[i]);
                else after.Enqueue(values[i]);
            }

            // Move items before divider back into the array.
            int index = start;
            while (before.Count > 0)
            {
                values[index] = before.Dequeue();
                index++;
            }

            // Add the divider.
            values[index] = divider;

            // Remember that this is the midpoint.
            int midpoint = index;

            // Add items after divider.
            index++;
            while (after.Count > 0)
            {
                values[index] = after.Dequeue();
                index++;
            }

            // Sort the two halves of the array.
            Quicksort(values, start, midpoint - 1);
            Quicksort(values, midpoint + 1, end);
        }
    }
}