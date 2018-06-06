using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer
{
    public static class Vars
    {
        /// <summary>
        /// Путь к исполняемому файлу
        /// </summary>
        public static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Список полных имён файлов
        /// </summary>
        public static List<Tag> Files = new List<Tag>();

        /// <summary>
        /// Ссылка на форму
        /// </summary>
        public static Form1 Link1;
        public static Form2 Link2;

        public static int counter;
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
                "Все форматы|*.mp3;*.m4a;*.mp4;*.ogg;*.opus;*.ac3;*.ape;*.mpc;*.flac;*.wma;*.tta;*.alac;*.wv"
                + "|MPEG Audio Layer III (*.mp3)|*.mp3"
				+ "|Advanced Audio Coding (*.m4a;*.mp4)|*.m4a;*.mp4"
				;
        }

        /// <summary>
        /// Перерисовка формы
        /// </summary>
        /// <param name="index"></param>
        public static void RefreshForm(int index)
        {
            Link1.label1.Text = TimeSpan.FromSeconds(Audio.GetPosOfStream(Audio.Stream)).ToString();
            Link1.label2.Text = TimeSpan.FromSeconds(Audio.GetTimeOfStream(Audio.Stream)).ToString();
            Link1.colorSlider1.Maximum = Audio.GetTimeOfStream(Audio.Stream);
            Link1.colorSlider1.Value = Audio.GetPosOfStream(Audio.Stream);
            counter = 0;
            RefreshPlayList(index);
        }

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

        public static void Visualisation(bool isRadio)
        {
            counter++;
            if (isRadio)
            {
                if (counter == 2)
                {
                    Link2.pictureBox1.Image = Audio.Visualisation.CreateSpectrumLinePeak(Audio.Stream, Link2.pictureBox1.Width, Link2.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 2, 1, 1, 10, false, true, true);
                    Link2.pictureBox2.Image = Audio.Visualisation.CreateWaveForm(Audio.Stream, Link2.pictureBox1.Width, Link2.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 3, true, false, true);
                    counter = 0;
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
                if (counter == 2)
                {
                    Link1.pictureBox1.Image = Audio.Visualisation.CreateSpectrumLinePeak(Audio.Stream, Link1.pictureBox1.Width, Link1.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 2, 1, 1, 10, false, true, true);
                    Link1.pictureBox2.Image = Audio.Visualisation.CreateWaveForm(Audio.Stream, Link1.pictureBox1.Width, Link1.pictureBox1.Height, Color.Red, Color.Yellow, Color.White, Color.Black, 3, true, false, true);
                    counter = 0;
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
    }
}
