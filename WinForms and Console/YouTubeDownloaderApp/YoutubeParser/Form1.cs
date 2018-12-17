using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xNet;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace YoutubeParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void test(string ssilka)
        {
            string image = "", name = "";
            HttpRequest danni = new HttpRequest();
            string response = danni.Get(ssilka).ToString();

            HtmlAgilityPack.HtmlDocument hap = new HtmlAgilityPack.HtmlDocument();
            hap.LoadHtml(response);
            IEnumerable<HtmlNode> list = hap.DocumentNode.QuerySelectorAll("ol.item-section");
            foreach (HtmlNode item in list)
            {
                IEnumerable<HtmlNode> list2 = item.QuerySelectorAll("li>div.yt-lockup.yt-lockup-tile.yt-lockup-video.vve-check.clearfix>div.yt-lockup-dismissable.yt-uix-tile>div.yt-lockup-thumbnail.contains-addto>a.yt-uix-sessionlink.spf-link>div.yt-thumb.video-thumb>span.yt-thumb-simple>img");
                foreach (HtmlNode item2 in list2)
                {
                    image += item2.GetAttributeValue("src", null) + Environment.NewLine;
                }
                IEnumerable<HtmlNode> list3 = item.QuerySelectorAll("li>div.yt-lockup.yt-lockup-tile.yt-lockup-video.vve-check.clearfix>div.yt-lockup-dismissable.yt-uix-tile>div.yt-lockup-content>h3.yt-lockup-title>a.yt-uix-tile-link.yt-ui-ellipsis.yt-ui-ellipsis-2.yt-uix-sessionlink.spf-link");
                foreach (HtmlNode item2 in list3)
                {
                    name += item2.GetAttributeValue("title", null) + Environment.NewLine;
                }
            }

            richTextBox1.Text = image;

            //File.WriteAllText("test.html", response, Encoding.UTF8);
            //Process.Start("test.html");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test(textBox1.Text);
        }
    }
}
