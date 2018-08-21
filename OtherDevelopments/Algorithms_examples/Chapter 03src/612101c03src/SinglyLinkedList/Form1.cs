using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinglyLinkedList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The linked list sentinel.
        private PeopleCell TopCell = new PeopleCell() { Name = "<top>", Next = null };

        private void Form1_Load(object sender, EventArgs e)
        {
            // Make some test people.
            TopCell.InsertAfter("Arthur");
            TopCell.InsertAfter("Betty");
            TopCell.InsertAfter("Charles");
            TopCell.InsertAfter("Deena");

            // Display the list.
            DisplayList();
        }

        // Display the people in the list.
        private void DisplayList()
        {
            peopleListBox.Items.Clear();
            for (PeopleCell cell = TopCell; cell != null; cell = cell.Next)
                peopleListBox.Items.Add(cell.Name);
        }

        // Add the new name to the top of the list.
        private void addToTopButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Name must not be blank.");
                nameTextBox.Focus();
                return;
            }

            try
            {
                TopCell.InsertAfter(nameTextBox.Text);
                nameTextBox.Clear();
                nameTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DisplayList();
        }

        // Add the new name after the indicated name.
        private void addAfterButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Name must not be blank.");
                nameTextBox.Focus();
                return;
            }
            if (afterNameTextBox.Text == "")
            {
                MessageBox.Show("After name must not be blank.");
                afterNameTextBox.Focus();
                return;
            }

            try
            {
                TopCell.InsertAfter(afterNameTextBox.Text, nameTextBox.Text);
                nameTextBox.Clear();
                nameTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DisplayList();
        }

        // Delete the indicated cell.
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Name must not be blank.");
                nameTextBox.Focus();
                return;
            }

            try
            {
                TopCell.Delete(nameTextBox.Text);
                nameTextBox.Clear();
                nameTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DisplayList();
        }
    }
}
