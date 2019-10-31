using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BitrateChanger
{
    class Program
    {
        private static bool next = true;

        private static void Error(string text)
        {
            Console.WriteLine(text);
            next = false;
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    Error("Ошибка создания каталога " + path);
                }
            }
        }

        private static void Main(string[] args)
        {
            if (args.Length >= 3)
            {
                if (Directory.Exists(args[0]))
                {
                    CreateDirectory(args[1]);
                    if (next)
                    {
                        if (Regex.IsMatch(args[2], "^32|96|128|160|192|256|320$"))
                        {
                            bool subdir = false;
                            bool random = false;
                            for (int i = 3; i < args.Length; i++)
                            {
                                switch (args[i])
                                {
                                    case "\\subdir":
                                        subdir = true;
                                        break;
                                    case "\\random":
                                        random = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            string[] files;
                            args[0] = args[0].TrimEnd(new char[] { '\\' });
                            args[1] = args[1].TrimEnd(new char[] { '\\' });
                            if (subdir)
                            {
                                files = Directory.GetFiles(args[0], "*.mp3", SearchOption.AllDirectories);
                            }
                            else
                            {
                                files = Directory.GetFiles(args[0], "*.mp3", SearchOption.TopDirectoryOnly);
                            }
                            int bitrate = Convert.ToInt32(args[2]) * 1024;
                            foreach (string item in files)
                            {
                                FileInfo fileInfo = new FileInfo(item);
                                string path = args[1] + fileInfo.DirectoryName.Replace(args[0], "");
                                CreateDirectory(path);
                                if (next)
                                {
                                    string name = fileInfo.Name;
                                    if (random && !Regex.IsMatch(name, "^([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}).*$"))
                                    {
                                        name = Guid.NewGuid() + "_" + name;
                                    }
                                    path = Path.Combine(path, name);
                                    using (MediaFoundationReader reader = new MediaFoundationReader(item))
                                    {
                                        MediaFoundationEncoder.EncodeToMp3(reader, path, bitrate);
                                        Console.WriteLine("{0} -> {1}", item, path);
                                    }
                                }
                                else
                                {
                                    next = true;
                                }
                            }
                        }
                        else
                        {
                            Error("Неверный битрейт для выходных файлов");
                        }
                    }
                }
                else
                {
                    Error("Неверный путь к каталогу с исходными файлами");
                }
            }
            else
            {
                Error("Недостаточное количество параметров");
            }
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
