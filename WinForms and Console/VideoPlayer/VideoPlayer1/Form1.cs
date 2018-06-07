using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VideoPlayer1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Reset();
            openFileDialog1.Filter = "Все форматы|*.avi;*.mp4;*.mkv;*.wmv;*.3gp"
                + "|Audio Video Interleave (*.avi)|*.avi"
                + "|MPEG-4 (*.mp4)|*.mp4"
                + "|Matroska (*.mkv)|*.mkv"
                + "|Windows Media Video (*.wmv)|*.wmv"
                + "|Third Generation Partnership Project (*.3gp)|*.3gp";
        }

        private void Reset()
        {
            label4.Text = string.Empty;
            label1.Text = "00:00:00/00:00:00";
            colorSlider1.Value = 0;
            colorSlider1.Maximum = 100;
        }

        private void EnableComponent(bool flag)
        {
            timer1.Enabled = flag;
            if (flag)
            {
                button1.Text = "Пауза";
            }
            else
            {
                button1.Text = "Играть";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VideoClass.isInit)
            {
                if (VideoClass.isPlaying)
                {
                    VideoClass.Pause();
                    EnableComponent(false);
                }
                else
                {
                    VideoClass.Play();
                    EnableComponent(true);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            VideoClass.Size = panel1.Size;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            VideoClass.Dispose();
            button2_Click(this, new EventArgs());
            Reset();
            if (VideoClass.Init(openFileDialog1.FileName, panel1))
            {
                VideoClass.Volume = colorSlider2.Value - 10000;
                label4.Text = VideoClass.FileName;
                colorSlider1.Maximum = VideoClass.Duration;
            }
            else
            {
                MessageBox.Show("Воспроизведение этого файла невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VideoClass.Stop();
            EnableComponent(false);
            colorSlider1.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = VideoClass.UpdateProgressText();
            colorSlider1.Value = Convert.ToInt32(VideoClass.Position);
            if (VideoClass.isEnding)
            {
                if (!checkBox1.Checked)
                {
                    button2_Click(this, new EventArgs());
                }
                VideoClass.Position = 0.0;
                label1.Text = VideoClass.UpdateProgressText();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (label4.Text != string.Empty)
            {
                MessageBox.Show(VideoClass.GetInfo(), "Информация о файле", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            if (VideoClass.isInit)
            {
                VideoClass.Position = colorSlider1.Value;
            }
            else
            {
                colorSlider1.Value = 0;
            }
        }

        private void colorSlider2_Scroll(object sender, ScrollEventArgs e)
        {
            VideoClass.Volume = colorSlider2.Value - 10000;
        }

        private void colorSlider2_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = string.Format("{0:f1}%", (colorSlider2.Value) / 100f);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            button2_Click(this, new EventArgs());
            VideoClass.Dispose();
        }
    }
}
