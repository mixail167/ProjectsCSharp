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

namespace Selectionsort
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
            Selectionsort(Items);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Verify the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use selectionsort to sort the array.
        private void Selectionsort(int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                // Find the smallest item that hasn't been sorted yet.
                int bestJ = i;
                int bestValue = values[i];
                for (int j = i + 1; j < values.Length; j++)
                    if (values[j] < bestValue)
                    {
                        bestJ = j;
                        bestValue = values[j];
                    }

                // Swap the smallest item into position i.
                int temp = values[bestJ];
                values[bestJ] = values[i];
                values[i] = temp;
            }
        }
    }
}
