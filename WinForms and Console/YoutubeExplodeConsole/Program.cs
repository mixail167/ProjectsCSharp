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
                            YoutubeClient youtube = new YoutubeClient();
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
                                            string playlistId = row.Substring(row.IndexOf("list=") + 5, 34);
                                            try
                                            {
                                                Playlist playlist = await youtube.Playlists.GetAsync(playlistId);
                                                if (playlist != null)
                                                {
                                                    IAsyncEnumerable<PlaylistVideo> videos = youtube.Playlists.GetVideosAsync(playlist.Id);
                                                    await foreach (PlaylistVideo video in videos)
                                                    {
                                                        rows.Add(string.Concat("https://www.youtube.com/watch?v=", video.Id, row.Substring(row.IndexOf('|'))));
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
                                List<Tuple<Video, IVideoStreamInfo, string>> videoList = new List<Tuple<Video, IVideoStreamInfo, string>>();
                                foreach (string row in rows)
                                {
                                    string[] partsRow = row.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    string id = partsRow[0].Substring(partsRow[0].IndexOf('=') + 1, 11);
                                    string url = string.Concat("https://www.youtube.com/watch?v=", id);
                                    string path = partsRow[2];
                                    Message(string.Format("Поиск видео по URL {0}.\n", url));
                                    int resolution = Convert.ToInt32(partsRow[1]);
                                    try
                                    {
                                        Video videoInfo = await youtube.Videos.GetAsync(id);
                                        StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(id);
                                        IVideoStreamInfo videoStreamInfo = streamManifest.GetMuxedStreams().Where(p => p.Container == Container.Mp4 && Convert.ToInt32(p.VideoQuality.Label.Substring(0, p.VideoQuality.Label.Length - 1)) <= resolution).OrderByDescending(p => p.VideoQuality.Label).GetWithHighestVideoQuality();
                                        if (videoStreamInfo != null)
                                        {
                                            videoList.Add(new Tuple<Video, IVideoStreamInfo, string>(videoInfo, videoStreamInfo, path));
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
                                if (videoList.Count != 0)
                                {
                                    char[] invalidPathChars = Path.GetInvalidPathChars();
                                    char[] invalidNameChars = Path.GetInvalidFileNameChars();
                                    foreach (Tuple<Video, IVideoStreamInfo, string> video in videoList)
                                    {
                                        try
                                        {
                                            string titleReplaced = ReplaceChars(invalidNameChars, video.Item1.Title);
                                            string title = titleReplaced;
                                            string filePathReplaced = ReplaceChars(invalidPathChars, video.Item3);
                                            string filePath;
                                            int j = 1;
                                            do
                                            {
                                                filePath = Path.Combine(video.Item3, string.Format("{0}.{1}", title, video.Item2.Container.Name));
                                                title = titleReplaced + string.Format(" ({0})", j);
                                                j++;
                                            } while (File.Exists(filePath));
                                            if (!Directory.Exists(video.Item3))
                                            {
                                                Directory.CreateDirectory(video.Item3);
                                            }
                                            using Progress progress = new Progress(video.Item1.Title, filePath, video.Item2.Size.Bytes);
                                            progress.Message += Progress_Message;
                                            await youtube.Videos.Streams.DownloadAsync(video.Item2, filePath, progress);

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
                                            Message(string.Format("Видео {0}: {1}\n", video.Item1.Title, exception.Message), true);
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
    }
}