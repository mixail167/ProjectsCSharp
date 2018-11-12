using MineSweeper.Classes;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

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
            SoundCheckBox.IsChecked = WAVPlayer.Sound;
            if (Properties.Settings.Default.DefaultLanguage.Equals(CultureInfo.GetCultureInfo("en-US")))
            {
                LanguageComboBox.SelectedIndex = 0;
            }
            else
            {
                LanguageComboBox.SelectedIndex = 1;
            }
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
            WAVPlayer.Sound = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.Level = (int)level;
            Properties.Settings.Default.Sound = WAVPlayer.Sound;
            Properties.Settings.Default.Save();
        }

        private void SoundCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            WAVPlayer.Sound = false;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (LanguageComboBox.SelectedIndex)
            {
                case 0:
                    App.Set(CultureInfo.GetCultureInfo("en-US"));
                    break;
                default:
                    App.Set(CultureInfo.GetCultureInfo("ru-RU"));
                    break;
            }
        }

        public Level GetLevel()
        {
            return level;
        }
    }
}
