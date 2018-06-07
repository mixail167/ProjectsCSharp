using Microsoft.DirectX;
using Microsoft.DirectX.AudioVideoPlayback;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TagLib;

namespace VideoPlayer1
{
    public static class VideoClass
    {
        private static Video video;

        private static string filePath;

        private static string fileName;

        public static bool isInit;

        public static bool isPlaying
        {
            get { return video.Playing; }
        }

        public static Size Size
        {
            set
            {
                try
                {
                    video.Size = value;
                }
                catch
                {

                }
            }
        }

        public static int Volume
        {
            set
            {
                try
                {
                    video.Audio.Volume = value;
                }
                catch
                {

                }
            }
        }


        public static bool isEnding
        {
            get
            {
                if (video.CurrentPosition == video.Duration)
                {
                    return true;
                }
                return false;
            }
        }

        public static double Position
        {
            get { return video.CurrentPosition; }
            set { video.CurrentPosition = value; }
        }

        public static int Duration
        {
            get { return Convert.ToInt32(video.Duration); }
        }

        public static string FileName
        {
            get { return fileName; }
        }

        public static bool Init(string filePath1, Panel control)
        {
            try
            {
                isInit = false;
                Size size = control.Size;
                filePath = filePath1;
                video = new Video(filePath);
                video.Open(filePath);
                video.Owner = control;
                video.Size = control.Size = size;
                string[] parts = filePath.Split('\\');
                fileName = parts[parts.Length - 1];
                isInit = true;
                return isInit;
            }
            catch (Exception)
            {
                return isInit;
            }
        }

        public static void Play()
        {
            try
            {
                video.Play();
            }
            catch (Exception)
            {

            }

        }

        public static void Pause()
        {
            try
            {
                video.Pause();
            }
            catch (Exception)
            {

            }
        }

        public static void Stop()
        {
            try
            {
                video.Stop();
            }
            catch (Exception)
            {

            }
        }

        public static string UpdateProgressText()
        {
            return string.Format("{0}/{1}", TimeSpan.FromSeconds(video.CurrentPosition).ToString("hh\\:mm\\:ss"),
                                            TimeSpan.FromSeconds(video.Duration).ToString("hh\\:mm\\:ss"));
        }

        public static string GetInfo()
        {
            File file = File.Create(filePath);
            return string.Format("{0} [{1}x{2}]\n{3}\n{4}", file.Tag.Title, file.Properties.VideoWidth,
                                                            file.Properties.VideoHeight, file.Tag.Comment,
                                                            file.Tag.Copyright);
        }

        public static void Dispose()
        {
            try
            {
                video.Dispose();
            }
            catch (Exception)
            {

            }
        }
    }
}
