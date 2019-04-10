using System;
using System.Diagnostics;
using System.Windows.Forms;
using Translator.Properties;

namespace Translator
{
    public partial class Form1 : Form
    {
        Translator translator;

        public Form1()
        {
            InitializeComponent();
            translator = new Translator();
            comboBox1.SelectedItem = Settings.Default.InputLang;
            comboBox2.SelectedItem = Settings.Default.OutputLang;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object obj = comboBox1.SelectedItem;
            comboBox1.SelectedItem = comboBox2.SelectedItem;
            comboBox2.SelectedItem = obj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Translate();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Shift)
            {
                Translate();
            }
        }

        private void Translate()
        {
            if (richTextBox1.Text != string.Empty)
            {
                try
                {
                    richTextBox2.Clear();
                    richTextBox2.Text = translator.Translate(richTextBox1.Text, translator.GetLangPair(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.InputLang = comboBox1.SelectedItem.ToString();
            Settings.Default.Save();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.OutputLang = comboBox2.SelectedItem.ToString();
            Settings.Default.Save();
        }
    }
}
