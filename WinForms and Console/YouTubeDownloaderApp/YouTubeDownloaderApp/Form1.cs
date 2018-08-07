using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using YoutubeExtractor;

namespace YouTubeDownloaderApp
{
    public partial class Form1 : Form
    {
        Thread thread;
        VideoDownloader downloader;

        private void ResetForm()
        {
            thread = null;
            button1.Text = "&Download";
            textBox1.Text = string.Empty;
            progressBar1.Value = 0;
            label3.Text = "0%";
            progressBar1.Update();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (thread == null)
            {
                try
                {
                    string url = textBox1.Text.Replace("https://", "http // ");
                    try
                    {
                        url = url.Remove(url.IndexOf("&"));
                    }
                    catch
                    {

                    }
                    IEnumerable<VideoInfo> videos = DownloadUrlResolver.GetDownloadUrls(url);
                    VideoInfo video = videos.First(p => p.VideoType == VideoType.Mp4 && p.Resolution == Convert.ToInt32(comboBox1.Text));
                    if (video != null)
                    {
                        if (video.RequiresDecryption)
                        {
                            DownloadUrlResolver.DecryptDownloadUrl(video);
                        }
                        saveFileDialog1.FileName = video.Title;
                        saveFileDialog1.InitialDirectory = Properties.Settings.Default.initialDirectory;
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Properties.Settings.Default.initialDirectory = Path.GetDirectoryName(saveFileDialog1.FileName);
                            Properties.Settings.Default.Save();
                            downloader = new VideoDownloader(video, saveFileDialog1.FileName);
                            downloader.DownloadProgressChanged += downloader_DownloadProgressChanged;
                            downloader.DownloadFinished += downloader_DownloadFinished;
                            thread = new Thread(() => { downloader.Execute(); })
                            {
                                IsBackground = true
                            };
                            thread.Start();
                            button1.Text = "&Cancel";
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                thread.Abort();
                while (thread.ThreadState != ThreadState.Aborted)
                {

                }
                downloader.DownloadProgressChanged -= downloader_DownloadProgressChanged;
                downloader.DownloadFinished -= downloader_DownloadFinished;                
                ResetForm();
                try
                {
                    File.Delete(saveFileDialog1.FileName);
                }
                catch (Exception)
                {

                }
            }
        }

        void downloader_DownloadFinished(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate()
                {
                    ResetForm();
                    if (checkBox1.Checked)
                    {
                        System.Diagnostics.Process.Start((sender as VideoDownloader).SavePath);
                    }
                }
                ));
        }

        private void downloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            Invoke(new MethodInvoker(delegate()
                {
                    if (thread != null)
                    {
                        progressBar1.Value = (int)e.ProgressPercentage;
                        label3.Text = string.Format("{0:0.##}%", e.ProgressPercentage);
                        progressBar1.Update();
                    }
                }
                ));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
        }
    }
}
