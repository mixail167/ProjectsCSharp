using MineSweeper.Classes;
using System;
using System.ComponentModel;
using System.Windows;

namespace MineSweeper.Windows
{
    /// <summary>
    /// Логика взаимодействия для EndGameWindow.xaml
    /// </summary>
    public partial class EndGameWindow : Window
    {
        bool win;
        Level currentLevel;
        TimeSpan time;
        bool exit;

        public EndGameWindow(Level currentLevel, bool win, TimeSpan time)
        {
            InitializeComponent();
            exit = false;
            this.time = time;
            this.currentLevel = currentLevel;
            this.win = win;
            Time.Content = time.ToString(@"hh\:mm\:ss");
            switch (currentLevel)
            {
                case Level.Easy:
                    SetData(Properties.Settings.Default.EasyLevelCountGames + 1,
                        Properties.Settings.Default.EasyLevelWinGames + ((win) ? 1 : 0),
                        Properties.Settings.Default.EasyLevelBestTime,
                        "EasyLevel");
                    break;
                case Level.Middle:
                    SetData(Properties.Settings.Default.MiddleLevelCountGames + 1,
                        Properties.Settings.Default.MiddleLevelWinGames + ((win) ? 1 : 0),
                        Properties.Settings.Default.MiddleLevelBestTime,
                        "MiddleLevel");
                    break;
                case Level.Hard:
                    SetData(Properties.Settings.Default.HardLevelCountGames + 1,
                        Properties.Settings.Default.HardLevelWinGames + ((win) ? 1 : 0),
                        Properties.Settings.Default.HardLevelBestTime,
                        "HardLevel");
                    break;
            }
            if (win)
            {
                TitleText.Text = Application.Current.Resources.MergedDictionaries[0]["Win"].ToString();
            }
        }

        void SetData(int countGames, int winGames, TimeSpan bestTime, string key)
        {
            CountGames.Content = countGames;
            WinGames.Content = winGames;
            BestTime.Content = bestTime.ToString(@"hh\:mm\:ss");
            PercentWin.Content = string.Format("{0:f2}%", winGames * 1.0f / countGames);
            LevelText.Content = Application.Current.Resources.MergedDictionaries[0][key];
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Save();
            Close();
        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = null;
            Save();
            exit = true;
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DialogResult == null && !exit)
            {
                DialogResult = true;
                Save();
            }
            WAVPlayer.StopSound();
        }

        public bool Exit
        {
            get { return exit; }
        }

        private void Save()
        {
            switch (currentLevel)
            {
                case Level.Easy:
                    Properties.Settings.Default.EasyLevelCountGames++;
                    Properties.Settings.Default.EasyLevelWinGames += (win) ? 1 : 0;
                    if (win && (Properties.Settings.Default.EasyLevelBestTime > time || Properties.Settings.Default.EasyLevelBestTime == new TimeSpan()))
                    {
                        Properties.Settings.Default.EasyLevelBestTime = time;
                    }
                    break;
                case Level.Middle:
                    Properties.Settings.Default.MiddleLevelCountGames++;
                    Properties.Settings.Default.MiddleLevelWinGames += (win) ? 1 : 0;
                    if (win && (Properties.Settings.Default.MiddleLevelBestTime > time || Properties.Settings.Default.MiddleLevelBestTime == new TimeSpan()))
                    {
                        Properties.Settings.Default.MiddleLevelBestTime = time;
                    }
                    break;
                case Level.Hard:
                    Properties.Settings.Default.HardLevelCountGames++;
                    Properties.Settings.Default.HardLevelWinGames += (win) ? 1 : 0;
                    if (win && (Properties.Settings.Default.HardLevelBestTime > time || Properties.Settings.Default.HardLevelBestTime == new TimeSpan()))
                    {
                        Properties.Settings.Default.HardLevelBestTime = time;
                    }
                    break;
            }
            Properties.Settings.Default.Save();
        }
    }
}
