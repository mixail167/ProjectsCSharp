using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiplyTriangularArrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Make two triangular arrays.
            const int numRows = 3;
            TriangularIntArray array1 = new TriangularIntArray(numRows);
            array1[0, 0] = 1;
            array1[1, 0] = 2;
            array1[1, 1] = 3;
            array1[2, 0] = 4;
            array1[2, 1] = 5;
            array1[2, 2] = 6;

            TriangularIntArray array2 = new TriangularIntArray(numRows);
            array2[0, 0] = 10;
            array2[1, 0] = 20;
            array2[1, 1] = 30;
            array2[2, 0] = 40;
            array2[2, 1] = 50;
            array2[2, 2] = 60;

            TriangularIntArray array3 = array1.TimesFull(array2);
            TriangularIntArray array4 = array1.Times(array2);

            resultTextBox.Text =
                array1.ToString() + Environment.NewLine +
                array2.ToString() + Environment.NewLine +
                array3.ToString() + Environment.NewLine +
                array4.ToString();
            resultTextBox.Select(0, 0);
        }
    }
}
