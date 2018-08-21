using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriorityQueue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The array.
        private const int MaxItems = 100;
        private const int NumItems = 0;
        private int Count = 0;
        private string[] Values = new string[MaxItems];
        private int[] Priorities = new int[MaxItems];

        // Add an item to the priority queue.
        private void pushButton_Click(object sender, EventArgs e)
        {
            // Verify inputs.
            string value = valueTextBox.Text;
            if (value.Length == 0)
            {
                MessageBox.Show("The value must not be blank.");
                valueTextBox.Focus();
                return;
            }
            int priority;
            if (!int.TryParse(priorityTextBox.Text, out priority))
            {
                MessageBox.Show("The priority must be an integer.");
                priorityTextBox.Focus();
                return;
            }

            // Add the item to the priority queue.
            Push(Values, Priorities, ref Count, value, priority);

            valueTextBox.Clear();
            priorityTextBox.Clear();
            valueTextBox.Focus();
            DisplayItems();
            popButton.Enabled = true;
        }

        // Remove an item to the priority queue.
        private void popButton_Click(object sender, EventArgs e)
        {
            string value;
            int priority;
            Pop(Values, Priorities, ref Count, out value, out priority);
            valueTextBox.Text = value;
            priorityTextBox.Text = priority.ToString();

            valueTextBox.Focus();
            DisplayItems();
            popButton.Enabled = (Count > 0);
        }

        // Add the item to the priority queue.
        private void Push(string[] values, int[] priorities, ref int count, string value, int priority)
        {
            // Add the item at the bottom of the heap.
            values[count] = value;
            priorities[count] = priority;
            count++;

            // Start at the new item and work up to the root.
            int index = count - 1;
            while (index != 0)
            {
                // Find the parent's index.
                int parent = (index - 1) / 2;

                // If child <= parent, we're done so
                // break out of the while loop.
                if (priorities[index] <= priorities[parent]) break;

                // Swap the parent and child.
                string tempValue = values[index];
                values[index] = values[parent];
                values[parent] = tempValue;

                int tempPriority = priorities[index];
                priorities[index] = priorities[parent];
                priorities[parent] = tempPriority;

                // Move to the parent.
                index = parent;
            }
        }

        // Remove the highest priority item from the priority queue.
        private void Pop(string[] values, int[] priorities, ref int count, out string value, out int priority)
        {
            // Save the return values.
            value = values[0];
            priority = priorities[0];

            // Replace the root with the last node.
            values[0] = values[count - 1];
            priorities[0] = priorities[count - 1];
            count--;

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
                if ((priorities[index] >= priorities[child1]) &&
                    (priorities[index] >= priorities[child2])) break;

                // Get the index of the child with the larger value.
                int swapChild;
                if (priorities[child1] > priorities[child2])
                    swapChild = child1;
                else
                    swapChild = child2;

                // Swap with the larger child.
                string tempValue = values[index];
                values[index] = values[swapChild];
                values[swapChild] = tempValue;

                int tempPriority = priorities[index];
                priorities[index] = priorities[swapChild];
                priorities[swapChild] = tempPriority;

                // Move to the child node.
                index = swapChild;
            }
        }

        // Display the values and priorities.
        private void DisplayItems()
        {
            valuesListBox.Items.Clear();
            prioritiesListBox.Items.Clear();
            for (int i = 0; i < Count; i++)
            {
                valuesListBox.Items.Add(Values[i]);
                prioritiesListBox.Items.Add(Priorities[i]);
            }
        }
    }
}
