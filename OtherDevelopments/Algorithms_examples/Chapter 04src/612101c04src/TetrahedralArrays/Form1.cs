using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrahedralArrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Demonstrate a tetrahedral array.
        private void Form1_Load(object sender, EventArgs e)
        {
            const int numRows = 4;

            // Make a lower triangular array.
            TetrahedralArray<string> tet_array = new TetrahedralArray<string>(numRows);
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    for (int hgt = 0; hgt <= col; hgt++)
                    {
                        tet_array[row, col, hgt] =
                            "(" + row.ToString() +
                            "," + col.ToString() +
                            "," + hgt.ToString() + ")";
                    }
                }
            }

            // Make a 2-D array holding the first array's labels.
            Label[, ,] Labels =
            {
                {
                    { label000, null, null, null},
                    { null, null, null, null},
                    { null, null, null, null},
                    { null, null, null, null},
                },
                {
                    { label100, null, null, null},
                    { label110, label111, null, null},
                    { null, null, null, null},
                    { null, null, null, null},
                },
                {
                    { label200, null, null, null},
                    { label210, label211, null, null},
                    { label220, label221, label222, null},
                    { null, null, null, null},
                },
                {
                    { label300, null, null, null},
                    { label310, label311, null, null},
                    { label320, label321, label322, null},
                    { label330, label331, label332, label333},
                },
            };

            // Display the array values.
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    for (int hgt = 0; hgt <= col; hgt++)
                    {
                        Labels[row, col, hgt].Text = tet_array[row, col, hgt];
                    }
                }
            }
        }
    }
}
