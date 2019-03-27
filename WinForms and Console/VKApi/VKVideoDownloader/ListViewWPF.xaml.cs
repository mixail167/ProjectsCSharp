using System.Collections.Generic;
using System.Windows.Controls;

namespace VKVideoDownloader
{
    /// <summary>
    /// Логика взаимодействия для ListViewWPF.xaml
    /// </summary>
    public partial class ListViewWPF : UserControl
    {
        public ListViewWPF()
        {
            InitializeComponent();
        }

        public List<Video> ItemSource
        {
            get { return listView.ItemsSource as List<Video>; }
            set { listView.ItemsSource = value; }
        }

        public void ModifyCheck(bool checkedValue)
        {
            List<Video> videos = ItemSource;
            if (videos != null)
            {
                foreach (Video item in ItemSource)
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
            List<Video> videos = ItemSource;
            if (videos != null)
            {
                foreach (Video item in ItemSource)
                {
                    item.SetCurrentFileFromFiles(quality);
                }
                listView.Items.Refresh();
            }
        }
    }
}
