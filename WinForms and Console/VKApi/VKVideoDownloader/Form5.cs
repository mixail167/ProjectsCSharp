using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using VKVideoDownloader.Properties;

namespace VKVideoDownloader
{
    public partial class Form5 : MetroForm
    {
        readonly WebClient client;
        readonly List<Video> list;
        bool cancel;

        public Form5(List<Video> list)
        {
            InitializeComponent();
            StyleManager = metroStyleManager1;
            metroCheckBox1.Checked = Settings.Default.Remember1;
            if (metroCheckBox1.Checked)
            {
                metroTextBoxPlaceHolder5.Text = Settings.Default.Path;
            }
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            this.list = list;
        }

        private void MetroButton6_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                metroTextBoxPlaceHolder5.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private async Task Download()
        {
            Exception exception = null;
            try
            {
                if (Directory.Exists(metroTextBoxPlaceHolder5.Text))
                {
                    string path = metroTextBoxPlaceHolder5.Text;
                    if (!path.EndsWith("\\"))
                    {
                        path += "\\";
                    }
                    if (metroCheckBox1.Checked)
                    {
                        Settings.Default.Path = metroTextBoxPlaceHolder5.Text;
                    }
                    Settings.Default.Remember1 = metroCheckBox1.Checked;
                    Settings.Default.Save();
                    metroButton7.Text = "Отменить";
                    metroProgressBar2.Maximum = list.Count;
                    cancel = false;
                    char[] invalidChars = Path.GetInvalidFileNameChars();
                    for (int i = 0; i < list.Count&&!cancel; i++)
                    {
                        string title = list[i].Title;
                        foreach (char item in invalidChars)
                        {
                            if (title.IndexOf(item) != -1)
                            {
                                title = title.Replace(item, '_');
                            }
                        }
                        string fullpath = path + title + ".mp4";
                        metroLabel10.Text = string.Format("Путь: {0}.", fullpath);
                        metroLabel12.Text = string.Format("Загружается видео {0}/{1}.", i + 1, list.Count);
                        try
                        {
                            await client.DownloadFileTaskAsync(list[i].CurrentFile.Item2, fullpath);
                        }
                        catch (Exception ex)
                        {
                            richTextBox1.Text += string.Format("Видео {0}: {1}\n", list[i].Title, ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                CancelDownload(exception);
            }
        }

        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if(e.Error != null)
                {
                    CancelDownload(e.Error);
                }
                else
                {
                    metroProgressBar2.Value++;
                }
            }
            metroProgressBar1.Value = 0;

        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            metroProgressBar1.Value = e.ProgressPercentage;
            metroLabel11.Text = string.Format("Загружено {0}/{1}.", GetSize(e.BytesReceived), GetSize(e.TotalBytesToReceive));
        }

        private async void MetroButton7_Click(object sender, EventArgs e)
        {
            if (metroButton7.Text == "Загрузить")
            {
                await Download();
            }
            else
            {
                cancel = true;
                client.CancelAsync();
            }
        }

        private void CancelDownload(Exception exception)
        {
            metroButton7.Text = "Загрузить";
            metroProgressBar2.Value = 0;
            metroLabel10.Text = string.Empty;
            metroLabel11.Text = string.Empty;
            metroLabel12.Text = string.Empty;
            if (exception != null)
            {
                richTextBox1.Text += exception.Message + "\n";
            }
        }

        private string GetSize(long value)
        {
            double new_value;
            string unit;
            if (value >= 1073741824)
            {
                new_value = value * 1.0 / 1073741824;
                unit = "Гб";
                return string.Format("{0:f1} {1}", new_value, unit);
            }
            else if (value >= 1048576)
            {
                new_value = value * 1.0 / 1048576;
                unit = "Мб";
                return string.Format("{0:f2} {1}", new_value, unit);
            }
            else if (value >= 1024)
            {
                new_value = value * 1.0 / 1024;
                unit = "Кб";
                return string.Format("{0:f2} {1}", new_value, unit);
            }
            else
            {
                unit = "Б";
                return string.Format("{0} {1}", value, unit);
            }
        }

        private async void MetroTextBoxPlaceHolder3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (metroButton7.Text == "Загрузить")
                {
                    await Download();
                }
                else
                {
                    client.CancelAsync();
                }
            }
        }
    }
}
