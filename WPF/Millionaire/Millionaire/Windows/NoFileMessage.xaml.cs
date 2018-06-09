using System.Windows;

namespace Millionaire
{
    /// <summary>
    /// Логика взаимодействия для NoFileMessage.xaml
    /// </summary>
    public partial class NoFileMessage : Window
    {
        public bool next;
        public NoFileMessage()
        {
            InitializeComponent();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            DownloadFile downloadFile = new DownloadFile(false);
            Hide();
            downloadFile.ShowDialog();
            next = downloadFile.next;
            Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            next = false;
            Close();
        }
        
    }
}
