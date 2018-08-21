using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakLoopReverse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // List 1.
            // Make a list with no loop.
            Cell i1 = new Cell("I", null);
            Cell h1 = new Cell("H", i1);
            Cell g1 = new Cell("G", h1);
            Cell f1 = new Cell("F", g1);
            Cell e1 = new Cell("E", f1);
            Cell d1 = new Cell("D", e1);
            Cell c1 = new Cell("C", d1);
            Cell b1 = new Cell("B", c1);
            Cell a1 = new Cell("A", b1);
            Cell sentinel1 = new Cell("", a1);

            // Display the list.
            list1TextBox.Text = ListToString(sentinel1, 15);

            // Indicate whether the list has a loop.
            list1HasLoopTextBox.Text = HasLoopReversing(sentinel1).ToString();

            // Redisplay the list.
            list1NewListTextBox.Text = ListToString(sentinel1, 15);

            // List 2.
            // Make a list with no loop.
            Cell i2 = new Cell("I", null);
            Cell h2 = new Cell("H", i2);
            Cell g2 = new Cell("G", h2);
            Cell f2 = new Cell("F", g2);
            Cell e2 = new Cell("E", f2);
            Cell d2 = new Cell("D", e2);
            Cell c2 = new Cell("C", d2);
            Cell b2 = new Cell("B", c2);
            Cell a2 = new Cell("A", b2);
            Cell sentinel2 = new Cell("", a2);
            i2.Next = d2;

            // Display the list.
            list2TextBox.Text = ListToString(sentinel2, 15);

            // Indicate whether the list has a loop.
            list2HasLoopTextBox.Text = HasLoopReversing(sentinel2).ToString();

            // Redisplay the list.
            list2NewListTextBox.Text = ListToString(sentinel2, 15);
        }

        // Return true if the list has a loop.
        private bool HasLoopReversing(Cell sentinel)
        {
            // If the list is empty, it has no loops.
            if (sentinel.Next == null) return false;

            // Loop through the list reversing links.
            Cell newSentinel = ReverseList(sentinel);

            // Loop through the list again to restore the links.
            ReverseList(newSentinel);

            // If the reversed list starts with the same cell
            // as the original list, then there is a loop.
            // Return the result.
            return (newSentinel == sentinel);
        }

        // Reverse the loop once and return the new top of the list.
        private Cell ReverseList(Cell sentinel)
        {
            Cell prevCell = null;
            Cell currCell = sentinel;
            while (currCell != null)
            {
                // Reverse the link out of this cell.
                Cell nextCell = currCell.Next;
                currCell.Next = prevCell;

                // Move to the next cell.
                prevCell = currCell;
                currCell = nextCell;
            }

            // Return the last cell we visited.
            return prevCell;
        }

        // Return a string representation of the list.
        private string ListToString(Cell sentinel, int maxCells)
        {
            string result = "";
            int i = 0;
            for (Cell cell = sentinel.Next; cell != null; cell = cell.Next)
            {
                result += " " + cell.Value;
                if (++i > maxCells) break;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }
    }
}
