using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int[,] input = new int[3, 5];
        Web NW1;

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button2.Enabled = true;
            openFileDialog1.Title = "Укажите тестируемый файл";
            openFileDialog1.ShowDialog();
            pictureBox1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            Bitmap im = pictureBox1.BackgroundImage as Bitmap;
            for (var i = 0; i <= 5; i++)
                listBox1.Items.Add(" ");
            for (var x = 0; x < 3; x++)
                for (var y = 0; y < 5; y++)
                {
                    int n = (im.GetPixel(x, y).R);
                    if (n >= 250) n = 0;
                    else n = 1;
                    listBox1.Items[y] = listBox1.Items[y] + "  " + Convert.ToString(n);
                    input[x, y] = n;
                }
            Recognize();
        }

        public void Recognize()
        {
            NW1.Mul_w();
            NW1.Sum();
            if (NW1.Rez()) listBox1.Items.Add(" - True, Sum = " + Convert.ToString(NW1.sum));
            else listBox1.Items.Add(" - False, Sum = " + Convert.ToString(NW1.sum));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NW1 = new Web(3, 5, input);
            openFileDialog1.Title = "Укажите файл весов";
            openFileDialog1.ShowDialog();
            string s = openFileDialog1.FileName;
            StreamReader sr = File.OpenText(s);
            string line;
            string[] s1;
            int k = 0;
            while ((line = sr.ReadLine()) != null)
            {
                s1 = line.Split(' ');
                for (int i = 0; i < s1.Length; i++)
                {
                    listBox1.Items.Add("");
                    if (k < 5)
                    {
                        NW1.weight[i, k] = Convert.ToInt32(s1[i]);
                        listBox1.Items[k] += Convert.ToString(NW1.weight[i, k]);
                    }
                }
                k++;
            }
            sr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NW1.Rez() == false)
                NW1.IncW(input);
            else NW1.DecW(input);
            try
            {
                using (StreamWriter writer = new StreamWriter("w.txt", false))
                {
                    for (int y = 0; y < 5; y++)
                    {
                        writer.WriteLine(Convert.ToString(NW1.weight[0, y]) + " " + Convert.ToString(NW1.weight[1, y]) + " " + Convert.ToString(NW1.weight[2, y]));
                    }
                }
                button2.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Данные не сохранены!", "Ошибка");
            }
        }
    }
}
