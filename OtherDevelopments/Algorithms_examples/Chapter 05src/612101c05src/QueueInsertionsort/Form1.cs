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

namespace QueueInsertionsort
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

            // Initially consider the last item in the queue to be sorted.
            int numSorted = 1;
            int numUnsorted = numItems - 1;

            for (int i = 0; i < numItems - 1; i++)
            {
                // Take the next item and position it.

                // Pull the first item off the queue.
                T nextItem = queue1.Dequeue();

                // Pull the other unsorted items off the queue.
                for (int j = 0; j < numUnsorted - 1; j++)
                    queue1.Enqueue(queue1.Dequeue());

                // Move the sorted items to the beginning of the queue,
                // inserting nextItem in its proper position.
                bool addedNextItem = false;
                for (int j = 0; j < numSorted; j++)
                {
                    T testItem = queue1.Dequeue();
                    if (!addedNextItem && (testItem.CompareTo(nextItem) >= 0))
                    {
                        // testItem >= nextItem. Insert nextItem first.
                        queue1.Enqueue(nextItem);
                        addedNextItem = true;
                    }
                    queue1.Enqueue(testItem);
                }

                // If we haven't added nextItem (because it is
                // bigger than the sorted items), do so now.
                if (!addedNextItem) queue1.Enqueue(nextItem);

                // Update the counts.
                numSorted++;
                numUnsorted--;
            }
        }
    }
}
