using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VKVideoDownloader
{
    /// <summary>
    /// Логика взаимодействия для ListViewWPF.xaml
    /// </summary>
    public partial class ListViewWPF : UserControl
    {
        List<Video> source;
        int count;

        public delegate void CountChangedHandler();
        public event CountChangedHandler CountChanged;

        public ListViewWPF()
        {
            InitializeComponent();
        }

        public List<Video> ItemsSource
        {
            get { return listView.ItemsSource as List<Video>; }
            set { listView.ItemsSource = value; }
        }

        public int Count
        {
            get { return count; }
        }

        public List<Video> Source
        {
            get { return source; }
            set { source = value; }
        }

        public void SetSource()
        {
            listView.ItemsSource = source;
        }

        public void SetSource(List<Video> videos)
        {
            listView.ItemsSource = videos;
        }

        public void ClearSource()
        {
            listView.ItemsSource = null;
            source = null;
        }


        public void ModifyCheck(bool checkedValue)
        {
            List<Video> videos = source;
            if (videos != null)
            {
                foreach (Video item in videos)
                {
                    item.IsChecked = checkedValue;
                    count = (checkedValue) ? count + 1 : count - 1;
                }
                listView.Items.Refresh();
            }
        }

        public void ModifyQuality(string quality)
        {
            List<Video> videos = listView.ItemsSource as List<Video>;
            if (videos != null)
            {
                foreach (Video item in videos)
                {
                    item.SetCurrentFileFromFiles(quality);
                }
                listView.Items.Refresh();
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {   
            count++;
            CountChanged();
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (count != 0)
            {
                count--;
                CountChanged();
            }
        }
    }
}
