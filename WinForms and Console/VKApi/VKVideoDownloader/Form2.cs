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
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using VKVideoDownloader.Properties;

namespace VKVideoDownloader
{
    public partial class Form2 : MetroForm
    {
        string access_token;
        long id;
        ListViewWPF list;

        public Form2(string access_token, string id)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.id = 0;  
            list = elementHost1.Child as ListViewWPF;
            list.CountChanged += list_CountChanged;
            GetProfileInfo(access_token, id);
        }

        void list_CountChanged()
        {
            metroLabel16.Text = list.Count.ToString();
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

        private void metroTextBoxPlaceHolder2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetVideos();
            }
        }

        private void GetVideos()
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
            metroLabel16.Text = "0";
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
                string error;
                int count = Functions.GetCount(string.Format(url, access_token, id, album, 0), out error);
                if (count > 0)
                {
                    int countThreads = Convert.ToInt32(Math.Ceiling(count * 1.0 / 200));
                    Form3 form3 = new Form3(access_token, id, album, url, countThreads, count);
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
                else if (count < 0)
                {
                    metroLabel13.Text = error;
                }
            }
            else
            {
                metroLabel13.Text = "Отсутствует соединение с Интернет";
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            GetVideos();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            list.ModifyCheck(true);
            metroLabel16.Text = list.Count.ToString();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            list.ModifyCheck(false);
            metroLabel16.Text = list.Count.ToString();
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
                foreach (object item in list.listView.Items)
                {
                    try
                    {
                        ContentPresenter contentPresenter = list.listView.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                        contentPresenter.ApplyTemplate();
                        TextBlock textBlock = contentPresenter.ContentTemplate.FindName("Title", contentPresenter) as TextBlock;
                        List<int> indexes = new List<int>();
                        for (int index = 0; index != -1; )
                        {
                            index = textBlock.Text.IndexOf(metroTextBoxPlaceHolder4.Text, index, StringComparison.CurrentCultureIgnoreCase);
                            if (index != -1)
                            {
                                indexes.Add(index);
                                index++;
                            }
                        }
                        string text = textBlock.Text;
                        textBlock.Inlines.Clear();
                        int offset = 0;
                        for (int j = 0; j < indexes.Count; j++)
                        {
                            textBlock.Inlines.Add(new Run(text.Substring(offset, indexes[j] - offset)));
                            textBlock.Inlines.Add(new Run(text.Substring(indexes[j], metroTextBoxPlaceHolder4.Text.Length)) { Background = System.Windows.Media.Brushes.Orange });
                            offset = indexes[j] + metroTextBoxPlaceHolder4.Text.Length;
                        }
                        textBlock.Inlines.Add(new Run(text.Substring(offset)));
                    }
                    catch (Exception)
                    {

                    }
                }
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (InterNet.IsConnected)
            {
                long id = (metroTextBoxPlaceHolder1.Text == string.Empty || metroTextBoxPlaceHolder1.isPlaceHolder()) ? this.id : Convert.ToInt64(metroTextBoxPlaceHolder1.Text);
                string url;
                if (metroRadioButton1.Checked || (metroRadioButton2.Checked && id == this.id))
                {
                    url = Resources.GetAlbums;
                }
                else
                {
                    url = Resources.GetAlbumsWithMinus;
                }
                Form4 form4 = new Form4(access_token, id, url);
                if(form4.ShowDialog() == DialogResult.OK)
                {
                    metroTextBoxPlaceHolder2.Text = form4.ID.ToString();
                }
            }
            else
            {
                metroLabel13.Text = "Отсутствует соединение с Интернет";
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (list.Source != null)
            {
                List<Video> videos = list.Source.Where<Video>(x => x.IsChecked).ToList<Video>();
                SetSort(videos);
            }
        }

        private void MetroButton7_Click_1(object sender, EventArgs e)
        {
            List<Video> videos = list.Source;
            if (videos != null && videos.Count > 0)
            {
                videos = videos.Where<Video>(x => x.IsChecked == true).ToList<Video>();
                if (videos != null && videos.Count > 0)
                {
                    Form5 form5 = new Form5(videos);
                    form5.ShowDialog();
                }
            }
        }
    }
}
