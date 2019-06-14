using System.Collections.Generic;
using System.Windows.Controls;

namespace VKVideoDownloader
{
    /// <summary>
    /// Логика взаимодействия для ListAlbumWPF.xaml
    /// </summary>
    public partial class ListAlbumWPF : UserControl
    {
        List<Album> source;
        long id;

        public delegate void AlbumSelectedHandler();
        public event AlbumSelectedHandler AlbumSelected;

        public ListAlbumWPF()
        {
            InitializeComponent();
        }

        public List<Album> Source
        {
            get { return source; }
            set { source = value; }
        }

        public long ID
        {
            get { return id; }
        }

        public void SetSource()
        {
            listView.ItemsSource = source;
        }

        public List<Album> ItemsSource
        {
            get { return listView.ItemsSource as List<Album>; }
            set { listView.ItemsSource = value; }
        }

        public void ClearSource()
        {
            listView.ItemsSource = null;
            source = null;
        }

        public void SetSource(List<Album> albums)
        {
            listView.ItemsSource = albums;
        }

        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {
                id = (listView.SelectedItems[0] as Album).ID;
                AlbumSelected();
            }
        }
    }
}
