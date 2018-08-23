using MineSweeper.Classes;
using MineSweeper.Windows;
using System;
using System.Globalization;
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


            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;
            //Заполняем меню смены языка:
            //menuLanguage.Items.Clear();
            //foreach (var lang in App.Languages)
            //{
            //    MenuItem menuLang = new MenuItem();
            //    menuLang.Header = lang.DisplayName;
            //    menuLang.Tag = lang;
            //    menuLang.IsChecked = lang.Equals(currLang);
            //    menuLang.Click += ChangeLanguageClick;
            //    menuLanguage.Items.Add(menuLang);
            //}
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            //foreach (MenuItem i in menuLanguage.Items)
            //{
            //    CultureInfo ci = i.Tag as CultureInfo;
            //    i.IsChecked = ci != null && ci.Equals(currLang);
            //}
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null) {
                    App.Language = lang;
                }
            }
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
    }
}
