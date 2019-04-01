using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VKVideoDownloader.Properties;

namespace VKVideoDownloader
{
    public partial class Form2 : MetroForm
    {
        string access_token;
        long id;
        ListViewWPF list;
        WebClient client;

        public Form2(string access_token, string id)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.id = 0;
            metroCheckBox1.Checked = Settings.Default.Remember1;
            if (metroCheckBox1.Checked)
            {
                metroTextBoxPlaceHolder3.Text = Settings.Default.Path;
            }
            client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            list = elementHost1.Child as ListViewWPF;
            GetProfileInfo(access_token, id);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format(Resources.Page, id));
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1(false);
            form1.ShowDialog();
            if (form1.DialogResult == DialogResult.OK)
            {
                GetProfileInfo(form1.AccessToken, form1.ID);
            }

        }

        private void GetProfileInfo(string access_token, string id)
        {
            this.access_token = access_token;
            this.id = Convert.ToInt64(id);
            linkLabel1.Text = "Неизвестный пользователь";
            if (InterNet.IsConnected)
            {
                string url = string.Format(Resources.GetProfileInfo, access_token);
                Request request = new Request(url);
                try
                {
                    dynamic json = JObject.Parse(request.Get());
                    if (json.response.first_name != string.Empty || json.response.last_name != string.Empty)
                    {
                        linkLabel1.Text = string.Format("{0} {1}", json.response.first_name, json.response.last_name);
                    }
                }
                catch (Exception)
                {
                    metroLabel13.Text = "Ошибка при получении данных";
                }
            }
            else
            {
                metroLabel13.Text = "Отсутствует соединение с Интернет";
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            MetroTextBoxPlaceHolder metroTextBox = sender as MetroTextBoxPlaceHolder;
            if (!metroTextBox.isPlaceHolder())
            {
                if (Functions.CheckValid(metroTextBox.Text, "^[0-9]{0,12}$"))
                {
                    metroTextBox.Tag = metroTextBox.Text;
                }
                else if (metroTextBox.Tag != null)
                {
                    metroTextBox.Text = metroTextBox.Tag.ToString();
                }
            }
        }

        private async void metroTextBoxPlaceHolder2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await GetVideos();
            }
        }

        private async Task<int> GetCount(string url, long id, long album)
        {
            url = string.Format(url, access_token, id, album, 0);
            Request request = new Request(url);
            int count = 0;
            try
            {
                JObject json = JObject.Parse(await request.GetAsync());
                if (json.ContainsKey("response"))
                {
                    JObject response = json["response"] as JObject;
                    if (response.ContainsKey("count"))
                    {
                        count = Convert.ToInt32(response["count"]);
                    }
                }
                else if (json.ContainsKey("error"))
                {
                    JObject error = json["error"] as JObject;
                    int code = Convert.ToInt32(error["error_code"]);
                    string message = error["error_msg"].ToString();
                    metroLabel13.Text = string.Format("Ошибка {0}: {1}", code, message);
                }
            }
            catch (Exception)
            {

            }
            return count;
        }

        private async Task GetVideos()
        {
            list.ClearSource();
            metroTextBoxPlaceHolder4.Text = metroTextBoxPlaceHolder4.PlaceHolder;
            metroLabel8.ForeColor = Color.Red;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox2.Tag = null;
            pictureBox3.Tag = null;
            pictureBox4.Tag = null;
            metroLabel6.Text = "0";
            metroLabel13.Text = "Пожалуйста, подождите. Выполняется поиск видео";
            if (InterNet.IsConnected)
            {
                long album = (metroTextBoxPlaceHolder2.Text == string.Empty || metroTextBoxPlaceHolder2.isPlaceHolder()) ? 0 : Convert.ToInt64(metroTextBoxPlaceHolder2.Text);
                long id = (metroTextBoxPlaceHolder1.Text == string.Empty || metroTextBoxPlaceHolder1.isPlaceHolder()) ? this.id : Convert.ToInt64(metroTextBoxPlaceHolder1.Text);
                string url;
                if (metroRadioButton1.Checked || (metroRadioButton2.Checked && id == this.id))
                {
                    url = Resources.GetVideos;
                }
                else
                {
                    url = Resources.GetVideosWithMinus;
                }
                int count = await GetCount(url, id, album);
                if (count > 0)
                {
                    int countThreads = Convert.ToInt32(Math.Ceiling(count * 1.0 / 200));
                    Form3 form3 = new Form3(access_token, id, album, url, countThreads);
                    form3.ShowDialog();
                    switch (form3.GetDialogResult())
                    {
                        case DialogResult.Abort:
                            metroLabel13.Text = form3.GetLastError();
                            break;
                        case DialogResult.OK:
                            metroLabel13.Text = string.Empty;
                            list.Source = form3.GetVideos();
                            list.SetSource();
                            metroLabel6.Text = list.Source.Count.ToString();
                            break;
                        default:
                            metroLabel13.Text = "Поиск отменен";
                            break;
                    }
                }
            }
            else
            {
                metroLabel13.Text = "Отсутствует соединение с Интернет";
            }
        }

        private async void metroButton3_Click(object sender, EventArgs e)
        {
            await GetVideos();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            list.ModifyCheck(true);
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            list.ModifyCheck(false);
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.ModifyQuality(metroComboBox1.Items[metroComboBox1.SelectedIndex].ToString());
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SortItems(pictureBox2, 0);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SortItems(pictureBox3, 1);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SortItems(pictureBox4, 2);
        }

        private void SortItems(PictureBox pictureBox, int index)
        {
            if (list.ItemsSource != null && list.ItemsSource.Count != 0)
            {
                List<Video> videos = list.ItemsSource as List<Video>;
                if (pictureBox.Tag as Nullable<bool> == true)
                {
                    SetImage(pictureBox, Resources.strelka2);
                    switch (index)
                    {
                        case 0:
                            list.ItemsSource = videos.OrderByDescending(x => x.Title).ToList<Video>();
                            break;
                        case 1:
                            list.ItemsSource = videos.OrderByDescending(x => x.DateForSort).ToList<Video>();
                            break;
                        case 2:
                            list.ItemsSource = videos.OrderByDescending(x => x.DurationForSort).ToList<Video>();
                            break;
                    }
                }
                else
                {
                    SetImage(pictureBox, Resources.strelka);
                    switch (index)
                    {
                        case 0:
                            list.ItemsSource = videos.OrderBy(x => x.Title).ToList<Video>();
                            break;
                        case 1:
                            list.ItemsSource = videos.OrderBy(x => x.DateForSort).ToList<Video>();
                            break;
                        case 2:
                            list.ItemsSource = videos.OrderBy(x => x.DurationForSort).ToList<Video>();
                            break;
                    }
                }
            }
        }

        private void SetImage(PictureBox pictureBox, Bitmap image)
        {
            if (pictureBox == pictureBox2)
            {
                pictureBox3.Image = null;
                pictureBox4.Image = null;
                pictureBox3.Tag = null;
                pictureBox4.Tag = null;
            }
            else if (pictureBox == pictureBox3)
            {
                pictureBox2.Image = null;
                pictureBox4.Image = null;
                pictureBox2.Tag = null;
                pictureBox4.Tag = null;
            }
            else if (pictureBox == pictureBox4)
            {
                pictureBox3.Image = null;
                pictureBox2.Image = null;
                pictureBox3.Tag = null;
                pictureBox2.Tag = null;
            }
            pictureBox.Image = image;
            if (pictureBox.Tag as Nullable<bool> == true)
            {
                pictureBox.Tag = false;
            }
            else
            {
                pictureBox.Tag = true;
            }
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                metroTextBoxPlaceHolder3.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private async Task Download()
        {
            List<Video> videos = list.ItemsSource as List<Video>;
            if (videos != null && videos.Count > 0)
            {
                videos = videos.Where<Video>(x => x.IsChecked == true).ToList<Video>();
                if (videos != null && videos.Count > 0)
                {
                    Exception exception = null;
                    try
                    {
                        metroLabel13.Text = "Пожалуйста, подождите. Выполняется загрузка видео";
                        if (Directory.Exists(metroTextBoxPlaceHolder3.Text))
                        {
                            string path = metroTextBoxPlaceHolder3.Text;
                            if (!path.EndsWith("\\"))
                            {
                                path += "\\";
                            }
                            if (metroCheckBox1.Checked)
                            {
                                Settings.Default.Path = metroTextBoxPlaceHolder3.Text;
                            }
                            Settings.Default.Remember1 = metroCheckBox1.Checked;
                            Settings.Default.Save();
                            metroButton7.Text = "Отменить";
                            metroProgressBar2.Maximum = videos.Count;
                            for (int i = 0; i < videos.Count; i++)
                            {
                                string fullpath = path + videos[i].Title + ".mp4";
                                metroLabel10.Text = fullpath;
                                metroLabel12.Text = string.Format("{0}/{1}", i, videos.Count);
                                await client.DownloadFileTaskAsync(videos[i].CurrentFile.Item2, fullpath);
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
            }
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                CancelDownload(e.Error);
            }
            else
            {
                metroProgressBar2.Value++;
            }
            metroProgressBar1.Value = 0;

        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            metroProgressBar1.Value = e.ProgressPercentage;
            metroLabel11.Text = string.Format("{0}/{1}", GetSize(e.BytesReceived), GetSize(e.TotalBytesToReceive));
        }

        private async void metroButton7_Click(object sender, EventArgs e)
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

        private void CancelDownload(Exception exception)
        {
            metroButton7.Text = "Загрузить";
            metroProgressBar2.Value = 0;
            metroLabel10.Text = string.Empty;
            metroLabel11.Text = string.Empty;
            metroLabel12.Text = string.Empty;
            if (exception == null)
            {
                metroLabel13.Text = string.Empty;
            }
            else
            {
                metroLabel13.Text = exception.Message;
            }
        }

        private string GetSize(long value)
        {
            double new_value;
            string unit;
            if (value >= 1000000000)
            {
                new_value = value * 1.0 / 1000000000;
                unit = "Гб";
                return string.Format("{0:f1} {1}", new_value, unit);
            }
            else if (value >= 1000000)
            {
                new_value = value * 1.0 / 1000000;
                unit = "Мб";
                return string.Format("{0:f2} {1}", new_value, unit);
            }
            else if (value >= 1000)
            {
                new_value = value * 1.0 / 1000;
                unit = "Кб";
                return string.Format("{0:f2} {1}", new_value, unit);
            }
            else
            {
                unit = "Б";
                return string.Format("{0} {1}", value, unit);
            }
        }

        private async void metroTextBoxPlaceHolder3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Download();
            }
        }

        private void metroLabel13_MouseEnter(object sender, EventArgs e)
        {
            metroToolTip1.SetToolTip(metroLabel13, metroLabel13.Text);
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            ResetFilter();
        }

        private void ResetFilter()
        {
            if (list.Source != null)
            {
                SetSort(list.Source);
                metroLabel8.ForeColor = Color.Red;
            }
        }

        private void Filter()
        {
            if (metroTextBoxPlaceHolder4.Text.Trim() == string.Empty || metroTextBoxPlaceHolder4.isPlaceHolder())
            {
                ResetFilter();
            }
            else if (list.Source != null)
            {
                List<Video> videos = list.Source.Where<Video>(x => x.Title.IndexOf(metroTextBoxPlaceHolder4.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList<Video>();
                SetSort(videos);
                metroLabel8.ForeColor = Color.White;
            }
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void SetSort(List<Video> videos)
        {
            switch (pictureBox2.Tag as Nullable<bool>)
            {
                case true:
                    list.ItemsSource = videos.OrderBy(x => x.Title).ToList<Video>();
                    return;
                case false:
                    list.ItemsSource = videos.OrderByDescending(x => x.Title).ToList<Video>();
                    return;
            }
            switch (pictureBox3.Tag as Nullable<bool>)
            {
                case true:
                    list.ItemsSource = videos.OrderBy(x => x.DateForSort).ToList<Video>();
                    return;
                case false:
                    list.ItemsSource = videos.OrderByDescending(x => x.DateForSort).ToList<Video>();
                    return;
            }
            switch (pictureBox4.Tag as Nullable<bool>)
            {
                case true:
                    list.ItemsSource = videos.OrderBy(x => x.DurationForSort).ToList<Video>();
                    return;
                case false:
                    list.ItemsSource = videos.OrderByDescending(x => x.DurationForSort).ToList<Video>();
                    return;
            }
            list.SetSource(videos);
        }

        private void metroTextBoxPlaceHolder4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Filter();
            }
        }
    }
}
