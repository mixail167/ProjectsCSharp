﻿using System;
using System.Timers;

namespace YouTubeDownloaderMass
{
    class Progress : IProgress<double>, IDisposable
    {
        private readonly string title;
        private readonly string filePath;
        private double percent;
        private readonly long size;
        private double bytesCount;
        private bool first;
        private readonly Timer timer;

        public delegate void MessageHandler(string message);
        public event MessageHandler Message;

        public Progress(string title, string filePath, long size)
        {
            percent = 0;
            bytesCount = 0;
            first = true;
            this.title = title;
            this.filePath = filePath;
            this.size = size;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!first)
            {
                double bytesCount = percent * size;
                double speed = bytesCount - this.bytesCount;
                this.bytesCount = bytesCount;
                Message(string.Format("\rПрогресс загрузки: {0:P2}. Скорость: {1}.", percent, SpeedToString(speed)));
            }
        }

        public void Dispose()
        {
            timer.Stop();
            Message(string.Format("\rВидео загружено в {0}.\n", filePath));
        }

        public void Report(double value)
        {
            if (first)
            {
                Message(string.Format("Загрузка видео {0}.\n", title));
                first = false;
            }
            percent = value;
        }

        private string SpeedToString(double speed)
        {
            string si;
            if (speed >= 1073741824)
            {
                speed /= 1073741824;
                si = "ГБ";
            }
            else if (speed >= 1048576)
            {
                speed /= 1048576;
                si = "МБ";
            }
            else if (speed >= 1024)
            {
                speed /= 1024;
                si = "КБ";
            }
            else
            {
                si = "Б";
            }
            return string.Format("{0:f1} {1}/c", speed, si);
        }
    }
}