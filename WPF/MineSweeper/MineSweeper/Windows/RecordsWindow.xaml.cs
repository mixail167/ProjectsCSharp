using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                    break;
                case 2:
                    Properties.Settings.Default.HardLevelCountGames = 0;
                    Properties.Settings.Default.HardLevelWinGames = 0;
                    break;
                default:
                    Properties.Settings.Default.EasyLevelCountGames = 0;
                    Properties.Settings.Default.EasyLevelWinGames = 0;
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
                    try
                    {
                        if (Properties.Settings.Default.MiddleLevelCountGames == 0)
                            throw new DivideByZeroException();
                        PercentWin.Content = string.Format("{0:#.##}%", Properties.Settings.Default.MiddleLevelWinGames * 1.0f / Properties.Settings.Default.MiddleLevelCountGames);

                    }
                    catch (DivideByZeroException)
                    {
                        PercentWin.Content = "0%";
                    }
                    break;
                case 2:
                    CountGames.Content = Properties.Settings.Default.HardLevelCountGames;
                    WinGames.Content = Properties.Settings.Default.HardLevelWinGames;
                    try
                    {
                        if (Properties.Settings.Default.HardLevelCountGames == 0)
                            throw new DivideByZeroException();
                        PercentWin.Content = string.Format("{0:#.##}%", Properties.Settings.Default.HardLevelWinGames * 1.0f / Properties.Settings.Default.HardLevelCountGames);

                    }
                    catch (DivideByZeroException)
                    {
                        PercentWin.Content = "0%";
                    }
                    break;
                default:
                    CountGames.Content = Properties.Settings.Default.EasyLevelCountGames;
                    WinGames.Content = Properties.Settings.Default.EasyLevelWinGames;
                    try
                    {
                        if (Properties.Settings.Default.EasyLevelCountGames == 0)
                            throw new DivideByZeroException();
                        PercentWin.Content = string.Format("{0:#.##}%", Properties.Settings.Default.EasyLevelWinGames * 1.0f / Properties.Settings.Default.EasyLevelCountGames);

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
