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

namespace StackSelectionsort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The stack holding the items.
        private Stack<int> ItemStack = new Stack<int>();

        // Make random items.
        private void makeItemsButton_Click(object sender, EventArgs e)
        {
            int numItems = int.Parse(numItemsTextBox.Text);
            int min = int.Parse(minimumTextBox.Text);
            int max = int.Parse(maximumTextBox.Text);

            Random rand = new Random();
            ItemStack = new Stack<int>();
            for (int i = 0; i < numItems; i++)
            {
                ItemStack.Push(rand.Next(min, max));
            }

            DisplayItems();
        }

        // Display the items.
        private void DisplayItems()
        {
            itemsListBox.Items.Clear();

            // Display up to 100 items.
            foreach (int item in ItemStack.Take(100))
                itemsListBox.Items.Add(item);
        }

        // Sort the items.
        private void sortButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Sort the items.
            SortStack(ItemStack);

            // Verify the sort.
            int[] itemArray = ItemStack.ToArray();
            for (int i = 1; i < itemArray.Length; i++)
            {
                Debug.Assert(itemArray[i - 1] <= itemArray[i]);
            }

            // Display the sorted items.
            DisplayItems();

            Cursor = Cursors.Default;
        }

        // Sort the items in the queue.
        private void SortStack<T>(Stack<T> stack) where T : System.IComparable<T>
        {
            // Make a temporary stack.
            Stack<T> tempStack = new Stack<T>();

            int numItems = stack.Count;
            int numSorted = 0;
            int numUnsorted = numItems;
            for (int i = 0; i < numItems; i++)
            {
                // Find the item that belongs in sorted position i.

                // Pull the first item off the stack.
                // Assume it will be the biggest item considered.
                T biggest = stack.Pop();

                // Move the other unsorted items into the
                // temporary stack, keeping track of the biggest.
                for (int j = 0; j < numUnsorted - 1; j++)
                {
                    T testItem = stack.Pop();
                    if (testItem.CompareTo(biggest) > 0)
                    {
                        // This item is bigger. Move the old biggest item to
                        // the temporary stack and replace it with this item.
                        tempStack.Push(biggest);
                        biggest = testItem;
                    }
                    else
                    {
                        // This item is not bigger. Just move it
                        // to the temporary stack.
                        tempStack.Push(testItem);
                    }
                }

                // Add the biggest item to the end of the queue.
                stack.Push(biggest);

                // Move the unsorted items back into the original stack.
                while (tempStack.Count > 0) stack.Push(tempStack.Pop());

                // Update the counts.
                numSorted++;
                numUnsorted--;
            }
        }
    }
}
