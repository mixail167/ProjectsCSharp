using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkedListDeque
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Deque variables.
        private Cell TopSentinel, BottomSentinel;

        // Initialize the sentinels.
        private void Form1_Load(object sender, EventArgs e)
        {
            TopSentinel = new Cell();
            BottomSentinel = new Cell();
            TopSentinel.Next = BottomSentinel;
            BottomSentinel.Prev = TopSentinel;
        }

        // Add an item at the top of the deque.
        private void enqueueTopButton_Click(object sender, EventArgs e)
        {
            Cell cell = new Cell();
            cell.Value = itemTextBox.Text;
            cell.Next = TopSentinel.Next;
            cell.Next.Prev = cell;
            cell.Prev = TopSentinel;
            TopSentinel.Next = cell;

            DisplayList();

            itemTextBox.Clear();
            itemTextBox.Focus();
        }

        // Remove an item from the top of the deque.
        private void dequeueTopButton_Click(object sender, EventArgs e)
        {
            // Make sure there's an item to dequeue.
            if (TopSentinel.Next == BottomSentinel)
            {
                MessageBox.Show("Deque is empty.");
                return;
            }

            // Remove the item.
            Cell cell = TopSentinel.Next;
            TopSentinel.Next = cell.Next;
            TopSentinel.Next.Prev = TopSentinel;

            itemTextBox.Text = cell.Value;
            itemTextBox.Focus();
            itemTextBox.Select(0, 0);

            DisplayList();
        }

        // Add an item at the bottom of the deque.
        private void enqueueBottomButton_Click(object sender, EventArgs e)
        {
            Cell cell = new Cell();
            cell.Value = itemTextBox.Text;
            cell.Prev = BottomSentinel.Prev;
            cell.Prev.Next = cell;
            cell.Next = BottomSentinel;
            BottomSentinel.Prev = cell;

            DisplayList();

            itemTextBox.Clear();
            itemTextBox.Focus();
        }

        // Remove an item from the bottom of the deque.
        private void dequeueBottomButton_Click(object sender, EventArgs e)
        {
            // Make sure there's an item to dequeue.
            if (TopSentinel.Next == BottomSentinel)
            {
                MessageBox.Show("Deque is empty.");
                return;
            }

            // Remove the item.
            Cell cell = BottomSentinel.Prev;
            BottomSentinel.Prev = cell.Prev;
            BottomSentinel.Prev.Next = BottomSentinel;

            itemTextBox.Text = cell.Value;
            itemTextBox.Focus();
            itemTextBox.Select(0, 0);

            DisplayList();
        }

        // Display the current list.
        private void DisplayList()
        {
            queueListBox.Items.Clear();
            for (Cell cell = TopSentinel.Next; cell.Next != null; cell = cell.Next)
                queueListBox.Items.Add(cell.Value);
        }
    }
}
