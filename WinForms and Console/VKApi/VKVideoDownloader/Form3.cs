using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace VKVideoDownloader
{
    public partial class Form3 : MetroForm
    {
        string access_token;
        string url;
        long album;
        long id;
        int count;
        int countThreads;
        Semaphore semaphore;
        List<Video> videos;
        string lastError;

        public Form3(string access_token, long id, long album, string url, int countThreads)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.id = id;
            this.access_token = access_token;
            this.album = album;
            this.url = url;
            count = countThreads;
            this.countThreads = countThreads;
            videos = new List<Video>();
            semaphore = new Semaphore(1, 1);
        }

        private void ThreadFunction(object value)
        {
            try
            {
                if (InterNet.IsConnected)
                {
                    int offset = Convert.ToInt32(value);
                    Request request = new Request(string.Format(url, access_token, id, album, offset));
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
                                if (item.ContainsKey("files"))
                                {
                                    Video video = new Video();
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
                                    if (video.Files.Count == 0)
                                    {
                                        continue;
                                    }
                                    video.Title = item["title"].ToString();
                                    video.Description = item["description"].ToString();
                                    video.SetDate(Convert.ToInt64(item["date"]));
                                    video.SetDuration(Convert.ToInt32(item["duration"]));
                                    video.CurrentFile = new Tuple<string, string>(video.Files[video.Files.Count - 1].Item1, video.Files[video.Files.Count - 1].Item2);
                                    video.SetPhoto(item["photo_130"].ToString());
                                    semaphore.WaitOne();
                                    videos.Add(video);
                                    semaphore.Release();
                                }
                            }
                        }
                    }
                }
                else
                {
                    lastError = "Отсутствует соединение с Интернет";
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
                    if (lastError != string.Empty)
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
            for (int i = 0; i < countThreads; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadFunction), i * 200);
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

        public string GetLastError()
        {
            return lastError;
        }
    }
}
