using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using YoutubeExtractor;

namespace YouTubeDownloaderMass
{
    class Program
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        static void Main(string[] args)
        {
            int description;
            if (InternetGetConnectedState(out description, 0))
            {
                if (args.Length > 0)
                {
                    List<string> list = new List<string>();
                    foreach (string fileName in args)
                    {
                        if (File.Exists(fileName))
                        {
                            if (Path.GetExtension(fileName).Equals(".csv"))
                            {
                                Console.WriteLine("Чтение файла {0}.", fileName);
                                try
                                {
                                    using (StreamReader streamReader = new StreamReader(fileName))
                                    {
                                        list.Add(streamReader.ReadToEnd());
                                    }
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine("Файл {0}: {1}.", fileName, exception.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Файл {0} должен иметь расширение '.csv'.", fileName);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Файл {0} не существует.", fileName);
                        }
                    }
                    if (list.Count != 0)
                    {
                        Console.WriteLine("Обработка содержимого файлов.");
                        string allText = string.Join("\n", list);
                        list = allText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        const string pattern = @"^(http(s)?\:\/\/)?(www\.)?youtube\.com\/watch\?v=([-_0-9a-zA-Z]){11}(&([-=_0-9a-zA-Z&])*)?;(144|360|480|720|1080);([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""]))+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])\\)+)$";
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (!Regex.IsMatch(list[i], pattern))
                            {
                                Console.WriteLine("Строка {0} имеет недопустимый формат.", list[i]);
                                list.RemoveAt(i);
                                i--;
                            }
                        }
                        if (list.Count != 0)
                        {
                            List<Tuple<VideoInfo, string>> videoList = new List<Tuple<VideoInfo, string>>();
                            foreach (string item in list)
                            {
                                string[] parts = item.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                parts[0] = parts[0].Substring(parts[0].IndexOf('=') + 1, 11);
                                string url = string.Concat("https://www.youtube.com/watch?v=", parts[0]);
                                Console.WriteLine("Поиск видео по URL {0}.", url);
                                int resolution = Convert.ToInt32(parts[1]);
                                for (int i = 0; i < 5; i++)
                                {
                                    try
                                    {
                                        VideoInfo video = DownloadUrlResolver.GetDownloadUrls(url, false)
                                                                                  .OrderByDescending(p => p.Resolution)
                                                                                  .FirstOrDefault(p => p.VideoType == VideoType.Mp4 && p.Resolution <= resolution && p.AudioType != AudioType.Unknown);
                                        if (video != null)
                                        {
                                            videoList.Add(new Tuple<VideoInfo, string>(video, parts[2]));
                                        }
                                        else
                                        {
                                            Console.WriteLine("Видео по URL {0} не найдено.", url);
                                        }
                                        break;
                                    }
                                    catch (YoutubeParseException)
                                    {
                                        if (i == 4)
                                        {
                                            Console.WriteLine("Видео по URL {0} не найдено.", url);
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        Console.WriteLine("URL: {0}: {1}", url, exception.Message);
                                        break;
                                    }
                                }
                            }
                            list.Clear();
                            if (videoList.Count != 0)
                            {
                                char[] invalidChars = Path.GetInvalidFileNameChars();            
                                for (int i = 0; i < videoList.Count; i++)
                                {
                                    try
                                    {
                                        if (videoList[i].Item1.RequiresDecryption)
                                        {
                                            DownloadUrlResolver.DecryptDownloadUrl(videoList[i].Item1);
                                        }
                                        string title = videoList[i].Item1.Title;
                                        foreach (char item in invalidChars)
                                        {
                                            if (title.IndexOf(item) != -1)
                                            {
                                                title = title.Replace(item, '_');
                                            }
                                        }
                                        VideoDownloader downloader = new VideoDownloader(videoList[i].Item1, Path.Combine(videoList[i].Item2, title + videoList[i].Item1.VideoExtension));
                                        downloader.DownloadStarted += downloader_DownloadStarted;
                                        downloader.DownloadFinished += downloader_DownloadFinished;
                                        if (!Directory.Exists(videoList[i].Item2))
                                        {
                                            Directory.CreateDirectory(videoList[i].Item2);
                                        }
                                        downloader.Execute();
                                    }
                                    catch (Exception exception)
                                    {
                                        Console.WriteLine("Видео {0}: {1}", videoList[i].Item1.Title, exception.Message);
                                    }
                                }
                            }
                            else
                            {
                                Pause("Нет данных для загрузки видео.");
                            }
                        }
                        else
                        {
                            Pause("Нет данных для поиска видео.");
                        }
                    }
                    else
                    {
                        Pause("Нет данных для обработки.");
                    }
                }
                else
                {
                    Pause("Отсутствует параметр 'Путь к файлу'.");
                }
            }
            else
            {
                Pause("Отсутствует соединение с сетью Интернет.");
            }
        }

        private static void downloader_DownloadStarted(object sender, EventArgs e)
        {
            VideoDownloader downloader = sender as VideoDownloader;
            Console.WriteLine("Загрузка видео {0}.", downloader.Video.Title);
        }

        static void downloader_DownloadFinished(object sender, EventArgs e)
        {
            VideoDownloader downloader = sender as VideoDownloader;
            Console.WriteLine("Видео загружено в {0}.", downloader.SavePath);
        }

        private static void Pause(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
