using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class Form2 : Form
    {
        public Form2(int volume, bool soundOff)
        {
            InitializeComponent();
            CommonInterface.Link2 = this;
            colorSlider1.Value = volume;
            checkBox2.Checked = soundOff;
            CommonInterface.Volume = Properties.Settings.Default.Volume2;
            Audio.Volume = colorSlider1.Value;
        }

        public Form2()
        {
            InitializeComponent();
            CommonInterface.Link2 = this;
            colorSlider1.Value = Properties.Settings.Default.Volume;
            checkBox2.Checked = Properties.Settings.Default.SoundOff;
            CommonInterface.Volume = Properties.Settings.Default.Volume2;
            Audio.Volume = colorSlider1.Value;
        }

        public Form2(string path, bool isFile = false)
        {
            InitializeComponent();
            CommonInterface.Link2 = this;
            colorSlider1.Value = Properties.Settings.Default.Volume;
            checkBox2.Checked = Properties.Settings.Default.SoundOff;
            CommonInterface.Volume = Properties.Settings.Default.Volume2;
            Audio.Volume = colorSlider1.Value;
            if (isFile)
            {
                CommonInterface.ReadPlayList(path, true);
            }
            else
            {
                CommonInterface.GetFilesFromFolder(path, true);
            }
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
            if (CommonInterface.IsValid(textBox1.Text, CommonInterface.URLPattern) && !CommonInterface.IsValid(textBox1.Text, CommonInterface.PathPattern2))
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
            Properties.Settings.Default.SoundOff = checkBox2.Checked;
            Properties.Settings.Default.Volume = colorSlider1.Value;
            Properties.Settings.Default.Volume2 = CommonInterface.Volume;
            Properties.Settings.Default.Save();
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
                CommonInterface.RadioAddreses.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                textBox1.Text = CommonInterface.RadioAddreses[listBox1.SelectedIndex];
            }
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                CommonInterface.Volume = colorSlider1.Value;
                colorSlider1.Value = 0;
                Audio.SetVolumeToStream(Audio.Stream, colorSlider1.Value);
            }
            else if (CommonInterface.Volume != -1)
            {
                colorSlider1.Value = CommonInterface.Volume;
                Audio.SetVolumeToStream(Audio.Stream, colorSlider1.Value);
            }
        }

        private void button_open_folder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CommonInterface.GetFilesFromFolder(folderBrowserDialog1.SelectedPath, true);
            }
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    if (Directory.Exists(filePath))
                    {
                        CommonInterface.GetFilesFromFolder(filePath, true);
                    }
                    else
                    {
                        CommonInterface.CheckExtension(new FileInfo(filePath), true);
                    }
                }
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Equals(toolStripMenuItem1))
            {
                button1_Click(this, new EventArgs());
            }
            else
            {
                button4_Click(this, new EventArgs());
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listBox1.Items.Count == 0 || listBox1.SelectedIndex == -1)
            {
                e.Cancel = true;
            }
        }

        private void listView1_Leave(object sender, EventArgs e)
        {
            listBox1.SelectedItems.Clear();
        }
    }
}
