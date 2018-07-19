using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using YoutubeExtractor;

namespace YouTubeDownloaderApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<VideoInfo> videos = DownloadUrlResolver.GetDownloadUrls(textBox1.Text);
                VideoInfo video = videos.First(p => p.VideoType == VideoType.Mp4 && p.Resolution == Convert.ToInt32(comboBox1.Text));
                if (video != null)
                {
                    if (video.RequiresDecryption)
                    {
                        DownloadUrlResolver.DecryptDownloadUrl(video);
                    }
                    saveFileDialog1.FileName = video.Title;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        VideoDownloader downloader = new VideoDownloader(video, saveFileDialog1.FileName);
                        downloader.DownloadProgressChanged += downloader_DownloadProgressChanged;
                        downloader.DownloadFinished += downloader_DownloadFinished;
                        Thread thread = new Thread(() => { downloader.Execute(); })
                        {
                            IsBackground = true
                        };
                        thread.Start();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void downloader_DownloadFinished(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate()
                {
                    progressBar1.Value = 0;
                    label3.Text = "0%";
                    progressBar1.Update(); 
                    if (checkBox1.Checked)
                    {
                        Process.Start((sender as VideoDownloader).SavePath);
                    }
                }
                ));
        }

        private void downloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            Invoke(new MethodInvoker(delegate()
                {
                    progressBar1.Value = (int)e.ProgressPercentage;
                    label3.Text = string.Format("{0:0.##}%", e.ProgressPercentage);
                    progressBar1.Update();                    
                }
                ));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("https://", "http // ");
        }
    }
}
