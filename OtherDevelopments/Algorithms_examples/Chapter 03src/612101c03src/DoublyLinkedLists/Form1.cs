using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoublyLinkedLists
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The doubly-linked list.
        private DoublyLinkedList<string, string> MyList =
            new DoublyLinkedList<string, string>();

        // Set an item's value in the list.
        private void setButton_Click(object sender, EventArgs e)
        {
            MyList[keyTextBox.Text] = valueTextBox.Text;
            keyTextBox.Clear();
            valueTextBox.Clear();
            ShowValues();
        }

        // Find an item.
        private void findButton_Click(object sender, EventArgs e)
        {
            try
            {
                valueTextBox.Text = MyList[keyTextBox.Text];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ShowValues();
        }

        // Delete an item.
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                MyList.Delete(keyTextBox.Text);
                valueTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ShowValues();
        }

        // Display the values.
        private void ShowValues()
        {
            valuesListBox.Items.Clear();
            foreach (KeyValuePair<string, string> pair in MyList)
            {
                valuesListBox.Items.Add(pair.Key + ": " + pair.Value);
            }
        }
    }
}
