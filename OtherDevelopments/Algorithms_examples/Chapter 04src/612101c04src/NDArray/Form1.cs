using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDArray
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Array bounds.
            const int lb0 = 1;
            const int ub0 = 3;
            const int lb1 = 2;
            const int ub1 = 4;
            const int lb2 = 10;
            const int ub2 = 12;

            // Make and fill an array.
            NDArray<string> values = new NDArray<string>(lb0, ub0, lb1, ub1, lb2, ub2);
            for (int row = lb0; row <= ub0; row++)
            {
                for (int col = lb1; col <= ub1; col++)
                {
                    for (int hgt = lb2; hgt <= ub2; hgt++)
                    {
                        values[row, col, hgt] =
                            "(" + row.ToString() +
                            "," + col.ToString() +
                            "," + hgt.ToString() + ")";
                    }
                }
            }

            // Get the values.
            string txt = "";
            for (int row = lb0; row <= ub0; row++)
            {
                txt += "Row " + row.ToString() + Environment.NewLine;
                for (int col = lb1; col <= ub1; col++)
                {
                    txt += "    ";
                    for (int hgt = lb2; hgt <= ub2; hgt++)
                    {
                        txt += values[row, col, hgt] + " ";
                    }
                    txt += Environment.NewLine;
                }
                txt += Environment.NewLine;
            }
            txtValues.Text = txt;
            txtValues.Select(0, 0);
        }
    }
}
