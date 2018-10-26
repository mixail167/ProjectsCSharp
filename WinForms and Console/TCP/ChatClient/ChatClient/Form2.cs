using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form2 : Form
    {
        Form1 link;

        public Form2(string text, Form1 link)
        {
            InitializeComponent();
            label1.Text = text;
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width - 5, Screen.PrimaryScreen.WorkingArea.Height - Height - 5);
            timer1.Enabled = true;
            this.link = link;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (link.notifyIcon1.Visible)
                {
                    link.ModifyStateForm(true);
                }
                if (link.WindowState != FormWindowState.Normal)
                {
                    link.WindowState = FormWindowState.Normal;
                }
                if (!link.Focused)
                {
                    link.Focus();
                }
            }
            catch
            {

            }
            finally
            {
                Close();
            }
        }
    }
}
