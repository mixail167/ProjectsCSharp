using System;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace VKVideoDownloader
{
    public class Album
    {
        int id;
        string title;
        DateTime date;
        BitmapImage photo;
        int count;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Date
        {
            get { return date.ToShortDateString(); }
        }

        public void SetDate(long value)
        {
            date = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(value).ToLocalTime();
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
                    photo = new BitmapImage();
                    photoStream.Position = 0;
                    photo.BeginInit();
                    photo.StreamSource = photoStream;
                    photo.CacheOption = BitmapCacheOption.OnLoad;
                    photo.EndInit();
                    photo.Freeze();
                    photoStream.Close();
                }
            }
        }

        public BitmapImage Photo
        {
            get { return photo; }
        }
    }
}
