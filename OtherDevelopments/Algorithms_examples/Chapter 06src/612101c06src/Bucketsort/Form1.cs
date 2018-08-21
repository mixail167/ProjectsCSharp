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

namespace Bucketsort
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
            int numBuckets = int.Parse(numBucketsTextBox.Text);

            // Sort.
            DateTime startTime = DateTime.Now;
            Bucketsort(Items, MaxValue, numBuckets);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Validate the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use bucketsort to sort the array.
        private void Bucketsort(int[] values, int max, int numBuckets)
        {
            // Make buckets.
            Cell[] buckets = new Cell[numBuckets];

            // Create bucket sentinels.
            for (int i = 0; i < numBuckets; i++) buckets[i] = new Cell();

            // Calculate the number of values per bucket.
            float itemsPerBucket = (max + 1f) / numBuckets;

            // Distribute.
            for (int i = 0; i < values.Length; i++)
            {
                // Calculate the bucket number.
                int value = values[i];
                int num = (int)(value / itemsPerBucket);

                // Insert the item in this bucket.
                Cell afterMe = buckets[num];
                while ((afterMe.Next != null) && (afterMe.Next.Value < value))
                    afterMe = afterMe.Next;
                Cell cell = new Cell(value);
                cell.Next = afterMe.Next;
                afterMe.Next = cell;
            }

            // Gather the values back into the array.
            int index = 0;
            for (int i = 0; i < numBuckets; i++)
            {
                // Copy the values in bucket i into the array (skipping the sentinel).
                Cell cell = buckets[i].Next;
                while (cell != null)
                {
                    values[index] = cell.Value;
                    index++;
                    cell = cell.Next;
                }
            }
        }

        // Use insertionsort to sort the list.
        private Cell Insertionsort(Cell input)
        {
            // Make a sentinel for the sorted list.
            Cell sentinel = new Cell();
            sentinel.Next = null;

            // Skip the input list's sentinel.
            // Commented out because this list doesn't have a sentinel.
            //input = input.Next;

            // Repeat until we have inserted all of the items in the new list.
            while (input != null)
            {
                // Get the next cell to add to the list.
                Cell nextCell = input;

                // Move input to input.Next for the next trip through the loop.
                input = input.Next;

                // See where to add the next item in the sorted list.
                Cell afterMe = sentinel;
                while ((afterMe.Next != null) &&
                      (afterMe.Next.Value < nextCell.Value))
                {
                    afterMe = afterMe.Next;
                }

                // Insert the item in the sorted list.
                nextCell.Next = afterMe.Next;
                afterMe.Next = nextCell;
            }

            // Return the sorted list.
            // Skip the sentinel for this program.
            return sentinel.Next;
        }
    }
}
