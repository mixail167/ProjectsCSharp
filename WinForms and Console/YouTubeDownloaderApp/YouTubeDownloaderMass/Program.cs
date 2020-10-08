using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;
using YoutubeExtractor;

namespace YouTubeDownloaderMass
{
    class Program
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        static bool errorIndicator = false;
        static bool first = true;
        static Timer timer;
        static int bytesCount;
        static double percent;

        static void Main(string[] args)
        {
            Console.Title = "YouTubeDownloader. Массовая загрузка видео";
            if (args.Length > 0)
            {
                if (InternetGetConnectedState(out int description, 0))
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
                            const string pattern = @"^(http(s)?\:\/\/)?(www\.)?youtube\.com\/watch\?v=([-_0-9a-zA-Z]){11}(&([-=_0-9a-zA-Z&])*)?\|(144|240|360|480|720|1080)\|([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?)+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?\\)+)$";
                            const string pattern2 = @"^(http(s)?\:\/\/)?(www\.)?youtube\.com\/playlist\?list=([-_0-9a-zA-Z]){34}(&([-=_0-9a-zA-Z&])*)?\|(144|240|360|480|720|1080)\|([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?)+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?\\)+)$";
                            foreach (string item in list)
                            {
                                if (!Regex.IsMatch(item, pattern))
                                {
                                    if (!Regex.IsMatch(item, pattern2))
                                    {
                                        Message(string.Format("Строка {0} имеет недопустимый формат.", item), true);
                                    }
                                    else
                                    {
                                        if (CheckDrive(item, out char driverName))
                                        {
                                            string playlistId = item.Substring(item.IndexOf("list=") + 5, 34);
                                            try
                                            {
                                                YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
                                                {
                                                    ApiKey = "AIzaSyDUYvMgMMv-FCdRdD426c4bcJKZ4WtZpn0",
                                                    ApplicationName = "YoutubeProject"
                                                });
                                                string nextPageToken = string.Empty;
                                                while (nextPageToken != null)
                                                {
                                                    PlaylistItemsResource.ListRequest playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                                                    playlistItemsListRequest.PlaylistId = playlistId;
                                                    playlistItemsListRequest.MaxResults = 50;
                                                    playlistItemsListRequest.PageToken = nextPageToken;
                                                    PlaylistItemListResponse playlistItemsListResponse = playlistItemsListRequest.Execute();
                                                    foreach (PlaylistItem playlistItem in playlistItemsListResponse.Items)
                                                    {
                                                        list2.Add(string.Concat("https://www.youtube.com/watch?v=", playlistItem.Snippet.ResourceId.VideoId));
                                                    }
                                                    nextPageToken = playlistItemsListResponse.NextPageToken;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Message(string.Format("Плейлист {0}: {1}", playlistId, ex.Message), true);
                                            }
                                        }
                                        else
                                        {
                                            Message(string.Format("Строка {0}: Диск {1} не существует.", item, driverName), true);
                                        }
                                    }
                                }
                                else
                                {
                                    list2.Add(item);
                                }
                            }
                            list.Clear();
                            foreach (string item in list2)
                            {
                                if (CheckDrive(item, out char driverName))
                                {
                                    list.Add(item);
                                }
                                else
                                {
                                    Message(string.Format("Строка {0}: Диск {1} не существует.", item, driverName), true);
                                }
                            }
                            list2.Clear();
                            if (list.Count != 0)
                            {
                                List<Tuple<VideoInfo, string>> videoList = new List<Tuple<VideoInfo, string>>();
                                foreach (string item in list)
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
                                                .FirstOrDefault(p => p.VideoType == VideoType.Mp4 && p.Resolution <= resolution &&
                                                                     p.Resolution > 0 && p.AudioType != AudioType.Unknown);
                                            if (video != null)
                                            {
                                                if (video.RequiresDecryption)
                                                {
                                                    DownloadUrlResolver.DecryptDownloadUrl(video);
                                                }
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
                                        catch (VideoNotAvailableException)
                                        {
                                            Message(string.Format("URL: {0}: Необходима авторизация.", url), true);
                                            break;
                                        }
                                        catch (DecipherSignatureException)
                                        {
                                            Message(string.Format("URL: {0}: Невозможно расшифровать цифровую подпись.", url), true);
                                            break;
                                        }
                                        catch (Exception exception)
                                        {
                                            Message(string.Format("URL: {0}: {1}", url, exception.Message), true);
                                            break;
                                        }
                                    }
                                }
                                list.Clear();
                                if (videoList.Count != 0)
                                {
                                    timer = new Timer(1000);
                                    timer.Elapsed += Timer_Elapsed;
                                    char[] invalidChars = Path.GetInvalidFileNameChars();
                                    for (int i = 0; i < videoList.Count; i++)
                                    {
                                        try
                                        {
                                            string title1 = videoList[i].Item1.Title;
                                            foreach (char item in invalidChars)
                                            {
                                                if (title1.IndexOf(item) != -1)
                                                {
                                                    title1 = title1.Replace(item, '_');
                                                }
                                            }
                                            string title = title1;
                                            for (int j = 1; File.Exists(Path.Combine(videoList[i].Item2, title + videoList[i].Item1.VideoExtension)); j++)
                                            {
                                                title = title1 + string.Format(" ({0})", j);
                                            }
                                            VideoDownloader downloader = new VideoDownloader(videoList[i].Item1, Path.Combine(videoList[i].Item2, title + videoList[i].Item1.VideoExtension));
                                            downloader.DownloadStarted += Downloader_DownloadStarted;
                                            downloader.DownloadFinished += Downloader_DownloadFinished;
                                            downloader.DownloadProgressChanged += Downloader_DownloadProgressChanged;
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

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                Console.CursorTop--;
            }
            Message(string.Format("{0,-" + (Console.BufferWidth - 1) + "}", string.Format("Прогресс загрузки: {0:f2}%, скорость: {1}", percent, SpeedToString(bytesCount))));
            bytesCount = 0;
        }

        static void Downloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            bytesCount += e.BytesReceived;
            percent = e.ProgressPercentage;
        }

        private static void Downloader_DownloadStarted(object sender, EventArgs e)
        {
            bytesCount = 0;
            percent = 0;
            first = true;
            timer.Start();
            VideoDownloader downloader = sender as VideoDownloader;
            Message(string.Format("Загрузка видео {0}.", downloader.Video.Title));
        }

        static void Downloader_DownloadFinished(object sender, EventArgs e)
        {
            timer.Stop();
            VideoDownloader downloader = sender as VideoDownloader;
            Console.CursorTop--;
            Message(string.Format("{0,-" + (Console.BufferWidth - 1) + "}", string.Format("Видео загружено в {0}.", downloader.SavePath)));
        }

        private static void Message(string message, bool error = false)
        {
            Console.CursorLeft = 0;
            Console.WriteLine(message);
            if (!errorIndicator && error)
            {
                errorIndicator = error;
            }
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

        private static bool CheckDrive(string searchString, out char driveNameOut)
        {
            char driverName = searchString[searchString.LastIndexOf('|') + 1];
            driveNameOut = driverName;
            if (DriveInfo.GetDrives().FirstOrDefault(p => p.Name.StartsWith(driverName.ToString(), true, null)) == null)
            {
                return false;
            }
            return true;
        }
    }
}