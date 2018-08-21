using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkedLists
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The SortedList.
        private SortedLinkedList<string> MySortedList = new SortedLinkedList<string>();

        // Add an item to the list.
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                MySortedList.Add(valueTextBox.Text);
                valueTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ShowValues();
            valueTextBox.Focus();
        }

        // Delete the item.
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                MySortedList.Delete(valueTextBox.Text);
                valueTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ShowValues();
            valueTextBox.Focus();
        }

        // Display the values.
        private void ShowValues()
        {
            valuesListBox.Items.Clear();
            foreach (string value in MySortedList)
            {
                valuesListBox.Items.Add(value);
            }
        }
    }
}
