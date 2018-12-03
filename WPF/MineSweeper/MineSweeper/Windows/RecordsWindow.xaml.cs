using System;
using System.Windows;
using System.Windows.Controls;

namespace MineSweeper.Windows
{
    /// <summary>
    /// Логика взаимодействия для RecordsWindow.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        public RecordsWindow(Level level)
        {
            InitializeComponent();
            LevelComboBox.SelectedIndex = (int)level;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetData();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            switch (LevelComboBox.SelectedIndex)
            {
                case 1:
                    Properties.Settings.Default.MiddleLevelCountGames = 0;
                    Properties.Settings.Default.MiddleLevelWinGames = 0;
                    Properties.Settings.Default.MiddleLevelBestTime = new TimeSpan();
                    break;
                case 2:
                    Properties.Settings.Default.HardLevelCountGames = 0;
                    Properties.Settings.Default.HardLevelWinGames = 0;
                    Properties.Settings.Default.HardLevelBestTime = new TimeSpan();
                    break;
                default:
                    Properties.Settings.Default.EasyLevelCountGames = 0;
                    Properties.Settings.Default.EasyLevelWinGames = 0;
                    Properties.Settings.Default.EasyLevelBestTime = new TimeSpan();
                    break;
            }
            Properties.Settings.Default.Save();
            SetData();
        }

        private void SetData()
        {
            switch (LevelComboBox.SelectedIndex)
            {
                case 1:
                    CountGames.Content = Properties.Settings.Default.MiddleLevelCountGames;
                    WinGames.Content = Properties.Settings.Default.MiddleLevelWinGames;
                    BestTime.Content = Properties.Settings.Default.MiddleLevelBestTime.ToString(@"hh\:mm\:ss");
                    try
                    {
                        if (Properties.Settings.Default.MiddleLevelCountGames == 0)
                            throw new DivideByZeroException();
                        PercentWin.Content = string.Format("{0:f2}%", Properties.Settings.Default.MiddleLevelWinGames * 1.0f / Properties.Settings.Default.MiddleLevelCountGames);

                    }
                    catch (DivideByZeroException)
                    {
                        PercentWin.Content = "0%";
                    }
                    break;
                case 2:
                    CountGames.Content = Properties.Settings.Default.HardLevelCountGames;
                    WinGames.Content = Properties.Settings.Default.HardLevelWinGames;
                    BestTime.Content = Properties.Settings.Default.HardLevelBestTime.ToString(@"hh\:mm\:ss");
                    try
                    {
                        if (Properties.Settings.Default.HardLevelCountGames == 0)
                            throw new DivideByZeroException();
                        PercentWin.Content = string.Format("{0:f2}%", Properties.Settings.Default.HardLevelWinGames * 1.0f / Properties.Settings.Default.HardLevelCountGames);

                    }
                    catch (DivideByZeroException)
                    {
                        PercentWin.Content = "0%";
                    }
                    break;
                default:
                    CountGames.Content = Properties.Settings.Default.EasyLevelCountGames;
                    WinGames.Content = Properties.Settings.Default.EasyLevelWinGames;
                    BestTime.Content = Properties.Settings.Default.EasyLevelBestTime.ToString(@"hh\:mm\:ss");
                    try
                    {
                        if (Properties.Settings.Default.EasyLevelCountGames == 0)
                            throw new DivideByZeroException();
                        PercentWin.Content = string.Format("{0:f2}%", Properties.Settings.Default.EasyLevelWinGames * 1.0f / Properties.Settings.Default.EasyLevelCountGames);

                    }
                    catch (DivideByZeroException)
                    {
                        PercentWin.Content = "0%";
                    }
                    break;
            }
        }
    }
}
