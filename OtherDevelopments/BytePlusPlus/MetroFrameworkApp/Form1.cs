using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetroFrameworkApp
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();

            this.StyleManager = metroStyleManager1;
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            metroTabControl1.Visible = metroToggle1.Checked;
        }
    }
}
