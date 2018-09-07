using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Game15
{
    public partial class Form2 : Form
    {
        private bool isMouseDown;
        private Point mouseCoordinates;

        public Form2()
        {
            InitializeComponent();
            switch (Properties.Settings.Default.Language.ToString())
            {
                case "ru-RU":
                    comboBox1.SelectedIndex = 1;
                    break;
                default:
                    comboBox1.SelectedIndex = 0;
                    break;
            }
            switch (Properties.Settings.Default.GameMode)
            {
                case "Classic":
                    radioButton1.Checked = true;
                    break;
                default:
                    radioButton2.Checked = true;
                    break;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mouseCoordinates = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (e.X > mouseCoordinates.X)
                {
                    this.Location = new Point(this.Location.X + 1, this.Location.Y);
                }
                else if (e.X < mouseCoordinates.X)
                {
                    this.Location = new Point(this.Location.X - 1, this.Location.Y);
                }
                if (e.Y > mouseCoordinates.Y)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y + 1);
                }
                else if (e.Y < mouseCoordinates.Y)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y - 1);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    ChangeLanguage("ru-RU");
                    Properties.Settings.Default.Language = new CultureInfo("ru-RU");
                    break;
                default:
                    ChangeLanguage("en-US");
                    Properties.Settings.Default.Language = new CultureInfo("en-US");
                    break;
            }
        }

        private void ChangeLanguage(string newLanguageString)
        {
            CultureInfo newCultureInfo = new CultureInfo(newLanguageString);
            ComponentResourceManager resources;
            foreach (Form item in Application.OpenForms)
            {
                resources = new ComponentResourceManager(item.GetType());
                foreach (Control control in item.Controls)
                {                    
                    resources.ApplyResources(control, control.Name, newCultureInfo);
                }
                if (item is Form1)
                {
                    Form1.SetMessageBoxProperties(resources, newCultureInfo);
                    Form1 form1 = item as Form1;
                    form1.openFileDialog1.Title = resources.GetString("openFileDialog1.Title", newCultureInfo);
                    form1.openFileDialog1.Filter = resources.GetString("openFileDialog1.Filter", newCultureInfo);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Properties.Settings.Default.GameMode = "Classic";
            else Properties.Settings.Default.GameMode = "Image";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
