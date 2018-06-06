using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private static readonly string Pattern = "(?:(?:ht|f)tps?://)?(?:[\\-\\w]+:[\\-\\w]+@)?(?:[0-9a-z][\\-0-9a-z]*[0-9a-z]\\.)+[a-z]{2,6}(?::\\d{1,5})?(?:[?/\\\\#][?!^$.(){}:|=[\\]+\\-/\\\\*;&~#@,%\\wА-Яа-я]*)?";

        /// <summary>
        /// Список полных имён файлов
        /// </summary>
        public static List<Tag> Files = new List<Tag>();

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
            Iterator = 0;
            RefreshPlayList(index);
        }

        /// <summary>
        /// Перерисовка плейлиста
        /// </summary>
        /// <param name="index"></param>
        public static void RefreshPlayList(int index)
        {
            foreach (ListViewItem item in Link1.listView1.Items)
            {
                if (item.Index == index)
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
        public static void Visualisation(bool isRadio)
        {
            Iterator++;
            if (isRadio)
            {
                if (Iterator == 2)
                {
                    Link2.pictureBox1.Image = Audio.Visualisation.CreateSpectrumLinePeak(Audio.Stream, Link2.pictureBox1.Width, Link2.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 2, 1, 1, 10, false, true, true);
                    Link2.pictureBox2.Image = Audio.Visualisation.CreateWaveForm(Audio.Stream, Link2.pictureBox1.Width, Link2.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 3, true, false, true);
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
                    Link1.pictureBox2.Image = Audio.Visualisation.CreateWaveForm(Audio.Stream, Link1.pictureBox1.Width, Link1.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 3, true, false, true);
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
        public static void ReadPlayList(string filepath, bool isRadio)
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
                            AddTrackOrURL(str, true);
                        }
                        else
                        {
                            if (File.Exists(str))
                            {
                                AddTrackOrURL(str, false);
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
        public static void AddTrackOrURL(string filepath, bool isRadio)
        {
            Tag tag = new Tag(filepath, isRadio);
            if (!tag.Error)
            {
                if (isRadio)
                {
                    Link2.listBox1.Items.Add(tag.Path);
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
        /// <returns></returns>
        public static bool IsValid(string url)
        {
            if (Regex.IsMatch(url, Pattern, RegexOptions.IgnoreCase))
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
                    foreach (Object item in Link2.listBox1.Items)
                    {
                        output += item.ToString() + "\n";
                    }
                }
                else
                {
                    foreach (Tag item in CommonInterface.Files)
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
    }
}
