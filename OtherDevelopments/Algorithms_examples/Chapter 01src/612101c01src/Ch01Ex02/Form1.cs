using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch01Ex02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Operations the program can execute in various lengths of time.
            const double second = 1000000;
            const double minute = second * 60;
            const double hour = minute * 60;
            const double day = hour * 24;
            const double week = day * 7;
            const double year = day * 365.25f;

            AddRow("Second", second);
            AddRow("Minute", minute);
            AddRow("Hour", hour);
            AddRow("Day", day);
            AddRow("Week", week);
            AddRow("Year", year);
        }

        private void AddRow(string name, double steps)
        {
            ListViewItem item = timeListView.Items.Add(name);
            double value = Math.Pow(2, steps);          // Log N
            item.SubItems.Add(value.ToString("e0"));
            value = steps * steps;                      // Sqrt(N)
            item.SubItems.Add(value.ToString("e0"));
            value = steps;                              // N
            item.SubItems.Add(value.ToString("e0"));
            value = Math.Sqrt(steps);                   // N^2
            item.SubItems.Add(value.ToString("n0"));
            value = Math.Log(steps, 2);                 // 2^N
            item.SubItems.Add(value.ToString("n0"));
            value = InverseFactorial(steps);            // N!
            item.SubItems.Add(value.ToString("n0"));
        }

        private int InverseFactorial(double value)
        {
            for (int i = 1; ; i++)
            {
                if (Factorial(i) > value) return i - 1;
            }
        }

        private double Factorial(int n)
        {
            double result = 1;
            for (int i = 2; i < n; i++) result *= i;
            return result;
        }
    }
}
