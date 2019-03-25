using MetroFramework.Controls;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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

                }
            }
            else
            {

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
            if (InterNet.IsConnected)
            {
                long album = (metroTextBoxPlaceHolder2.Text == string.Empty || metroTextBoxPlaceHolder2.isPlaceHolder()) ? 0 : Convert.ToInt64(metroTextBoxPlaceHolder2.Text);
                long id = (metroTextBoxPlaceHolder1.Text == string.Empty || metroTextBoxPlaceHolder1.isPlaceHolder()) ? this.id : Convert.ToInt64(metroTextBoxPlaceHolder1.Text);
                string url;
                if (metroRadioButton1.Checked || (metroRadioButton2.Checked && id == this.id))
                {
                    url = string.Format(Resources.GetVideos, access_token, id, album);
                }
                else
                {
                    url = string.Format(Resources.GetVideosWithMinus, access_token, id, album);
                }
                Request request = new Request(url);
                try
                {
                    JObject json = JObject.Parse(request.Get());
                    if (json.ContainsKey("error"))
                    {

                    }
                    else if (json.ContainsKey("response"))
                    {
                        JObject response = json["response"] as JObject;
                        JArray items = response["items"] as JArray;
                        if (items.HasValues)
                        {
                            List<Video> videos = new List<Video>();
                            foreach (JObject item in items)
                            {
                                Video video = new Video();
                                video.Title = item["title"].ToString();
                                video.Description = item["description"].ToString();
                                video.SetDate(Convert.ToInt64(item["date"]));
                                video.SetPhoto(item["photo_130"].ToString());
                                video.SetDuration(Convert.ToInt32(item["duration"]));
                                JObject files = item["files"] as JObject;
                                if (files.ContainsKey("mp4_144"))
                                {
                                    video.Files.Add(new Tuple<string, string>("mp4, 144", files["mp4_144"].ToString()));
                                }
                                if (files.ContainsKey("mp4_240"))
                                {
                                    video.Files.Add(new Tuple<string, string>("mp4, 240", files["mp4_240"].ToString()));
                                }
                                if (files.ContainsKey("mp4_360"))
                                {
                                    video.Files.Add(new Tuple<string, string>("mp4, 360", files["mp4_360"].ToString()));
                                }
                                if (files.ContainsKey("mp4_480"))
                                {
                                    video.Files.Add(new Tuple<string, string>("mp4, 480", files["mp4_480"].ToString()));
                                }
                                if (files.ContainsKey("mp4_720"))
                                {
                                    video.Files.Add(new Tuple<string, string>("mp4, 720", files["mp4_720"].ToString()));
                                }
                                if (files.ContainsKey("mp4_1080"))
                                {
                                    video.Files.Add(new Tuple<string, string>("mp4, 1080", files["mp4_1080"].ToString()));
                                }
                                videos.Add(video);
                            }
                            list.ItemSource = videos;
                            metroLabel6.Text = videos.Count.ToString();
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            GetVideos();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            list.ModifyCheck(true);
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            list.ModifyCheck(false);
        }
    }
}
