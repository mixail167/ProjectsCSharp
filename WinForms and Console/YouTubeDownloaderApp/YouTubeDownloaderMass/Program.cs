using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloaderMass
{
    class Program
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        static bool errorIndicator = false;

        static async Task Main(string[] args)
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
                                    Message(string.Format("Чтение файла {0}.\n", fileName));
                                    try
                                    {
                                        using StreamReader streamReader = new StreamReader(fileName);
                                        list.Add(streamReader.ReadToEnd());
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
                        if (list.Count != 0)
                        {
                            Console.WriteLine("Обработка содержимого файлов.");
                            YoutubeClient youtube = new YoutubeClient();
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
                                        Message(string.Format("Строка {0} имеет недопустимый формат.\n", item), true);
                                    }
                                    else
                                    {
                                        if (CheckDrive(item, out char driverName))
                                        {
                                            string playlistId = item.Substring(item.IndexOf("list=") + 5, 34);
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
                                            }
                                        }
                                        else
                                        {
                                            Message(string.Format("Строка {0}: Диск {1} не существует.\n", item, driverName), true);
                                        }
                                    }
                                }
                                else
                                {
                                    if (CheckDrive(item, out char driverName))
                                    {
                                        list2.Add(item);
                                    }
                                    else
                                    {
                                        Message(string.Format("Строка {0}: Диск {1} не существует.\n", item, driverName), true);
                                    }
                                }
                            }
                            list.Clear();
                            if (list2.Count != 0)
                            {
                                List<Tuple<Video, IVideoStreamInfo, string>> videoList = new List<Tuple<Video, IVideoStreamInfo, string>>();
                                foreach (string item in list2)
                                {
                                    string[] parts = item.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    parts[0] = parts[0].Substring(parts[0].IndexOf('=') + 1, 11);
                                    string url = string.Concat("https://www.youtube.com/watch?v=", parts[0]);
                                    Message(string.Format("Поиск видео по URL {0}.\n", url));
                                    int resolution = Convert.ToInt32(parts[1]);
                                    try
                                    {
                                        Video videoInfo = await youtube.Videos.GetAsync(parts[0]);
                                        StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(parts[0]);
                                        IVideoStreamInfo videoStreamInfo = streamManifest.GetMuxedStreams().Where(p => p.Container == Container.Mp4 && Convert.ToInt32(p.VideoQuality.Label.Substring(0, p.VideoQuality.Label.Length - 1)) <= resolution).OrderByDescending(p => p.VideoQuality.Label).GetWithHighestVideoQuality();
                                        if (videoStreamInfo != null)
                                        {
                                            videoList.Add(new Tuple<Video, IVideoStreamInfo, string>(videoInfo, videoStreamInfo, parts[2]));
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
                                list2.Clear();
                                if (videoList.Count != 0)
                                {
                                    char[] invalidChars = Path.GetInvalidFileNameChars();
                                    foreach (Tuple<Video, IVideoStreamInfo, string> item in videoList)
                                    {
                                        try
                                        {
                                            string title1 = item.Item1.Title;
                                            foreach (char item1 in invalidChars)
                                            {
                                                if (title1.IndexOf(item1) != -1)
                                                {
                                                    title1 = title1.Replace(item1, '_');
                                                }
                                            }
                                            string title = title1;
                                            string filePath;
                                            string fileName;
                                            int j = 1;
                                            do
                                            {
                                                filePath = Path.Combine(item.Item3, string.Format("{0}.{1}", title, item.Item2.Container.Name));
                                                fileName = title;
                                                title = title1 + string.Format(" ({0})", j);
                                                j++;
                                            } while (File.Exists(filePath));
                                            if (!Directory.Exists(item.Item3))
                                            {
                                                Directory.CreateDirectory(item.Item3);
                                            }
                                            using Progress progress = new Progress(fileName, filePath, item.Item2.Size.Bytes);
                                            progress.Message += Progress_Message;
                                            await youtube.Videos.Streams.DownloadAsync(item.Item2, filePath, progress);

                                        }
                                        //catch (WebException exception)
                                        //{
                                        //    if (exception.Status == WebExceptionStatus.ProtocolError)
                                        //    {
                                        //        switch ((exception.Response as HttpWebResponse).StatusCode)
                                        //        {
                                        //            case System.Net.HttpStatusCode.Forbidden:
                                        //                Message(string.Format("Видео {0}: Доступ запрещен.", videoList[i].Item1.Title), true);
                                        //                break;
                                        //            default:
                                        //                Message(string.Format("Видео {0}: Неизвестная ошибка.", videoList[i].Item1.Title), true);
                                        //                break;
                                        //        }
                                        //    }
                                        //}
                                        catch (Exception exception)
                                        {
                                            Message(string.Format("Видео {0}: {1}\n", item.Item1.Title, exception.Message), true);
                                        }
                                    }
                                    videoList.Clear();
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
                        Message(@"URL https://www.youtube.com недоступен.\n", true);
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

        private static void Progress_Message(string message)
        {
            Message(message);
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
    }
}