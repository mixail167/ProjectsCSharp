﻿using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using VKVideoDownloader.Properties;

namespace VKVideoDownloader
{
    public partial class Form4 : MetroForm
    {
        ListAlbumWPF list;
        string url;
        string access_token;
        long id;

        public Form4(string access_token, long id, string url)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.url = url;
            this.id = id;
            this.access_token = access_token;
            list = elementHost1.Child as ListAlbumWPF;
            list.AlbumSelected += list_AlbumSelected;
        }

        public long ID
        {
            get { return list.ID; }
        }

        private void list_AlbumSelected()
        {
            DialogResult = DialogResult.OK;
        }

        private void GetAlbums()
        {
            list.ClearSource();
            metroTextBoxPlaceHolder4.Text = metroTextBoxPlaceHolder4.PlaceHolder;
            metroLabel8.ForeColor = Color.Red;
            pictureBox2.Image = null;
            pictureBox2.Tag = null;
            metroLabel6.Text = "0";
            metroLabel13.Text = "Пожалуйста, подождите. Выполняется поиск альбомов";
            Form3 form3 = new Form3(access_token, id, url);
            form3.ShowDialog();
            switch (form3.GetDialogResult())
            {
                case DialogResult.Abort:
                    metroLabel13.Text = form3.GetLastError();
                    break;
                case DialogResult.OK:
                    metroLabel13.Text = string.Empty;
                    list.Source = form3.GetAlbums();
                    list.SetSource();
                    metroLabel6.Text = list.Source.Count.ToString();
                    break;
                default:
                    Close();
                    break;
            }
        }

        private void Form4_Shown(object sender, EventArgs e)
        {
            GetAlbums();
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

        private void SetSort(List<Album> albums)
        {
            switch (pictureBox2.Tag as Nullable<bool>)
            {
                case true:
                    list.ItemsSource = albums.OrderBy(x => x.Title).ToList<Album>();
                    return;
                case false:
                    list.ItemsSource = albums.OrderByDescending(x => x.Title).ToList<Album>();
                    return;
            }
            list.SetSource(albums);
        }


        private void Filter()
        {
            if (metroTextBoxPlaceHolder4.Text.Trim() == string.Empty || metroTextBoxPlaceHolder4.isPlaceHolder())
            {
                ResetFilter();
            }
            else if (list.Source != null)
            {
                List<Album> videos = list.Source.Where<Album>(x => x.Title.IndexOf(metroTextBoxPlaceHolder4.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList<Album>();
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


        private void metroTextBoxPlaceHolder4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Filter();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (list.ItemsSource != null && list.ItemsSource.Count != 0)
            {
                List<Album> albums = list.ItemsSource as List<Album>;
                if (pictureBox2.Tag as Nullable<bool> == true)
                {
                    pictureBox2.Image = Resources.strelka2;
                    list.ItemsSource = albums.OrderByDescending(x => x.Title).ToList<Album>();
                    pictureBox2.Tag = false;
                }
                else
                {
                    pictureBox2.Image = Resources.strelka;
                    list.ItemsSource = albums.OrderBy(x => x.Title).ToList<Album>();
                    pictureBox2.Tag = true;
                }
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            GetAlbums();
        }

        private void metroLabel13_MouseEnter(object sender, EventArgs e)
        {
            metroToolTip1.SetToolTip(metroLabel13, metroLabel13.Text);
        }
    }
}