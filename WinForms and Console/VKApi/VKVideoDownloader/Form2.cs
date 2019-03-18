using MetroFramework.Controls;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
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

        public Form2(string access_token, string id)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.id = 0;
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
                long id = (metroTextBoxPlaceHolder1.Text == string.Empty || metroTextBoxPlaceHolder1.isPlaceHolder()) ? this.id : Convert.ToInt64(metroTextBoxPlaceHolder2.Text);
                    string url = string.Format(Resources.GetVideos, access_token, metroTextBoxPlaceHolder1.Text, album);
                    Request request = new Request(url);
                    try
                    {
                        dynamic json = JObject.Parse(request.Get());
                        JArray items =  json.response.items;
                        if (items.HasValues)
                        {
                            ListViewWPF list = elementHost1.Child as ListViewWPF;
                            foreach (JObject item in items)
                            {

                            } 
                        }
                        else
                        {

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
    }
}
