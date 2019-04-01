using System.Collections.Generic;
using System.Windows.Controls;

namespace VKVideoDownloader
{
    /// <summary>
    /// Логика взаимодействия для ListViewWPF.xaml
    /// </summary>
    public partial class ListViewWPF : UserControl
    {
        List<Video> source;

        public ListViewWPF()
        {
            InitializeComponent();
        }

        public List<Video> ItemsSource
        {
            get { return listView.ItemsSource as List<Video>; }
            set { listView.ItemsSource = value; }
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
            List<Video> videos = listView.ItemsSource as List<Video>;
            if (videos != null)
            {
                foreach (Video item in videos)
                {
                    item.IsChecked = checkedValue;
                }
                listView.Items.Refresh();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
    }
}
