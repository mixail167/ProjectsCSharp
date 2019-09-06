using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace VKVideoDownloader
{
    public enum Search
    {
        Video,
        Album,
        User
    }

    public partial class Form3 : MetroForm
    {
        readonly string access_token;
        readonly string url;
        readonly long album;
        readonly long id;
        int count;
        readonly int countThreads;
        readonly Semaphore semaphore;
        readonly List<Video> videos;
        readonly List<Album> albums;
        string lastError;
        readonly Search key;

        public Form3(string access_token, long id, long album, string url, int countThreads, int count)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.id = id;
            this.access_token = access_token;
            this.url = url;
            this.album = album;
            this.count = countThreads;
            this.countThreads = countThreads;
            videos = new List<Video>();
            semaphore = new Semaphore(1, 1);
            metroLabel13.Text = "Пожалуйста, подождите.\nВыполняется поиск видео";
            key = Search.Video;
            metroProgressSpinner1.Maximum = count;
        }

        public Form3(string access_token, long id, string url)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.access_token = access_token;
            this.id = id;
            this.url = url;
            albums = new List<Album>();
            semaphore = new Semaphore(1, 1);
            metroLabel13.Text = "Пожалуйста, подождите.\nВыполняется поиск альбомов";
            key = Search.Album;
        }

        private void ThreadFunction(object value)
        {
            try
            {
                if (InterNet.IsConnected)
                {
                    int offset = Convert.ToInt32(value);
                    Request request;
                    switch (key)
                    {
                        case Search.Video:
                            request = new Request(string.Format(url, access_token, id, album, offset));
                            break;
                        case Search.Album:
                            request = new Request(string.Format(url, access_token, id, offset));
                            break;
                        case Search.User:
                            request = null;
                            break;
                        default:
                            request = null;
                            break;
                    }
                    JObject json = JObject.Parse(request.Get());
                    if (json.ContainsKey("error"))
                    {
                        JObject error = json["error"] as JObject;
                        int code = Convert.ToInt32(error["error_code"]);
                        string message = error["error_msg"].ToString();
                        lastError = string.Format("Ошибка {0}: {1}", code, message);

                    }
                    else if (json.ContainsKey("response"))
                    {
                        JObject response = json["response"] as JObject;
                        JArray items = response["items"] as JArray;
                        if (items.HasValues)
                        {
                            foreach (JObject item in items)
                            {
                                semaphore.WaitOne();
                                Invoke(new MethodInvoker(() => { metroProgressSpinner1.Value++; }));
                                semaphore.Release();
                                switch (key)
                                {
                                    case Search.Video:
                                        if (item.ContainsKey("files"))
                                        {
                                            Video video = new Video();
                                            JObject files = item["files"] as JObject;
                                            if (files.ContainsKey("mp4_144"))
                                            {
                                                video.Files.Add(new Tuple<int, string>(144, files["mp4_144"].ToString()));
                                            }
                                            if (files.ContainsKey("mp4_240"))
                                            {
                                                video.Files.Add(new Tuple<int, string>(240, files["mp4_240"].ToString()));
                                            }
                                            if (files.ContainsKey("mp4_360"))
                                            {
                                                video.Files.Add(new Tuple<int, string>(360, files["mp4_360"].ToString()));
                                            }
                                            if (files.ContainsKey("mp4_480"))
                                            {
                                                video.Files.Add(new Tuple<int, string>(480, files["mp4_480"].ToString()));
                                            }
                                            if (files.ContainsKey("mp4_720"))
                                            {
                                                video.Files.Add(new Tuple<int, string>(720, files["mp4_720"].ToString()));
                                            }
                                            if (files.ContainsKey("mp4_1080"))
                                            {
                                                video.Files.Add(new Tuple<int, string>(1080, files["mp4_1080"].ToString()));
                                            }
                                            if (video.Files.Count == 0)
                                            {
                                                continue;
                                            }
                                            video.Title = item["title"].ToString();
                                            video.Description = item["description"].ToString();
                                            video.SetDate(Convert.ToInt64(item["date"]));
                                            video.SetDuration(Convert.ToInt32(item["duration"]));
                                            video.CurrentFile = new Tuple<int, string>(video.Files[video.Files.Count - 1].Item1, video.Files[video.Files.Count - 1].Item2);
                                            video.SetPhoto(item["photo_130"].ToString());
                                            semaphore.WaitOne();
                                            videos.Add(video);
                                            semaphore.Release();
                                        }
                                        break;
                                    case Search.Album:
                                        Album album1 = new Album();
                                        int count = Convert.ToInt32(item["count"].ToString());
                                        if (count != 0)
                                        {
                                            album1.Count = count;
                                            album1.Title = item["title"].ToString();
                                            album1.SetDate(Convert.ToInt64(item["updated_time"]));
                                            album1.SetPhoto(item["photo_160"].ToString());
                                            album1.ID = Convert.ToInt32(item["id"]);
                                            semaphore.WaitOne();
                                            albums.Add(album1);
                                            semaphore.Release();
                                        }
                                        break;
                                    case Search.User:
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    semaphore.WaitOne();
                    lastError = "Отсутствует соединение с Интернет";
                    semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                semaphore.WaitOne();
                lastError = ex.Message;
                semaphore.Release();
            }
            finally
            {
                semaphore.WaitOne();
                count--;
                if (count == 0)
                {
                    if (lastError == null)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Abort;
                    }
                }
                semaphore.Release();
            }
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            switch (key)
            {
                case Search.Video:
                    for (int i = 0; i < countThreads; i++)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadFunction), i * 200);
                    }
                    break;
                case Search.Album:
                    string error;
                    count = Functions.GetCount(string.Format(url, access_token, id, 0), out error);
                    if (count > 0)
                    {
                        metroProgressSpinner1.Maximum = count;
                        int countThreads = Convert.ToInt32(Math.Ceiling(count * 1.0 / 100));
                        count = countThreads;
                        for (int i = 0; i < countThreads; i++)
                        {
                            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadFunction), i * 100);
                        }
                    }
                    else if (count < 0)
                    {
                        lastError = error;
                        this.DialogResult = DialogResult.Abort;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.No;
                    }
                    break;
                case Search.User:
                    break;
                default:
                    break;
            }
        }

        public DialogResult GetDialogResult()
        {
            return this.DialogResult;
        }

        public List<Video> GetVideos()
        {
            return videos;
        }

        public List<Album> GetAlbums()
        {
            return albums;
        }


        public string GetLastError()
        {
            return lastError;
        }
    }
}
