using System;
using System.Windows.Forms;

namespace TransportTask
{
    public partial class Form1 : Form
    {
        Methods methods;

        public Form1()
        {
            InitializeComponent();
            numericUpDown2.Minimum = 2;
            numericUpDown1.Minimum = 2;
            dataGridView2.Rows.Add();
            dataGridView3.Rows.Add();
            methods = Methods.MMT;
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(numericUpDown1.Value);
            while (dataGridView1.Rows.Count < count)
            {
                dataGridView1.Rows.Add();
                dataGridView2.Columns.Add("", "");
                dataGridView4.Rows.Add();
            }
            while (dataGridView1.Rows.Count > count)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 1);
                dataGridView4.Rows.RemoveAt(dataGridView4.Rows.Count - 1);
            }
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(numericUpDown2.Value);
            while (dataGridView1.Columns.Count < count)
            {
                dataGridView1.Columns.Add("", "");
                dataGridView3.Columns.Add("", "");
                dataGridView4.Columns.Add("", "");
            }
            while (dataGridView1.Columns.Count > count)
            {
                dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
                dataGridView3.Columns.RemoveAt(dataGridView3.Columns.Count - 1);
                dataGridView4.Columns.RemoveAt(dataGridView4.Columns.Count - 1);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TransportTaskExecutor transportTask = new TransportTaskExecutor();
            uint[,] matrix = new uint[dataGridView1.RowCount, dataGridView1.ColumnCount];
            uint[] a = new uint[dataGridView2.ColumnCount];
            uint[] b = new uint[dataGridView3.ColumnCount];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                try
                {
                    a[i] = Convert.ToUInt32(dataGridView2[i, 0].Value);
                }
                catch { }
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (i == 0)
                    {
                        try
                        {
                            b[j] = Convert.ToUInt32(dataGridView3[j, 0].Value);
                        }
                        catch { }
                    }
                    try
                    {
                        matrix[i, j] = Convert.ToUInt32(dataGridView1[j, i].Value);
                    }
                    catch { }
                }
            }
            //uint[,] matrix = { { 2, 3, 1, 2 }, { 3, 4, 5, 1 }, { 3, 6, 3, 4 } };
            //uint[] a = { 150, 100, 100 };
            //uint[] b = { 140, 100, 70, 40 };
            transportTask.Matrix = matrix;
            transportTask.A = a;
            transportTask.B = b;
            if (transportTask.CheckOpen())
            {
                MessageBox.Show("Задача открытая!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (transportTask.Solve(methods))
            {
                uint[,] result = transportTask.Result;
                label4.Text = transportTask.F.ToString();
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView4.ColumnCount; j++)
                    {
                        dataGridView4[j, i].Value = result[i, j];
                    }
                }
            }
            else
            {
                MessageBox.Show("Задача неразрешима!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                methods = Methods.MMT;
            }
            else if (radioButton2.Checked)
            {
                methods = Methods.NWA;
            }
            else if (radioButton3.Checked)
            {
                methods = Methods.MAF;
            }
        }
    }
}
