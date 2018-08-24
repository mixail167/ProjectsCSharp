using MineSweeper.Classes;
using MineSweeper.Windows;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MineSweeper
{
    public enum Level { Easy = 0, Middle = 1, Hard = 2};


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Level currentLevel;

        public MainWindow()
        {
            InitializeComponent();
            switch (Properties.Settings.Default.Level)
            {
                case 1:
                    currentLevel = Level.Middle;
                    break;
                case 2:
                    currentLevel = Level.Hard;
                    break;
                default:
                    currentLevel = Level.Easy;
                    break;
            }
            WAVPlayer.sound = Properties.Settings.Default.Sound;
        }

        private void RecordsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RecordsWindow recordsWindow = new RecordsWindow(currentLevel);
            recordsWindow.ShowDialog();
        }

        private void ParametersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ParametersWindow parametersWindow = new ParametersWindow(currentLevel);
            parametersWindow.ShowDialog();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
