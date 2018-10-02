using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AudioPlayer
{
    public static class CommonInterface
    {
        /// <summary>
        /// Путь к исполняемому файлу
        /// </summary>
        public static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Регулярное выражение для валидации URL-адреса
        /// </summary>
        public static readonly string URLPattern = "(?:(?:ht|f)tps?://)?(?:[\\-\\w]+:[\\-\\w]+@)?(?:[0-9a-z][\\-0-9a-z]*[0-9a-z]\\.)+[a-z]{2,6}(?::\\d{1,5})?(?:[?/\\\\#][?!^$.(){}:|=[\\]+\\-/\\\\*;&~#@,%\\wА-Яа-я]*)?";

        /// <summary>
        /// Регулярное выражение для валидации пути к файлу/папке
        /// </summary>
        public static readonly string PathPattern2 = @"\A(?:\b[a-z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*)\Z";

        /// <summary>
        /// Список полных имён файлов
        /// </summary>
        public static List<Tag> Files = new List<Tag>();

        /// <summary>
        /// Список URL-адресов радиостанций
        /// </summary>
        public static List<string> RadioAddreses = new List<string>();

        /// <summary>
        /// Ссылка на главную форму
        /// </summary>
        public static Form1 Link1;

        /// <summary>
        /// Ссылка на форму радио
        /// </summary>
        public static Form2 Link2;

        /// <summary>
        /// Счётчик обновления визуализации
        /// </summary>
        public static int Iterator;

        /// <summary>
        /// Текущий номер трека
        /// </summary>
        public static int CurrentTrackNumber = -1;

        /// <summary>
        /// Громкость
        /// </summary>
        public static int Volume;

        /// <summary>
        /// Флаг приостановки 
        /// </summary>
        public static bool Pause = false;

        /// <summary>
        /// Установка фильтра на расширение файлов
        /// </summary>
        public static void SetFileFilter()
        {
            Link1.openFileDialog1.Filter =
                "Все форматы|*.mp3;*.m4a;*.mp4;*.ogg;*.opus;*.ac3;*.ape;*.mpc;*.flac;*.wma;*.tta;*.alac;*.wv;*.m3u"
                + "|MPEG Audio Layer III (*.mp3)|*.mp3"
                + "|Advanced Audio Coding (*.m4a;*.mp4)|*.m4a;*.mp4"
                + "|OGG Vorbis Audio (*.ogg)|*.ogg"
                + "|OPUS Audio (*.opus)|*.opus"
                + "|Dolby Digital AC3 (*.ac3)|*.ac3"
                + "|Monkey's Audio (*.ape)|*.ape"
                + "|MusePack (*.mpc)|*.mpc"
                + "|Free Lossless Audio Codec (*.flac)|*.flac"
                + "|Windows Media Audio (*.wma)|*.wma"
                + "|True Audio (*.tta)|*.tta"
                + "|Apple Lossless Audio Codec (*.alac)|*.alac"
                + "|WavPack (*.wv)|*.wv"
                + "|Playlist (*.m3u)|*.m3u";
        }

        /// <summary>
        /// Перерисовка главной формы
        /// </summary>
        /// <param name="index"></param>
        public static void RefreshForm(int index)
        {
            Link1.label1.Text = TimeSpan.FromSeconds(Audio.GetPosOfStream(Audio.Stream)).ToString();
            Link1.label2.Text = TimeSpan.FromSeconds(Audio.GetTimeOfStream(Audio.Stream)).ToString();
            Link1.colorSlider1.Maximum = Audio.GetTimeOfStream(Audio.Stream);
            Link1.colorSlider1.Value = Audio.GetPosOfStream(Audio.Stream);
            Link1.label4.Text = Files[index].FileName;
            Link1.pictureBox4.Image = Files[index].Image;
            Iterator = 0;
            RefreshPlayList(index);
            Link1.timer1.Enabled = true;
        }

        /// <summary>
        /// Очистка формы
        /// </summary>
        public static void ClearForm()
        {
            Audio.Stop();
            Link1.timer1.Enabled = false;
            Link1.colorSlider1.Value = 0;
            Link1.label1.Text = "00:00:00";
            Link1.label2.Text = "00:00:00";
            Link1.label4.Text = string.Empty;
            Link1.pictureBox1.Image = null;
            Link1.pictureBox2.Image = null;
            Link1.pictureBox3.BackColor = Color.Black;
            Link1.pictureBox4.Image = null;
        }

        /// <summary>
        /// Перерисовка плейлиста
        /// </summary>
        /// <param name="index"></param>
        public static void RefreshPlayList(int index1)
        {
            foreach (ListViewItem item in Link1.listView1.Items)
            {
                if (item.Index == index1)
                {
                    item.BackColor = Color.Yellow;
                }
                else
                {
                    item.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// Визуализация звука
        /// </summary>
        /// <param name="isRadio"></param>
        public static void Visualisation(bool isRadio = false)
        {
            Iterator++;
            if (isRadio)
            {
                if (Iterator == 2)
                {
                    Link2.pictureBox1.Image = Audio.Visualisation.CreateSpectrumLinePeak(Audio.Stream, Link2.pictureBox1.Width, Link2.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 2, 1, 1, 10, false, true, true);
                    Link2.pictureBox2.Image = Audio.Visualisation.CreateWaveForm(Audio.Stream, Link2.pictureBox2.Width, Link2.pictureBox2.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 3, true, false, true);
                    Iterator = 0;
                }
                if (Audio.Visualisation.DetectFrequency(Audio.Stream, 10, 500, true) > 0.3)
                {
                    Link2.pictureBox3.BackColor = Color.Red;
                }
                else
                {
                    Link2.pictureBox3.BackColor = Color.Black;
                }
            }
            else
            {
                if (Iterator == 2)
                {
                    Link1.pictureBox1.Image = Audio.Visualisation.CreateSpectrumLinePeak(Audio.Stream, Link1.pictureBox1.Width, Link1.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 2, 1, 1, 10, false, true, true);
                    Link1.pictureBox2.Image = Audio.Visualisation.CreateWaveForm(Audio.Stream, Link1.pictureBox2.Width, Link1.pictureBox2.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 3, true, false, true);
                    Iterator = 0;
                }
                if (Audio.Visualisation.DetectFrequency(Audio.Stream, 10, 500, true) > 0.3)
                {
                    Link1.pictureBox3.BackColor = Color.Red;
                }
                else
                {
                    Link1.pictureBox3.BackColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Чтение файла плейлиста
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="isRadio"></param>
        public static void ReadPlayList(string filepath, bool isRadio = false)
        {
            if (File.Exists(filepath))
            {
                StreamReader reader = new StreamReader(filepath);
                while (!reader.EndOfStream)
                {
                    try
                    {
                        string str = reader.ReadLine();
                        if (isRadio)
                        {
                            AddTrackOrURL(str, isRadio);
                        }
                        else
                        {
                            if (File.Exists(str))
                            {
                                AddTrackOrURL(str);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Добавление трека или URL-адреса
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="isRadio"></param>
        public static void AddTrackOrURL(string filepath, bool isRadio = false)
        {
            Tag tag = new Tag(filepath, isRadio);
            if (!tag.Error)
            {
                if (isRadio)
                {
                    if (!CommonInterface.RadioAddreses.Contains(tag.Path))
                    {
                        CommonInterface.RadioAddreses.Add(tag.Path);
                        Link2.listBox1.Items.Add(tag.Path);
                    }
                }
                else
                {
                    CommonInterface.Files.Add(tag);
                    Link1.listView1.Items.Add(new ListViewItem(new string[] 
                    { 
                        tag.FileName,
                        tag.Title,
                        tag.Artist,
                        tag.Album,
                        tag.Genre,
                        tag.Year,
                        tag.BitRate.ToString(),
                        tag.Channels,
                        tag.Freq.ToString()
                    }));
                }
            }
        }

        /// <summary>
        /// Валидация URL-адреса
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsValid(string url, string pattern)
        {
            if (Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Сохранение плейлиста
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="isRadio"></param>
        internal static void SavePlayList(string filepath, bool isRadio)
        {
            try
            {
                StreamWriter writer = new StreamWriter(filepath, false, Encoding.UTF8);
                string output = string.Empty;
                if (isRadio)
                {
                    foreach (string item in RadioAddreses)
                    {
                        output += item + "\n";
                    }
                }
                else
                {
                    foreach (Tag item in Files)
                    {
                        output += item.Path + "\n";
                    }
                }
                writer.Write(output.TrimEnd('\n'));
                writer.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Плейлист не сохранен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Получение файлов из каталогов
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isRadio"></param>
        internal static void GetFilesFromFolder(string path, bool isRadio = false)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            //DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            //foreach (DirectoryInfo item in dirs)
            //{
            //    GetFilesFromFolder(item.FullName, isRadio);
            //}
            foreach (FileInfo item in files)
            {
                CheckExtension(item, isRadio);
            }
        }

        /// <summary>
        /// Проверка расширения файла
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isRadio"></param>
        internal static void CheckExtension(FileInfo file, bool isRadio = false)
        {
            switch (file.Extension)
            {
                case ".mp3":
                case ".m4a":
                case ".mp4":
                case ".ogg":
                case ".opus":
                case ".ac3":
                case ".ape":
                case ".mpc":
                case ".flac":
                case ".wma":
                case ".tta":
                case ".alac":
                case ".wv":
                    if (!isRadio)
                    {
                        AddTrackOrURL(file.FullName);
                    }
                    break;
                case ".m3u":
                    ReadPlayList(file.FullName, isRadio);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Удаление трека
        /// </summary>
        /// <param name="index"></param>
        internal static void DeleteTrack(int index)
        {
            Files.RemoveAt(index);
            Link1.listView1.Items.RemoveAt(index);
        }

        /// <summary>
        /// Воспроизведение (обертка)
        /// </summary>
        /// <param name="flag"></param>
        internal static void Play(bool flag)
        {
            if (Link1.listView1.Items.Count != 0)
            {
                if (!flag && Link1.listView1.SelectedItems.Count != 0)
                {
                    if (Audio.Play(Files[Link1.listView1.SelectedItems[0].Index].Path, Audio.Volume))
                    {
                        CurrentTrackNumber = Link1.listView1.SelectedItems[0].Index;
                        RefreshForm(CurrentTrackNumber);
                    }
                    else
                    {
                        DeleteTrack(Link1.listView1.SelectedItems[0].Index);
                        ClearForm();
                    }
                }
                else if (flag && CurrentTrackNumber >= 0)
                {
                    if (Audio.Play(Files[CurrentTrackNumber].Path, Audio.Volume))
                    {
                        RefreshForm(CurrentTrackNumber);
                    }
                    else
                    {
                        DeleteTrack(CurrentTrackNumber);
                        ClearForm();
                    }
                }
            }
            Pause = false;
        }

        /// <summary>
        /// Случайный трек
        /// </summary>
        internal static void RandomTrack()
        {
            Random rand = new Random();
            while (Files.Count > 0)
            {
                CurrentTrackNumber = rand.Next(Files.Count);
                if (Audio.Play(Files[CurrentTrackNumber].Path, Audio.Volume))
                {
                    RefreshForm(CurrentTrackNumber);
                    return;
                }
                else
                {
                    DeleteTrack(CurrentTrackNumber);
                }
            }
            ClearForm();
        }

        internal static void SetVolume()
        {
            Volume = -1;
            Link1.checkBox2.Checked = false;
            Audio.SetVolumeToStream(Audio.Stream, Link1.colorSlider2.Value);
        }
    }
}
