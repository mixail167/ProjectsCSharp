using MineSweeper.Classes;
using System.ComponentModel;
using System.Windows;

namespace MineSweeper.Windows
{
    /// <summary>
    /// Логика взаимодействия для ParametersWindow.xaml
    /// </summary>
    public partial class ParametersWindow : Window
    {
        Level level;

        public ParametersWindow(Level level)
        {
            InitializeComponent();
            switch (level)
            {
                case Level.Easy:
                    EasyRadioButton.IsChecked = true;
                    break;
                case Level.Middle:
                    MiddleRadioButton.IsChecked = true;
                    break;
                default:
                    HardRadioButton.IsChecked = true;
                    break;
            }
            SoundCheckBox.IsChecked = WAVPlayer.sound;
        }

        private void EasyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Easy;
        }

        private void MiddleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Middle;
        }

        private void HardRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Hard;
        }

        private void SoundCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WAVPlayer.sound = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.Level = (int)level;
            Properties.Settings.Default.Sound = WAVPlayer.sound;
            Properties.Settings.Default.Save();
        }

        private void SoundCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            WAVPlayer.sound = false;
        }
    }
}
