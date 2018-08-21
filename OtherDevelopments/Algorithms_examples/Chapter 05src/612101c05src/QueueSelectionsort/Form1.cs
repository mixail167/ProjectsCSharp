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

namespace QueueSelectionsort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The queue holding the items.
        private Queue<int> ItemQueue = new Queue<int>();

        // Make random items.
        private void makeItemsButton_Click(object sender, EventArgs e)
        {
            int numItems = int.Parse(numItemsTextBox.Text);
            int min = int.Parse(minimumTextBox.Text);
            int max = int.Parse(maximumTextBox.Text);

            Random rand = new Random();
            ItemQueue = new Queue<int>();
            for (int i = 0; i < numItems; i++)
            {
                ItemQueue.Enqueue(rand.Next(min, max));
            }

            DisplayItems();
        }

        // Display the items.
        private void DisplayItems()
        {
            itemsListBox.Items.Clear();

            // Display up to 100 items.
            foreach (int item in ItemQueue.Take(100))
                itemsListBox.Items.Add(item);
        }

        // Sort the items.
        private void sortButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Sort the items.
            SortQueue(ItemQueue);

            // Verify the sort.
            int[] itemArray = ItemQueue.ToArray();
            for (int i = 1; i < itemArray.Length; i++)
            {
                Debug.Assert(itemArray[i - 1] <= itemArray[i]);
            }

            // Display the sorted items.
            DisplayItems();

            Cursor = Cursors.Default;
        }

        // Sort the items in the queue.
        private void SortQueue<T>(Queue<T> queue1) where T : System.IComparable<T>
        {
            int numItems = queue1.Count;
            int numSorted = 0;
            int numUnsorted = numItems;
            for (int i = 0; i < numItems; i++)
            {
                // Find the item that belongs in sorted position i.

                // Pull the first item off the queue.
                // Assume it will be the biggest item considered.
                T biggest = queue1.Dequeue();

                // Pull the other unsorted items off the
                // queue, keeping track of the biggest.
                for (int j = 0; j < numUnsorted - 1; j++)
                {
                    T testItem = queue1.Dequeue();
                    if (testItem.CompareTo(biggest) > 0)
                    {
                        // This item is bigger. Add the old biggest
                        // item to the queue and replace it with this item.
                        queue1.Enqueue(biggest);
                        biggest = testItem;
                    }
                    else
                    {
                        // This item is not bigger. Just move it
                        // to end of the queue.
                        queue1.Enqueue(testItem);
                    }
                }

                // Add the biggest item to the end of the queue.
                queue1.Enqueue(biggest);

                // Move the sorted items to the beginning of the queue.
                for (int j = 0; j < numSorted; j++)
                {
                    queue1.Enqueue(queue1.Dequeue());
                }

                // Update the counts.
                numSorted++;
                numUnsorted--;
            }
        }
    }
}
