using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class Form2 : Form
    {
        public Form2(int volume)
        {
            InitializeComponent();
            CommonInterface.Link2 = this;
            colorSlider1.Value = volume;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CommonInterface.IsValid(textBox1.Text))
            {
                Audio.PlayRadio(textBox1.Text, Audio.Volume);
                CommonInterface.Iterator = 0;
                if (Audio.Stream == 0)
                {
                    MessageBox.Show("Радиостанция не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    timer1.Enabled = true;
                }
            }
        }

        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            Audio.SetVolumeToStream(Audio.Stream, colorSlider1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Audio.Stop();
            CommonInterface.Iterator = 0;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.BackColor = Color.Black;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            button2_Click(this, new EventArgs());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CommonInterface.Visualisation(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            CommonInterface.ReadPlayList(openFileDialog1.FileName, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                saveFileDialog1.ShowDialog();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            CommonInterface.SavePlayList(saveFileDialog1.FileName, true);
        }
    }
}
