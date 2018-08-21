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

namespace StackInsertionsort
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

        // Use a second stack to sort the items.
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

        // Sort the items in the stack.
        private void SortStack<T>(Stack<T> stack1) where T : System.IComparable<T>
        {
            // Make the temporary stack.
            Stack<T> stack2 = new Stack<T>();

            int numItems = stack1.Count;
            for (int i = 0; i < numItems; i++)
            {
                // Pull off the first item.
                T nextItem = stack1.Pop();

                // Move the other unsorted items to the second stack.
                for (int j = 0; j < numItems - i - 1; j++)
                {
                    stack2.Push(stack1.Pop());
                }

                // Move sorted items to the second stack until
                // we find out where nextItem belongs.
                while (stack1.Count > 0)
                {
                    T testItem = stack1.Pop();
                    if (testItem.CompareTo(nextItem) >= 0)
                    {
                        // testItem >= nextItem so nextItem
                        // belongs after it in the sorted list.
                        // Put testItem back in the list.
                        stack1.Push(testItem);
                        break;
                    }
                    stack2.Push(testItem);
                }

                // Add nextItem at this position.
                stack1.Push(nextItem);

                // Move the remaining items back onto the list.
                while (stack2.Count > 0)
                    stack1.Push(stack2.Pop());
            }
        }
    }
}
