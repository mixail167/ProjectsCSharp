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

namespace Insertionsort
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
            Insertionsort(Items);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Verify the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use insertionsort to sort the array.
        private void Insertionsort(int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                // Move item i into position in the sorted part of the array.
                // Find the first item greater than values[i].
                int j = 0;
                while (values[j] < values[i]) j++;

                // Move the item into position j.
                int value = values[i];      // Save values[i].
                // Move the items down to make room.
                for (int k = i; k > j; k--) values[k] = values[k - 1];
                // Insert item i.
                values[j] = value;
            }
        }
    }
}
