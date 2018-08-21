using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindLoops
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The list sentinel.
        private ValueCell TopCell;

        // Make a list that contains a loop.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Make and display the original values.
            BuildList();
            DisplayList(loopedListBox);

            if (TopCell.ContainsLoop())
                list1StatusLabel.Text = "Loop";
            else
                list1StatusLabel.Text = "No loop";

            // Break the loop and redisplay the values.
            TopCell.BreakLoop();
            DisplayList(unloopedListBox);

            if (TopCell.ContainsLoop())
                list2StatusLabel.Text = "Loop";
            else
                list2StatusLabel.Text = "No loop";
        }

        // The number of cells in the list.
        private const int NumCells = 7;

        // The cell to which the last cell connects.
        private const int LoopCell = 2;

        // Make a list that contains a loop.
        private void BuildList()
        {
            // Make some cells.
            ValueCell[] cells = new ValueCell[NumCells];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new ValueCell();
                cells[i].Value = i + 1;
            }

            // Link the cells.
            for (int i = 0; i < cells.Length - 1; i++)
            {
                cells[i].Next = cells[i + 1];
            }

            // Make the loop.
            cells[cells.Length - 1].Next = cells[LoopCell];

            // Prepare the sentinel.
            TopCell = new ValueCell() { Next = cells[0] };
        }

        // Display the list in the indicated ListBox.
        private void DisplayList(ListBox lst)
        {
            // The maximum number of cells we will list.
            const int maxCells = 14;

            // Display list.
            lst.Items.Clear();
            int cellNum = 0;
            for (ValueCell cell = TopCell.Next; cell != null; cell = cell.Next)
            {
                lst.Items.Add(cell.Value);

                // Stop after a while.
                if (++cellNum > maxCells)
                {
                    lst.Items.Add("...");
                    break;
                }
            }
        }
    }
}
