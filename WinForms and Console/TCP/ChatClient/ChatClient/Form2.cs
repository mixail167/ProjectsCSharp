using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form2 : Form
    {
        public Form2(string text)
        {
            InitializeComponent();
            label1.Text = text;
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width - 5, Screen.PrimaryScreen.WorkingArea.Height - Height - 5);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
