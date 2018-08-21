using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SparseArrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The sparse array.
        private SparseArray<string> MyArray = new SparseArray<string>("---");

        // Get a value.
        private void getButton_Click(object sender, EventArgs e)
        {
            int row = int.Parse(rowTextBox.Text);
            int col = int.Parse(colTextBox.Text);
            valueTextBox.Text = MyArray[row, col];

            ShowValues();
        }

        // Set a value.
        private void setButton_Click(object sender, EventArgs e)
        {
            int row = int.Parse(rowTextBox.Text);
            int col = int.Parse(colTextBox.Text);
            MyArray[row, col] = valueTextBox.Text;
            valueTextBox.Clear();

            ShowValues();
        }

        // Clear the value.
        private void clearButton_Click(object sender, EventArgs e)
        {
            int row = int.Parse(rowTextBox.Text);
            int col = int.Parse(colTextBox.Text);
            MyArray.DeleteEntry(row, col);
            valueTextBox.Clear();

            ShowValues();
        }

        // Show all of the values.
        private void ShowValues()
        {
            valuesListBox.Items.Clear();
            foreach (ArrayValue<string> value in MyArray)
            {
                valuesListBox.Items.Add("MyArray[" +
                    value.Row.ToString() + ", " +
                    value.Column.ToString() + "] = " +
                    value.Value);
            }
        }
    }
}
