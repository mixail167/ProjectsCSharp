using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursiveInterpolationSearch
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

            //@
            int index, steps = 0;
            foreach (int i in Items)
            {
                index = InterpolationSearch(Items, 0, Items.Length - 1, i, ref steps);
                if (index < 0) throw new ArgumentException();
            }
            for (int i = Items[0] - 100; i < Items[0]; i++)
            {
                index = InterpolationSearch(Items, 0, Items.Length - 1, i, ref steps);
                if (index >= 0) throw new ArgumentException();
            }
            for (int i = Items[Items.Length - 1] + 1; i < Items[Items.Length - 1] + 100; i++)
            {
                index = InterpolationSearch(Items, 0, Items.Length - 1, i, ref steps);
                if (index >= 0) throw new ArgumentException();
            }
        }

        // Find the indicated item.
        private void findButton_Click(object sender, EventArgs e)
        {
            int target = int.Parse(targetTextBox.Text);
            int steps = 0;
            int index = InterpolationSearch(Items, 0, Items.Length - 1, target, ref steps);
            itemsListBox.SelectedIndex = index;
            itemsListBox.TopIndex = index;
            indexTextBox.Text = index.ToString();
            numStepsTextBox.Text = steps.ToString();
        }

        // Return the index of the target item in the values array.
        // If the item appears more than once in the array,
        // this method doesn't necessarily return the first instance.
        // Return -1 if the item isn't in the array.
        private int InterpolationSearch(int[] values, int min, int max, int target, ref int steps)
        {
            steps++;

            // Prevent division by zero.
            if (values[min] == values[max])
            {
                // This must be the item if it's in the array).
                if (values[min] == target) return min;
                return -1;
            }

            // Find the dividing item.
            int mid = min + (max - min) *
                (target - values[min]) / (values[max] - values[min]);

            // If mid is out of bounds, then the target isn't in the array.
            if ((mid < min) || (mid > max)) return -1;

            // See if we need to search the left or right half.
            if (values[mid] < target) min = mid + 1;
            else if (values[mid] > target) max = mid - 1;
            else return mid;

            // See if we should recurse.
            if (min <= max) return InterpolationSearch(values, min, max, target, ref steps);

            // If we get here, then the target isn't in the array.
            return -1;
        }

        private void itemsListBox_Click(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex >= 0)
                targetTextBox.Text = itemsListBox.Items[itemsListBox.SelectedIndex].ToString();
        }
    }
}
