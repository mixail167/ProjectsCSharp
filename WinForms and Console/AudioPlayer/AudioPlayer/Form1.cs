using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class Form1 : Form
    {
        ListViewHitTestInfo listViewHitTestInfo;
        ToolTip toolTip;

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
                    CommonInterface.ReadPlayList(item);
                }
                else
                {
                    CommonInterface.AddTrackOrURL(item);
                }
            }
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            if ((listView1.Items.Count != 0) && (listView1.SelectedItems.Count != 0))
            {
                string current = CommonInterface.Files[listView1.SelectedItems[0].Index].Path;
                CommonInterface.CurrentTrackNumber = listView1.SelectedItems[0].Index;
                label4.Text = CommonInterface.Files[listView1.SelectedItems[0].Index].FileName;
                if (Audio.Play(current, Audio.Volume))
                {
                    CommonInterface.RefreshForm(listView1.SelectedItems[0].Index);
                    timer1.Enabled = true;
                }
                else CommonInterface.ClearForm();
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
            else if (Audio.Stream == 0)
                CommonInterface.ClearForm();
            if (Audio.EndPlaylist)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    CommonInterface.ClearForm();
                }
                CommonInterface.CurrentTrackNumber = -1;
                Audio.EndPlaylist = false;
            }
            CommonInterface.Visualisation();
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            CommonInterface.ClearForm();
        }

        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            Audio.SetPosOfScroll(Audio.Stream, colorSlider1.Value);
        }

        private void colorSlider2_Scroll(object sender, ScrollEventArgs e)
        {
            CommonInterface.Volume = -1;
            checkBox2.Checked = false;
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
                CommonInterface.ClearForm();
                CommonInterface.CurrentTrackNumber = 0;
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                {
                    CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                    timer1.Enabled = true;
                }
                else timer1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                CommonInterface.ClearForm();
                CommonInterface.CurrentTrackNumber = CommonInterface.Files.Count - 1;
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                {
                    CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                    timer1.Enabled = true;
                }
                else timer1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0 && CommonInterface.CurrentTrackNumber > 0)
            {
                CommonInterface.ClearForm();
                CommonInterface.CurrentTrackNumber--;
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                {
                    CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                    timer1.Enabled = true;
                }
                else timer1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0 && CommonInterface.CurrentTrackNumber >= 0 && CommonInterface.CurrentTrackNumber < CommonInterface.Files.Count - 1)
            {
                CommonInterface.ClearForm();
                CommonInterface.CurrentTrackNumber++;
                label4.Text = CommonInterface.Files[CommonInterface.CurrentTrackNumber].FileName;
                if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                {
                    CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);
                    timer1.Enabled = true;
                }
                else timer1.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CommonInterface.ClearForm();
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
                    CommonInterface.ClearForm();
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
            else if (e.Button == MouseButtons.Left)
            {
                button_Play_Click(this, new EventArgs());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Audio.Free();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                saveFileDialog1.ShowDialog();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            CommonInterface.SavePlayList(saveFileDialog1.FileName, false);
        }

        private void button_open_folder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CommonInterface.GetFilesFromFolder(folderBrowserDialog1.SelectedPath);
            }
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                button6_Click(this, new EventArgs());
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    CommonInterface.CheckExtension(new FileInfo(filePath));
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                CommonInterface.Volume = colorSlider2.Value;
                colorSlider2.Value = 0;
                Audio.SetVolumeToStream(Audio.Stream, colorSlider2.Value);
            }
            else if (CommonInterface.Volume != -1)
            {
                colorSlider2.Value = CommonInterface.Volume;
                Audio.SetVolumeToStream(Audio.Stream, colorSlider2.Value);
            }
        }

        private void listView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            if (listViewHitTestInfo != null && listViewHitTestInfo.Location == ListViewHitTestLocations.Label)
            {
                toolTip = new ToolTip();
                toolTip.Popup += toolTip1_Popup;
                toolTip.SetToolTip(listView1, listViewHitTestInfo.SubItem.Text);
            }
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                listViewHitTestInfo = listView1.HitTest(e.Location);
            }
            catch (Exception)
            {

            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            try
            {
                if (listViewHitTestInfo.SubItem.Text != toolTip.GetToolTip(listView1))
                    throw new Exception();
            }
            catch (Exception)
            {
                e.Cancel = true;
            }
        }
    }
}
