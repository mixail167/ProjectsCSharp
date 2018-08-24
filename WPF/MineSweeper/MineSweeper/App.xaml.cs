using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;

namespace MineSweeper
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Set(MineSweeper.Properties.Settings.Default.DefaultLanguage);
        }

        public static void Set(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentUICulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\Resources\\lang.xaml", UriKind.Relative);
                    break;
                case "ru-RU":
                    dict.Source = new Uri("..\\Resources\\lang.ru-RU.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\lang.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
            MineSweeper.Properties.Settings.Default.DefaultLanguage = cultureInfo;
            MineSweeper.Properties.Settings.Default.Save();
        }       
    }
}
