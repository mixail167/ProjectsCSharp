using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoDArray
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Demonstrate the NZArray class.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Make the array.
            TwoDArray<string> array = new TwoDArray<string>(1, 10, 2000, 2010);

            // Fill the array.
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 2000; col <= 2010; col++)
                {
                    array[row, col] =
                        "(" + row.ToString("D2") +
                        ", " + col.ToString() + ")";
                }
            }

            // Display the values.
            string txt = "";
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 2000; col <= 2010; col++)
                {
                    txt += array[row, col] + " ";
                }
                txt += Environment.NewLine;
            }
            txtValues.Text = txt;
            txtValues.Select(0, 0);
        }
    }
}
