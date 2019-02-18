using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoRun
{
    public partial class Form1 : Form
    {
        RegistryKey key;

        public Form1()
        {
            InitializeComponent();
            key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run\");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Введите имя переменной реестра.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Введите путь к файлу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!File.Exists(textBox2.Text))
                {
                    MessageBox.Show("Файл " + textBox2.Text + " не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    key.SetValue(textBox1.Text, textBox2.Text);
                    MessageBox.Show("Значение успешно добавлено в реестра.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != string.Empty)
                {
                    key.DeleteValue(textBox1.Text);
                    MessageBox.Show("Значение успешно удалено из реестра.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Введите имя переменной реестра.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            key.Flush();
            key.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(key);
            form2.ShowDialog();
            if (form2.KeyValue != null)
            {
                textBox1.Text = form2.KeyValue.Item1;
                textBox2.Text = form2.KeyValue.Item2;
            }
        }
    }
}
