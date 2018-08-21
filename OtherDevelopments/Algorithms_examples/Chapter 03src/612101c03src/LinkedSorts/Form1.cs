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

namespace LinkedSorts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The top sentinel.
        private ValueCell TopCell = new ValueCell() { Value = 0, Next = null };

        // A copy of the unsorted values.
        private ValueCell CopyTopCell;

        // Make a list of random items.
        private void makeItemsButton_Click(object sender, EventArgs e)
        {
            int numItems = int.Parse(numItemsTextBox.Text);
            int minimum = int.Parse(minimumTextBox.Text);
            int maximum = int.Parse(maximumTextBox.Text);
            Random rand = new Random();

            // Delete the previous list if there is one.
            //(This isn't necessary in C#.)

            // Add cells.
            ValueCell newTop = null;
            for (int i = 0; i < numItems; i++)
            {
                ValueCell newCell = new ValueCell();
                newCell.Value = rand.Next(minimum, maximum);
                newCell.Next = newTop;
                newTop = newCell;
            }

            // Make a new sentinel.
            TopCell = new ValueCell() { Value = 0, Next = newTop };

            // Make a copy.
            CopyTopCell = TopCell.CopyList();

            // Display the list.
            DisplayList();
        }

        // Display the items.
        private void DisplayList()
        {
            valueListBox.Items.Clear();
            int i = 0;
            for (ValueCell cell = TopCell.Next; cell != null; cell = cell.Next)
            {
                valueListBox.Items.Add(cell.Value);

                // Only list up to 1000 items.
                if (++i > 1000) break;
            }
        }

        // Reset the list to the original insorted values.
        private void resetButton_Click(object sender, EventArgs e)
        {
            TopCell = CopyTopCell.CopyList();
            DisplayList();
        }

        // Use selectionsort to sort the list.
        private void selectionsortButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timeTextBox.Clear();

            DateTime startTime = DateTime.Now;
            TopCell.Selectionsort();
            DateTime stopTime = DateTime.Now;

            Debug.Assert(TopCell.IsSorted());
            TimeSpan elapsed = stopTime - stopTime;
            timeTextBox.Text = elapsed.TotalSeconds.ToString("0.00") + " sec";
            DisplayList();
            Cursor = Cursors.Default;
        }

        // Use insertionsort to sort the list.
        private void insertionsortButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timeTextBox.Clear();

            DateTime startTime = DateTime.Now;
            TopCell.Insertionsort();
            DateTime stopTime = DateTime.Now;

            Debug.Assert(TopCell.IsSorted());
            TimeSpan elapsed = stopTime - stopTime;
            timeTextBox.Text = elapsed.TotalSeconds.ToString("0.00") + " sec";
            DisplayList();
            Cursor = Cursors.Default;
        }
    }
}
