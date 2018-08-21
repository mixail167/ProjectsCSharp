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

namespace Heapsort
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
            Heapsort(Items);
            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine(elapsed.TotalSeconds.ToString("0.00") + " seconds");

            // Verify the sort.
            for (int i = 1; i < Items.Length; i++)
                Debug.Assert(Items[i] >= Items[i - 1]);

            itemsListBox.DataSource = Items.Take(1000).ToArray();
        }

        // Use heapsort to sort the array.
        private void Heapsort(int[] values)
        {
            // Make the array into a heap.
            MakeHeap(values);

            // Pop items from the root to the end of the array.
            for (int i = values.Length - 1; i > 0; i--)
            {
                // Remove the top item and restore the heap property.
                int value = RemoveTopItem(values, i + 1);

                // Save the top item past the end of the tree.
                values[i] = value;
            }
        }

        // Make the array into a heap.
        private void MakeHeap(int[] values)
        {
            // Add each item to the heap one at a time.
            for (int i = 0; i < values.Length; i++)
            {
                // Start at the new item and work up to the root.
                int index = i;
                while (index != 0)
                {
                    // Find the parent's index.
                    int parent = (index - 1) / 2;

                    // If child <= parent, we're done so
                    // break out of the while loop.
                    if (values[index] <= values[parent]) break;

                    // Swap the parent and child.
                    int temp = values[index];
                    values[index] = values[parent];
                    values[parent] = temp;

                    // Move to the parent.
                    index = parent;
                }
            }
        }

        // Remove the top item from a heap with
        // count items and restore its heap property.
        private int RemoveTopItem(int[] values, int count)
        {
            // Save the top item to return later.
            int result = values[0];

            // Move the last item to the root.
            values[0] = values[count - 1];

            // Restore the heap property.
            int index = 0;
            for (; ; )
            {
                // Find the child indices.
                int child1 = 2 * index + 1;
                int child2 = 2 * index + 2;

                // If a child index is off the end of the tree,
                // use the parent's index.
                if (child1 >= count) child1 = index;
                if (child2 >= count) child2 = index;

                // If the heap property is satisfied, we're done.
                if ((values[index] >= values[child1]) &&
                    (values[index] >= values[child2])) break;

                // Get the index of the child with the larger value.
                int swapChild;
                if (values[child1] > values[child2])
                    swapChild = child1;
                else
                    swapChild = child2;

                // Swap with the larger child.
                int temp = values[index];
                values[index] = values[swapChild];
                values[swapChild] = temp;

                // Move to the child node.
                index = swapChild;
            }

            // Return the value we removed from the root.
            return result;
        }
    }
}