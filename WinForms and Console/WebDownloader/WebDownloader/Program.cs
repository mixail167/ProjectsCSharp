using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;

namespace WebDownloader
{
    class Program
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        static bool errorIndicator = false;
        static bool first = true;
        static bool endOfDownload;
        static Timer timer;
        static long bytesCount;
        static double percent;

        static void Main(string[] args)
        {
            Console.Title = "WebDownloader. Массовая загрузка файлов";
            if (args.Length > 0)
            {
                int description;
                if (InternetGetConnectedState(out description, 0) && (new Ping()).Send("www.google.com").Status == IPStatus.Success)
                {
                    List<string> list = new List<string>();
                    foreach (string fileName in args)
                    {
                        if (File.Exists(fileName))
                        {
                            if (Path.GetExtension(fileName).Equals(".dwl"))
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
                                Message(string.Format("Файл {0} должен иметь расширение '.dwl'.", fileName), true);
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
                        const string pattern = @"^(((https?)|(ftp)):\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w\.-]*)*\/?\|.+\|([a-zA-Z]:\\|[a-zA-Z]:(\\(\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?)+|[a-zA-Z]:\\((\b[^ \\\/\*\:\?\<\>\|\""][^\\\/\*\:\?\<\>\|\""]*[^ \\\/\*\:\?\<\>\|\""]\b|[^ \\\/\*\:\?\<\>\|\""])[\)]?\\)+)$";
                        foreach (string item in list)
                        {
                            if (!Regex.IsMatch(item, pattern))
                            {
                                Message(string.Format("Строка {0} имеет недопустимый формат.", item), true);
                            }
                            else
                            {
                                char driverName;
                                if (CheckDrive(item, out driverName))
                                {
                                    list2.Add(item);
                                }
                                else
                                {
                                    Message(string.Format("Строка {0}: Диск {1} не существует.", item, driverName), true);
                                }
                            }
                        }
                        list.Clear();
                        if (list2.Count != 0)
                        {
                            List<Tuple<string, string, string>> videoList = new List<Tuple<string, string, string>>();
                            foreach (string item in list2)
                            {
                                string[] parts = item.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                videoList.Add(new Tuple<string, string, string>(parts[0], parts[1], parts[2]));
                            }
                            list2.Clear();
                            if (videoList.Count != 0)
                            {
                                timer = new Timer(1000);
                                timer.Elapsed += timer_Elapsed;
                                char[] invalidChars = Path.GetInvalidFileNameChars();
                                for (int i = 0; i < videoList.Count; i++)
                                {
                                    try
                                    {
                                        string title1 = videoList[i].Item2;
                                        foreach (char item in invalidChars)
                                        {
                                            if (title1.IndexOf(item) != -1)
                                            {
                                                title1 = title1.Replace(item, '_');
                                            }
                                        }
                                        string title = title1;
                                        int j = 1;
                                        while (true)
                                        {
                                            if (File.Exists(Path.Combine(videoList[i].Item3, title)))
                                            {
                                                title = Path.GetFileNameWithoutExtension(title1) + string.Format(" ({0})", j) + Path.GetExtension(title1);
                                                j++;
                                            }
                                            else break;
                                        }
                                        WebClient downloader = new WebClient();
                                        downloader.DownloadFileCompleted += downloader_DownloadFinished;
                                        downloader.DownloadProgressChanged += downloader_DownloadProgressChanged;
                                        if (!Directory.Exists(videoList[i].Item3))
                                        {
                                            Directory.CreateDirectory(videoList[i].Item3);
                                        }
                                        DownloadStarted(title);
                                        downloader.DownloadFileAsync(new Uri(videoList[i].Item1), Path.Combine(videoList[i].Item3, title));
                                        while (!endOfDownload)
                                        {
                                            System.Threading.Thread.Sleep(1000);
                                        }
                                    }
                                    catch (WebException exception)
                                    {
                                        if (exception.Status == WebExceptionStatus.ProtocolError)
                                        {
                                            switch ((exception.Response as HttpWebResponse).StatusCode)
                                            {
                                                case System.Net.HttpStatusCode.Forbidden:
                                                    Message(string.Format("URL {0}: Доступ запрещен.", videoList[i].Item1), true);
                                                    break;
                                                default:
                                                    Message(string.Format("URL {0}: Неизвестная ошибка.", videoList[i].Item1), true);
                                                    break;
                                            }
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        Message(string.Format("URL {0}: {1}", videoList[i].Item1, exception.Message), true);
                                    }
                                }
                                videoList.Clear();
                            }
                            else
                            {
                                Message("Нет данных для загрузки.", true);
                            }
                        }
                        else
                        {
                            Message("Нет данных для поиска.", true);
                        }
                    }
                    else
                    {
                        Message("Нет данных для обработки.", true);
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

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
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

        static void downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            bytesCount += e.BytesReceived;
            percent = e.ProgressPercentage;
        }

        private static void DownloadStarted(string title)
        {
            bytesCount = 0;
            percent = 0;
            first = true;
            endOfDownload = false;
            timer.Start();
            Message(string.Format("Загрузка файла {0}.", title));
        }

        static void downloader_DownloadFinished(object sender, AsyncCompletedEventArgs e)
        {
            timer.Stop();
            if (!first)
            {
                Console.CursorTop--;
            }
            Message(string.Format("{0,-" + (Console.BufferWidth - 1) + "}", "Файл загружен."));
            endOfDownload = true;
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
