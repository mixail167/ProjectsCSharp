using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using xNet;
using YoutubeExtractor;

namespace YouTubeDownloaderMass
{
    class Program
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        static bool errorIndicator = false;
        static bool first = true;
        static Stopwatch time = new Stopwatch();

        static void Main(string[] args)
        {
            Console.Title = "YouTubeDownloader. Массовая загрузка видео";
            if (args.Length > 0)
            {
                int description;
                if (InternetGetConnectedState(out description, 0))
                {
                    if ((new Ping()).Send("www.youtube.com").Status == IPStatus.Success)
                    {
                        List<string> list = new List<string>();
                        foreach (string fileName in args)
                        {
                            if (File.Exists(fileName))
                            {
                                if (Path.GetExtension(fileName).Equals(".ydl"))
                                {
                                    Message(string.Format("Чтение файла {0}.", fileName));
                                    try
                                    {
                                        using (StreamReader streamReader = new StreamReader(fileName))
                                        {
                                            list.Add(streamReader.ReadToEnd());
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        Message(string.Format("Файл {0}: {1}.", fileName, exception.Message), true);
                                    }
                                }
                                else
                                {
                                    Message(string.Format("Файл {0} должен иметь расширение '.ydl'.", fileName), true);
                                }
                            }
                            else
                            {
                                Message(string.Format("Файл {0} не существует.", fileName), true);
                            }
                        }
                        if (list.Count != 0)
                        {
                            Console.WriteLine("Обработка содержимого файлов.");
                            List<string> list2 = new List<string>();
                            string allText = string.Join("\n", list);
                            list = allText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            const string pattern = @"^(http(s)?\:\/\/)?(www\.)?youtube\.com\/watch\?v=([-_0-9a-zA-Z]){11}(&([-=_0-9a-zA-Z&])*)?\|(144|360|480|720|1080)\|([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""]))+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])\\)+)$";
                            const string pattern2 = @"^(http(s)?\:\/\/)?(www\.)?youtube\.com\/playlist\?list=([-_0-9a-zA-Z]){34}(&([-=_0-9a-zA-Z&])*)?\|(144|360|480|720|1080)\|([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""]))+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])\\)+)$";
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (!Regex.IsMatch(list[i], pattern))
                                {
                                    if (!Regex.IsMatch(list[i], pattern2))
                                    {
                                        Message(string.Format("Строка {0} имеет недопустимый формат.", list[i]), true);
                                    }
                                    else
                                    {
                                        string url = list[i].Substring(0, list[i].IndexOf("=") + 35);
                                        HttpRequest request = new HttpRequest();
                                        string response = request.Get(url).ToString();
                                        if (response != null && response != string.Empty)
                                        {
                                            HtmlDocument document = new HtmlDocument();
                                            document.LoadHtml(response);
                                            IEnumerable<HtmlNode> htmlNodes = document.DocumentNode.QuerySelectorAll("a.pl-video-title-link.yt-uix-tile-link.yt-uix-sessionlink.spf-link");
                                            if (htmlNodes != null && htmlNodes.Count() > 0)
                                            {
                                                foreach (HtmlNode item in htmlNodes)
                                                {
                                                    string href = item.GetAttributeValue("href", null).Replace("amp;", "");
                                                    if (href != null && href != string.Empty)
                                                    {
                                                        list2.Add(string.Concat("https://www.youtube.com", href, list[i].Substring(list[i].IndexOf("|"))));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Message(string.Format("Видео в плейлисте {0} не найдено.", list[i]), true);
                                            }
                                        }
                                        else
                                        {
                                            Message(string.Format("Ответ от {0} не был получен.", url), true);
                                        }
                                    }
                                }
                                else
                                {
                                    list2.Add(list[i]);
                                }
                            }
                            list.Clear();
                            for (int i = 0; i < list2.Count; i++)
                            {
                                char driverName = list2[i][list2[i].LastIndexOf('|') + 1];
                                if (DriveInfo.GetDrives().FirstOrDefault(p => p.Name.StartsWith(driverName.ToString(), true, null)) == null)
                                {
                                    Message(string.Format("Строка {0}: Диск {1} не существует.", list2[i], driverName), true);
                                    list2 = RemoveItem(list2, i, out i);
                                }
                            }
                            if (list2.Count != 0)
                            {
                                List<Tuple<VideoInfo, string>> videoList = new List<Tuple<VideoInfo, string>>();
                                foreach (string item in list2)
                                {
                                    string[] parts = item.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    parts[0] = parts[0].Substring(parts[0].IndexOf('=') + 1, 11);
                                    string url = string.Concat("https://www.youtube.com/watch?v=", parts[0]);
                                    Message(string.Format("Поиск видео по URL {0}.", url));
                                    int resolution = Convert.ToInt32(parts[1]);
                                    for (int i = 0; i < 5; i++)
                                    {
                                        try
                                        {
                                            VideoInfo video = DownloadUrlResolver.GetDownloadUrls(url)
                                                                                      .OrderByDescending(p => p.Resolution)
                                                                                      .FirstOrDefault(p => p.VideoType == VideoType.Mp4 &&
                                                                                                        p.Resolution <= resolution &&
                                                                                                        p.AudioType != AudioType.Unknown);
                                            if (video != null)
                                            {
                                                videoList.Add(new Tuple<VideoInfo, string>(video, parts[2]));
                                            }
                                            else
                                            {
                                                Message(string.Format("Видео по URL {0} не найдено.", url), true);
                                            }
                                            break;
                                        }
                                        catch (YoutubeParseException)
                                        {
                                            if (i == 4)
                                            {
                                                Message(string.Format("Видео по URL {0} не найдено.", url), true);
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            Message(string.Format("URL: {0}: {1}", url, exception.Message), true);
                                            break;
                                        }
                                    }
                                }
                                list2.Clear();
                                if (videoList.Count != 0)
                                {
                                    char[] invalidChars = Path.GetInvalidFileNameChars();
                                    for (int i = 0; i < videoList.Count; i++)
                                    {
                                        try
                                        {
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
                                            downloader.DownloadProgressChanged += downloader_DownloadProgressChanged;
                                            if (!Directory.Exists(videoList[i].Item2))
                                            {
                                                Directory.CreateDirectory(videoList[i].Item2);
                                            }
                                            downloader.Execute();
                                        }
                                        catch (WebException exception)
                                        {
                                            if (exception.Status == WebExceptionStatus.ProtocolError)
                                            {
                                                switch ((exception.Response as HttpWebResponse).StatusCode)
                                                {
                                                    case System.Net.HttpStatusCode.Forbidden:
                                                        Message(string.Format("Видео {0}: Доступ запрещен.", videoList[i].Item1.Title), true);
                                                        break;
                                                    default:
                                                        Message(string.Format("Видео {0}: Неизвестная ошибка.", videoList[i].Item1.Title), true);
                                                        break;
                                                }
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            Message(string.Format("Видео {0}: {1}", videoList[i].Item1.Title, exception.Message), true);
                                        }
                                        finally
                                        {
                                            first = true;
                                        }
                                    }
                                    videoList.Clear();
                                }
                                else
                                {
                                    Message("Нет данных для загрузки видео.", true);
                                }
                            }
                            else
                            {
                                Message("Нет данных для поиска видео.", true);
                            }
                        }
                        else
                        {
                            Message("Нет данных для обработки.", true);
                        }
                    }
                    else
                    {
                        Message(@"URL https://www.youtube.com недоступен.", true);
                    }
                }
                else
                {
                    Message("Отсутствует соединение с сетью Интернет.", true);
                }
            }
            else
            {
                Message("Отсутствует параметр 'Путь к файлу'.", true);
            }
            if (errorIndicator)
            {
                Message("Операция завершена с ошибками.");
            }
            else
            {
                Message("Операция успешно завершена.");
            }
            Console.ReadKey();
        }

        static void downloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            long milliseconds = GetElapsedTime();
            double speed = milliseconds != 0 ? (double)(e.BytesReceived * 1000) / milliseconds : 0;
            if (first)
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                first = false;
            }
            else
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            }
            Message(string.Format("Прогресс загрузки: {0:f2}%, средняя скорость: {1,-20}", e.ProgressPercentage, SpeedToString(speed)));
        }

        private static void downloader_DownloadStarted(object sender, EventArgs e)
        {
            VideoDownloader downloader = sender as VideoDownloader;
            Message(string.Format("Загрузка видео {0}.", downloader.Video.Title));
        }

        static void downloader_DownloadFinished(object sender, EventArgs e)
        {
            StopTime();
            VideoDownloader downloader = sender as VideoDownloader;
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Message(string.Format("Видео загружено в {0}.", downloader.SavePath));
        }

        private static void Message(string message, bool error = false)
        {
            Console.WriteLine(message);
            if (!errorIndicator && error)
            {
                errorIndicator = error;
            }
        }

        private static List<string> RemoveItem(List<string> list, int index, out int outIndex)
        {
            list.RemoveAt(index);
            outIndex = index - 1;
            return list;
        }

        private static long GetElapsedTime()
        {
            time.Stop();
            long milliseconds = time.ElapsedMilliseconds;
            time.Start();
            return milliseconds;
        }

        private static void StopTime()
        {
            time.Stop();
            time.Reset();
        }

        private static string SpeedToString(double speed)
        {
            string si;
            if (speed >= 1073741824)
            {
                speed /= 1073741824;
                si = "Гб";
            }
            else if (speed >= 1048576)
            {
                speed /= 1048576;
                si = "Мб";
            }
            else if (speed >= 1024)
            {
                speed /= 1024;
                si = "Кб";
            }
            else
            {
                si = "Б";
            }
            return string.Format("{0:f1} {1}/c", speed, si);
        }
    }
}
