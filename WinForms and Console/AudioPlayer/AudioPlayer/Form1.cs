using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class Form1 : Form
    {
        private ListViewHitTestInfo listViewHitTestInfo;
        private ToolTip toolTip;

        public Form1()
        {
            InitializeComponent();
            CommonInterface.Link1 = this;
            CommonInterface.SetFileFilter();
            Audio.InitAudio(Audio.HZ);
            comboBox1.SelectedIndex = Properties.Settings.Default.RepeatMode;
            checkBox1.Checked = Properties.Settings.Default.RandomMode;
            colorSlider2.Value = Properties.Settings.Default.Volume;
            checkBox2.Checked = Properties.Settings.Default.SoundOff;
            CommonInterface.Volume = Properties.Settings.Default.Volume2;
            Audio.Volume = colorSlider2.Value;
        }

        public Form1(string path, bool isFile = false)
        {
            InitializeComponent();
            CommonInterface.Link1 = this;
            CommonInterface.SetFileFilter();
            Audio.InitAudio(Audio.HZ);
            comboBox1.SelectedIndex = Properties.Settings.Default.RepeatMode;
            checkBox1.Checked = Properties.Settings.Default.RandomMode;
            colorSlider2.Value = Properties.Settings.Default.Volume;
            checkBox2.Checked = Properties.Settings.Default.SoundOff;
            CommonInterface.Volume = Properties.Settings.Default.Volume2;
            Audio.Volume = colorSlider2.Value;
            if (isFile)
            {
                CommonInterface.AddTrackOrURL(path);
            }
            else
            {
                CommonInterface.GetFilesFromFolder(path);
            }
        }

        private void button_Open_file_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            listView1.BeginUpdate();
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
            listView1.EndUpdate();
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            if (!CommonInterface.Pause)
            {
                CommonInterface.Play(false);
            }
            else if (CommonInterface.CurrentTrackNumber >= 0)
            {
                CommonInterface.Play(true);
            }
            else
            {
                CommonInterface.Play(false);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = TimeSpan.FromSeconds(Audio.GetPosOfStream(Audio.Stream)).ToString();
            colorSlider1.Value = Audio.GetPosOfStream(Audio.Stream);
            if (Audio.ToNextTrack())
            {
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
            if (!Audio.isStoped)
            {
                Audio.SetPosOfScroll(Audio.Stream, colorSlider1.Value);
            }
            else if (e.OldValue != -1)
            {
                colorSlider1.Value = e.OldValue;
            }
            else
            {
                colorSlider1.Value = 0;
            }
        }

        private void colorSlider2_Scroll(object sender, ScrollEventArgs e)
        {
            CommonInterface.SetVolume();
        }

        private void button_Pause_Click(object sender, EventArgs e)
        {
            if (CommonInterface.Pause)
            {
                CommonInterface.Play(true);
            }
            else
            {
                Audio.Pause();
            }
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
                while (CommonInterface.Files.Count > 0)
                {
                    if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                    {
                        CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);                  
                        return;
                    }
                    else
                    {
                        CommonInterface.DeleteTrack(CommonInterface.CurrentTrackNumber);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                CommonInterface.ClearForm();
                while (CommonInterface.Files.Count > 0)
                {
                    CommonInterface.CurrentTrackNumber = CommonInterface.Files.Count - 1;
                    if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                    {
                        CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);                   
                        return;
                    }
                    else
                    {
                        CommonInterface.DeleteTrack(CommonInterface.CurrentTrackNumber);
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0 && CommonInterface.CurrentTrackNumber >= 0)
            {
                if (!Audio.Random && CommonInterface.CurrentTrackNumber > 0)
                {
                    CommonInterface.ClearForm();
                    while (CommonInterface.CurrentTrackNumber > 0)
                    {
                        CommonInterface.CurrentTrackNumber--;
                        if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                        {
                            CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);                   
                            return;
                        }
                        else
                        {
                            CommonInterface.DeleteTrack(CommonInterface.CurrentTrackNumber);
                        }
                    }
                    if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                    {
                        CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);                  
                    }
                }
                else if (Audio.Random)
                {
                    CommonInterface.RandomTrack();
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0 && CommonInterface.CurrentTrackNumber >= 0)
            {
                if (!Audio.Random && CommonInterface.CurrentTrackNumber < CommonInterface.Files.Count - 1)
                {
                    while (CommonInterface.CurrentTrackNumber < CommonInterface.Files.Count - 1)
                    {
                        CommonInterface.CurrentTrackNumber++;
                        if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                        {
                            CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);                   
                            return;
                        }
                        else
                        {
                            CommonInterface.DeleteTrack(CommonInterface.CurrentTrackNumber);
                            CommonInterface.CurrentTrackNumber--;
                        }
                    }
                    if (Audio.Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Audio.Volume))
                    {
                        CommonInterface.RefreshForm(CommonInterface.CurrentTrackNumber);                   
                    }
                }
                else if (Audio.Random)
                {
                    CommonInterface.RandomTrack();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CommonInterface.ClearForm();
            Form2 form2 = new Form2(Audio.Volume, checkBox2.Checked);
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
                else if (listView1.SelectedItems[0].Index < CommonInterface.CurrentTrackNumber)
                {
                    CommonInterface.CurrentTrackNumber--;
                }
                CommonInterface.DeleteTrack(listView1.SelectedItems[0].Index);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CommonInterface.Play(false);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Audio.Free();
            Properties.Settings.Default.Volume = colorSlider2.Value;
            Properties.Settings.Default.RepeatMode = comboBox1.SelectedIndex;
            Properties.Settings.Default.RandomMode = checkBox1.Checked;
            Properties.Settings.Default.SoundOff = checkBox2.Checked;
            Properties.Settings.Default.Volume2 = CommonInterface.Volume;
            Properties.Settings.Default.Save();
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
                listView1.BeginUpdate();
                CommonInterface.GetFilesFromFolder(folderBrowserDialog1.SelectedPath);
                listView1.EndUpdate();
            }
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                button6_Click(this, new EventArgs());
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                CommonInterface.GetDataFromClipboard();
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                listView1.BeginUpdate();
                foreach (string filePath in files)
                {
                    if (Directory.Exists(filePath))
                    {
                        CommonInterface.GetFilesFromFolder(filePath);
                    }
                    else
                    {
                        CommonInterface.CheckExtension(new FileInfo(filePath));
                    }
                }
                listView1.EndUpdate();
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
                toolTip.Popup += toolTip_Popup;
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

        private void toolTip_Popup(object sender, PopupEventArgs e)
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

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Equals(toolStripMenuItem1))
            {
                CommonInterface.Play(false);
            }
            else if (e.ClickedItem.Equals(toolStripMenuItem3))
            {
                CommonInterface.GetDataFromClipboard();
            }
            else
            {
                button6_Click(this, new EventArgs());
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem2.Visible = false;
                toolStripMenuItem3.Visible = true;
            }
            else
            {
                toolStripMenuItem1.Visible = true;
                toolStripMenuItem2.Visible = true;
                toolStripMenuItem3.Visible = false;
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = new Size(100, 100);
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            if (pictureBox4.Image != null)
            {
                e.Graphics.DrawImage(pictureBox4.Image, new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height));
            }
            else e.Graphics.DrawImage(pictureBox4.BackgroundImage, new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height));
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show(".", pictureBox4, 0, -105, int.MaxValue);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(pictureBox4);
        }

        private void colorSlider1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.PageDown:
                case Keys.PageUp:
                case Keys.Home:
                case Keys.End:
                    if (!Audio.isStoped)
                    {
                        e.Handled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (colorSlider2.Value >= colorSlider2.MouseWheelBarPartitions)
                {
                    colorSlider2.Value -= colorSlider2.MouseWheelBarPartitions;
                }
                else
                {
                    colorSlider2.Value = colorSlider2.Minimum;
                }
            }
            else
            {
                if (colorSlider2.Value <= colorSlider2.Maximum - colorSlider2.MouseWheelBarPartitions)
                {
                    colorSlider2.Value += colorSlider2.MouseWheelBarPartitions;
                }
                else
                {
                    colorSlider2.Value = colorSlider2.Maximum;
                }
            }
            CommonInterface.SetVolume();
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }

        private void listView1_MouseEnter(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                listView1.Focus();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (CommonInterface.Link3 == null)
            {
                Form3 form3 = new Form3();
                form3.Show();
            }
            else
            {
                CommonInterface.Link3.groupBox1.Focus();
            }
        }
    }
}
