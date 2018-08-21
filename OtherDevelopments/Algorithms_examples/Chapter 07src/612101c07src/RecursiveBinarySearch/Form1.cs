using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursiveBinarySearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The items.
        private int[] Items;

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
            Items = new int[numItems];
            for (int i = 0; i < numItems; i++)
            {
                Items[i] = rand.Next(min, max);
            }

            // Sort the items.
            Array.Sort(Items);

            // Display the items.
            itemsListBox.DataSource = Items;
            findButton.Enabled = true;
        }

        // Find the indicated item.
        private void findButton_Click(object sender, EventArgs e)
        {
            int target = int.Parse(targetTextBox.Text);
            int steps = 0;
            int index = BinarySearch(Items, 0, Items.Length - 1, target, ref steps);
            itemsListBox.SelectedIndex = index;
            itemsListBox.TopIndex = index;
            indexTextBox.Text = index.ToString();
            numStepsTextBox.Text = steps.ToString();
        }

        // Return the index of the target item in the values array.
        // If the item appears more than once in the array,
        // this method doesn't necessarily return the first instance.
        // Return -1 if the item isn't in the array.
        private int BinarySearch(int[] values, int min, int max, int target, ref int steps)
        {
            steps++;

            // Find the dividing item.
            int mid = (min + max) / 2;

            // See if we found it.
            if (target == values[mid]) return mid;

            // See if we need to search the left or right half.
            if (target < values[mid]) max = mid - 1;
            else min = mid + 1;

            if (min <= max) return BinarySearch(values, min, max, target, ref steps);

            // If we get here, then the target is not in the array.
            return -1;
        }

        private void itemsListBox_Click(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex >= 0)
                targetTextBox.Text = itemsListBox.Items[itemsListBox.SelectedIndex].ToString();
        }
    }
}
