using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplodeConsole
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
                                List<VideoData> videoList = new List<VideoData>();
                                foreach (string row in rows)
                                {
                                    string[] partsRow = row.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    string id = partsRow[0].Substring(partsRow[0].IndexOf('=') + 1, 11);
                                    int resolution = Convert.ToInt32(partsRow[1]);
                                    string path = partsRow[2];
                                    Message(string.Format("Поиск данных по URL https://www.youtube.com/watch?v={0}.\n", id));
                                    try
                                    {
                                        StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(id);
                                        if (streamManifest != null)
                                        {
                                            List<IStreamInfo> streams = new List<IStreamInfo>();
                                            ClosedCaptionTrackInfo trackInfo = null;
                                            string title = string.Empty;
                                            IEnumerable<VideoOnlyStreamInfo> videoOnlyStreams = streamManifest.GetVideoOnlyStreams();
                                            if (videoOnlyStreams.Count() > 0)
                                            {
                                                IStreamInfo videoStreamInfo = videoOnlyStreams.Where(p => p.Container == Container.Mp4 && p.VideoQuality.MaxHeight <= resolution).GetWithHighestVideoQuality();
                                                if (videoStreamInfo != null)
                                                {
                                                    Video videoInfo = await youtube.Videos.GetAsync(id);
                                                    if (videoInfo != null)
                                                    {
                                                        title = videoInfo.Title;
                                                    }
                                                    ClosedCaptionManifest trackManifest = await youtube.Videos.ClosedCaptions.GetManifestAsync(id);
                                                    if (trackManifest != null)
                                                    {
                                                        try
                                                        {
                                                            trackInfo = trackManifest.GetByLanguage("ru");
                                                        }
                                                        catch { }
                                                    }
                                                    streams.Add(videoStreamInfo);
                                                }
                                                else
                                                {
                                                    Message(string.Format("Видео по URL https://www.youtube.com/watch?v={0} не найдено.\n", id), true);
                                                }
                                            }
                                            else
                                            {
                                                Message(string.Format("Видео по URL https://www.youtube.com/watch?v={0} не найдено.\n", id), true);
                                            }
                                            IEnumerable<AudioOnlyStreamInfo> audioOnlyStreams = streamManifest.GetAudioOnlyStreams();
                                            if (audioOnlyStreams.Count() > 0)
                                            {
                                                IStreamInfo audioStreamInfo = audioOnlyStreams.Where(p => p.Container == Container.Mp4 || p.Container == Container.Mp3).GetWithHighestBitrate();
                                                if (audioStreamInfo != null)
                                                {
                                                    streams.Add(audioStreamInfo);
                                                }
                                                else
                                                {
                                                    Message(string.Format("Аудио по URL https://www.youtube.com/watch?v={0} не найдено.\n", id), true);
                                                }
                                            }
                                            else
                                            {
                                                Message(string.Format("Аудио по URL https://www.youtube.com/watch?v={0} не найдено.\n", id), true);
                                            }
                                            if (streams.Count > 0)
                                            {
                                                videoList.Add(new VideoData(title, streams, trackInfo, path));
                                            }
                                        }
                                        else
                                        {
                                            Message(string.Format("Данных по URL https://www.youtube.com/watch?v={0} не найдено.\n", id), true);
                                        }

                                    }
                                    catch (Exception exception)
                                    {
                                        Message(string.Format("URL: https://www.youtube.com/watch?v={0}: {1}\n", id, exception.Message), true);
                                    }
                                }
                                rows.Clear();
                                if (videoList.Count != 0)
                                {
                                    foreach (VideoData video in videoList)
                                    {
                                        try
                                        {
                                            string videoPath = GetFilePath(video.SavePath, video.TitleReplaced, video.Extension);
                                            string trackPath = string.Empty;
                                            if (video.TrackInfo != null)
                                            {
                                                trackPath = GetFilePath(video.SavePath, video.TitleReplaced, "srt");
                                            }
                                            if (!Directory.Exists(video.SavePath))
                                            {
                                                Directory.CreateDirectory(video.SavePath);
                                            }
                                            using Progress progress = new Progress(video.Title, videoPath, video.Size);
                                            progress.Message += Progress_Message;
                                            if (video.TrackInfo != null)
                                            {
                                                await youtube.Videos.DownloadAsync(video.Streams, new List<ClosedCaptionTrackInfo>() { video.TrackInfo }, new ConversionRequestBuilder(videoPath).Build(), progress);
                                            }
                                            else
                                            {
                                                await youtube.Videos.DownloadAsync(video.Streams, new ConversionRequestBuilder(videoPath).Build(), progress);
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            Message(string.Format("Видео/Аудио {0}: {1}\n", video.Title, exception.Message), true);
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
                                Message("Нет данных для поиска.\n", true);
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

        private static string GetFilePath(string filePath, string title, string extension)
        {
            string title_temp = title;
            string filePath_temp;
            int i = 1;
            do
            {
                filePath_temp = Path.Combine(filePath, string.Format("{0}.{1}", title_temp, extension));
                title_temp = string.Format("{0} ({1})", title, i);
                i++;
            } while (File.Exists(filePath_temp));
            return filePath_temp;
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
            if (DriveInfo.GetDrives().FirstOrDefault(p => p.Name.StartsWith(driverName.ToString(), true, CultureInfo.InvariantCulture)) == null)
            {
                return false;
            }
            return true;
        }
    }
}