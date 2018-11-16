using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YoutubeExtractor;

namespace YouTubeDownloaderMass
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (string fileName in args)
                {
                    if (File.Exists(fileName) && Path.GetExtension(fileName).Equals(".csv"))
                    {
                        try
                        {
                            string text;
                            using (StreamReader streamReader = new StreamReader(fileName))
                            {
                                text = streamReader.ReadToEnd();
                            }
                            string[] lines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string line in lines)
                            {
                                string[] parts = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                if (parts.Length == 3 && Regex.IsMatch(parts[0], "^([0-9a-zA-Z]){11}") && Regex.IsMatch(parts[1], "^(144|360|480|720|1080)$") &&
                                    (Regex.IsMatch(parts[2], @"^([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""]))+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])\\)+)$")))
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        try
                                        {
                                            int resolution = Convert.ToInt32(parts[1]);
                                            VideoInfo video = DownloadUrlResolver.GetDownloadUrls(string.Concat("https://www.youtube.com/watch?v=", parts[0]), false)
                                                                .OrderByDescending(p => p.Resolution)
                                                                .FirstOrDefault(p => p.VideoType == VideoType.Mp4 && p.Resolution <= resolution && p.AudioType != AudioType.Unknown);
                                            if (video != null)
                                            {
                                                if (video.RequiresDecryption)
                                                {
                                                    DownloadUrlResolver.DecryptDownloadUrl(video);
                                                }
                                                VideoDownloader downloader = new VideoDownloader(video, Path.Combine(parts[2], video.Title + video.VideoExtension));
                                                downloader.DownloadStarted += downloader_DownloadStarted;
                                                downloader.DownloadFinished += downloader_DownloadFinished;
                                                if (!Directory.Exists(parts[2]))
                                                {
                                                    Directory.CreateDirectory(parts[2]);
                                                }
                                                downloader.Execute();
                                            }
                                            break;
                                        }
                                        catch (Exception exception)
                                        {
                                            if (!(exception is YoutubeParseException))
                                            {
                                                Console.WriteLine(exception.Message);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
                }
            }
        }

        private static void downloader_DownloadStarted(object sender, EventArgs e)
        {
            VideoDownloader downloader = sender as VideoDownloader;
            Console.WriteLine("Downloading {0}", downloader.Video.Title);
        }

        static void downloader_DownloadFinished(object sender, EventArgs e)
        {
            VideoDownloader downloader = sender as VideoDownloader;
            Console.WriteLine("Downloaded to {0}", downloader.SavePath);
        }
    }
}
