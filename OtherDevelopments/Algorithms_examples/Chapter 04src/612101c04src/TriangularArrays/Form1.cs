using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriangularArrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Demonstrate a triangular array.
        private void Form1_Load(object sender, EventArgs e)
        {
            const int numRows = 4;

            // Make a lower triangular array.
            TriangularArray<string> array1 = new TriangularArray<string>(numRows);
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    array1[row, col] = "(" + row.ToString() +
                        "," + col.ToString() + ")";
                }
            }

            // Make a 2-D array holding the first array's labels.
            Label[,] aLabels =
            {
                { a00Label, null, null, null},
                { a10Label, a11Label, null, null},
                { a20Label, a21Label, a22Label, null},
                { a30Label, a31Label, a32Label, a33Label},
            };

            // Display the array values.
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    aLabels[row, col].Text = array1[row, col];
                }
            }

            // Make an upper triangular array.
            TriangularArray<string> array2 = new TriangularArray<string>(numRows);
            for (int row = 0; row < numRows; row++)
            {
                for (int col = row; col < numRows; col++)
                {
                    array2[row, col] = "(" + row.ToString() +
                        "," + col.ToString() + ")";
                }
            }

            // Make a 2-D array holding the first array's labels.
            Label[,] bLabels =
            {
                { b00Label, b01Label, b02Label, b03Label},
                { null, b11Label, b12Label, b13Label},
                { null, null, b22Label, b23Label},
                { null, null, null, b33Label},
            };

            // Display the array values.
            for (int row = 0; row < numRows; row++)
            {
                for (int col = row; col < numRows; col++)
                {
                    bLabels[row, col].Text = array2[row, col];
                }
            }
        }
    }
}
