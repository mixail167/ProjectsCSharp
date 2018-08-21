using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FillArrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Fill arrays in various ways.
        private void goButton_Click(object sender, EventArgs e)
        {
            valuesTextBox.Clear();

            // Make the array.
            int numRows = int.Parse(numRowsTextBox.Text);
            int numCols = int.Parse(numColsTextBox.Text);
            int[,] values = new int[numRows, numCols];

            // Fill with 0\1.
            FillArrayLLtoUR(values, 0, 1);
            valuesTextBox.AppendText(GetArrayString(values) + Environment.NewLine);

            // Fill with 0/1.
            FillArrayULtoLR(values, 0, 1);
            valuesTextBox.AppendText(GetArrayString(values) + Environment.NewLine);

            // Fill by circles.
            FillArrayWithDistances(values);
            valuesTextBox.AppendText(GetArrayString(values) + Environment.NewLine);
        }

        // Fill the array diagonally with the
        // indicated values on the lower left and upper right.
        private void FillArrayLLtoUR(int[,] values, int llValue, int urValue)
        {
            for (int row = 0; row <= values.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= values.GetUpperBound(1); col++)
                {
                    if (row >= col) values[row, col] = urValue;
                    else values[row, col] = llValue;
                }
            }
        }

        // Fill the array diagonally with the
        // indicated values on the upper left and lower right.
        private void FillArrayULtoLR(int[,] values, int ulValue, int lrValue)
        {
            int maxCol = values.GetUpperBound(1);
            for (int row = 0; row <= values.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= values.GetUpperBound(1); col++)
                {
                    if (row > maxCol - col) values[row, col] = ulValue;
                    else values[row, col] = lrValue;
                }
            }
        }

        // Fill the array in circles.
        private void FillArrayInCircles(int[,] values, int startValue, int increment)
        {
            int maxRow = values.GetUpperBound(0);
            int maxCol = values.GetUpperBound(1);
            int levels = (2 + Math.Min(maxRow, maxCol)) / 2;

            int value = startValue;
            for (int level = 0; level < levels; level++)
            {
                // Fill in horizontal values.
                for (int col = level; col < maxCol - level + 1; col++)
                {
                    values[level, col] = value;
                    values[maxRow - level, col] = value;
                }
                for (int row = level; row < maxRow - level; row++)
                {
                    values[row, level] = value;
                    values[row, maxCol - level] = value;
                }
                value += increment;
            }
        }

        // Fill each entry with its distance to the edge.
        private void FillArrayWithDistances(int[,] values)
        {
            int maxRow = values.GetUpperBound(0);
            int maxCol = values.GetUpperBound(1);

            for (int row = 0; row <= maxRow; row++)
            {
                for (int col = 0; col <= maxCol; col++)
                {
                    values[row, col] =
                        Math.Min(
                            Math.Min(row, col),
                            Math.Min(maxRow - row, maxCol - col));
                }
            }
        }

        // Return a string representing the array's values.
        private string GetArrayString(int[,] values)
        {
            string txt = "";
            for (int row = 0; row <= values.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= values.GetUpperBound(1); col++)
                {
                    txt += values[row, col].ToString() + " ";
                }
                txt += Environment.NewLine;
            }
            return txt;
        }
    }
}
