using Jace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace integrationApp
{
    public partial class Form1 : Form
    {
        bool lockFlag;
        CalculationEngine engine;

        public Form1()
        {
            InitializeComponent();
            engine = new CalculationEngine();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double temp = engine.Calculate(textBox1.Text);
                lockFlag = true;
            }
            catch (Exception)
            {
                lockFlag = false;
            }
            if (lockFlag)
                pictureBox1.BackgroundImage = Properties.Resources.icon_ok;
            else
                pictureBox1.BackgroundImage = Properties.Resources.icon_error;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Minimum = numericUpDown1.Value + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lockFlag)
            {
                double x1 = (double)numericUpDown1.Value;
                double x2 = (double)numericUpDown2.Value;
                int count = Convert.ToInt32(numericUpDown3.Value);
                double y;
                if (RectangleRole(x1,x2, count, out y))
                {
                    textBox2.Text = y.ToString();
                }
            }
        }

        public bool RectangleRole(double x1, double x2, int count, out double y)
        {
            double y_temp = 0;
            double dx = (x2 - x1) / count;
            double x = x1;
            for (int i = 0; i < count; i++)
            {
                //if (parser.Evaluate(ReplaceValue(x)))
                //{
                //    y_temp += dx * parser.Result;
                //    x += dx;
                //}
                //else
                //{
                //    y = 0;
                //    return false;
                //}
            }
            y = y_temp;
            return true;
        }

        //private string ReplaceValue(double value)
        //{
        //    string text = textBox1.Text.Replace("x", value.ToString());
        //    text = text.Replace(string.Format("e{0}p", value), "exp");
        //    return text;
        //}
    }
}
