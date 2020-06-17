using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(numericUpDown1.Value);
            int height = Convert.ToInt32(numericUpDown2.Value);
            Algorithms algorithm;
            if (radioButton1.Checked)
            {
                algorithm = Algorithms.DFS;
            }
            else if (radioButton2.Checked)
            {
                algorithm = Algorithms.Prime;
            }
            else
            {
                algorithm = Algorithms.Kruskal;
            }
            Form2 form2 = new Form2(width, height, algorithm);
            form2.ShowDialog();
            form2.Dispose();
        }
    }
}
