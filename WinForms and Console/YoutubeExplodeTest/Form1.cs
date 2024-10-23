using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplodeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webView21.EnsureCoreWebView2Async();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.Navigate("https://accounts.google.com/ServiceLogin?continue=https%3A%2F%2Fwww.youtube.com");
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            await Qwerty();
        }

        private async Task Qwerty()
        {
            List<CoreWebView2Cookie> coreWebView2Cookies = await webView21.CoreWebView2.CookieManager.GetCookiesAsync("");
            List<Cookie> cookies = new List<Cookie>();
            foreach (CoreWebView2Cookie item in coreWebView2Cookies)
            {
                cookies.Add(new Cookie(item.Name, item.Value, item.Path, item.Domain));
            }
            YoutubeClient youtube = new YoutubeClient();
            string videoUrl = "https://www.youtube.com/watch?v=c9DIoSNoQNs";
            StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            IStreamInfo streamInfo = streamManifest.GetVideoOnlyStreams().GetWithHighestVideoQuality();
            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
        }

    }
}
