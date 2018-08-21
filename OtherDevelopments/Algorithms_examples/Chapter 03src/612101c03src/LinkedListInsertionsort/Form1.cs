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

namespace LinkedListInsertionsort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int NumItems = 100;

        // The unsorted items.
        private Cell UnsortedSentinel = null;

        // Make some random items.
        private void randomizeButton_Click(object sender, EventArgs e)
        {
            // Make the sentinel.
            UnsortedSentinel = new Cell();

            // Make a random number generator.
            Random rand = new Random();

            // Add random items.
            for (int i = 0; i < NumItems; i++)
            {
                Cell newCell = new Cell();
                newCell.Value = rand.Next(10000, 99999);
                newCell.Next = UnsortedSentinel.Next;
                UnsortedSentinel.Next = newCell;
            }

            // Display the items.
            DisplayList(UnsortedSentinel, unsortedListBox);

            insertionsortListBox.Items.Clear();
            selectionsortListBox.Items.Clear();

            insertionsortButton.Enabled = true;
            selectionsortButton.Enabled = true;
        }

        // Use insertionsort to sort the items.
        private void insertionsortButton_Click(object sender, EventArgs e)
        {
            // Copy the unsorted list so we don't destroy it.
            Cell copy = CopyList(UnsortedSentinel);

            // Sort the items.
            Cell sorted = Insertionsort(copy);

            // Verify the sort.
            Debug.Assert(IsSorted(sorted));

            // Display the sorted items.
            DisplayList(sorted, insertionsortListBox);
        }

        // Use selectionsort to sort the items.
        private void selectionsortButton_Click(object sender, EventArgs e)
        {
            // Copy the unsorted list so we don't destroy it.
            Cell copy = CopyList(UnsortedSentinel);

            // Sort the items.
            Cell sorted = Selectionsort(copy);

            // Verify the sort.
            Debug.Assert(IsSorted(sorted));

            // Display the sorted items.
            DisplayList(sorted, selectionsortListBox);
        }

        // Display a list in a ListBox.
        private void DisplayList(Cell sentinel, ListBox lst)
        {
            lst.Items.Clear();
            for (Cell cell = sentinel.Next; cell != null; cell = cell.Next)
                lst.Items.Add(cell.Value);
        }

        // Return the list of items in a string.
        private string ListToString(Cell sentinel)
        {
            string result = "";
            for (Cell cell = sentinel.Next; cell != null; cell = cell.Next)
                result += " " + cell.Value.ToString();
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        // Return the number of cells in the list.
        private int ListLength(Cell sentinel)
        {
            int count = 0;
            for (Cell cell = sentinel.Next; cell != null; cell = cell.Next)
                count++;
            return count;
        }

        // Copy a list.
        private Cell CopyList(Cell oldSentinel)
        {
            // Make the new list's sentinel.
            Cell newSentinel = new Cell();

            // Keep track of the last item we've added so far.
            Cell lastAdded = newSentinel;

            // Copy items.
            for (Cell oldCell = oldSentinel.Next; oldCell != null; oldCell = oldCell.Next)
            {
                // Make a new item.
                lastAdded.Next = new Cell();

                // Move to the new item.
                lastAdded = lastAdded.Next;

                // Set the new item's value.
                lastAdded.Value = oldCell.Value;
            }

            // End with null. (We don't actually need to do this in C#
            // because a new Cell object's Next value defaults to null.
            lastAdded.Next = null;

            // Return the new list's sentinel.
            return newSentinel;
        }

        // Use insertionsort to sort the list.
        private Cell Insertionsort(Cell input)
        {
            // Make a sentinel for the sorted list.
            Cell sentinel = new Cell();
            sentinel.Next = null;

            // Skip the input list's sentinel.
            input = input.Next;

            // Repeat until we have inserted all of the items in the new list.
            while (input != null)
            {
                // Get the next cell to add to the list.
                Cell nextCell = input;

                // Move input to input.Next for the next trip through the loop.
                input = input.Next;

                // See where to add the next item in the sorted list.
                Cell afterMe = sentinel;
                while ((afterMe.Next != null) &&
                      (afterMe.Next.Value < nextCell.Value))
                {
                    afterMe = afterMe.Next;
                }

                // Insert the item in the sorted list.
                nextCell.Next = afterMe.Next;
                afterMe.Next = nextCell;
            }

            // Return the sorted list.
            return sentinel;
        }

        // Use selectionsort to sort the list.
        private Cell Selectionsort(Cell input)
        {
            // Make a sentinel for the sorted list.
            Cell sentinel = new Cell();
            sentinel.Next = null;

            // Repeat until the input list is empty.
            while (input.Next != null)
            {
                // Find the largest item in the input list.
                // The cell afterMe will be the cell before
                // the one with the largest value.
                Cell bestAfterMe = input;
                int bestValue = bestAfterMe.Next.Value;

                // Start looking with the next item.
                Cell afterMe = input.Next;
                while (afterMe.Next != null)
                {
                    if (afterMe.Next.Value > bestValue)
                    {
                        bestAfterMe = afterMe;
                        bestValue = afterMe.Next.Value;
                    }
                    afterMe = afterMe.Next;
                }

                // Remove the best cell from the unsorted list.
                Cell bestCell = bestAfterMe.Next;
                bestAfterMe.Next = bestCell.Next;

                // Add the best cell at the beginning of the sorted list.
                bestCell.Next = sentinel.Next;
                sentinel.Next = bestCell;
            }

            // Return the sorted list.
            return sentinel;
        }

        // Verify that a list is sorted.
        private bool IsSorted(Cell sentinel)
        {
            // If the list has 0 or 1 items, then it's sorted.
            if (sentinel.Next == null) return true;
            if (sentinel.Next.Next == null) return true;

            // Compare the other items.
            for (sentinel = sentinel.Next;
                sentinel.Next != null;
                sentinel = sentinel.Next)
            {
                if (sentinel.Value > sentinel.Next.Value) return false;
            }

            // If we get here, the list is sorted.
            return true;
        }
    }
}
