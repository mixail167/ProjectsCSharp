using NAudio.Wave;
using System;
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
                                    try
                                    {
                                        TagLib.Tag tag = TagLib.File.Create(item).Tag;
                                        if (!tag.IsEmpty)
                                        {
                                            TagLib.File file = TagLib.File.Create(path);
                                            file.Tag.MusicBrainzReleaseArtistId = tag.MusicBrainzReleaseArtistId;
                                            file.Tag.MusicBrainzTrackId = tag.MusicBrainzTrackId;
                                            file.Tag.MusicBrainzDiscId = tag.MusicBrainzDiscId;
                                            file.Tag.MusicIpId = tag.MusicIpId;
                                            file.Tag.AmazonId = tag.AmazonId;
                                            file.Tag.MusicBrainzReleaseStatus = tag.MusicBrainzReleaseStatus;
                                            file.Tag.MusicBrainzReleaseType = tag.MusicBrainzReleaseType;
                                            file.Tag.MusicBrainzReleaseCountry = tag.MusicBrainzReleaseCountry;
                                            file.Tag.Pictures = tag.Pictures;
                                            file.Tag.Artists = tag.Artists;
                                            file.Tag.MusicBrainzReleaseId = tag.MusicBrainzReleaseId;
                                            file.Tag.MusicBrainzArtistId = tag.MusicBrainzArtistId;
                                            file.Tag.Copyright = tag.Copyright;
                                            file.Tag.Conductor = tag.Conductor;
                                            file.Tag.Title = tag.Title;
                                            file.Tag.TitleSort = tag.TitleSort;
                                            file.Tag.Performers = tag.Performers;
                                            file.Tag.PerformersSort = tag.PerformersSort;
                                            file.Tag.AlbumArtists = tag.AlbumArtists;
                                            file.Tag.AlbumArtistsSort = tag.AlbumArtistsSort;
                                            file.Tag.Composers = tag.Composers;
                                            file.Tag.Album = tag.Album;
                                            file.Tag.ComposersSort = tag.ComposersSort;
                                            file.Tag.Comment = tag.Comment;
                                            file.Tag.Genres = tag.Genres;
                                            file.Tag.Year = tag.Year;
                                            file.Tag.Track = tag.Track;
                                            file.Tag.TrackCount = tag.TrackCount;
                                            file.Tag.Disc = tag.Disc;
                                            file.Tag.DiscCount = tag.DiscCount;
                                            file.Tag.Lyrics = tag.Lyrics;
                                            file.Tag.Grouping = tag.Grouping;
                                            file.Tag.BeatsPerMinute = tag.BeatsPerMinute;
                                            file.Tag.AlbumSort = tag.AlbumSort;
                                            file.Save();
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("{0}: невозможно изменить теги.", path);
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
