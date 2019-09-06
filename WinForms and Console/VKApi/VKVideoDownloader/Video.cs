using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Media.Imaging;

namespace VKVideoDownloader
{
    public class Video
    {
        TimeSpan duration;
        private bool isChecked;
        private string title;
        private string description;
        private Tuple<int, string> currentFile;
        private BitmapImage photo;
        private readonly List<Tuple<int, string>> files;

        public Video()
        {
            files = new List<Tuple<int, string>>();
            IsChecked = false;
        }

        public bool IsChecked { get => isChecked; set => isChecked = value; }

        public string Title { get => title; set => title = value; }

        public string Description { get => description; set => description = value; }

        public string Date
        {
            get { return DateForSort.ToShortDateString(); }
        }

        public DateTime DateForSort { get; private set; }

        public void SetDate(long value)
        {
            DateForSort = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(value).ToLocalTime();
        }

        public void SetDuration(int value)
        {
            duration = new TimeSpan(0, 0, value);
        }

        public TimeSpan DurationForSort
        {
            get { return duration; }
        }

        public string Duration
        {
            get { return duration.ToString(@"hh\:mm\:ss"); }
        }

        public void SetPhoto(string url)
        {
            using (WebClient client = new WebClient())
            {
                MemoryStream photoStream = new MemoryStream();
                try
                {
                    byte[] bytes = client.DownloadData(url);
                    photoStream.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    Properties.Resources.photo_160x120.Save(photoStream, ImageFormat.Png);
                }
                finally
                {
                    Photo = new BitmapImage();
                    photoStream.Position = 0;
                    Photo.BeginInit();
                    Photo.StreamSource = photoStream;
                    Photo.CacheOption = BitmapCacheOption.OnLoad;
                    Photo.EndInit();
                    Photo.Freeze();
                    photoStream.Close();
                }
            }
        }

        public BitmapImage Photo { get => photo; private set => photo = value; }

        public List<Tuple<int, string>> Files => files;

        public Tuple<int, string> CurrentFile { get => currentFile; set => currentFile = value; }

        public void SetCurrentFileFromFiles(int quality)
        {
            Tuple<int, string> file = files.OrderByDescending(p => p.Item1).FirstOrDefault(p => p.Item1 <= quality);
            currentFile = (file != null) ? file : files[0];
        }
    }
}
