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

        internal void ModifyCheck(bool checkedValue)
        {
            foreach (Video item in ItemSource)
            {
                item.IsChecked = checkedValue;
            }
            listView.Items.Refresh();
        }
    }
}
