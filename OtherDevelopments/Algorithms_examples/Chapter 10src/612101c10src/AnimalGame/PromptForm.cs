using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalGame
{
    public partial class PromptForm : Form
    {
        public PromptForm()
        {
            InitializeComponent();
        }

        // Validate and hide the form.
        private void okButton_Click(object sender, EventArgs e)
        {
            if (responseTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please make a response.");
            }
            else
            {
                this.Hide();
            }
        }
    }
}
