using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertionsortPriorityQueue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The queue variables.
        private Cell QueueSentinel = new Cell(0, "");

        // Add an item to the queue.
        private void enqueueButton_Click(object sender, EventArgs e)
        {
            string value = itemTextBox.Text;
            int priority = int.Parse(priorityTextBox.Text);
            itemTextBox.Clear();
            priorityTextBox.Clear();
            itemTextBox.Focus();

            // See where the item belongs.
            Cell afterMe = QueueSentinel;
            while ((afterMe.Next != null) && (afterMe.Next.Priority < priority))
                afterMe = afterMe.Next;

            // Insert the item.
            Cell newCell = new Cell(priority, value);
            newCell.Next = afterMe.Next;
            afterMe.Next = newCell;

            // Display the list.
            DisplayList();
        }

        // Remove the top item from the queue.
        private void dequeueButton_Click(object sender, EventArgs e)
        {
            // Make sure there is something to dequeue.
            if (QueueSentinel.Next == null)
            {
                MessageBox.Show("Queue is empty.");
                return;
            }

            // Display the value and priority.
            itemTextBox.Text = QueueSentinel.Next.Value;
            priorityTextBox.Text = QueueSentinel.Next.Priority.ToString();
            itemTextBox.Focus();

            // Remove the item.
            QueueSentinel = QueueSentinel.Next;

            // Display the list.
            DisplayList();
        }

        // Display the current list.
        private void DisplayList()
        {
            queueListBox.Items.Clear();
            for (Cell cell = QueueSentinel.Next; cell != null; cell = cell.Next)
            {
                queueListBox.Items.Add(cell.Priority.ToString() + '\t' + cell.Value);
            }
        }
    }
}
