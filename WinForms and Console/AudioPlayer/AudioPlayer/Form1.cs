using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            CommonInterface.Link1 = this;
            CommonInterface.SetFileFilter();
            Audio.InitAudio(Audio.HZ);
            comboBox1.SelectedIndex = 0;
        }

        private void button_Open_file_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            foreach (string item in openFileDialog1.FileNames)
            {
                if (item.EndsWith(".m3u"))
                {
                    CommonInterface.ReadPlayList(item, false);
                }
                else
                {
                    CommonInterface.AddTrackOrURL(item, false);
                }
            }
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            if ((listView1.Items.Count != 0) && (listView1.SelectedItems.Count != 0))
            {
                string current = CommonInterface.Files[listView1.SelectedItems[0].Index].Path;
                CommonInterface.CurrentTrackNumber = listView1.SelectedItems[0].Index;
                Audio.Play(current, Audio.Volume);
                label4.Text = CommonInterface.Files[listView1.SelectedItems[0].Index].FileName;
                CommonInterface.RefreshForm(listView1.SelectedItems[0].Index);
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = TimeSpan.FromSeconds(Audio.GetPosOfStream(Audio.Stream)).ToString();
            colorSlider1.Value = Audio.GetPosOfStream(Audio.Stream);
            if (Audio.ToNextTrack())
            {
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
            }
            if (Audio.EndPlaylist)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    button_Stop_Click(this, new EventArgs());
                }
                CommonInterface.CurrentTrackNumber = -1;
                Audio.EndPlaylist = false;
            }
            CommonInterface.Visualisation(false);
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            Audio.Stop();
            timer1.Enabled = false;
            colorSlider1.Value = 0;
            label1.Text = "00:00:00";
            label2.Text = "00:00:00";
            label4.Text = string.Empty;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.BackColor = Color.Black;
        }

        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            Audio.SetPosOfScroll(Audio.Stream, colorSlider1.Value);
        }

        private void colorSlider2_Scroll(object sender, ScrollEventArgs e)
        {
            Audio.SetVolumeToStream(Audio.Stream, colorSlider2.Value);
        }

        private void button_Pause_Click(object sender, EventArgs e)
        {
            Audio.Pause();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                Audio.Repeat = true;
            }
            else
            {
                Audio.Repeat = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Audio.Random = true;
            }
            else
            {
                Audio.Random = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                button_Stop_Click(this, new EventArgs());
                CommonInterface.CurrentTrackNumber = 0;
                Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume);
                CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                timer1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                button_Stop_Click(this, new EventArgs());
                CommonInterface.CurrentTrackNumber = CommonInterface.Files.Count - 1;
                Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume);
                CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                timer1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0 && CommonInterface.CurrentTrackNumber > 0)
            {
                button_Stop_Click(this, new EventArgs());
                CommonInterface.CurrentTrackNumber--;
                Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume);
                CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                timer1.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0 && CommonInterface.CurrentTrackNumber >= 0 && CommonInterface.CurrentTrackNumber < CommonInterface.Files.Count - 1)
            {
                button_Stop_Click(this, new EventArgs());
                CommonInterface.CurrentTrackNumber++;
                Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume);
                CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                timer1.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button_Stop_Click(this, new EventArgs());
            Form2 form2 = new Form2(Audio.Volume);
            form2.ShowDialog();
            colorSlider2.Value = Audio.Volume;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0 && listView1.SelectedItems.Count != 0)
            {
                if (listView1.SelectedItems[0].Index == CommonInterface.CurrentTrackNumber)
                {
                    button_Stop_Click(this, new EventArgs());
                    CommonInterface.CurrentTrackNumber = -1;
                }
                CommonInterface.Files.RemoveAt(listView1.SelectedItems[0].Index);
                listView1.Items.RemoveAt(listView1.SelectedItems[0].Index);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                button6_Click(this, new EventArgs());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Audio.Free();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count!=0)
            {
                saveFileDialog1.ShowDialog();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            CommonInterface.SavePlayList(saveFileDialog1.FileName, false);
        }
    }
}
