using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddSparseArrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Make some sparse arrays.
            SparseIntArray array1 = new SparseIntArray(0);
            array1[1, 1] = 101;
            array1[-1, -1] = -101;
            array1[3, 3] = 303;
            array1[2, 4] = 204;
            array1[4, 1] = 401;

            SparseIntArray array2 = new SparseIntArray(0);
            array2[1, 0] = 100;
            array2[3, 3] = 303;
            array2[1, 4] = 104;
            array2[4, 1] = 401;
            array2[10, 10] = 1010;

            SparseIntArray array3 = array1.Add(array2);

            resultTextBox.Text =
                array1.ToString() + Environment.NewLine +
                array2.ToString() + Environment.NewLine +
                array3.ToString();
            resultTextBox.Select(0, 0);
        }
    }
}
