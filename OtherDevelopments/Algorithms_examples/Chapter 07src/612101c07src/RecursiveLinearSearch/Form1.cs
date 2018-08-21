using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursiveLinearSearch
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
            int index = LinearSearch(Items, target, 0, ref steps);
            itemsListBox.SelectedIndex = index;
            itemsListBox.TopIndex = index;
            indexTextBox.Text = index.ToString();
            numStepsTextBox.Text = steps.ToString();
        }

        // Return the index of the target item in the values array.
        // If the item appears more than once in the array,
        // this method doesn't necessarily return the first instance.
        // Return -1 if the item isn't in the array.
        private int LinearSearch(int[] values, int target, int index, ref int steps)
        {
            steps++;

            // Stop if we've reached the end of the array.
            if (index == values.Length) return -1;

            // See if the target is at this index.
            if (values[index] == target) return index;

            // Stop if we've passed where the target would be.
            if (values[index] > target) return -1;

            // Search the next index.
            return LinearSearch(values, target, index + 1, ref steps);
        }

        private void itemsListBox_Click(object sender, EventArgs e)
        {
            if (itemsListBox.SelectedIndex >= 0)
                targetTextBox.Text = itemsListBox.Items[itemsListBox.SelectedIndex].ToString();
        }
    }
}
