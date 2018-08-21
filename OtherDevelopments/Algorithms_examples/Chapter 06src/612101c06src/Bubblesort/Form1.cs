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

namespace Bubblesort
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

        // Use bubblesort to sort the array.
        private void Bubblesort(int[] values)
        {
            // Repeat until the array is sorted.
            bool notSorted = true;
            while (notSorted)
            {
                // Assume there will be no swap during this pass.
                notSorted = false;

                // Examine the array to see if any two
                // adjacent items are out of order.
                for (int i = 1; i < values.Length; i++)
                {
                    // See if items i and i - 1 are out of order.
                    if (values[i] < values[i - 1])
                    {
                        // Swap them.
                        int temp = values[i];
                        values[i] = values[i - 1];
                        values[i - 1] = temp;
                        notSorted = true;
                    }
                }
            }
        }
    }
}
