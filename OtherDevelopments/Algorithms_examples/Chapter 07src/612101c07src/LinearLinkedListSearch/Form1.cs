using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearLinkedListSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The items.
        private Cell TopCell;

        // Make and sort the items.
        private void createButton_Click(object sender, EventArgs e)
        {
            itemsListBox.DataSource = null;

            // Get the parameters.
            int min = int.Parse(minTextBox.Text);
            int max = int.Parse(maxTextBox.Text) + 1;
            int numItems = int.Parse(numItemsTextBox.Text);

            // Make the items.
            Random rand = new Random();
            int[] items = new int[numItems];
            for (int i = 0; i < numItems; i++)
            {
                items[i] = rand.Next(min, max);
            }

            // Sort the items.
            Array.Sort(items);

            // Copy the items into a linked list.
            TopCell = new Cell(0, null);
            for (int i = numItems - 1; i >= 0; i--)
                TopCell.Next = new Cell(items[i], TopCell.Next);

            // Display the items.
            itemsListBox.DataSource = items;
            findButton.Enabled = true;
        }

        // Find the indicated item.
        private void findButton_Click(object sender, EventArgs e)
        {
            int target = int.Parse(targetTextBox.Text);
            int steps;
            int index = LinearLinkedListSearch(TopCell, target, out steps);
            itemsListBox.SelectedIndex = index;
            itemsListBox.TopIndex = index;
            indexTextBox.Text = index.ToString();
            numStepsTextBox.Text = steps.ToString();
        }

        // Return the index of the target item in the values array.
        // If the item appears more than once in the array,
        // this method doesn't necessarily return the first instance.
        // Return -1 if the item isn't in the array.
        private int LinearLinkedListSearch(Cell top, int target, out int steps)
        {
            steps = 0;
            int index = 0;
            top = top.Next;
            while (top != null)
            {
                steps++;

                // See if this is the item.
                if (top.Value == target) return index;

                // See if we've passed the target's location.
                if (top.Value > target) break;

                // Move to the next item.
                top = top.Next;
                index++;
            }

            // The target isn't in the list.
            return -1;
        }

        private void itemsListBox_Click(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex >= 0)
                targetTextBox.Text = itemsListBox.Items[itemsListBox.SelectedIndex].ToString();
        }
    }
}
