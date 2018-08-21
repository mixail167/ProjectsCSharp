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

namespace Countingsort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The items.
        private int[] Items;

        // The largest item in the array.
        private int MaxValue;

        // Make random items.
        private void generateButton_Click(object sender, EventArgs e)
        {
            itemsListBox.DataSource = null;
            int numItems = int.Parse(numItemsTextBox.Text);
            Items = new int[numItems];

            Random rand = new Random();
            MaxValue = int.Parse(maxItemTextBox.Text);
            for (int i = 0; i < numItems; i++) Items[i] = rand.Next(0, MaxValue + 1);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
            sortButton.Enabled = true;
        }

        // Sort the items.
        private void sortButton_Click(object sender, EventArgs e)
        {
            itemsListBox.DataSource = null;

            // Sort.
            DateTime startTime = DateTime.Now;
            Countingsort(Items, MaxValue);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Validate the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use countingsort to sort the array.
        private void Countingsort(int[] values, int max)
        {
            // Make an array to hold the counts.
            int[] counts = new int[max + 1];

            // Initialize the array to hold the counts.
            // (This is not necessary in C#.)
            //for (int i = 0; i <= MaxValue; i++) counts[i] = 0;

            // Count the items with each value.
            for (int i = 0; i < values.Length; i++)
            {
                // Add 1 to the count for this value.
                counts[values[i]]++;
            }

            // Copy the values back into the array.
            int index = 0;
            for (int i = 0; i <= max; i++)
            {
                // Copy the value i into the array counts[i] times.
                for (int j = 0; j < counts[i]; j++)
                {
                    values[index] = i;
                    index++;
                }
            }
        }
    }
}
