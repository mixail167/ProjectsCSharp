using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VKVideoDownloader
{
    public class Video
    {
        string title;
        string description;
        DateTime date;
        TimeSpan duration;
        List<Tuple<string, string>> files;
        BitmapImage photo;
        Tuple<string, string> currentFile;
        bool isChecked;

        public Video()
        {
            files = new List<Tuple<string, string>>();
            isChecked = false;
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Date
        {
            get { return date.ToShortDateString(); }
        }

        public DateTime DateForSort
        {
            get { return date; }
        }

        public void SetDate(long value)
        {
            date = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(value).ToLocalTime();
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
                using (MemoryStream photoStream = new MemoryStream(client.DownloadData(url)))
                {
                    photoStream.Position = 0;
                    photo = new BitmapImage();
                    photo.BeginInit();
                    photo.StreamSource = photoStream;
                    photo.CacheOption = BitmapCacheOption.OnLoad;
                    photo.EndInit();
                    photo.Freeze();
                }
            }
        }

        public BitmapImage Photo
        {
            get { return photo; }
        }

        public List<Tuple<string, string>> Files
        {
            get { return files; }
        }

        public Tuple<string, string> CurrentFile
        {
            get { return currentFile; }
            set { currentFile = value; }
        }

        public void SetCurrentFileFromFiles(string quality)
        {
            foreach (Tuple<string, string> item in files)
            {
                if (item.Item1 == quality)
                {
                    currentFile = item;
                    break;
                }
            }
        }
    }
}
