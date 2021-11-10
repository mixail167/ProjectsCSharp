using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;
using YoutubeExtractor;

namespace YoutubeExtractorConsole
{
    class Program
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        private static bool errorIndicator = false;
        private static Timer timer;
        private static double percent;
        private static bool firstInfo;

        static void Main(string[] args)
        {
            Console.Title = "YouTubeDownloader. Массовая загрузка видео";
            if (args.Length > 0)
            {
                if (InternetGetConnectedState(out int description, 0))
                {
                    if (new Ping().Send("www.youtube.com").Status == IPStatus.Success)
                    {
                        List<string> texts = new List<string>();
                        foreach (string fileName in args)
                        {
                            if (File.Exists(fileName))
                            {
                                if (Path.GetExtension(fileName).Equals(".ydl"))
                                {
                                    Message(string.Format("Чтение файла {0}.\n", fileName));
                                    try
                                    {
                                        using StreamReader streamReader = new StreamReader(fileName);
                                        texts.Add(streamReader.ReadToEnd());
                                    }
                                    catch (Exception exception)
                                    {
                                        Message(string.Format("Файл {0}: {1}.\n", fileName, exception.Message), true);
                                    }
                                }
                                else
                                {
                                    Message(string.Format("Файл {0} должен иметь расширение '.ydl'.\n", fileName), true);
                                }
                            }
                            else
                            {
                                Message(string.Format("Файл {0} не существует.\n", fileName), true);
                            }
                        }
                        if (texts.Count != 0)
                        {
                            Console.WriteLine("Обработка содержимого файлов.");
                            List<string> rows = new List<string>();
                            string allText = string.Join("\n", texts);
                            texts = allText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            const string patternVideo = @"^(http(s)?\:\/\/)?(www\.)?youtube\.com\/watch\?v=([-_0-9a-zA-Z]){11}(&([-=_0-9a-zA-Z&])*)?\|(144|240|360|480|720|1080)\|([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?)+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?\\)+)$";
                            const string patternPlaylist = @"^(http(s)?\:\/\/)?(www\.)?youtube\.com\/playlist\?list=([-_0-9a-zA-Z]){34}(&([-=_0-9a-zA-Z&])*)?\|(144|240|360|480|720|1080)\|([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?)+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?\\)+)$";
                            foreach (string row in texts)
                            {
                                if (!Regex.IsMatch(row, patternVideo))
                                {
                                    if (!Regex.IsMatch(row, patternPlaylist))
                                    {
                                        Message(string.Format("Строка {0} имеет недопустимый формат.\n", row), true);
                                    }
                                    else
                                    {
                                        if (CheckDrive(row, out char driverName))
                                        {
                                            /*string playlistId = item.Substring(item.IndexOf("list=") + 5, 34);
                                            try
                                            {
                                                Playlist playlist = await youtube.Playlists.GetAsync(playlistId);
                                                if (playlist != null)
                                                {
                                                    IAsyncEnumerable<PlaylistVideo> videos = youtube.Playlists.GetVideosAsync(playlist.Id);
                                                    await foreach (PlaylistVideo video in videos)
                                                    {
                                                        list2.Add(string.Concat("https://www.youtube.com/watch?v=", video.Id, item.Substring(item.IndexOf('|'))));
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Message(string.Format("Плейлист {0}: {1}\n", playlistId, ex.Message), true);
                                            }*/
                                        }
                                        else
                                        {
                                            Message(string.Format("Строка {0}: Диск {1} не существует.\n", row, driverName), true);
                                        }
                                    }
                                }
                                else
                                {
                                    if (CheckDrive(row, out char driverName))
                                    {
                                        rows.Add(row);
                                    }
                                    else
                                    {
                                        Message(string.Format("Строка {0}: Диск {1} не существует.\n", row, driverName), true);
                                    }
                                }
                            }
                            texts.Clear();
                            if (rows.Count != 0)
                            {
                                List<Tuple<VideoInfo, string>> listVideos = new List<Tuple<VideoInfo, string>>();
                                foreach (string row in rows)
                                {
                                    string[] parts = row.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    parts[0] = parts[0].Substring(parts[0].IndexOf('=') + 1, 11);
                                    string url = string.Concat("https://www.youtube.com/watch?v=", parts[0]);
                                    Message(string.Format("Поиск видео по URL {0}.\n", url));
                                    int resolution = Convert.ToInt32(parts[1]);
                                    try
                                    {
                                        VideoInfo videoInfo = DownloadUrlResolver.GetDownloadUrls(url).OrderByDescending(x => x.Resolution).FirstOrDefault(x => x.VideoType == VideoType.Mp4 && x.Resolution <= resolution && x.AudioType != AudioType.Unknown);
                                        if (videoInfo != null)
                                        {
                                            if (videoInfo.RequiresDecryption)
                                            {
                                                DownloadUrlResolver.DecryptDownloadUrl(videoInfo);
                                            }
                                            listVideos.Add(new Tuple<VideoInfo, string>(videoInfo, parts[2]));
                                        }
                                        else
                                        {
                                            Message(string.Format("Видео по URL {0} не найдено.\n", url), true);
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        Message(string.Format("URL: {0}: {1}\n", url, exception.Message), true);
                                        break;
                                    }
                                }
                                rows.Clear();
                                if (listVideos.Count != 0)
                                {
                                    char[] invalidPathChars = Path.GetInvalidPathChars();
                                    char[] invalidNameChars = Path.GetInvalidFileNameChars();
                                    timer = new Timer(1000);
                                    timer.Elapsed += Timer_Elapsed;
                                    foreach (Tuple<VideoInfo, string> video in listVideos)
                                    {
                                        try
                                        {
                                            string titleReplaced = ReplaceChars(invalidNameChars, video.Item1.Title);
                                            string title = titleReplaced;
                                            string filePathReplaced = ReplaceChars(invalidPathChars, video.Item2);
                                            string filePath;
                                            int j = 1;
                                            do
                                            {
                                                filePath = Path.Combine(filePathReplaced, string.Format("{0}{1}", title, video.Item1.VideoExtension));
                                                title = titleReplaced + string.Format(" ({0})", j);
                                                j++;
                                            } while (File.Exists(filePath));
                                            if (!Directory.Exists(video.Item2))
                                            {
                                                Directory.CreateDirectory(video.Item2);
                                            }
                                            VideoDownloader videoDownloader = new VideoDownloader(video.Item1, filePath);
                                            videoDownloader.DownloadStarted += VideoDownloader_DownloadStarted;
                                            videoDownloader.DownloadProgressChanged += VideoDownloader_DownloadProgressChanged;
                                            videoDownloader.DownloadFinished += VideoDownloader_DownloadFinished;
                                            firstInfo = true;
                                            videoDownloader.Execute();
                                        }
                                        catch (Exception exception)
                                        {
                                            Message(string.Format("Видео {0}: {1}\n", video.Item1.Title, exception.Message), true);
                                        }
                                    }
                                    listVideos.Clear();
                                }
                                else
                                {
                                    Message("Нет данных для загрузки видео.\n", true);
                                }
                            }
                            else
                            {
                                Message("Нет данных для поиска видео.\n", true);
                            }
                        }
                        else
                        {
                            Message("Нет данных для обработки.\n", true);
                        }
                    }
                    else
                    {
                        Message("URL https://www.youtube.com недоступен.\n", true);
                    }
                }
                else
                {
                    Message("Отсутствует соединение с сетью Интернет.\n", true);
                }
            }
            else
            {
                Message("Отсутствует параметр 'Путь к файлу'.\n", true);
            }
            if (errorIndicator)
            {
                Message("Операция завершена с ошибками.\n");
            }
            else
            {
                Message("Операция успешно завершена.\n");
            }
            Console.ReadKey();
        }

        private static string ReplaceChars(char[] chars, string title)
        {
            foreach (char item in chars)
            {
                if (title.IndexOf(item) != -1)
                {
                    title = title.Replace(item, '_');
                }
            }
            return title;
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!firstInfo)
            {
                Message(string.Format("\rПрогресс загрузки: {0:P2}.", percent));
            }
        }

        private static void VideoDownloader_DownloadFinished(object sender, EventArgs e)
        {
            timer.Stop();
            VideoDownloader videoDownloader = sender as VideoDownloader;
            Message(string.Format("\rВидео загружено в {0}.\n", videoDownloader.SavePath));
        }

        private static void VideoDownloader_DownloadStarted(object sender, EventArgs e)
        {
            if (firstInfo)
            {
                firstInfo = false;
                timer.Start();
                VideoDownloader videoDownloader = sender as VideoDownloader;
                Message(string.Format("Загрузка видео {0}.\n", videoDownloader.Video.Title));
            }
        }

        private static void VideoDownloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            percent = e.ProgressPercentage / 100;
        }

        private static void Message(string message, bool error = false)
        {
            Console.Write(message);
            if (!errorIndicator && error)
            {
                errorIndicator = error;
            }
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

        //private static string SpeedToString(double speed)
        //{
        //    string si;
        //    if (speed >= 1073741824)
        //    {
        //        speed /= 1073741824;
        //        si = "ГБ";
        //    }
        //    else if (speed >= 1048576)
        //    {
        //        speed /= 1048576;
        //        si = "МБ";
        //    }
        //    else if (speed >= 1024)
        //    {
        //        speed /= 1024;
        //        si = "КБ";
        //    }
        //    else
        //    {
        //        si = "Б";
        //    }
        //    return string.Format("{0:f1} {1}/c", speed, si);
        //}
    }
}