using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        /// Ссылка на форму эквалайзера
        /// </summary>
        public static Form3 Link3;

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
            Link1.listView1.EnsureVisible(index);
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
            }
            else
            {
                if (Iterator == 2)
                {
                    Link1.pictureBox1.Image = Audio.Visualisation.CreateSpectrumLinePeak(Audio.Stream, Link1.pictureBox1.Width, Link1.pictureBox1.Height, Color.Red, Color.Yellow, Color.Gray, Color.Black, 2, 1, 1, 10, false, true, true);
                    Link1.pictureBox2.Image = Audio.Visualisation.CreateWaveForm(Audio.Stream, Link1.pictureBox2.Width, Link1.pictureBox2.Height, Color.Red, Color.Yellow, Color.Gray, Color.Black, 3, true, false, true);
                    Iterator = 0;
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
                using (StreamWriter writer = new StreamWriter(filepath, false, Encoding.UTF8))
                {
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
                }
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

        public static void SetEffectFromSettings()
        {
            Link3.colorSlider4.Value = (int)Properties.Settings.Default.Echo;
            Link3.colorSlider5.Value = (int)Properties.Settings.Default.Chorus;
            Link3.colorSlider1.Value = -(int)(Properties.Settings.Default.EQ0 * 10f);
            Link3.colorSlider2.Value = -(int)(Properties.Settings.Default.EQ1 * 10f);
            Link3.colorSlider3.Value = -(int)(Properties.Settings.Default.EQ2 * 10f);
            Link3.colorSlider6.Value = -(int)(Properties.Settings.Default.EQ3 * 10f);
            Link3.colorSlider7.Value = -(int)(Properties.Settings.Default.EQ4 * 10f);
            Link3.colorSlider8.Value = -(int)(Properties.Settings.Default.EQ5 * 10f);
            Link3.colorSlider9.Value = -(int)(Properties.Settings.Default.EQ6 * 10f);
            Link3.colorSlider10.Value = -(int)(Properties.Settings.Default.EQ7 * 10f);
            Link3.colorSlider11.Value = -(int)(Properties.Settings.Default.EQ8 * 10f);
            Link3.colorSlider12.Value = -(int)(Properties.Settings.Default.EQ9 * 10f);
            Link3.colorSlider13.Value = -(int)(Properties.Settings.Default.EQ10 * 10f);
            Link3.colorSlider14.Value = -(int)(Properties.Settings.Default.EQ11 * 10f);
            Link3.colorSlider15.Value = -(int)(Properties.Settings.Default.EQ12 * 10f);
            Link3.colorSlider16.Value = -(int)(Properties.Settings.Default.EQ13 * 10f);
            Link3.colorSlider17.Value = -(int)(Properties.Settings.Default.EQ14 * 10f);
            Link3.colorSlider18.Value = -(int)(Properties.Settings.Default.EQ15 * 10f);
            Link3.colorSlider19.Value = -(int)(Properties.Settings.Default.EQ16 * 10f);
            Link3.colorSlider20.Value = -(int)(Properties.Settings.Default.EQ17 * 10f);
            Link3.colorSlider21.Value = (int)((4f - Properties.Settings.Default.VolumeFX) * 100);
        }

        public static void SetEffect()
        {
            if (Link3.checkBox1.Checked)
            {
                Audio.SetEffects((float)Link3.colorSlider4.Value,
                                     (float)Link3.colorSlider5.Value,
                                     4f - (float)Link3.colorSlider21.Value / 100f,
                                    new float[] 
                            { 
                                -(float)Link3.colorSlider1.Value / 10f,
                                -(float)Link3.colorSlider2.Value / 10f,
                                -(float)Link3.colorSlider3.Value / 10f,
                                -(float)Link3.colorSlider6.Value / 10f,
                                -(float)Link3.colorSlider7.Value / 10f,
                                -(float)Link3.colorSlider8.Value / 10f,
                                -(float)Link3.colorSlider9.Value / 10f,
                                -(float)Link3.colorSlider10.Value / 10f,
                                -(float)Link3.colorSlider11.Value / 10f,
                                -(float)Link3.colorSlider12.Value / 10f,
                                -(float)Link3.colorSlider13.Value / 10f,
                                -(float)Link3.colorSlider14.Value / 10f,
                                -(float)Link3.colorSlider15.Value / 10f,
                                -(float)Link3.colorSlider16.Value / 10f,
                                -(float)Link3.colorSlider17.Value / 10f,
                                -(float)Link3.colorSlider18.Value / 10f,
                                -(float)Link3.colorSlider19.Value / 10f,
                                -(float)Link3.colorSlider20.Value / 10f
                            });
            }
        }

        public static void GetDataFromClipboard(bool isRadio = false)
        {
            if (Clipboard.ContainsFileDropList())
            {
                StringCollection files = Clipboard.GetFileDropList();
                if (isRadio)
                {
                    Link2.listBox1.BeginUpdate();
                }
                else
                {
                    Link1.listView1.BeginUpdate();
                }
                foreach (string filePath in files)
                {
                    if (Directory.Exists(filePath))
                    {
                        CommonInterface.GetFilesFromFolder(filePath, isRadio);
                    }
                    else
                    {
                        CommonInterface.CheckExtension(new FileInfo(filePath), isRadio);
                    }
                }
                if (isRadio)
                {
                    Link2.listBox1.EndUpdate();
                }
                else
                {
                    Link1.listView1.EndUpdate();
                }
            }
            else if (isRadio && Clipboard.ContainsText())
            {
                AddTrackOrURL(Clipboard.GetText(), isRadio);
            }
        }
    }
}
